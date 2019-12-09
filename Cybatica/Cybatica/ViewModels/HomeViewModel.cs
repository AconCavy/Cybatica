using Cybatica.Models;
using Cybatica.Services;
using Cybatica.Utilities;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Cybatica.ViewModels
{
    public class HomeViewModel : ReactiveObject, IDisposable
    {
        private readonly ICybaticaHandler _cybaticaHandler;

        private readonly IDisposable _cleanUp;
        private SessionType _sessionType;

        public HomeViewModel(ICybaticaHandler cybaticaHandler = null)
        {
            _cybaticaHandler = cybaticaHandler ?? Locator.Current.GetService<ICybaticaHandler>();

            _cleanUp = this.WhenAnyValue(x => x._cybaticaHandler.ElapsedTime)
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.ElapsedTime);

            var canCaptureBaseExecute = this.WhenAnyValue(x => x.IsConnecting, x => x.IsCapturing,
                (x, y) => x && !y);
            StartBaseSessionCommand = ReactiveCommand.CreateFromTask(
                async () => await StartSessionAsync(SessionType.Base), canCaptureBaseExecute);

            var canCaptureDataExecute = this.WhenAnyValue(x => x.IsConnecting, x => x.IsBaseStored, x => x.IsCapturing,
                (x, y, z) => x && y && !z);
            StartDataSessionCommand = ReactiveCommand.CreateFromTask(
                async () => await StartSessionAsync(SessionType.Data), canCaptureDataExecute);

            var canStopExecute = this.WhenAnyValue(x => x.IsConnecting, x => x.IsCapturing,
                (x, y) => x && y);
            StopSessionCommand = ReactiveCommand.CreateFromTask(StopSessionAsync, canStopExecute);

            var canConnectExecute = this.WhenAnyValue(x => x.IsConnecting, x => !x);
            ConnectCommand = ReactiveCommand.CreateFromTask(ConnectAsync, canConnectExecute);

            var canDisconnectExecute = this.WhenAnyValue(x => x.IsConnecting);
            DisconnectCommand = ReactiveCommand.CreateFromTask(DisconnectAsync, canDisconnectExecute);

            AboutCommand = ReactiveCommand.CreateFromTask(async () => { await Shell.Current.GoToAsync("homeAbout"); });
        }

        [Reactive] public TimeSpan ElapsedTime { [ObservableAsProperty] get; private set; }
        [Reactive] public bool IsConnecting { get; private set; }
        [Reactive] public bool IsBaseStored { get; private set; }
        [Reactive] public bool IsCapturing { get; private set; }

        public ICommand StartBaseSessionCommand { get; }
        public ICommand StartDataSessionCommand { get; }
        public ICommand StopSessionCommand { get; }
        public ICommand ConnectCommand { get; }
        public ICommand DisconnectCommand { get; }
        public ICommand AboutCommand { get; }

        private async Task StartSessionAsync(SessionType sessionType)
        {
            var result = await Application.Current.MainPage.DisplayAlert(
                "Start Session",
                $"Do you start {sessionType} session?",
                "Start",
                "Cancel");

            if (!result) return;

            _sessionType = sessionType;

            IsCapturing = true;
            _cybaticaHandler.InitializeSession();
            _cybaticaHandler.StartSession(_sessionType);
        }

        private async Task StopSessionAsync()
        {
            var result = await Application.Current.MainPage.DisplayAlert(
                "Stop Session",
                "Do you stop this data session?",
                "Stop",
                "Cancel");

            if (!result) return;

            try
            {
                _cybaticaHandler.StopSession();
            }
            catch (InsufficientDataException)
            {
                await Application.Current.MainPage.DisplayAlert(
                "Insufficient data",
                "Please record the base session for at least 1 minute.",
                "OK");
                IsCapturing = false;
                return;
            }
            
            IsBaseStored = _cybaticaHandler.IsBaseSessionStored;
            IsCapturing = false;

            result = await Application.Current.MainPage.DisplayAlert(
                "Save Session",
                "Do you save this data session?",
                "Save",
                "Cancel");

            if (!result) return;
            _cybaticaHandler.Export();
        }

        private async Task ConnectAsync()
        {
            _cybaticaHandler.Discover();
            var devices = _cybaticaHandler.GetDiscoveredDevices().ToArray();

            var result = await Application.Current.MainPage.DisplayActionSheet(
                "Choose Device",
                "Cancel",
                null,
                devices.Select(x => x.SerialNumber).ToArray());

            if (result == null || result.Equals("Cancel")) return;

            var device = devices.First(x => x.SerialNumber.Equals(result));
            if (device == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Alert",
                    "Empatica E4 not found",
                    "OK");
                return;
            }

            _cybaticaHandler.Connect(device);
            IsConnecting = true;
            IsBaseStored = false;
            IsCapturing = false;
        }

        private async Task DisconnectAsync()
        {
            if (IsCapturing) await StopSessionAsync();
            if (IsCapturing) return;

            var result = await Application.Current.MainPage.DisplayAlert(
                "Disconnect Device",
                "Do you disconnect the device?",
                "Disconnect",
                "Cancel");

            if (!result) return;

            _cybaticaHandler.Disconnect();

            IsConnecting = false;
            IsBaseStored = false;
            IsCapturing = false;
        }

        public void Dispose()
        {
            _cleanUp?.Dispose();
        }
    }
}
using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Cybatica.Services;
using Cybatica.Utilities;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Cybatica.ViewModels
{
    public class HomeViewModel : ReactiveObject
    {
        private readonly ICybaticaHandler _cybaticaHandler;

        private readonly Stopwatch _stopwatch;
        private IDisposable _cleanUp;
        private SessionType _sessionType;

        public HomeViewModel()
        {
            _cybaticaHandler = Locator.Current.GetService<ICybaticaHandler>();
            _stopwatch = new Stopwatch();

            this.WhenAnyValue(x => x.Time)
                .ToPropertyEx(this, x => x.ElapsedTime);

            var canStopExecute = this.WhenAnyValue(x => x.IsConnecting, x => x.IsCapturing,
                (x, y) => x && y);
            StopSessionCommand = ReactiveCommand.CreateFromTask(StopSessionAsync, canStopExecute);

            var canCaptureBaseExecute = this.WhenAnyValue(x => x.IsConnecting, x => x.IsCapturing,
                (x, y) => x && !y);
            StartBaseSessionCommand = ReactiveCommand.CreateFromTask(
                async () => await StartSessionAsync(SessionType.Base), canCaptureBaseExecute);

            var canCaptureDataExecute = this.WhenAnyValue(x => x.IsConnecting, x => x.IsBaseStored, x => x.IsCapturing,
                (x, y, z) => x && y && !z);
            StartDataSessionCommand = ReactiveCommand.CreateFromTask(
                async () => await StartSessionAsync(SessionType.Data), canCaptureDataExecute);

            var canConnectExecute = this.WhenAnyValue(x => x.IsConnecting, x => !x);
            ConnectCommand = ReactiveCommand.CreateFromTask(ConnectAsync, canConnectExecute);

            var canDisconnectExecute = this.WhenAnyValue(x => x.IsConnecting);
            DisconnectCommand = ReactiveCommand.CreateFromTask(DisconnectAsync, canDisconnectExecute);

            AboutCommand = ReactiveCommand.CreateFromTask(async () => { await Shell.Current.GoToAsync("about"); });
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

        [Reactive] private TimeSpan Time { get; set; }

        private async Task StopSessionAsync()
        {
            if (_sessionType == SessionType.Base) IsBaseStored = true;

            _cybaticaHandler.StopSession();
            IsCapturing = false;
            _stopwatch.Stop();
            _cleanUp?.Dispose();
            _cleanUp = null;

            var saveAnswer = await Application.Current.MainPage.DisplayAlert(
                "Save Session",
                "Do you save this data session?",
                "Yes",
                "No");

            if (saveAnswer)
            {
                var shareDataExporter = new ShareDataExporter("CybaticaSession", "csv");
                shareDataExporter.Export(FileSystem.CacheDirectory, _cybaticaHandler.EmpaticaSession,
                    _cybaticaHandler.OcsSession);
            }
        }

        private async Task StartSessionAsync(SessionType sessionType)
        {
            var result = await Application.Current.MainPage.DisplayAlert(
                "Start Session",
                $"Do you start {sessionType} session?",
                "Yes",
                "No");

            if (!result) return;

            _sessionType = sessionType;

            IsCapturing = true;
            _cybaticaHandler.InitializeSession();
            _cybaticaHandler.StartSession();
            _stopwatch.Reset();
            _stopwatch.Start();
            _cleanUp = Observable.Interval(TimeSpan.FromSeconds(0.01))
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(_ => Time = _stopwatch.Elapsed);
        }

        private async Task ConnectAsync()
        {
            _cybaticaHandler.Discover();
            var devices = _cybaticaHandler.Devices.Select(x => x.SerialNumber).ToArray();

            var result = await Application.Current.MainPage.DisplayActionSheet(
                "Choose Device",
                "Cancel",
                null,
                devices);

            if (result == null || result.Equals("Cancel")) return;

            var device = _cybaticaHandler.Devices.First(x => x.SerialNumber.Equals(result));
            if (device == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Alert",
                    "Empatica E4 not found",
                    "Cancel");
                return;
            }

            _cybaticaHandler.Connect(device);
            IsConnecting = true;
            IsBaseStored = false;
            IsCapturing = false;
        }

        private async Task DisconnectAsync()
        {
            var disconnectAnswer = await Application.Current.MainPage.DisplayAlert(
                "Alert",
                "Do you disconnect the device?",
                "Yes",
                "No");

            if (!disconnectAnswer) return;

            _cybaticaHandler.Disconnect();

            if (IsCapturing) await StopSessionAsync();

            IsConnecting = false;
            IsBaseStored = false;
            IsCapturing = false;
        }

        private enum SessionType
        {
            Base,
            Data
        }
    }
}
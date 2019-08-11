using Cybatica.Services;
using Cybatica.Utilities;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Cybatica.ViewModels
{
    public class HomeViewModel : ReactiveObject
    {
        [Reactive] public TimeSpan ElapsedTime { [ObservableAsProperty] get; private set; }
        [Reactive] public bool IsConnecting { get; private set; }
        [Reactive] public bool IsBaseStored { get; private set; }
        [Reactive] public bool IsCapturing { get; private set; }

        public ReactiveCommand<Unit, Unit> StartBaseSessionCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> StartDataSessionCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> StopSessionCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> ConnectCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> DisconnectCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> LicensesCommand { get; private set; }

        [Reactive] private TimeSpan Time { get; set; }

        private readonly INavigation _navigation;
        private readonly ICybaticaHandler _cybaticaHandler;
        private readonly Stopwatch _stopwatch;
        private IDisposable _cleanUp;
        private SessionType _sessionType;

        private enum SessionType
        {
            Base,
            Data
        }

        public HomeViewModel(INavigation navigation)
        {
            _navigation = navigation;
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

            LicensesCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var page = Locator.Current.GetService<IViewFor<LicenseViewModel>>() as Page;
                await _navigation.PushAsync(page);
            });
        }

        private async Task StopSessionAsync()
        {
            if (_sessionType == SessionType.Base)
            {
                IsBaseStored = true;
            }

            _cybaticaHandler.StopSession();
            IsCapturing = false;
            _stopwatch.Stop();
            _cleanUp?.Dispose();
            _cleanUp = null;

            var saveAnswer = await Application.Current.MainPage.DisplayAlert(
                title: "Save Session",
                message: "Do you save this data session?",
                accept: "Yes",
                cancel: "No");

            if (saveAnswer)
            {
                var shareDataExporter = new ShareDataExporter("CybaticaSession", "csv");
                shareDataExporter.Export(FileSystem.CacheDirectory, _cybaticaHandler.EmpaticaSession);
            }


        }

        private async Task StartSessionAsync(SessionType sessionType)
        {
            var result = await Application.Current.MainPage.DisplayAlert(
                title: "Start Session",
                message: $"Do you start {sessionType} session?",
                accept: "Yes",
                cancel: "No");

            if (!result)
            {
                return;
            }

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
            var devices = _cybaticaHandler.Devices.Select(x => x.SerialNumber).ToArray();
            if (devices == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    title: "Alert",
                    message: "Empatica E4 not found",
                    cancel: "Cancel");
                return;
            }

            var result = await Application.Current.MainPage.DisplayActionSheet(
                    title: "Choose Device",
                    cancel: "Cancel",
                    destruction: null,
                    buttons: devices);

            if (result == null || result.Equals("Cancel"))
            {
                return;
            }

            var device = _cybaticaHandler.Devices.First(x => x.SerialNumber.Equals(result));
            if (device == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    title: "Alert",
                    message: "Empatica E4 not found",
                    cancel: "Cancel");
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
                    title: "Alert",
                    message: "Do you disconnect the device?",
                    accept: "Yes",
                    cancel: "No");

            if (!disconnectAnswer)
            {
                return;
            }

            _cybaticaHandler.Disconnect();

            if (IsCapturing)
            {
                await StopSessionCommand.Execute();
            }

            IsConnecting = false;
            IsBaseStored = false;
            IsCapturing = false;
        }
    }
}

using Cybatica.Empatica;
using Cybatica.Utilities;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Cybatica.ViewModels
{
    public class HomeViewModel : ReactiveObject
    {
        [Reactive] public float ElapsedTime { get; private set; }
        [Reactive] public bool IsConnecting { get; private set; }
        [Reactive] public bool IsBaseStored { get; private set; }
        [Reactive] public bool IsCapturing { get; private set; }

        public ReactiveCommand<Unit, Unit> Stop { get; private set; }
        public ReactiveCommand<Unit, Unit> CaptureBase { get; private set; }
        public ReactiveCommand<Unit, Unit> CaptureData { get; private set; }
        public ReactiveCommand<Unit, Unit> Connect { get; private set; }
        public ReactiveCommand<Unit, Unit> Disconnect { get; private set; }
        public ReactiveCommand<Unit, Unit> Licenses { get; private set; }

        public INavigation Navigation { get; set; }

        private SessionType _sessionType;


        private enum SessionType
        {
            Base,
            Data
        }

        public HomeViewModel()
        {
            var canStopExecute = this.WhenAnyValue(x => x.IsConnecting, x => x.IsCapturing,
                (x, y) => x && y);
            Stop = ReactiveCommand.CreateFromTask(StopCommandAsync, canStopExecute);

            var canCaptureBaseExecute = this.WhenAnyValue(x => x.IsConnecting, x => x.IsCapturing,
                (x, y) => x && !y);
            CaptureBase = ReactiveCommand.Create(CaptureBaseCommand, canCaptureBaseExecute);

            var canCaptureDataExecute = this.WhenAnyValue(x => x.IsConnecting, x => x.IsBaseStored, x => x.IsCapturing,
                (x, y, z) => x && y && !z);
            CaptureData = ReactiveCommand.Create(CaptureDataCommand, canCaptureDataExecute);

            var canConnectExecute = this.WhenAnyValue(x => x.IsConnecting, x => !x);
            Connect = ReactiveCommand.CreateFromTask(ConnectCommandAsync, canConnectExecute);

            var canDisconnectExecute = this.WhenAnyValue(x => x.IsConnecting);
            Disconnect = ReactiveCommand.CreateFromTask(DisconnectCommandAsync, canDisconnectExecute);

            Licenses = ReactiveCommand.CreateFromTask(async () =>
            {
                var page = Locator.Current.GetService<IViewFor<LicenseViewModel>>() as Page;
                await Navigation.PushAsync(page);
            });
        }

        private async Task StopCommandAsync()
        {
            if (_sessionType == SessionType.Base)
            {
                IsBaseStored = true;
                // TODO : Implements base session completed action

            }
            else if (_sessionType == SessionType.Data)
            {
                // TODO : Implements data session completed action
                var saveAnswer = await Application.Current.MainPage.DisplayAlert(
                title: "Save",
                message: "Do you save this data session?",
                accept: "Yes",
                cancel: "No");

                if (saveAnswer)
                {
                    var shareDataExporter = new ShareDataExporter("CybaticaData", "csv");
                    var session = new EmpaticaSession();
                    shareDataExporter.Export(FileSystem.CacheDirectory, session);
                }
            }
            IsCapturing = false;
        }

        private void CaptureBaseCommand()
        {
            _sessionType = SessionType.Base;
            IsCapturing = true;
            System.Console.WriteLine($"{_sessionType} : {IsCapturing}");

            // TODO : Implements capture base action
        }

        private void CaptureDataCommand()
        {
            _sessionType = SessionType.Data;
            IsCapturing = true;

            // TODO : Implements capture data action
        }

        private async Task ConnectCommandAsync()
        {
            var devices = new string[] { "device 1", "device 2" };
            var result = await Application.Current.MainPage.DisplayActionSheet(
                title: "Choose Device",
                cancel: "Cancel",
                destruction: null,
                buttons: devices);

            if (!(result.Equals("Cancel") || result == null))
            {
                // TODO : Implements connecting to Empatica E4 using handler
            }

            IsConnecting = true;
            IsBaseStored = false;
            IsCapturing = false;
        }

        private async Task DisconnectCommandAsync()
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

            if (IsCapturing)
            {
                await Stop.Execute();
            }

            // TODO : Implements disconnecting from Empatica E4

            IsConnecting = false;
            IsBaseStored = false;
            IsCapturing = false;
        }

    }
}

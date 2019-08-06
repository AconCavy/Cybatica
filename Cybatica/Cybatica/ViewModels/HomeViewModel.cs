using Cybatica.Empatica;
using Cybatica.Models;
using Cybatica.Services;
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

        public ReactiveCommand<Unit, Unit> StopCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> CaptureBaseCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> CaptureDataCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> ConnectCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> DisconnectCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> LicensesCommand { get; private set; }

        public INavigation Navigation { get; private set; }

        private SessionType _sessionType;

        private enum SessionType
        {
            Base,
            Data
        }

        public HomeViewModel(INavigation navigation)
        {
            Navigation = navigation;

            var canStopExecute = this.WhenAnyValue(x => x.IsConnecting, x => x.IsCapturing,
                (x, y) => x && y);
            StopCommand = ReactiveCommand.CreateFromTask(StopCommandAsync, canStopExecute);

            var canCaptureBaseExecute = this.WhenAnyValue(x => x.IsConnecting, x => x.IsCapturing,
                (x, y) => x && !y);
            CaptureBaseCommand = ReactiveCommand.Create(CaptureBase, canCaptureBaseExecute);

            var canCaptureDataExecute = this.WhenAnyValue(x => x.IsConnecting, x => x.IsBaseStored, x => x.IsCapturing,
                (x, y, z) => x && y && !z);
            CaptureDataCommand = ReactiveCommand.Create(CaptureData, canCaptureDataExecute);

            var canConnectExecute = this.WhenAnyValue(x => x.IsConnecting, x => !x);
            ConnectCommand = ReactiveCommand.CreateFromTask(ConnectCommandAsync, canConnectExecute);

            var canDisconnectExecute = this.WhenAnyValue(x => x.IsConnecting);
            DisconnectCommand = ReactiveCommand.CreateFromTask(DisconnectCommandAsync, canDisconnectExecute);

            LicensesCommand = ReactiveCommand.CreateFromTask(async () =>
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

        private void CaptureBase()
        {
            _sessionType = SessionType.Base;
            IsCapturing = true;

            // TODO : Implements capture base action
        }

        private void CaptureData()
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
                await StopCommand.Execute();
            }

            // TODO : Implements disconnecting from Empatica E4

            IsConnecting = false;
            IsBaseStored = false;
            IsCapturing = false;
        }

    }
}

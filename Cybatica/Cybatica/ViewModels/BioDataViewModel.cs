using Cybatica.Services;
using Cybatica.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Cybatica.ViewModels
{
    public class BioDataViewModel : ReactiveObject, ISupportsActivation 
    {
        [Reactive] public float Bvp { get; private set; }
        [Reactive] public float Ibi { get; private set; }
        [Reactive] public float Hr { get; private set; }
        [Reactive] public float Gsr { get; private set; }
        [Reactive] public float Temperature { get; private set; }

        public ReactiveCommand<Unit, Unit> ManageDevice { get; private set; }
        public ReactiveCommand<Unit, Unit> DisconnectDevice { get; private set; }
        public ReactiveCommand<Unit, Unit> NavigateToChartPage { get; private set; }

        public ViewModelActivator Activator { get; }
        public INavigation Navigation { get; set; }

        private readonly IEmpaticaHandler _handler;

        private readonly IObservable<long> _observer;

        private bool _isConnecting;

        public BioDataViewModel()
        {
            Activator = new ViewModelActivator();
            
            _handler = Locator.Current.GetService<IEmpaticaHandler>();

            _observer = Observable.Interval(TimeSpan.FromSeconds(1))
                .Where(_ => _isConnecting)
                .ObserveOn(RxApp.MainThreadScheduler);

            _isConnecting = false;

            NavigateToChartPage = ReactiveCommand.CreateFromTask(async () => {
                var page = Locator.Current.GetService<IViewFor<BioDataChartViewModel>>() as BioDataChartPage;
                await Navigation.PushAsync(page);

            });

            ManageDevice = ReactiveCommand.CreateFromTask(async () =>
            {
                var result = await Application.Current.MainPage.DisplayActionSheet(
                    title: "Choose Device",
                    cancel: "Cancel",
                    destruction: null,
                    buttons: _handler.Devices.Select(x => x.SerialNumber).ToArray());

                if (_isConnecting = !(result.Equals("Cancel") || result == null))
                {
                    var target = _handler.Devices.First(x => x.SerialNumber.Equals(result));
                    _handler.ConnectDevice(target);
                }

            });

            DisconnectDevice = ReactiveCommand.CreateFromTask(async () => 
            {
                var disconnectAnswer = await Application.Current.MainPage.DisplayAlert(
                    title: "Alert",
                    message: "Do you disconnect a device?",
                    accept: "Yes",
                    cancel: "No");

                if (!disconnectAnswer)
                {
                    return;
                }

                _handler.DisconnectDevice();

                var saveAnswer = await Application.Current.MainPage.DisplayAlert(
                    title: "Save",
                    message: "Do you save this session?",
                    accept: "Yes",
                    cancel: "No");

                if (saveAnswer)
                {
                    var shareDataExporter = new ShareDataExporter("CybaticaData", "csv");
                    shareDataExporter.Export(FileSystem.CacheDirectory, _handler.EmpaticaSession);
                }

                ResetValues();
                _handler.InitializeSession();
                _isConnecting = false;
            });

            this.WhenActivated(disposable =>
            {
                HandleActivation();

                Disposable.Create(() => HandleDeactivation())
                .DisposeWith(disposable);

                _observer.Subscribe(_ => FetchData())
                .DisposeWith(disposable);

            });

        }

        public void FetchData()
        {
            Bvp = _handler.GetLatestBvp().Value;
            Ibi = _handler.GetLatestIbi().Value;
            Gsr = _handler.GetLatestGsr().Value;
            Temperature = _handler.GetLatestTemperature().Value;
            Hr = _handler.GetLatestHr().Value;

        }

        private void ResetValues()
        {
            Bvp = default;
            Ibi = default;
            Gsr = default;
            Temperature = default;
            Hr = default;

        }

        private void HandleActivation()
        {
        }

        private void HandleDeactivation()
        {
        }
    }
}

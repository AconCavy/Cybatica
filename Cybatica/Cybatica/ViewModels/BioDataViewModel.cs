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
using Xamarin.Forms;

namespace Cybatica.ViewModels
{
    public class BioDataViewModel : ReactiveObject, ISupportsActivation 
    {
        [Reactive] public float BVP { get; private set; }
        [Reactive] public float IBI { get; private set; }
        [Reactive] public float HR { get; private set; }
        [Reactive] public float GSR { get; private set; }
        [Reactive] public float Temperature { get; private set; }

        public ReactiveCommand<Unit, Unit> ManageDevice { get; private set; }
        public ReactiveCommand<Unit, Unit> DisconnectDevice { get; private set; }
        public ReactiveCommand<Unit, Unit> NavigateToChartPage { get; private set; }

        public ViewModelActivator Activator { get; }
        public INavigation Navigation { get; set; }

        private readonly IEmpaticaHandler _handler;

        private readonly IObservable<long> _observer;

        public BioDataViewModel()
        {
            Activator = new ViewModelActivator();
            
            _handler = Locator.Current.GetService<IEmpaticaHandler>();

            _observer = Observable.Interval(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler);

            NavigateToChartPage = ReactiveCommand.CreateFromTask(async () => {
                var page = Locator.Current.GetService<IViewFor<EmpaticaChartViewModel>>() as EmpaticaChartPage;
                await Navigation.PushAsync(page);

            });

            ManageDevice = ReactiveCommand.CreateFromTask(async () =>
            {
                var result = await Application.Current.MainPage.DisplayActionSheet(
                    title: "Choose Device",
                    cancel: "Cancel",
                    destruction: null,
                    buttons: _handler.Devices.Select(x => x.SerialNumber).ToArray());

                if (!(result.Equals("Cancel") || result == null))
                {
                    var target = _handler.Devices.First(x => x.SerialNumber.Equals(result));
                    _handler.ConnectDevice(target);
                }

            });

            DisconnectDevice = ReactiveCommand.Create(_handler.DisconnectDevice);

            this.WhenActivated(disposable =>
            {
                HandleActivation();

                Disposable.Create(() => HandleDeactivation())
                .DisposeWith(disposable);

                _observer.Subscribe(_ => FetchData()).DisposeWith(disposable);

            });

        }

        public void FetchData()
        {
            BVP = _handler.GetLatestBVP().Value;
            GSR = _handler.GetLatestGSR().Value;
            IBI = _handler.GetLatestIBI().Value;
            Temperature = _handler.GetLatestTemperature().Value;
            HR = _handler.GetLatestHR().Value;

        }

        private void HandleActivation()
        {
            Console.WriteLine("EmpaticaParameterViewModel: Activated");      

        }

        private void HandleDeactivation()
        {
            Console.WriteLine("EmpaticaParameterViewModel: Deactivated");

        }
    }
}

using Cybatica.Empatica;
using Cybatica.Services;
using Cybatica.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReactiveUI.XamForms;
using Splat;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows.Input;
using Xamarin.Forms;

namespace Cybatica.ViewModels
{
    public class EmpaticaParameterViewModel : ReactiveObject, ISupportsActivation 
    {

        //[Reactive] public float BVP { get; private set; }
        public extern float BVP { [ObservableAsProperty] get; }

        [Reactive] public float GSR { get; private set; }

        [Reactive] public float IBI { get; private set; }

        [Reactive] public float HR { get; private set; }

        [Reactive] public float Temperature { get; private set; }

        public ICommand ManageDevice { get; private set; }

        public ICommand NavigateToChartPage { get; private set; }

        public ViewModelActivator Activator { get; }

        public INavigation Navigation { get; set; }

        private readonly IEmpaticaHandler _handler;

        public EmpaticaParameterViewModel()
        {
            Activator = new ViewModelActivator();
            
            _handler = Locator.Current.GetService<IEmpaticaHandler>();
            if(_handler == null)
            {
                _handler = new EmpaticaHandler();
                Locator.CurrentMutable.RegisterConstant(_handler, typeof(IEmpaticaHandler));
            }

            _handler.AuthenticateDevice();
    
            this.WhenActivated(disposable =>
            {
                HandleActivation();

                Disposable.Create(() => HandleDeactivation())
                .DisposeWith(disposable);

                this.WhenAnyValue(x => x._handler.DeviceDelegate.BVP)
                .Select(x => x.Value)
                .ToPropertyEx(this, x => x.BVP)
                .DisposeWith(disposable);

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

            //DisconnectDevice = ReactiveCommand.Create(_handler.DisconnectDevice);
            
            NavigateToChartPage = ReactiveCommand.CreateFromTask(async () => {
                
                await Navigation.PushAsync(Locator.Current.GetService<IViewFor<EmpaticaChartViewModel>>() as EmpaticaChartPage);
            });

        }

        public void FetchData()
        {
            if (_handler.GetDeviceStatus() != EmpaticaDeviceStatus.Connecting)
            {
                return;
            }

            Console.WriteLine("Fetching...");

            GSR = _handler.GetLatestGSR().Value;
            //BVP = _handler.GetLatestBVP().Value;
            IBI = _handler.GetLatestIBI().Value;
            Temperature = _handler.GetLatestTemperature().Value;
            HR = _handler.GetLatestHR().Value;

        }

        private void HandleActivation()
        {
            
        }

        private void HandleDeactivation()
        {

        }
    }
}

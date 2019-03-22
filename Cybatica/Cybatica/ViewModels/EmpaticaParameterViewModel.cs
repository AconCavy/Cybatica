using Cybatica.Empatica;
using Cybatica.Models;
using ReactiveUI;
using ReactiveUI.XamForms;
using ReactiveUI.Fody.Helpers;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Splat;

namespace Cybatica.ViewModels
{
    public class EmpaticaParameterViewModel : BaseViewModel, ISupportsActivation
    {

        [Reactive] public float BVP { get; private set; }

        [Reactive] public float GSR { get; private set; }

        [Reactive] public float IBI { get; private set; }

        [Reactive] public float HR { get; private set; }

        [Reactive] public float Temperature { get; private set; }


        public ICommand DiscoverDeviceCommand { get; private set; }
        public ICommand DisconnectCommand { get; private set; }

        public ViewModelActivator Activator { get; }

        private EmpaticaHandler _handler;
        private bool _isConnecting;

        public EmpaticaParameterViewModel()
        {
            Title = "EmpaticaParameter";
            _handler = new EmpaticaHandler();
            Activator = new ViewModelActivator();

            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));

            this.WhenActivated(disposable =>
            {
                HandleActivation();

                Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ => FetchData())
                .DisposeWith(disposable);

                Disposable.Create(() => HandleDeactivation())
                .DisposeWith(disposable);

            });

            DiscoverDeviceCommand = ReactiveCommand.CreateFromTask(async () =>
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

            DisconnectCommand = ReactiveCommand.Create(() =>
            {
                _handler.DisconnectDevice();
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
            BVP = _handler.GetLatestBVP().Value;
            IBI = _handler.GetLatestIBI().Value;
            Temperature = _handler.GetLatestTemperature().Value;
            HR = _handler.GetLatestHR().Value;

        }

        private void HandleActivation()
        {
            _handler.AuthenticateDevice();
        }

        private void HandleDeactivation()
        {

        }
    }
}

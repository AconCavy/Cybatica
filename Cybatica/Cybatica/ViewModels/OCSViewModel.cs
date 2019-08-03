using Cybatica.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Cybatica.ViewModels
{
    public class OCSViewModel : ReactiveObject, ISupportsActivation
    {
        [Reactive] public float Cybersickness { get; private set; }
        [Reactive] public float Nnmean { get; private set; }
        [Reactive] public float Sdnn { get; private set; }
        [Reactive] public float Rmssd { get; private set; }
        [Reactive] public float PpSd1 { get; private set; }
        [Reactive] public float PpSd2 { get; private set; }
        [Reactive] public float Scr { get; private set; }

        public ViewModelActivator Activator { get; private set; }

        private readonly CybaticaHandler _handler;
        private readonly IObservable<long> _observer;

        public OCSViewModel()
        {
            Activator = new ViewModelActivator();

            _handler = new CybaticaHandler();

            _observer = Observable.Interval(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler);

            this.WhenActivated(disposable =>
            {
                _observer.Subscribe(_ => FetchData())
                .DisposeWith(disposable);

            });

        }

        private void FetchData()
        {
            Cybersickness = _handler.GetCybersickness();
            Nnmean = _handler.GetNnmean();
            Sdnn = _handler.GetSdnn();
            Rmssd = _handler.GetRmssd();
            PpSd1 = _handler.GetPpSd1();
            PpSd2 = _handler.GetPpSd2();
            Scr = _handler.GetScr();

        }

        private void ResetValues()
        {
            Cybersickness = default;
            Nnmean = default;
            Sdnn = default;
            Rmssd = default;
            PpSd1 = default;
            PpSd2 = default;
            Scr = default;

        }

    }

}

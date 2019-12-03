using System;
using System.Reactive.Linq;
using System.Windows.Input;
using Cybatica.Empatica;
using Cybatica.Services;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using Xamarin.Forms;

namespace Cybatica.ViewModels
{
    public class BioDataViewModel : ReactiveObject
    {
        private readonly ICybaticaHandler _cybaticaHandler;

        public BioDataViewModel(ICybaticaHandler cybaticaHandler = null)
        {
            _cybaticaHandler = cybaticaHandler ?? Locator.Current.GetService<ICybaticaHandler>();

            this.WhenAnyValue(x => x._cybaticaHandler.Acceleration)
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Acceleration);

            this.WhenAnyValue(x => x._cybaticaHandler.Bvp)
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Bvp);

            this.WhenAnyValue(x => x._cybaticaHandler.Gsr)
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Gsr);

            this.WhenAnyValue(x => x._cybaticaHandler.Hr)
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Hr);

            this.WhenAnyValue(x => x._cybaticaHandler.Ibi)
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Ibi);

            this.WhenAnyValue(x => x._cybaticaHandler.Temperature)
                .SubscribeOn(RxApp.TaskpoolScheduler)
                .Sample(TimeSpan.FromSeconds(1))
                .ObserveOn(RxApp.MainThreadScheduler)
                .ToPropertyEx(this, x => x.Temperature);

            ChartCommand = ReactiveCommand.CreateFromTask(async () => { await Shell.Current.GoToAsync("bioChart"); });
        }

        [Reactive] public Acceleration Acceleration { [ObservableAsProperty] get; private set; }
        [Reactive] public Bvp Bvp { [ObservableAsProperty] get; private set; }
        [Reactive] public Gsr Gsr { [ObservableAsProperty] get; private set; }
        [Reactive] public Hr Hr { [ObservableAsProperty] get; private set; }
        [Reactive] public Ibi Ibi { [ObservableAsProperty] get; private set; }
        [Reactive] public Temperature Temperature { [ObservableAsProperty] get; private set; }

        public ICommand ChartCommand { get; }
    }
}
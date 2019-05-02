using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using System.Reactive.Disposables;
using Xamarin.Forms.Xaml;

namespace Cybatica.Views
{
    public partial class EmpaticaChartPage : ReactiveContentPage<EmpaticaChartViewModel>
	{
        public EmpaticaChartPage()
        {
            ViewModel = Locator.Current.GetService<IReactiveObject>(typeof(EmpaticaChartViewModel).FullName)
                as EmpaticaChartViewModel;

            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel,
                    vm => vm.BVP,
                    v => v.BVP.ItemsSource)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.IBI,
                    v => v.IBI.ItemsSource)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.HR,
                    v => v.HR.ItemsSource)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.GSR,
                    v => v.EDA.ItemsSource)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.Temperature,
                    v => v.Temperature.ItemsSource)
                    .DisposeWith(disposable);

            });

        }

	}

}
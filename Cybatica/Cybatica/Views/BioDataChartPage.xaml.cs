using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using System.Reactive.Disposables;
using Xamarin.Forms.Xaml;

namespace Cybatica.Views
{
    public partial class BioDataChartPage : ReactiveContentPage<BioDataChartViewModel>
	{
        public BioDataChartPage()
        {
            ViewModel = Locator.Current.GetService<IReactiveObject>(typeof(BioDataChartViewModel).FullName)
                as BioDataChartViewModel;

            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel,
                    vm => vm.Bvp,
                    v => v.Bvp.ItemsSource)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.Ibi,
                    v => v.Ibi.ItemsSource)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.Hr,
                    v => v.Hr.ItemsSource)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.Gsr,
                    v => v.Eda.ItemsSource)
                    .DisposeWith(disposable);

                this.OneWayBind(ViewModel,
                    vm => vm.Temperature,
                    v => v.Temperature.ItemsSource)
                    .DisposeWith(disposable);

            });

        }

	}

}
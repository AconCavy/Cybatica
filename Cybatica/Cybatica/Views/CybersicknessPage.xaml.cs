using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using System.Reactive.Disposables;
using Xamarin.Forms.Xaml;

namespace Cybatica.Views
{
    public partial class CybersicknessPage : ReactiveContentPage<CybersicknessViewModel>
	{
        public CybersicknessPage()
        {
            ViewModel = Locator.Current.GetService<IReactiveObject>(typeof(CybersicknessViewModel).FullName)
                as CybersicknessViewModel;

            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                this.OneWayBind(ViewModel,
                    vm => vm.Nnmean,
                    v => v.Nnmean.Text,
                    x => x.ToString("F2"));

                this.OneWayBind(ViewModel,
                    vm => vm.Sdnn,
                    v => v.Sdnn.Text,
                    x => x.ToString("F2"));

                this.OneWayBind(ViewModel,
                    vm => vm.Rmssd,
                    v => v.Rmssd.Text,
                    x => x.ToString("F2"));

                this.OneWayBind(ViewModel,
                    vm => vm.Sd1,
                    v => v.Sd1.Text,
                    x => x.ToString("F2"));

                this.OneWayBind(ViewModel,
                    vm => vm.Sd2,
                    v => v.Sd2.Text,
                    x => x.ToString("F2"));

            });
        }
	}
}
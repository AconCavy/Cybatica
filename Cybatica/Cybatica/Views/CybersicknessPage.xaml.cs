using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using Xamarin.Forms.Xaml;

namespace Cybatica.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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
                    vm => vm.PpSd1,
                    v => v.Sd1.Text,
                    x => x.ToString("F2"));

                this.OneWayBind(ViewModel,
                    vm => vm.PpSd2,
                    v => v.Sd2.Text,
                    x => x.ToString("F2"));

            });
        }
	}
}
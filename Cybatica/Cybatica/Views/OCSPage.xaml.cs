using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;
using Xamarin.Forms.Xaml;

namespace Cybatica.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OCSPage : ReactiveContentPage<OCSViewModel>
	{
        public OCSPage()
        {
            ViewModel = Locator.Current.GetService<IReactiveObject>(typeof(OCSViewModel).FullName)
                as OCSViewModel;

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

            });
        }
	}
}
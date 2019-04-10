using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.XamForms;
using Cybatica.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Splat;

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
                    vm => vm.NNMean,
                    v => v.NNMean.Text,
                    x => x.ToString("F2"));

                this.OneWayBind(ViewModel,
                    vm => vm.SDNN,
                    v => v.SDNN.Text,
                    x => x.ToString("F2"));

                this.OneWayBind(ViewModel,
                    vm => vm.RMSSD,
                    v => v.RMSSD.Text,
                    x => x.ToString("F2"));

                this.OneWayBind(ViewModel,
                    vm => vm.SD1,
                    v => v.SD1.Text,
                    x => x.ToString("F2"));

                this.OneWayBind(ViewModel,
                    vm => vm.SD2,
                    v => v.SD2.Text,
                    x => x.ToString("F2"));
            });
        }
	}
}
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

namespace Cybatica.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CybersicknessPage : ReactiveContentPage<CybersicknessViewModel>
	{
		public CybersicknessPage ()
		{
			InitializeComponent ();
		}
	}
}
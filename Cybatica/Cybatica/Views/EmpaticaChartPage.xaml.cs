using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ReactiveUI;
using ReactiveUI.XamForms;
using Cybatica.ViewModels;

namespace Cybatica.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EmpaticaChartPage : ReactiveContentPage<EmpaticaChartViewModel>
	{
		public EmpaticaChartPage ()
		{
			InitializeComponent ();
		}
	}
}
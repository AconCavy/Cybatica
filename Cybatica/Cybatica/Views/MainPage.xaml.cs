using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ReactiveUI;
using ReactiveUI.XamForms;
using Cybatica.ViewModels;

namespace Cybatica.Views
{
    public partial class MainPage : ReactiveTabbedPage<MainViewModel>
    {
        public MainPage()
        {
            this.ViewModel = new MainViewModel();
            InitializeComponent();
        }
    }
}

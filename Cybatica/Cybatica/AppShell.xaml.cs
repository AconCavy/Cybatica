using Cybatica.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cybatica
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoute();
        }

        private static void RegisterRoute()
        {
            Routing.RegisterRoute("homeAbout", typeof(AboutPage));
            Routing.RegisterRoute("ocsChart", typeof(OcsChartPage));
            Routing.RegisterRoute("bioChart", typeof(BioDataChartPage));
        }
    }
}
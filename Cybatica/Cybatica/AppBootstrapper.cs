using Cybatica.Models;
using Cybatica.Services;
using Cybatica.Utilities;
using ReactiveUI;
using Splat;

namespace Cybatica
{
    public class AppBootstrapper : ReactiveObject
    {
        public AppBootstrapper()
        {
            RegisterDependencies();
        }

        private static void RegisterDependencies()
        {
            // Services
            Locator.CurrentMutable.RegisterLazySingleton(
                () => new CybaticaHandler(), typeof(ICybaticaHandler));
            Locator.CurrentMutable.RegisterLazySingleton(
                () => new ShareDataExporter("Cybatica", "csv"), typeof(IDataExporter));
        }
    }
}
using Cybatica.Droid.Empatica;
using Cybatica.Empatica;
using Splat;

namespace Cybatica.Droid
{
    public class AppBootstrapper
    {
        public AppBootstrapper()
        {
            RegisterDependencies();
        }

        private static void RegisterDependencies()
        {
            Locator.CurrentMutable.RegisterLazySingleton(
                () => new EmpaticaHandler(), typeof(IEmpaticaHandler));
        }
    }
}
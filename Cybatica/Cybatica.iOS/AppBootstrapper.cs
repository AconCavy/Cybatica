using Cybatica.Empatica;
using Cybatica.iOS.Empatica;
using Splat;

namespace Cybatica.iOS
{
    public class AppBootstrapper
    {
        public AppBootstrapper()
        {
            RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            Locator.CurrentMutable.RegisterLazySingleton(
                () => new EmpaticaHandler(), typeof(IEmpaticaHandler));
        }
    }
}
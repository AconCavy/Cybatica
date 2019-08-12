using Cybatica.Empatica;
using Cybatica.Mocks;
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
                () => new MockEmpaticaHandler(), typeof(IEmpaticaHandler));
        }
    }
}
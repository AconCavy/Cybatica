using Cybatica.Empatica;
using Cybatica.Services;
using Splat;

namespace Cybatica.Droid
{
    public class AppBootstrapper
    {
        public AppBootstrapper()
        {
            RegisterDependencies();
        }

        private void RegisterDependencies()
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new MockEmpaticaDelegate(), typeof(IEmpaticaDelegate));
            Locator.CurrentMutable.RegisterLazySingleton(() => new MockEmpaticaDeviceDelegate(), typeof(IEmpaticaDeviceDelegate));
            Locator.CurrentMutable.RegisterConstant(new MockEmpaticaAPI(), typeof(IEmpaticaAPI));

        }
    }
}
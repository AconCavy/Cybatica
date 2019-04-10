using Cybatica.Empatica;
using Cybatica.Services;
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
            Locator.CurrentMutable.RegisterLazySingleton(() => new MockEmpaticaDelegate(), typeof(IEmpaticaDelegate));
            Locator.CurrentMutable.RegisterLazySingleton(() => new MockEmpaticaDeviceDelegate(), typeof(IEmpaticaDeviceDelegate));
            Locator.CurrentMutable.RegisterConstant(new MockEmpaticaAPI(), typeof(IEmpaticaAPI));
        }
    }
}
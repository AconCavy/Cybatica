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
            //MockRegisterDependencies();
        }

        private void RegisterDependencies()
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new EmpaticaDelegate(), typeof(IEmpaticaDelegate));
            Locator.CurrentMutable.RegisterLazySingleton(() => new EmpaticaDeviceDelegate(), typeof(IEmpaticaDeviceDelegate));
            Locator.CurrentMutable.RegisterConstant(new EmpaticaAPI(), typeof(IEmpaticaApi));
        }

        private void MockRegisterDependencies()
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new MockEmpaticaDelegate(), typeof(IEmpaticaDelegate));
            Locator.CurrentMutable.RegisterLazySingleton(() => new MockEmpaticaDeviceDelegate(), typeof(IEmpaticaDeviceDelegate));
            Locator.CurrentMutable.RegisterConstant(new MockEmpaticaApi(), typeof(IEmpaticaApi));
        }
    }
}
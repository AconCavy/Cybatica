using Cybatica.Droid.Empatica;
using Cybatica.Empatica;
using Cybatica.Services;
using Splat;

namespace Cybatica.Droid
{
    public class AppBootstrapper
    {
        public AppBootstrapper()
        {
            //RegisterDependencies();
            MockRegisterDependencies();
        }

        private void RegisterDependencies()
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new EmpaticaDelegate(), typeof(IEmpaticaDelegate));
            Locator.CurrentMutable.RegisterLazySingleton(() => new EmpaticaDeviceDelegate(), typeof(IEmpaticaDeviceDelegate));
            Locator.CurrentMutable.RegisterConstant(new EmpaticaAPI(), typeof(IEmpaticaAPI));

        }

        private void MockRegisterDependencies()
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new MockEmpaticaDelegate(), typeof(IEmpaticaDelegate));
            Locator.CurrentMutable.RegisterLazySingleton(() => new MockEmpaticaDeviceDelegate(), typeof(IEmpaticaDeviceDelegate));
            Locator.CurrentMutable.RegisterConstant(new MockEmpaticaAPI(), typeof(IEmpaticaAPI));

        }
    }
}
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
            //MockRegisterDependencies();
        }

        private void RegisterDependencies()
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new EmpaticaDeviceDelegate(), typeof(IEmpaticaDelegate));
            Locator.CurrentMutable.RegisterLazySingleton(() => new EmpaticaDeviceDelegate(), typeof(IEmpaticaDeviceDelegate));
            Locator.CurrentMutable.RegisterConstant(new EmpaticaApi(), typeof(IEmpaticaApi));

        }

        private void MockRegisterDependencies()
        {
            Locator.CurrentMutable.RegisterLazySingleton(() => new Mocks.MockEmpaticaDelegate(), typeof(IEmpaticaDelegate));
            Locator.CurrentMutable.RegisterLazySingleton(() => new Mocks.MockEmpaticaDeviceDelegate(), typeof(IEmpaticaDeviceDelegate));
            Locator.CurrentMutable.RegisterConstant(new Mocks.MockEmpaticaApi(), typeof(IEmpaticaApi));

        }
    }
}
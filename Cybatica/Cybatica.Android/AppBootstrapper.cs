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
            Locator.CurrentMutable.Register(() => new EmpaticaDelegate(), typeof(IEmpaticaDelegate));
            Locator.CurrentMutable.Register(() => new EmpaticaDeviceDelegate(), typeof(IEmpaticaDeviceDelegate));
            Locator.CurrentMutable.RegisterConstant(new EmpaticaAPI(), typeof(IEmpaticaApi));

        }

        private void MockRegisterDependencies()
        {
            Locator.CurrentMutable.Register(() => new MockEmpaticaDelegate(), typeof(IEmpaticaDelegate));
            Locator.CurrentMutable.Register(() => new MockEmpaticaDeviceDelegate(), typeof(IEmpaticaDeviceDelegate));
            Locator.CurrentMutable.RegisterConstant(new MockEmpaticaApi(), typeof(IEmpaticaApi));

        }
    }
}
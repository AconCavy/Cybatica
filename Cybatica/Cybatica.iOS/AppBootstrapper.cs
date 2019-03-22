using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Splat;
using Cybatica.Empatica;
using Cybatica.iOS.Empatica;

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
            Locator.CurrentMutable.RegisterLazySingleton(() => new EmpaticaAPI(), typeof(IEmpaticaAPIService));
            Locator.CurrentMutable.RegisterLazySingleton(() => new EmpaticaDelegate(), typeof(IEmpaticaDelegateService));
            Locator.CurrentMutable.RegisterLazySingleton(() => new EmpaticaDeviceDelegate(), typeof(IEmpaticaDeviceDelegateService));
            
        }
    }
}
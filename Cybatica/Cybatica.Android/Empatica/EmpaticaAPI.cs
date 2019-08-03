using Com.Empatica.Empalink;
using Cybatica.Empatica;
using Splat;
using System;

namespace Cybatica.Droid.Empatica
{
    public class EmpaticaApi : IEmpaticaApi
    {
        public Cybatica.Empatica.EmpaticaDevice Device =>
            new Cybatica.Empatica.EmpaticaDevice();

        private readonly EmpaticaDeviceDelegate _deviceDelegate;
        private EmpaDeviceManager _deviceManager;

        public EmpaticaApi()
        {
            _deviceDelegate = Locator.Current.GetService<IEmpaticaDeviceDelegate>()
                as EmpaticaDeviceDelegate;
            _deviceManager = new EmpaDeviceManager(Android.App.Application.Context,
                _deviceDelegate, _deviceDelegate);
        }


        public void AuthenticateWithAPIKey(string apiKey)
        {
            _deviceManager.AuthenticateWithAPIKey(apiKey);
        }

        public void Connect(Cybatica.Empatica.EmpaticaDevice device)
        {
            _deviceManager.StopScanning();
            var target = _deviceDelegate.GetDevice(device);
            _deviceManager.ConnectDevice(target);
            _deviceDelegate.SetConnectedTime();
        }

        public void Disconnect()
        {
            if (_deviceManager == null)
            {
                return;
            }
            _deviceManager.Disconnect();

            _deviceManager = null;
        }

        public void PrepareForBackground()
        {
            Console.WriteLine("Prepare for background on Android");
        }

        public void PrepareForResume()
        {
            Console.WriteLine("Prepare for resume on Android");
        }

        public void Discover()
        {
            _deviceManager.StartScanning();
        }
    }
}
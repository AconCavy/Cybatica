using Cybatica.Empatica;
using System;

namespace Cybatica.Droid.Empatica
{
    public class EmpaticaAPI : IEmpaticaApi
    {
        public EmpaticaDevice Device => new EmpaticaDevice();

        public EmpaticaAPI()
        {

        }


        public void AuthenticateWithAPIKey(string apiKey)
        {
            Console.WriteLine("Authenticated on Android");
        }

        public void Connect(EmpaticaDevice device)
        {
            Console.WriteLine("Connected on Android");
        }

        public void Disconnect()
        {
            Console.WriteLine("Disconnected on Android");
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
            
        }
    }
}
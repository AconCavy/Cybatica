using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Cybatica.Empatica;

namespace Cybatica.Droid.Empatica
{
    public class EmpaticaAPI : IEmpaticaAPI
    {
        public EmpaticaDevice Device => new EmpaticaDevice();

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

        public void PrepareForBackGround()
        {
            Console.WriteLine("Prepare for background on Android");
        }

        public void PrepareForResume()
        {
            Console.WriteLine("Prepare for resume on Android");
        }
    }
}
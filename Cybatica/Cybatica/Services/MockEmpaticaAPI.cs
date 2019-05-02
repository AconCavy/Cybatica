using System;
using System.Collections.Generic;
using System.Text;
using Cybatica.Empatica;

namespace Cybatica.Services
{
    public class MockEmpaticaAPI : IEmpaticaAPI
    {
        public EmpaticaDevice Device { get; private set; }

        public MockEmpaticaAPI()
        {
            Console.WriteLine("MockEmpaticaAPI() in mock");
        }

        public void AuthenticateWithAPIKey(string apiKey)
        {
            Console.WriteLine("AuthenticateWithAPIKey(string apiKey) in mock");
        }

        public void Connect(EmpaticaDevice device)
        {
            Console.WriteLine("Connect(EmpaticaDevice device) in mock");
        }

        public void Disconnect()
        {
            Console.WriteLine("Disconnect() in mock");
        }

        public void PrepareForBackground()
        {
            Console.WriteLine("PrepareForBackground() in mock");
        }

        public void PrepareForResume()
        {
            Console.WriteLine("PrepareForResume() in mock");
        }

        
    }
}

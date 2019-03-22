using System;
using System.Collections.Generic;
using System.Text;

namespace Cybatica.Empatica
{
    public interface IEmpaticaAPIService
    {
        EmpaticaDevice Device { get; }

        void AuthenticateWithAPIKey(string apiKey);

        void Connect(EmpaticaDevice device);

        void Disconnect();


    }
}

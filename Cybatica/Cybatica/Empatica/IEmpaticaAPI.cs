using System;
using System.Collections.Generic;
using System.Text;

namespace Cybatica.Empatica
{
    public interface IEmpaticaApi
    {
        EmpaticaDevice Device { get; }

        void AuthenticateWithAPIKey(string apiKey);

        void Connect(EmpaticaDevice device);

        void Disconnect();

        void PrepareForBackground();

        void PrepareForResume();

    }
}

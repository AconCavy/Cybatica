using System;
using System.Collections.Generic;
using System.Text;

namespace Cybatica.Empatica
{
    public interface IEmpaticaAPI
    {
        EmpaticaDevice Device { get; }

        void AuthenticateWithAPIKey(string apiKey);

        void Connect(EmpaticaDevice device);

        void Disconnect();

        void PrepareForBackGround();

        void PrepareForResume();

    }
}

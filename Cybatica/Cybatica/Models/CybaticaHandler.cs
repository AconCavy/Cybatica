using Cybatica.Empatica;
using Cybatica.Services;

namespace Cybatica.Models
{
    public class CybaticaHandler : ICybaticaHandler
    {
        public EmpaticaSession EmpaticaSession { get; }

        public CybaticaHandler()
        {
            EmpaticaSession = new EmpaticaSession();
        }

        public void InitializeSession()
        {
        }

        public void Authenticate(string key)
        {
        }

        public void Connect(EmpaticaDevice device)
        {
        }

        public void Disconnect()
        {
        }

    }
}

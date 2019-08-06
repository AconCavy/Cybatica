using Cybatica.Empatica;

namespace Cybatica.Services
{
    public interface ICybaticaHandler
    {
        EmpaticaSession EmpaticaSession { get; }

        void Authenticate(string key);

        void Connect(EmpaticaDevice device);

        void Disconnect();

        void InitializeSession();
    }
}

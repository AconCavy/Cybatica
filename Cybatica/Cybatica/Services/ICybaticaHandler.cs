using Cybatica.Empatica;
using Cybatica.Models;
using System.Collections.ObjectModel;

namespace Cybatica.Services
{
    public interface ICybaticaHandler
    {
        EmpaticaSession EmpaticaSession { get; }

        OCSSession OCSSession { get; }

        BioDataModel BioDataModel { get; }

        OCSModel OcsModel { get; }

        ReadOnlyCollection<EmpaticaDevice> Devices { get; }

        void Connect(EmpaticaDevice device);

        void Disconnect();

        void InitializeSession();

        void StartSession();

        void StopSession();

    }
}

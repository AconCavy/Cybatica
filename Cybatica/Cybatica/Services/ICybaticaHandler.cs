using System.Collections.ObjectModel;
using Cybatica.Empatica;
using Cybatica.Models;

namespace Cybatica.Services
{
    public interface ICybaticaHandler
    {
        EmpaticaSession EmpaticaSession { get; }

        OcsSession OcsSession { get; }

        BioDataModel BioDataModel { get; }

        OcsModel OcsModel { get; }

        ReadOnlyCollection<EmpaticaDevice> Devices { get; }

        void Connect(EmpaticaDevice device);

        void Disconnect();

        void Discover();

        void InitializeSession();

        void StartSession();

        void StopSession();
    }
}
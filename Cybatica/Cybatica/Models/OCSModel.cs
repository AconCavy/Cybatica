using Cybatica.Empatica;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Cybatica.Models
{
    public class OCSModel : ReactiveObject
    {
        [Reactive] public float Ocs { get; private set; }
        [Reactive] public float NnMean { get; private set; }
        [Reactive] public float SdNn { get; private set; }
        [Reactive] public float MeanEda { get; private set; }
        [Reactive] public float PeakEda { get; private set; }

        private readonly EmpaticaSession _empaticaSession;

        public OCSModel(EmpaticaSession session)
        {
            _empaticaSession = session;
        }
    }
}

using Cybatica.Empatica;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Cybatica.Models
{
    public class BioDataModel : ReactiveObject
    {
        [Reactive] public Bvp Bvp { get; private set; }
        [Reactive] public Ibi Ibi { get; private set; }
        [Reactive] public Hr Hr { get; private set; }
        [Reactive] public Gsr Gsr { get; private set; }
        [Reactive] public Temperature Temperature { get; private set; }
        [Reactive] public Acceleration Acceleration { get; private set; }

        private readonly EmpaticaSession _empaticaSession;

        public BioDataModel(EmpaticaSession session)
        {
            _empaticaSession = session;
        }
    }
}

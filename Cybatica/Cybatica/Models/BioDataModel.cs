using Cybatica.Empatica;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Cybatica.Models
{
    public class BioDataModel : ReactiveObject
    {
        [Reactive] public Bvp Bvp { get; set; }
        [Reactive] public Ibi Ibi { get; set; }
        [Reactive] public Hr Hr { get; set; }
        [Reactive] public Gsr Gsr { get; set; }
        [Reactive] public Temperature Temperature { get; set; }
        [Reactive] public Acceleration Acceleration { get; set; }

        public BioDataModel()
        {
        }

        public void Reset()
        {
            Bvp = default;
            Ibi = default;
            Hr = default;
            Gsr = default;
            Temperature = default;
            Acceleration = default;
        }
    }
}

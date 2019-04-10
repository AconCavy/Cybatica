using Cybatica.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace Cybatica.ViewModels
{
    public class CybersicknessViewModel : ReactiveObject
    {
        [Reactive] public float Cybersickness { get; private set; }
        [Reactive] public float NNMean { get; private set; }
        [Reactive] public float SDNN { get; private set; }
        [Reactive] public float RMSSD { get; private set; }
        [Reactive] public float SD1 { get; private set; }
        [Reactive] public float SD2 { get; private set; }
        [Reactive] public float SCR { get; private set; }

        public CybersicknessViewModel()
        {
        }
    }
}

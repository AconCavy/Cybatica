using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Cybatica.ViewModels
{
    public class CybersicknessViewModel : ReactiveObject
    {
        [Reactive] public float Cybersickness { get; private set; }
        [Reactive] public float Nnmean { get; private set; }
        [Reactive] public float Sdnn { get; private set; }
        [Reactive] public float Rmssd { get; private set; }
        [Reactive] public float Sd1 { get; private set; }
        [Reactive] public float Sd2 { get; private set; }
        [Reactive] public float Scr { get; private set; }

        public CybersicknessViewModel()
        {
            
        }
    }
}

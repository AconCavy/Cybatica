﻿using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace Cybatica.Models
{
    public class OCSModel : ReactiveObject
    {
        [Reactive] public float Ocs { get; set; }
        [Reactive] public float NnMean { get; set; }
        [Reactive] public float SdNn { get; set; }
        [Reactive] public float MeanEda { get; set; }
        [Reactive] public float PeakEda { get; set; }

        public OCSModel()
        {
        }

        public void Reset()
        {
            Ocs = default;
            NnMean = default;
            SdNn = default;
            MeanEda = default;
            PeakEda = default;
        }

    }
}

using Cybatica.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Cybatica.Utilities
{
    public class OcsCalculator
    {
        private readonly List<OcsParameter> _parameters;

        public OcsCalculator()
        {
            var assembly = typeof(OcsCalculator).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("Cybatica.ocs_parameters.json");

            using var reader = new StreamReader(stream ?? throw new InvalidOperationException());
            var json = reader.ReadToEnd();
            _parameters = JsonSerializer.Deserialize<List<OcsParameter>>(json);
        }

        public float CalculateOcs(float nnMean, float sdNn, float meanEda, float peakEda)
        {
            string[] parameters =
            {
                "Discomfort", "Fatigue", "Headache", "EyeStrain", "DifficultyFocusing", "IncreasedSalivation",
                "Sweating", "Nausea", "DifficultyConcentrating", "HeadFullness", "BlurredVision", "OpenedDizziness",
                "ClosedDizziness", "Vertigo", "StomachAwareness", "Burping"
            };
            var scores = new Dictionary<string, float>();

            foreach (var parameterName in parameters)
            {
                var parameter = _parameters.Find(x => x.Name.Equals(parameterName));
                var score = Math.Clamp(nnMean * parameter.NnMean + sdNn * parameter.SdNn + meanEda * parameter.MeanEda
                            + peakEda * parameter.PeakEda, 0, 3);
                scores.Add(parameterName, score);
            }

            string[] nauseaCategory =
            {
                "Discomfort", "IncreasedSalivation", "Sweating", "Nausea", "DifficultyConcentrating",
                "StomachAwareness", "Burping"
            };
//            {
//                "Sweating", "DifficultyConcentrating"
//            };
            string[] oculomotorCategory =
            {
                "Discomfort", "Fatigue", "Headache", "EyeStrain", "DifficultyFocusing", "DifficultyConcentrating",
                "BlurredVision"
            };
//            {
//                "Headache", "EyeStrain", "DifficultyFocusing", "DifficultyConcentrating", "BlurredVision"
//            };
            string[] disorientationCategory =
            {
                "DifficultyFocusing", "Nausea", "HeadFullness", "BlurredVision", "OpenedDizziness",
                "ClosedDizziness", "Vertigo"
            };
//            {
//                "DifficultyFocusing", "HeadFullness", "BlurredVision", "ClosedDizziness", "Vertigo"
//            };

            var nausea = scores.Where(x => nauseaCategory.Contains(x.Key)).Sum(x => x.Value);
            var oculomotor = scores.Where(x => oculomotorCategory.Contains(x.Key)).Sum(x => x.Value);
            var disorientation = scores.Where(x => disorientationCategory.Contains(x.Key)).Sum(x => x.Value);
            return 3.74f * (float) (nausea + oculomotor + disorientation);
        }
    }
}
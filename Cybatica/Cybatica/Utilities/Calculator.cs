using Cybatica.Empatica;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cybatica.Utilities
{
    public static class Calculator
    {
        public static float CalcOcs()
        {
            return 0;
        }

        public static float CalcNnMean(IEnumerable<Ibi> ibiList)
        {
            return CalcMean(ibiList.Select(x => x.Value));
        }

        public static float CalcSdNn(IEnumerable<Ibi> ibiList)
        {
            if (ibiList.Count() == 0)
            {
                return 0;
            }

            var valueList = ibiList.Select(x => x.Value);
            var result = CalcSd(valueList);
            return result;
        }

        public static float CalcRmsSd(IEnumerable<Ibi> ibiList)
        {
            if (ibiList.Count() < 2)
            {
                return 0;
            }

            var valueList = ibiList.Select(x => x.Value).ToArray();
            var count = valueList.Length - 1;
            var diffList = new float[count];

            for (var index = 0; index < count; index++)
            {
                diffList[index] = (valueList[index + 1] - valueList[index]);
            }

            var powMean = diffList.Select(x => x * x).Average();
            var result = (float)Math.Sqrt(powMean);

            return result;

        }

        public static float CalcPpSd1(IEnumerable<Ibi> ibiList)
        {
            if (ibiList.Count() == 0)
            {
                return 0;
            }

            var valueList = ibiList.Select(x => x.Value).ToArray();
            var count = valueList.Length - 1;
            var diffList = new float[count];
            var root2 = Math.Sqrt(2);

            for (var index = 0; index < count; index++)
            {
                diffList[index] = (valueList[index + 1] - valueList[index]) / (float)root2;
            }

            var result = CalcSd(diffList);
            return result;
        }

        public static float CalcPpSd2(IEnumerable<Ibi> ibiList)
        {
            if (ibiList.Count() == 0)
            {
                return 0;
            }

            var valueList = ibiList.Select(x => x.Value).ToArray();
            var count = valueList.Length - 1;
            var addList = new float[count];
            var root2 = Math.Sqrt(2);

            for (var index = 0; index < count; index++)
            {
                addList[index] = (valueList[index + 1] + valueList[index]) / (float)root2;
            }

            var result = CalcSd(addList);
            return result;
        }

        public static float CalcMeanEda(IEnumerable<Gsr> gsrList)
        {
            if (gsrList.Count() == 0)
            {
                return 0;
            }
            return CalcMean(gsrList.Select(x => x.Value));
        }

        public static float CalcPeakEda(IEnumerable<Gsr> gsrList)
        {
            if (gsrList.Count() == 0)
            {
                return 0;
            }
            return gsrList.Max(x => x.Value);
        }

        private static float CalcMean(IEnumerable<float> list)
        {
            if (list.Count() == 0)
            {
                return 0;
            }

            return list.Average();
        }

        private static float CalcSd(IEnumerable<float> list)
        {
            var count = list.Count();

            if (count == 0)
            {
                return 0;
            }

            var mean = list.Average();
            var variance = list.Select(x => (x - mean) * (x - mean)).Sum() / (count - 1);

            return (float)Math.Sqrt(variance);
        }

    }
}

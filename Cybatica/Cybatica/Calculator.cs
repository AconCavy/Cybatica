using Cybatica.Empatica;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cybatica
{
    public static class Calculator
    {
        public static float CalcCybersickness()
        {
            return 0;
        }

        public static float CalcNnmean(IEnumerable<Ibi> ibiList)
        {
            if(ibiList.Count() == 0)
            {
                return 0;
            }
            
            var result = ibiList.Select(x => x.Value).Average();
            return result;
        }

        public static float CalcSdnn(IEnumerable<Ibi> ibiList)
        {
            if (ibiList.Count() == 0)
            {
                return 0;
            }

            var valueList = ibiList.Select(x => x.Value);
            var result = CalcSd(valueList);
            return result;
        }

        public static float CalcRmssd(IEnumerable<Ibi> ibiList)
        {
            if (ibiList.Count() == 0)
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
            var result = (float) Math.Sqrt(powMean);

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
                diffList[index] = (valueList[index + 1] - valueList[index]) / (float) root2;
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
                addList[index] = (valueList[index + 1] + valueList[index]) / (float) root2;
            }

            var result = CalcSd(addList);
            return result;
        }

        public static float CalcScr()
        {
            var result = 0;
            return result;
        }

        private static float CalcSd(IEnumerable<float> list)
        {
            var count = list.Count();
            var mean = list.Average();
            var variance = list.Select(x => (x - mean) * (x - mean)).Sum() / (count - 1);

            var result = Math.Sqrt(variance);

            return (float) result;
        }

    }
}

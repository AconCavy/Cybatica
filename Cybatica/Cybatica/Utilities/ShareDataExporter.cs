using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Cybatica.Empatica;
using Cybatica.Models;
using Cybatica.Services;
using ReactiveUI;
using Xamarin.Essentials;

namespace Cybatica.Utilities
{
    public class ShareDataExporter : IDataExporter
    {
        private readonly string _directoryName;
        private readonly string _extension;

        public ShareDataExporter() :
            this("Share", "csv")
        {
        }

        public ShareDataExporter(string directoryName, string extension)
        {
            _directoryName = directoryName;
            _extension = extension;
        }

        public void Export(string path, EmpaticaSession empaticaSession, OcsSession ocsSession)
        {
            var directoryName = $"{_directoryName}{DateTime.Now.ToFileTime()}";
            var directoryPath = Path.Combine(path, directoryName);
            var fileName = directoryPath + ".zip";

            RxApp.MainThreadScheduler.Schedule(async () =>
            {
                await Observable.Start(() =>
                {
                    if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

                    var bvp = Path.Combine(directoryPath, $"BVP.{_extension}");
                    File.WriteAllText(bvp, FormatData(empaticaSession.Bvp.Items));

                    var ibi = Path.Combine(directoryPath, $"IBI.{_extension}");
                    File.WriteAllText(ibi, FormatData(empaticaSession.Ibi.Items));

                    var hr = Path.Combine(directoryPath, $"HR.{_extension}");
                    File.WriteAllText(hr, FormatData(empaticaSession.Hr.Items));

                    var gsr = Path.Combine(directoryPath, $"GSR.{_extension}");
                    File.WriteAllText(gsr, FormatData(empaticaSession.Gsr.Items));

                    var temperature = Path.Combine(directoryPath, $"Temperature.{_extension}");
                    File.WriteAllText(temperature, FormatData(empaticaSession.Temperature.Items));

                    var ocs = Path.Combine(directoryPath, $"OCS.{_extension}");
                    File.WriteAllText(ocs, FormatData(ocsSession.Ocs.Items));

                    var nnMean = Path.Combine(directoryPath, $"NNMean.{_extension}");
                    File.WriteAllText(nnMean, FormatData(ocsSession.NnMean.Items));

                    var sdNn = Path.Combine(directoryPath, $"SDNN.{_extension}");
                    File.WriteAllText(sdNn, FormatData(ocsSession.SdNn.Items));

                    var meanEda = Path.Combine(directoryPath, $"MeanEDA.{_extension}");
                    File.WriteAllText(meanEda, FormatData(ocsSession.MeanEda.Items));

                    var peakEda = Path.Combine(directoryPath, $"PeakEda.{_extension}");
                    File.WriteAllText(peakEda, FormatData(ocsSession.PeakEda.Items));

                    ZipFile.CreateFromDirectory(directoryPath, fileName);
                }, RxApp.TaskpoolScheduler);

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Export Data",
                    File = new ShareFile(fileName)
                });
            });
        }

        private static string FormatData<T>(IEnumerable<T> data)
        {
            var tmp = string.Join("\n", data);
            return tmp;
        }
    }
}
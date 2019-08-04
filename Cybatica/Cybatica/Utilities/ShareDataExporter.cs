using Cybatica.Empatica;
using Cybatica.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Xamarin.Essentials;

namespace Cybatica.Utilities
{
    public class ShareDataExporter : IDataExporter
    {
        private readonly string _directoryName;
        private readonly string _extention;

        public ShareDataExporter() :
            this("Share", "csv")
        {
        }

        public ShareDataExporter(string directoryName, string extention)
        {
            _directoryName = directoryName;
            _extention = extention;
        }

        public void Export(string path, EmpaticaSession empaticaSession)
        {
            var directoryName = $"{_directoryName}{DateTime.Now.ToFileTime()}";
            var directoryPath = Path.Combine(path, directoryName);
            var fileName = directoryPath + ".zip";

            RxApp.MainThreadScheduler.Schedule(async () =>
            {
                await Observable.Start(() =>
                {
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    var bvp = Path.Combine(directoryPath, $"BVP.{_extention}");
                    File.WriteAllText(bvp, FormatData(empaticaSession.Bvp.Items));

                    var ibi = Path.Combine(directoryPath, $"IBI.{_extention}");
                    File.WriteAllText(ibi, FormatData(empaticaSession.Ibi.Items));

                    var hr = Path.Combine(directoryPath, $"HR.{_extention}");
                    File.WriteAllText(hr, FormatData(empaticaSession.Hr.Items));

                    var gsr = Path.Combine(directoryPath, $"GSR.{_extention}");
                    File.WriteAllText(gsr, FormatData(empaticaSession.Gsr.Items));

                    var temperature = Path.Combine(directoryPath, $"Temperature.{_extention}");
                    File.WriteAllText(temperature, FormatData(empaticaSession.Temperature.Items));

                    ZipFile.CreateFromDirectory(directoryPath, fileName);

                }, RxApp.TaskpoolScheduler);

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Data Export",
                    File = new ShareFile(fileName)
                });
            });

        }

        private string FormatData<T>(IEnumerable<T> data)
        {
            var tmp = string.Join("\n", data);
            return tmp;

        }
    }
}

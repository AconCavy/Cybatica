using Cybatica.Views;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using System;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;

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

        /*
        public ReactiveCommand<Unit, Unit> Save { get; private set; }
        public ReactiveCommand<Unit, Unit> Export { get; private set; }
        
        private string _filePath;
        */

        public CybersicknessViewModel()
        {
            /*
            Save = ReactiveCommand.CreateFromObservable(() =>
            {
                var fileName = $"CybaticaExport{System.DateTime.Now}.csv";
                var _filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                return Observable.Start(() =>
                {
                    File.WriteAllText(_filePath, "Hello,World,hoge,hoge\nhoge");
                }, RxApp.TaskpoolScheduler);
            });

            Export = ReactiveCommand.CreateFromTask(async () =>
            {
                Console.WriteLine(File.Exists(_filePath));
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Export data",
                    File = new ShareFile(_filePath)
                });
            });
            */
        }
    }
}

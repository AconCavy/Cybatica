using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Cybatica.Models;
using Newtonsoft.Json;
using ReactiveUI;
using Xamarin.Forms;

namespace Cybatica.ViewModels
{
    public class AboutViewModel : ReactiveObject
    {
        public AboutViewModel()
        {
            var assembly = typeof(AboutViewModel).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("Cybatica.Licenses.json");

            using (var reader = new StreamReader(stream ?? throw new InvalidOperationException()))
            {
                var json = reader.ReadToEnd();
                var array = JsonConvert.DeserializeObject<License[]>(json);
                IEnumerable<License> filter;
                switch (Device.RuntimePlatform)
                {
                    case Device.Android:
                        filter = array.Where(x => x.IsVisibleAndroid);
                        break;
                    case Device.iOS:
                        filter = array.Where(x => x.IsVisibleiOS);
                        break;
                    default:
                        filter = array;
                        break;
                }

                Licenses = new ObservableCollection<License>(filter.OrderBy(x => x.Name));
            }
        }

        public ObservableCollection<License> Licenses { get; set; }
    }
}
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using Cybatica.Models;
using ReactiveUI;
using Xamarin.Forms;

namespace Cybatica.ViewModels
{
    public class AboutViewModel : ReactiveObject
    {
        private static string _licensesJson = "";

        public AboutViewModel()
        {
            if (_licensesJson.Equals(string.Empty))
            {
                var assembly = typeof(AboutViewModel).GetTypeInfo().Assembly;
                var stream = assembly.GetManifestResourceStream("Cybatica.Licenses.json");

                using var reader = new StreamReader(stream ?? throw new InvalidOperationException());
                _licensesJson = reader.ReadToEnd();
            }
            
            var array = JsonSerializer.Deserialize<License[]>(_licensesJson);
            var filter = Device.RuntimePlatform switch
            {
                Device.Android => array.Where(x => x.IsVisibleAndroid),
                Device.iOS => array.Where(x => x.IsVisibleiOS),
                _ => array
            };

            Licenses = new ObservableCollection<License>(filter.OrderBy(x => x.Name));
        }

        public ObservableCollection<License> Licenses { get; }
    }
}
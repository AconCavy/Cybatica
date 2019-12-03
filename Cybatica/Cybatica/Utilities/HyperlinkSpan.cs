using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Cybatica.Utilities
{
    public class HyperlinkSpan : Span
    {
        public static readonly BindableProperty UrlProperty =
            BindableProperty.Create(nameof(Url), typeof(string), typeof(HyperlinkSpan));

        public HyperlinkSpan()
        {
            TextDecorations = TextDecorations.Underline;
            TextColor = Color.Blue;
            GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => Browser.OpenAsync(new Uri(Url), BrowserLaunchMode.SystemPreferred))
            });
        }

        public string Url
        {
            get => (string) GetValue(UrlProperty);
            set => SetValue(UrlProperty, value);
        }
    }
}
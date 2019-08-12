﻿using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms.Xaml;

namespace Cybatica.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OcsPage : ReactiveContentPage<OcsViewModel>
    {
        public OcsPage()
        {
            ViewModel = new OcsViewModel(Navigation);

            InitializeComponent();

            this.WhenActivated(disposable =>
            {
                disposable(this.OneWayBind(ViewModel,
                    vm => vm.Ocs,
                    v => v.OCs.Text,
                    x => x.ToString("F2")));

                disposable(this.OneWayBind(ViewModel,
                    vm => vm.NnMean,
                    v => v.Nnmean.Text,
                    x => x.ToString("F2")));

                disposable(this.OneWayBind(ViewModel,
                    vm => vm.SdNn,
                    v => v.Sdnn.Text,
                    x => x.ToString("F2")));

                disposable(this.OneWayBind(ViewModel,
                    vm => vm.MeanEda,
                    v => v.MeanEda.Text,
                    x => x.ToString("F2")));

                disposable(this.OneWayBind(ViewModel,
                    vm => vm.PeakEda,
                    v => v.PeakEda.Text,
                    x => x.ToString("F2")));

                disposable(this.BindCommand(ViewModel,
                    vm => vm.ChartCommand,
                    v => v.Chart));
            });
        }
    }
}
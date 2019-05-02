﻿using Cybatica.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Splat;

namespace Cybatica.Views
{
    public partial class MainPage : ReactiveTabbedPage<MainViewModel>
    {
        public MainPage()
        {
            ViewModel = Locator.Current.GetService<IReactiveObject>(typeof(MainViewModel).FullName)
                as MainViewModel;

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Activator.Activate();

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.Activator.Deactivate();

        }
    }
}

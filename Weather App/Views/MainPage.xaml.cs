using System;
using Weather_App.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using SharedLibrary.Models;
using SharedLibrary.SharedLibraryVM;
using Windows.UI.Popups;
using Windows.Devices.Geolocation;
using Windows.System;

namespace Weather_App.Views
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
          
        }

    }
}

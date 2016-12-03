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
using Windows.ApplicationModel.Background;

namespace Weather_App.Views
{
    public sealed partial class MainPage : Page
    {

        private const string taskName = "WeatherBackgroundTask";
        private const string taskEntryPoint = "BackgroundTasks.WeatherBackgroundTask";

        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.RegisterBackgroundTask();
        }

        private async void RegisterBackgroundTask()
        {
            var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
            if (backgroundAccessStatus == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity ||
                backgroundAccessStatus == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
            {
                foreach (var task in BackgroundTaskRegistration.AllTasks)
                {
                    if (task.Value.Name == taskName)
                    {
                        task.Value.Unregister(true);
                    }
                }

                try
                {
                    BackgroundTaskBuilder taskBuilder = new BackgroundTaskBuilder();
                    taskBuilder.Name = taskName;
                    taskBuilder.TaskEntryPoint = taskEntryPoint;
                    taskBuilder.SetTrigger(new TimeTrigger(15, false));
                    var registration = taskBuilder.Register();
                }
                catch (System.Exception)
                {

                    return;
                }
            }
        }


        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //call GetPosition
                var position = await GeolocationVM.GetPosition();

                var lat = position.Coordinate.Latitude;
                var lon = position.Coordinate.Longitude;

                //var lat = 53.2707;
                //var lon = -9.0568;



                //Call ApiManager
                RootObject weather = await APIDataVM.GetWeather(lat, lon);

                //Schedule update
                //var uri = String.Format("http://weatherservice71390.azurewebsites.net/?lat={0}&lon={1}", lat, lon);

                //var tileContent = new Uri(uri);
                //var requestedInterval = PeriodicUpdateRecurrence.HalfHour;

                //var updater = TileUpdateManager.CreateTileUpdaterForApplication();
                //updater.StartPeriodicUpdate(tileContent, requestedInterval);

                //gets icons from openweathermap
                string icon = String.Format("http://openweathermap.org/img/w/{0}.png", weather.weather[0].icon);

                

                // string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", weather.weather[0].icon);


                //ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));



                double conv = .5556;
                int fah = (((int)weather.main.temp) - 32);
                int celcius = ((int)(fah * conv));

                TempText.Text = ((int)weather.main.temp).ToString() + (char)176;
                TempText1.Text = celcius.ToString() + (char)176 + "C";
                DescriptionText.Text = weather.weather[0].description;
                LocationText.Text = weather.name;
                WindText.Text = ((int)weather.wind.speed).ToString();


            }
            catch (Exception)
            {
                LocationText.Text = "Unable to access Weather";
            }


        }
    }
}

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
using Windows.UI.Xaml.Media.Imaging;

namespace Weather_App.Views
{
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.RegisterBackgroundTask();
        }

        private const string taskName = "WeatherBackgroundTask";
        private const string taskEntryPoint = "BackgroundTask.WeatherBackgroundTask";

        private async void RegisterBackgroundTask()
        {

            try
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
            catch (System.Exception)
            {

                return;
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                //progressRing.IsActive = true;
                //call GetPosition
                var position = await GeolocationVM.GetPosition();
                var lat = position.Coordinate.Latitude;
                var lon = position.Coordinate.Longitude;

                //Call ApiManager
                RootObject weather = await APIDataVM.GetWeather(lat, lon);
                //progressRing.IsActive = false;

                //gets icons from Assets folder - URI for acccess local resources ms-appx:///
                string icon = String.Format("ms-appx:///Icons/{0}.png", weather.weather[0].icon.Replace("d","").Replace("n",""));

                ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));
                /*
                double conv = .5556;
                int fah = (((int)weather.main.temp) - 32);
                int celcius = ((int)(fah * conv));
                */
                TempText.Text = (((int)weather.main.temp_min)- 32).ToString() + (char)176;
                
               // TempText1.Text = celcius.ToString() + (char)176 + "C";
                DescriptionText.Text = weather.weather[0].description;
                LocationText.Text = weather.name;
               // WindText.Text = ((int)weather.wind.speed).ToString();


            }
            catch (Exception)
            {
                LocationText.Text = "Unable to access Weather";
            }


        }
    }
}

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

        //public MainPageViewModel VM { get; set; }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            //Check to see if location is turned off
            LocationService service = new LocationService();

            //Check if location is off on device
           /* if (service.IsLocationServiceEnabled == true)
            {
                //Display message that location is off
                await new MessageDialog("This app needs to use your GPS to display information based on your current" +
                    " location. Turn on location services in settings.").ShowAsync();

            }*/


            try
            {
                //call GetPosition
                var position = await GeolocationVM.GetPosition();
                

                var lat = position.Coordinate.Latitude;
                var lon = position.Coordinate.Longitude;

                //Call ApiManager
                RootObject weather = await APIDataVM.GetWeather(lat, lon);

                //Schedule update
                //var uri = String.Format("http://weatherservice71390.azurewebsites.net/?lat={0}&lon={1}", lat, lon);

                //var tileContent = new Uri(uri);
                //var requestedInterval = PeriodicUpdateRecurrence.HalfHour;

                //var updater = TileUpdateManager.CreateTileUpdaterForApplication();
                //updater.StartPeriodicUpdate(tileContent, requestedInterval);

                //gets icons from openweathermap
                //string icon = String.Format("http://openweathermap.org/img/w/{0}.png", weather.weather[0].icon);

                //gets icons from Assets folder - URI for acccess local resources ms-appx:///
                //string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", weather.weather[0].icon);

                //ResultImage.Source = new BitmapImage(new Uri(icon, UriKind.Absolute));



                //double conv = .5556;
                //int fah = (((int)weather.main.temp) - 32);
                //int celcius = ((int)(fah * conv));

                ////TempText.Text = ((int)weather.main.temp).ToString() + (char)176;
                //TempText1.Text = celcius.ToString() + (char)176 + "C";
                //DescriptionText.Text = weather.weather[0].description;
                ////LocationText.Text = weather.name;
                //WindText.Text = ((int)weather.wind.speed).ToString();

            }
            catch (Exception)
            {
               // LocationText.Text = "Unable to access Weather";
            }

        }

    }
}

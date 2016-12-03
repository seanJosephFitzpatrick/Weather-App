using SharedLibrary.Models;
using SharedLibrary.SharedLibraryVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;

namespace BackgroundTask
{
    public sealed class WeatherBackgroundTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            // Get a deferral, to prevent the task from closing prematurely
            // while asynchronous code is still running.
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();

            //call GetPosition
            var position = await GeolocationVM.GetPosition();

            var lat = position.Coordinate.Latitude;
            var lon = position.Coordinate.Longitude;


            RootObject weather = await APIDataVM.GetWeather(lat, lon);

            string icon = String.Format("ms-appx:///Assets/Weather/{0}.png", weather.weather[0].icon);

            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueue(true);
            updater.Clear();

            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150PeekImage02);

            XmlNodeList tileTextAttributes = tileXml.GetElementsByTagName("text");
            tileTextAttributes[0].InnerText = weather.name;
            tileTextAttributes[1].InnerText = ("Tempreture : " + ((int)weather.main.temp).ToString() + "F");
            tileTextAttributes[2].InnerText = weather.weather[0].description;
            tileTextAttributes[3].InnerText = ("Speed : " + ((int)weather.wind.speed).ToString());

            XmlNodeList tileImageAttributes = tileXml.GetElementsByTagName("image");

            ((XmlElement)tileImageAttributes[0]).SetAttribute("src", icon);
            ((XmlElement)tileImageAttributes[0]).SetAttribute("alt", "red graphic");

            updater.Update(new TileNotification(tileXml));


            // Inform the system that the task is finished.
            deferral.Complete();
        }
    }
}

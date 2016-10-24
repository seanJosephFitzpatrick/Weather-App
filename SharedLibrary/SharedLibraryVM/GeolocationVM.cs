using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.System;
using Windows.UI.Popups;

namespace SharedLibrary.SharedLibraryVM
{
    public class GeolocationVM
    {
        public async static Task<Geoposition> GetPosition()
        {

            //request access to Geolocator
            var accessStatus = await Geolocator.RequestAccessAsync();

            //if access to location is denied throw exception
            if (accessStatus != GeolocationAccessStatus.Allowed) throw new Exception();

            var geolocator = new Geolocator { DesiredAccuracyInMeters = 0 };//DesiredAccuracyInMeters default value

            //get position
            var position = await geolocator.GetGeopositionAsync();

            //retrun position
            return position;
        }

    }
}

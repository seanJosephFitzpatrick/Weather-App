using Windows.Devices.Geolocation;

namespace SharedLibrary.SharedLibraryVM
{
    //Check to see if location setting is on/off
    public class LocationService
    {
        public bool IsLocationServiceEnabled
        {
            get
            {
                Geolocator locationservice = new Geolocator();
                if (locationservice.LocationStatus == PositionStatus.Disabled)
                {
                    
                    return false;
                }else {
                    
                    return true;
                }   
            }
        }
    }
}
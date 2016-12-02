using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Models
{
    public class Coord
    {
        public double lon { get; set; }
        public double lat { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }

    public class Wind
    {
        public double speed { get; set; }
        public double deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public double message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class RootObject
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }

    public class CurrentTime
    {


        private int _dt;
        public int dt
        {
            //Modified from https://www.youtube.com/watch?v=zm2tOtr8ReM tutorial
            get { return _dt; }
            set
            {
                _dt = value;

                /*
                epoch time - Taken from ( http://www.epochconverter.com/ )
                The Unix epoch (or Unix time or POSIX time or Unix timestamp) is the number of seconds that have elapsed since January 1, 1970 (midnight UTC/GMT), 
                not counting leap seconds (in ISO 8601: 1970-01-01T00:00:00Z). Literally speaking the epoch is Unix time 0 (midnight 1/1/1970), but 'epoch' is often
                used as a synonym for 'Unix time'. Many Unix systems store epoch dates as a signed 32-bit integer, which might cause problems on January 19, 2038 
                (known as the Year 2038 problem or Y2038).
                Human readable time 	Seconds
                1 hour	                3600 seconds
                1 day	                86400 seconds
                1 week	                604800 seconds
                1 month (30.44 days)    2629743 seconds
                1 year (365.24 days)    31556926 seconds
                 */

                var epochTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                Time = epochTime.AddSeconds(value);
            }
        }

        private DateTime _time;


        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }
    } 
}

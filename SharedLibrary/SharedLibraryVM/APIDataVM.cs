using Newtonsoft.Json;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.SharedLibraryVM
{
    public class APIDataVM
    {
        //Task object - when completed gives RootObject
        public async static Task<RootObject> GetWeather(double lat, double lon)
        {
            //Make call to web service
            var http = new HttpClient();

            //Return 10 days of json data
            //var url = String.Format("http://api.openweathermap.org/data/2.5/forecast/daily?lat={0}&lon={1}&cnt=10&appid=60543d045038ee177a09789c4171cee0&units=Metric", lat, lon);

            //Return current day of json data
            var url = String.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid=60543d045038ee177a09789c4171cee0&units=imperial", lat, lon);
            //response message
            var response = await http.GetAsync(url);

            //Grab result from response
            var result = await response.Content.ReadAsStringAsync();

            //serialize JSON
            var serializer = new DataContractJsonSerializer(typeof(RootObject));

            //memory stream for holding data
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(result));

            //get the data out of serializer
            var data = (RootObject)serializer.ReadObject(ms);//cast to RootObject

            //return the data
            return JsonConvert.DeserializeObject<RootObject>(result);
        }

        public async static Task<RootObjectDays> GetWeatherDays(double lat, double lon)
        {
            //Make call to web service
            var http = new HttpClient();

            //Return 10 days of json data
            var url = String.Format("http://api.openweathermap.org/data/2.5/forecast/daily?lat={0}&lon={1}&cnt=10&appid=60543d045038ee177a09789c4171cee0&units=imperial", lat, lon);

            //Return current day of json data
            //var url = String.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&appid=60543d045038ee177a09789c4171cee0&units=imperial", lat, lon);
            //response message
            var response = await http.GetAsync(url);

            //Grab result from response
            var resultDays = await response.Content.ReadAsStringAsync();

            //serialize JSON
            var serializer = new DataContractJsonSerializer(typeof(RootObjectDays));

            //memory stream for holding data
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(resultDays));

            //get the data out of serializer
            var data = (RootObjectDays)serializer.ReadObject(ms);//cast to RootObjectDays

            //return the data
            return JsonConvert.DeserializeObject<RootObjectDays>(resultDays);
        }
    }
}

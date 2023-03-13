using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IndianWeather
{
    public class ThirdPartyWeather
    {
        public void GetWeatherDetails(int lat, int lon)
        {
            string url = string.Format("https://api.open-meteo.com/v1/forecast?latitude={0}&longitude={1}&current_weather=true", lat, lon);

            var request = new HttpRequestMessage(HttpMethod.Get, url);



            using (var client = new HttpClient())
            {

                HttpResponseMessage response = client.Send(request);

                response.EnsureSuccessStatusCode();

                using (HttpContent content = response.Content)
                {

                    using (Stream responseBody = content.ReadAsStream())
                    { 

                    StreamReader reader = new StreamReader(responseBody, Encoding.UTF8);
                    Console.WriteLine(reader.ReadToEnd());
                }

                }

            }
        }
    }
}

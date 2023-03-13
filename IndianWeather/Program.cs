// See https://aka.ms/new-console-template for more information
using IndianWeather.DAL;
using Microsoft.Extensions.Configuration;


namespace IndianWeather
{
    public partial class Program
    {
        private static IConfiguration _iconfiguration;
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the city name: ");
            var city = Console.ReadLine();
            GetAppSettingsFile();
            PrintLocation(city);
        }

        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
 

            //var configuration = builder.Build();
            _iconfiguration = builder.Build();
        }

        static void PrintLocation(string city)
        {
            int lat, lon;
            var weatherDAL = new WeatherDAL(_iconfiguration);
            var result = weatherDAL.GetList(city);

            lat = result[0].Latitude;
            lon = result[0].Longitude;

            //result.ForEach(item =>
            //{
            //    Console.WriteLine("Latitude of the state is: {0}", item.Latitude);
            //    Console.WriteLine("Longitude of the state is: {0}", item.Longitude);
            //    lat= item.Latitude;
            //    lon= item.Longitude;
            //});
            
            ThirdPartyWeather tpw = new ThirdPartyWeather();
            tpw.GetWeatherDetails(lat, lon);
            Console.ReadKey();
        }
    }
}



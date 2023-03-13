using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IndianWeather.MODEL;

namespace IndianWeather.DAL
{
    public class WeatherDAL
    {
        private string _connectionString;
        public WeatherDAL(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
        }
        public List<WeatherMODEL> GetList(string city)
        {
            var listWeatherModel = new List<WeatherMODEL>();
            var Latitude = "";
            var Longitude = "";


            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("GetIndianLocation", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = new SqlParameter();
                    param.ParameterName = "@city";
                    param.Value = city;
                    cmd.Parameters.Add(param);
                    //con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    //Latitude = rdr[0].ToString();
                    //Longitude = rdr[1].ToString();
                    while (rdr.Read())
                    {
                        listWeatherModel.Add(new WeatherMODEL
                        {
                            // Latitude = Convert.ToInt32(rdr[0]),
                            Latitude = Convert.ToInt32(rdr[0]),
                            ///Longitude = rdr[1].ToString()
                            Longitude = Convert.ToInt32(rdr[1])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return listWeatherModel;
        }
    }
}

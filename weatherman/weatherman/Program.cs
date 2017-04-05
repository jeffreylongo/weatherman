using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace weatherman
{
    class Program
    {
        //this is user greeting and asking for name input.
        static void Greeting()
        {
            Console.WriteLine("Good Morrow...What do I call you?");
        }
        //this will be storing user name as a variable.
        static string NameInput()
        {
            var userName = Console.ReadLine();
            Console.WriteLine("Hello " + (userName) + "...enter a zipcode and I'll check the weather for you");
            return userName;
        }
        //this will get zip code
        static string GetZipCode()
        {
            var zipCode = Console.ReadLine();
            return zipCode;
        }

        static void Main(string[] args)
        {
            var stillUsing = true;
            while (stillUsing == true)
            {

                Greeting();
                var name = NameInput();
                var zipCode = GetZipCode();


                var url = $"http://api.openweathermap.org/data/2.5/weather?zip=" + (zipCode) + ",us&units=imperial&id=524901&APPID=314e971b1dcd934fb6afedbf9353557c";
                var request = WebRequest.Create(url);
                var response = request.GetResponse();
                var rawResponse = String.Empty;
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    rawResponse = reader.ReadToEnd();
                }
                var weatherDisplay = JsonConvert.DeserializeObject<RootObject>(rawResponse);

                Console.WriteLine($"The current TEMPERATURE is: " + (weatherDisplay.main.temp));
                Console.WriteLine($"The HIGH for today is: " + (weatherDisplay.main.temp_max));
                Console.WriteLine($"The LOW for today is: " + (weatherDisplay.main.temp_min));
                Console.WriteLine($"The current HUMIDITY is: " + (weatherDisplay.main.humidity));
                Console.WriteLine($"Looks like today is: " + (weatherDisplay.weather.First().description));

                const string connectionString =
                    @"Server=localhost\SQLEXPRESS;Database=Weather;Trusted_Connection=True;";

                using (var connection = new SqlConnection(connectionString))
                {
                    var text = @"INSERT INTO WeatherSearchTable (UserName, zipCode, Temp, Conditions)" +
                                "Values (@UserName, @ZipCode, @Temp, @Conditions)";

                    var cmd = new SqlCommand(text, connection);

                    cmd.Parameters.AddWithValue("@UserName", name);
                    cmd.Parameters.AddWithValue("@ZipCode", zipCode);
                    cmd.Parameters.AddWithValue("@Temp", weatherDisplay.main.temp);
                    cmd.Parameters.AddWithValue("@Conditions", weatherDisplay.weather.First().description);


                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();

                    Console.WriteLine("[C]ontinue OR [Q]uit");
                    var input = Console.ReadLine();
                    if (input.ToUpper() == "Q")
                    {
                        stillUsing = false;
                    }
                    else
                    {
                        stillUsing = true;
                    }
                }
                
            }
        }

    }
}

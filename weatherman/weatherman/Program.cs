using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        static void NameInput()
        {
            var userName = Console.ReadLine();
            Console.WriteLine("Hello " + (userName) + "...enter a zipcode and I'll check the weather for you");
        }

        static void Main(string[] args)
        {

            Greeting();
            NameInput();

            var zipCode = "";
            zipCode = Console.ReadLine();

            var url = $"http://api.openweathermap.org/data/2.5/weather?zip="+(zipCode)+ ",us&units=imperial&id=524901&APPID=314e971b1dcd934fb6afedbf9353557c";
            var request = WebRequest.Create(url);
            var response = request.GetResponse();
            var rawResponse = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                rawResponse = reader.ReadToEnd();
                //Console.WriteLine(rawResponse);
            }
            var weatherDisplay = JsonConvert.DeserializeObject<RootObject>(rawResponse);

            Console.WriteLine($"The current TEMPERATURE is: "+(weatherDisplay.main.temp));
            Console.WriteLine($"The HIGH for today is: "+(weatherDisplay.main.temp_max));
            Console.WriteLine($"The LOW for today is: " + (weatherDisplay.main.temp_min));
            Console.WriteLine($"The current HUMIDITY is: " + (weatherDisplay.main.humidity));
            Console.WriteLine($"Looks like today is: " + (weatherDisplay.weather.First().description));


            Console.ReadLine();
        }

    }
}

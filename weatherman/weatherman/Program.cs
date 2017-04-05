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

            var url = $"http://api.openweathermap.org/data/2.5/weather?zip="+(zipCode)+",us&id=524901&APPID=314e971b1dcd934fb6afedbf9353557c";
            var request = WebRequest.Create(url);
            var response = request.GetResponse();
            var rawResponse = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                rawResponse = reader.ReadToEnd();
                //Console.WriteLine(rawResponse);
            }
            var weather = JsonConvert.DeserializeObject<Weather>(rawResponse);

            Console.WriteLine(weather.temp);
            Console.WriteLine(weather.temp_max);
            Console.WriteLine(weather.temp_min);
            Console.WriteLine(weather.humidity);

            Console.ReadLine();
        }

    }
}

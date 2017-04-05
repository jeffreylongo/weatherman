using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherman
{
    class Program
    {
        static string userName;
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


            Console.ReadLine();
        }

    }
}

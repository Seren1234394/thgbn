using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using ConsoleApp333;

namespace погода
{
    class Program
    {
        static void Main(string[] args)
        {

            string key = "89d85e50002b33f7f5d98d12bd9f3c96";
            string city = "";

            Console.WriteLine("Напишите город, который вам интересен. \n");
            city = Console.ReadLine();
            Console.WriteLine();

            string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&units=metric&appid=" + key;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            {
                string result = streamReader.ReadToEnd();
                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(result);

                Console.WriteLine("Temperature in " + weatherResponse.Name + ": " + weatherResponse.Main.Temp + "°C \n");
               
            }
            Console.ReadLine();
        }
    }
}
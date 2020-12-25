﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        private static ITelegramBotClient botClient;
        private const string token = "7809fe29ca8896d2e3bd371196cd4b76";
        static void Main(string[] args)
        {
            
            botClient = new TelegramBotClient("1419427834:AAH45aX3gJECl3cUG4w917GCFk4dUKRhGFo") {Timeout = TimeSpan.FromSeconds(10)};

            var bot = botClient.GetMeAsync().Result;
            Console.WriteLine($"Бот в работе! \nBot:{bot.Id}");

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();

            Console.ReadKey();
            botClient.StopReceiving();
        }

        static string Show_Api(string town)
        {
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={town}&units=metric&appid={token}";

            var request = (HttpWebRequest)WebRequest.Create(url);

            var httpresponse = (HttpWebResponse)request.GetResponse();
            string response;
            using (var reader = new StreamReader(httpresponse.GetResponseStream()))
            {
                response = reader.ReadToEnd();
            }

            var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);

            var weatherText = $"В {weatherResponse.Name} {weatherResponse.Main.Temp}°C\n" +
                $"Ощущается {weatherResponse.Main.FeelsLike}°C\n" +
                $"Максималная {weatherResponse.Main.TempMax}°C и минимальная {weatherResponse.Main.TempMin}°C погода на сегодня\n";
            return weatherText;
        }

        private async static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            var text = e?.Message?.Text;
            if (text == null)
                return;

            try
            {
                var weatherText = Show_Api(text);
                await botClient.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: $"Погода в {text}: \n{weatherText}\n\n" +
                          $"Повторите город"
                    ).ConfigureAwait(false);
            }
            catch (Exception)
            {
                await botClient.SendTextMessageAsync(
            chatId: e.Message.Chat,
            text: "Город в котором узнать погоду"
            ).ConfigureAwait(false);
            }

        }
    }
}

using System;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using File = System.IO.File;

namespace TelegramBotExperiments
{

    class Program
    {
       
        static ITelegramBotClient bot = new TelegramBotClient("5776315946:AAG0SKOkbBV9ODaw_vefVJkbtyXv6zUPuHI");
       static List<Update> updates = new List<Update>();
        static public void SerializeUpdateJson(List<Update> update)
        {
            using (StreamWriter file = File.CreateText(@"test.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, update);
            }
        }
        //static public void SerializeUpdateJson(Update update)
        //{
        //    using (StreamWriter file = new StreamWriter(@"test.json", true))
        //    {
        //        JsonSerializer serializer = new JsonSerializer();
        //        serializer.Serialize(file, update);
        //    }
        //}
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            updates.Add(update);
            SerializeUpdateJson(updates);
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Добро пожаловать на борт, добрый путник!");
                    return;
                }
                await botClient.SendTextMessageAsync(message.Chat, "Привет-привет!!");
            }
        }
        
        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions,cancellationToken);
            Console.ReadLine();
        }
    }
}
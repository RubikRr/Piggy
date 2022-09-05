using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Piggy;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types.Enums;
using File = System.IO.File;

namespace TelegramBotExperiments
{

    class Program
    {
        static TelegramBot bot = new TelegramBot();
        static List<Update> updates = new List<Update>();

        //static public void SerializeUpdateJson(List<Update> update)
        //{
        //    using (StreamWriter file = File.CreateText(@"test.json"))
        //    {
        //        JsonSerializer serializer = new JsonSerializer();
        //        serializer.Serialize(file, update);
        //    }
        //}

        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            bot.PrintUpdate(update);
            //updates.Add(update);
            //SerializeUpdateJson(updates);

            if (update.Type == UpdateType.Message)
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
            Console.WriteLine(JsonConvert.SerializeObject(exception));
        }

        static void Main(string[] args)
        {
            bot.Start(HandleUpdateAsync, HandleErrorAsync);

            Console.ReadLine();
        }

    }
}
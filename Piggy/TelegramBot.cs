using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace Piggy
{
    public class TelegramBot
    {
        private readonly string Token= "5776315946:AAHuoQxNrX567yKgGG-YNjmwrdNaAjQqO2I";

        public TelegramBotClient Client { get; private set; }

        public TelegramBot()
        {
            Client = new TelegramBotClient(Token);


        }


        public void Start(Func<ITelegramBotClient,Update,CancellationToken,Task> HandleUpdateAsync, Func<ITelegramBotClient,Exception,CancellationToken,Task> HandleErrorAsync)
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            Client.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken);

        }

        public void PrintUpdate(Update update)
        {
            Console.WriteLine(JsonConvert.SerializeObject(update));
        }

    }
}

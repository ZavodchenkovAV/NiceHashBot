using NiceHashMon.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace NiceHashMon
{
    public class TelegramNotifyBot
    {
        private readonly ITelegramBotClient _bot;
        private List<long> userIdList = new List<long>() { 354104768, 389290036 };
        public TelegramNotifyBot()
        {
            _bot = new TelegramBotClient("525139244:AAEo6Cl3KuIZRdId0I4fqsqc6knsb2meIAg");
            _bot.OnMessage -= Bot_OnMessage;
            _bot.OnMessage += Bot_OnMessage;
            _bot.StartReceiving();
        }
        private async void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            Message msg = e.Message;

            if (msg == null) return;

            if (msg.Type == MessageType.TextMessage)
            {
                
            }
        }
        public async void Notify(CoinProfit coinProfit)
        {
            foreach(var userId in userIdList)
            {
                await _bot.SendTextMessageAsync(userId, $"Монета {coinProfit.CoinName}, Isprofit={coinProfit.IsProfit}");
            }
        }
    }
}

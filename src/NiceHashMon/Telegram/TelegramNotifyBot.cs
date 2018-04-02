using NiceHashMon.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace NiceHashMon.Telegram
{
    public class TelegramNotifyBot
    {
        private readonly ITelegramBotClient _bot;
        private Dictionary<long, bool> userIdList = new Dictionary<long, bool>();
        private readonly bool _botNotify;
        public TelegramNotifyBot()
        {
            _bot = new TelegramBotClient("525139244:AAEo6Cl3KuIZRdId0I4fqsqc6knsb2meIAg");
            _bot.OnMessage -= Bot_OnMessage;
            _bot.OnMessage += Bot_OnMessage;
            _bot.StartReceiving();
            userIdList.Add(354104768, true);
            userIdList.Add(389290036, true);
            _botNotify = Convert.ToBoolean(ConfigurationManager.AppSettings["BotNotify"]);
        }
        private async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            Message msg = e.Message;

            if (msg == null) return;

            if (msg.Type == MessageType.TextMessage)
            {
                var userid = msg.Chat.Id;
                if (!userIdList.ContainsKey(userid))
                    userIdList.Add(userid, true);
                else if (msg.Text == @"/off")
                    userIdList[userid] = false ;
            }
        }
        public async void Notify(CoinProfit coinProfit)
        {
            foreach(var userId in userIdList.Where(u=>u.Value))
            {
                string message;
                if (coinProfit.IsProfit==true)
                    message = $"Соломон, пора открывать ордер. Рубим бабло. Монета {coinProfit.CoinName}";
                else
                    message = $"Виталик, пора закрывать ордер. Уходим в минуса. Монета {coinProfit.CoinName}";

                if (_botNotify)
                    await _bot.SendTextMessageAsync(userId.Key, message);
            }
        }
    }
}

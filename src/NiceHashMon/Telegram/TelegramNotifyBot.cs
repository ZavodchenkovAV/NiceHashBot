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
            _bot = new TelegramBotClient(ConfigurationManager.AppSettings["BotToken"]);
            _bot.OnMessage -= Bot_OnMessage;
            _bot.OnMessage += Bot_OnMessage;
            _bot.StartReceiving();
            var notifyUsersText = ConfigurationManager.AppSettings["BotNotifyUsers"];
            var notifyUsersList = notifyUsersText.Split(',');
            foreach (var notifyUser in notifyUsersList)
            {
                long notifyUserId;
                if(long.TryParse(notifyUser,out notifyUserId))
                    userIdList.Add(notifyUserId, true);
            }
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
                    message = $"Соломон, пора открывать ордер. Рубим бабло. Монета {coinProfit.CoinName}, ProfitCountPercent={coinProfit.ProfitCountPercent:P2}, ProfitCountC1Percent={coinProfit.ProfitCountC1Percent:P2}";
                else
                    message = $"Виталик, пора закрывать ордер. Уходим в минуса. Монета {coinProfit.CoinName}, ProfitCountPercent={coinProfit.ProfitCountPercent:P2}, ProfitCountC1Percent={coinProfit.ProfitCountC1Percent:P2}";

                if (_botNotify)
                    await _bot.SendTextMessageAsync(userId.Key, message);
            }
        }
    }
}

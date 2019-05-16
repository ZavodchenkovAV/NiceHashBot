using NiceHashBotLib;
using NiceHashMon.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
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
            var token = ConfigurationManager.AppSettings["BotToken"];
            var host = ConfigurationManager.AppSettings["BotProxyHost"];
            var portText = ConfigurationManager.AppSettings["BotProxyPort"];
            if (!string.IsNullOrEmpty(host) && !string.IsNullOrEmpty(portText))
            {
                int port;
                if (int.TryParse(portText, out port))
                {
                    WebProxy proxyObject = new WebProxy(host, port);
                    _bot = new TelegramBotClient(token,proxyObject);
                }
            }
            else
                _bot = new TelegramBotClient(token);
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
        private void Bot_OnMessage(object sender, MessageEventArgs e)
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
        public async void Notify(CoinProfit coinProfit, OrderN order)
        {
            foreach(var userId in userIdList.Where(u=>u.Value))
            {
                string message=null;
                if (coinProfit.IsProfit == true)
                {
                    if(order!=null && coinProfit.CoinName== "Deeponion")
                    {
                        var limit = APIWrapper.OrderSetLimit(order.location, order.algo, order.id, 100);
                        message = $"Мустафа, увеличили лимит. Рубим бабло. Монета {coinProfit.CoinName}, ProfitCountPercent={coinProfit.ProfitCountPercent:P2}, ProfitCountC1Percent={coinProfit.ProfitCountC1Percent:P2}";
                    }
                    else
                        message = $"Соломон, пора открывать ордер. Рубим бабло. Монета {coinProfit.CoinName}, ProfitCountPercent={coinProfit.ProfitCountPercent:P2}, ProfitCountC1Percent={coinProfit.ProfitCountC1Percent:P2}";

                }
                else if (coinProfit.OrderId > 0 && order != null)
                {
                    var limit = APIWrapper.OrderSetLimit(order.location, order.algo, order.id, 0.01);
                    message = $"Виталик, уменьшили лимит. Монета {coinProfit.CoinName}, ProfitCountPercent={coinProfit.ProfitCountPercent:P2}, ProfitCountC1Percent={coinProfit.ProfitCountC1Percent:P2}";
                }
                else
                {
                    message = $"Ахмед, ордера нет и профита тоже. Имей в виду. Монета {coinProfit.CoinName}, ProfitCountPercent={coinProfit.ProfitCountPercent:P2}, ProfitCountC1Percent={coinProfit.ProfitCountC1Percent:P2}";
                }

                if (_botNotify &&!string.IsNullOrEmpty(message))
                    await _bot.SendTextMessageAsync(userId.Key, message);
            }
        }
    }
}

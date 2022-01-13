using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace botMessage
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ParsePage.Scaner();

            string token = File.ReadAllText(@"C:\Users\Vanya\Desktop\ЮУрГУ\3 семестр\С#\my\lab5\laba5\botMessage\token.txt");
            string groupId = "209052401";
            WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };

            var r = wc.DownloadString($"https://api.vk.com/method/groups.getLongPollServer?group_id={groupId}&access_token={token}&v=5.131");


            var mask = JObject.Parse(r)["response"].ToString();

            dynamic stuff = JsonConvert.DeserializeObject(mask);

            string key = stuff.key;
            string server = stuff.server;
            string ts = stuff.ts;
            Console.WriteLine($"{key,10}\n{server,10}\n{ts}");
            //var dialogs = wc.DownloadString($"https://api.vk.com/method/messages.getDialogs?v=5.41&access_token={token}&count=10&offset=3&v=5.82");
            //Console.WriteLine(dialogs); 

            string keyboardOptions = File.ReadAllText($@"C:\Users\Vanya\Desktop\ЮУрГУ\3 семестр\С#\my\lab5\laba5\botMessage\keyboard.json");
            string keyboardOptions1 = File.ReadAllText($@"C:\Users\Vanya\Desktop\ЮУрГУ\3 семестр\С#\my\lab5\laba5\botMessage\keyboard1.json");
            string keyboardOptions2 = File.ReadAllText($@"C:\Users\Vanya\Desktop\ЮУрГУ\3 семестр\С#\my\lab5\laba5\botMessage\keyboard2.json");
            string eventData = File.ReadAllText($@"C:\Users\Vanya\Desktop\ЮУрГУ\3 семестр\С#\my\lab5\laba5\botMessage\eventData.json");
            

            while (true)
            {
                

                //Новый ts
                var json = wc.DownloadString($"{server}?act=a_check&key={key}&ts={ts}&wait=25");
                dynamic mainJson = JsonConvert.DeserializeObject(json);
                ts = mainJson.ts;
               Console.WriteLine(json);

                //Cообщение от пользователя
                JObject obj = JObject.Parse(json);
                string type = obj["updates"].Select(jt => (string)jt["type"])
                    .FirstOrDefault();
                Console.WriteLine(type);

                switch (type)
                {
                    case "message_new":
                        {
                            Random rnd = new Random();
                            int randomId = rnd.Next(0, 999999);

                            string userMessage = obj["updates"]
                                .Select(jt => (string)jt["object"]["message"]["text"])
                                .FirstOrDefault();
                            string userId = obj["updates"]
                                .Select(jt => (string)jt["object"]["message"]["from_id"])
                                .FirstOrDefault();
                            string peerId = obj["updates"]
                                .Select(jt => (string)jt["object"]["message"]["peer_id"])
                                .FirstOrDefault();

                            var userInfo = wc.DownloadString($"https://api.vk.com/method/users.get?access_token={token}&user_id={userId}&fields=bdate,city,domain,county,timezoneonline,domain,has_mobile,contacts,site,education,universities,schools&v=5.131");
                            //Console.WriteLine(userInfo);
                            JObject userJson = JObject.Parse(userInfo);

                            string userName = userJson["response"]
                                .Where(jt => (string)jt["id"] == userId)
                                .Select(jt => (string)jt["first_name"])
                                .FirstOrDefault();
                            string userbDate = userJson["response"]
                                .Where(jt => (string)jt["id"] == userId)
                                .Select(jt => (string)jt["bdate"])
                                .FirstOrDefault();
                            string userDomain = userJson["response"]
                                .Where(jt => (string)jt["id"] == userId)
                                .Select(jt => (string)jt["domain"])
                                .FirstOrDefault();
                            //информация об отправителе

                            Console.WriteLine($"{userName},{userDomain},{userId},{userbDate},{peerId}");

                            if (userMessage != null)
                            {
                                Regex regexHi = new(@"привет.?\S*.?\S*");
                                Regex regexJoke = new Regex(@"\S*?.?расскажи шутку");
                                Regex regexJoke1 = new Regex(@"\S*?.?расскажи еще одну шуточку");
                                Regex regexSkill = new Regex(@"\S*?.?что ты умеешь?");
                                string lowerUserMessage = userMessage.ToLower();
                                if (regexHi.IsMatch(lowerUserMessage))
                                {
                                    string text = $"Привет, {userName}, что нужно?)";
                                    wc.DownloadString($"https://api.vk.com/method/messages.send?access_token={token}&peer_id={peerId}&random_id={randomId}&message={text}&keyboard={keyboardOptions}&v=5.82");

                                }
                                
                                else if (regexJoke.IsMatch(lowerUserMessage))
                                {
                                   
                                    string text3 = $"\tПользователь VK сделал бота, который показывает, кто из друзей заходил на Pornhub. Жду когда можно будет отсортировать этих друзей по жанрам";

                                    var answer = wc.DownloadString($"https://api.vk.com/method/messages.send?access_token={token}&peer_id={peerId}&keyboard={keyboardOptions1}&message={text3}&v=5.82");
                                }
                                else if (regexJoke1.IsMatch(lowerUserMessage))
                                {
                                    string text2 = $"- Слушай, я наверно делаю слишком много перепостов, и меня друзья в сетях начали принимать за бота. - Это ещё ничего, меня последнее время боты стали принимать за своего.\n\nНу хватит шуток я больше не знаю!!((";
                                    var answer = wc.DownloadString($"https://api.vk.com/method/messages.send?access_token={token}&peer_id={peerId}&keyboard={keyboardOptions2}&message={text2}&v=5.82");
                                    Console.WriteLine(answer);
                                }
                                else if (regexSkill.IsMatch(lowerUserMessage))
                                {
                                    string text = $"Я еще малыш ботик и почти ничего не умею) Но я быстро учусь и скоро стану крутым!!!";
                                    var answer = wc.DownloadString($"https://api.vk.com/method/messages.send?access_token={token}&peer_id={peerId}&message={text}&v=5.82");
                                }

                            switch (lowerUserMessage)
                                {
                                    case "кинь фото":
                                        {
                                            string text = "такая пойдет?";
                                            string photoId = "photo-209052401_457239018";
                                            var answer = wc.DownloadString($"https://api.vk.com/method/messages.send?access_token={token}&peer_id={peerId}&message={text}&attachment={photoId}&v=5.82");
                                            break;
                                        }
                                    case "плохой бот":
                                        {
                                            string text = $"ММгм я звоню в полицию!";
                                            var answer = wc.DownloadString($"https://api.vk.com/method/messages.send?access_token={token}&peer_id={peerId}&message={text}&v=5.82");
                                            break;
                                        }
                                }
                            }

                            break;
                        }
                    case "message_event":
                        {
                            //Console.WriteLine(answer);
                            string eventId = obj["updates"]
                        .Select(jt => (string)jt["object"]["event_id"])
                        .FirstOrDefault();
                            string peerId = obj["updates"]
                        .Select(jt => (string)jt["object"]["peer_id"])
                        .FirstOrDefault();
                            string userId = obj["updates"]
                        .Select(jt => (string)jt["object"]["user_id"])
                        .FirstOrDefault();

                            if (eventId != null)
                            {
                                var answer = wc.DownloadString($"https://api.vk.com/method/messages.sendMessageEventAnswer?event_id={eventId}&user_id={userId}&peer_id={peerId}&event_data={eventData}&access_token={token}&v=5.82");
                                Console.WriteLine(answer);
                            }
                            break;
                        }
                }

                //    var answer = wc.DownloadString($"https://api.vk.com/method/messages.send?access_token={token}&peer_id={peerId}&message={text}&v=5.82");

            }
        }
    }
}

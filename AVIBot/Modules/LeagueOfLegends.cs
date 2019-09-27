using System;
using System.Threading.Tasks;
using Discord.Commands;
using System.Net;
using Discord;
using Newtonsoft.Json;

namespace AVIBot.Modules
{
    public class LeagueOfLegends : ModuleBase<SocketCommandContext>
    {
        private static string apiKey = "RGAPI-322387d0-a783-4a2b-b193-fd2f4f452dc8";

        private dynamic CallAPI(string url, string apiKey)
        {
            url = url + "?api_key=" + apiKey;
            string json;
            using (WebClient client = new WebClient())
            {
                json = client.DownloadString(url);
            }

            var dataObject = JsonConvert.DeserializeObject<dynamic>(json);
            return dataObject;
        }

        [Command("lolstatus")]
        public async Task CheckLoLStatus()
        {
            var dataObject = CallAPI("https://euw1.api.riotgames.com/lol/status/v3/shard-data", apiKey);

            string response = "";

            foreach (var i in dataObject["services"])
            {
                string status = i["status"];
                string name = i["name"];
                response += name + ": " + status + "\n";
            }

            var embed = new EmbedBuilder();
            embed.WithTitle("EUW Status");
            embed.WithDescription(response);
            var color = new Discord.Color();
            if (dataObject["services"][0]["status"] == "online")
            { 
                color = new Discord.Color(0, 255, 0);
            }
            else
            {
                color = new Discord.Color(255, 0, 0);
            }
            embed.WithColor(color);
            embed.WithCurrentTimestamp();
            embed.WithThumbnailUrl("https://icon-library.net/images/league-of-legends-icon-png/league-of-legends-icon-png-17.jpg");
            var builtEmbed = embed.Build();
            
            await Context.Channel.SendMessageAsync(null, false, builtEmbed);
        }
    }
}
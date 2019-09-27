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
        [Command("lolstatus")]
        public async Task CheckLoLStatus()
        {
            string json;
            using (WebClient client = new WebClient())
            {
                json = client.DownloadString(
                    "https://euw1.api.riotgames.com/lol/status/v3/shard-data?api_key=RGAPI-322387d0-a783-4a2b-b193-fd2f4f452dc8");
            }

            var dataObject = JsonConvert.DeserializeObject<dynamic>(json);

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
            embed.WithColor(0, 255, 0);
            var builtEmbed = embed.Build();

            /*
            var shardStatus = dataObject["services"][0]["status"]; // Online, offline etc
            var shardName = dataObject["services"][0]["name"]; //Name
            */


            await Context.Channel.SendMessageAsync(null, false, builtEmbed);
        }
    }
}
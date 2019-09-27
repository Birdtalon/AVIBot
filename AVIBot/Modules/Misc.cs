using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace AVIBot.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        public async Task Echo([Remainder]string message)
        {
            var embed = new EmbedBuilder();
            embed.WithTitle("Message");
            embed.WithDescription(message);
            embed.WithColor(new Color(0, 255, 0));
            var built = embed.Build();

            await Context.Channel.SendMessageAsync("",false, built);
        }

        [Command("hello")]
        public async Task Hello()
        {
            await Context.Channel.SendMessageAsync("Hello " + Context.Message.Author.Username +
                                                   " I am " + Context.Client.CurrentUser.Username + ". Nice to meet you!");
        }
    }
}

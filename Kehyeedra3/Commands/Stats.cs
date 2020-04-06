using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kehyeedra3.Commands
{
    [Group]
    public class Stats : ModuleBase ///////////////////////////////////////////////
    {
        [Command("ping"), Summary("Shows ping to server")]
        public async Task Pong()
        {
            await Context.Channel.TriggerTypingAsync();
            await ReplyAsync($"My current ping is {Bot._bot.Latency}ms");
        }
    }
}

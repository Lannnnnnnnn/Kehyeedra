using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kehyeedra3.Commands
{
    [Group]
    public class HelpModule : ModuleBase ///////////////////////////////////////////////
    {
        private CommandService _service;

        public HelpModule(CommandService service) //Create a constructor for the commandservice dependency
        {
            _service = service;
        }
        /*[Command("commands"), Alias("coomands")]
        public async Task HelpAsync()
        {
            string debug = null;
            string prefix = Configuration.Load().Prefix;
            var builder = new EmbedBuilder()
            {
                Color = new Color(114, 137, 218),
                Description = "These are the commands you can use"
            };

            foreach (var module in _service.Modules)
            {
                string description = null;
                debug += $"{module.Name}\n";
                foreach (var cmd in module.Commands)
                {
                    var result = await cmd.CheckPreconditionsAsync(Context);
                    if (result.IsSuccess)
                        description += $"{prefix}{cmd.Aliases.First()}\n";
                    debug += $"{prefix}{cmd.Aliases.First()}\n";
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    builder.AddField(x =>
                    {
                        x.Name = module.Name;
                        x.Value = description;
                        x.IsInline = false;
                    });
                }
            }

            await ReplyAsync(debug);
        }

        [Command("command"), Alias("coomand")]
        public async Task HelpAsync(string command)
        {
            var result = _service.Search(Context, command);

            if (!result.IsSuccess)
            {
                await ReplyAsync($"Sorry, I couldn't find a command with the name **{command}**.");
                return;
            }

            string prefix = Configuration.Load().Prefix;
            var builder = new EmbedBuilder()
            {
                Color = new Color(0, 255, 0),
                Description = $"Here are some commands like **{command}**"
            };

            foreach (var match in result.Commands)
            {
                var cmd = match.Command;

                builder.AddField(x =>
                {
                    x.Name = string.Join(", ", cmd.Aliases);
                    x.Value = $"Parameters: {string.Join(", ", cmd.Parameters.Select(p => p.Name))}\n" +
                    $"Remarks: {cmd.Remarks}";
                    x.IsInline = false;
                });
            }

            await ReplyAsync("", false, builder.Build());
        }*/

        [Command("help"), Summary("Shows this thing")]
        public async Task HelpCommand([Remainder] string command = null)
        {
            if(command == null)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.AddField("AIMLbot", "Mention me to talk with me (don't expect intelligence)");
                foreach (var module in Bot._cmds.Modules)
                {
                    StringBuilder coommands = new StringBuilder("");
                    foreach(var cmd in module.Commands)
                    {
                        var result = await cmd.CheckPreconditionsAsync(Context, Bot._dmap).ConfigureAwait(false);
                        if (result.IsSuccess)
                        {
                            coommands.Append(cmd.Name);

                            if (cmd != module.Commands.LastOrDefault())
                                coommands.Append(", ");
                        }
                    }
                    embed.AddField(module.Name, coommands.ToString());
                }
                await ReplyAsync("Here's a list of commands search for the command to find what it be and what it do", false, embed.Build());
            }
            else
            {
                var res = Bot._cmds.Search(command);
                if (res.IsSuccess)
                {
                    EmbedBuilder embed = new EmbedBuilder();

                    var coomand = res.Commands.FirstOrDefault();

                    embed.AddField(coomand.Command.Name ?? "N/A", string.IsNullOrEmpty(coomand.Command.Summary) ? "No Summary Found" : coomand.Command.Summary);

                    await ReplyAsync($"Here's a command like **{command}**", false, embed.Build());
                }
                else
                {
                    await ReplyAsync("Check your input retard");
                }
            }
        }
    }
}

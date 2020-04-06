using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord;
using System.IO;
using Kehyeedra3.Preconditions;
using System.Net;
using System.Linq;
using System.Data;
using Kehyeedra3.Services;
using MySql.Data;
using MySql.Data.MySqlClient;
using Kehyeedra3.Services.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using Discord.Addons.Interactive;
using System.Threading.Channels;

namespace Kehyeedra3
{
     //public class Audio_module : ModuleBase<ICommandContext> ////////////////////////
    //{
    //    [Command("join", RunMode = RunMode.Async)]
    //    public async Task JoinCmd()
    //    {
    //        await Bot.AudioService.JoinAudio(Context.Guild, (Context.User as IVoiceState).VoiceChannel);
    //    }
    //    [Command("leave", RunMode = RunMode.Async)]
    //    public async Task LeaveCmd()
    //    {
    //        await Bot.AudioService.LeaveAudio(Context.Guild);
    //    }
    //    [Command("play", RunMode = RunMode.Async)]
    //    public async Task PlayCmd([Remainder] string song)
    //    {
    //        await Bot.AudioService.SendAudioAsync(Context.Guild, Context.Channel, song);
    //    }
    //}
    
    //public class Event : ModuleBase<ICommandContext> /////////////////////////
    //{
    //    [Command("coof"), Ratelimit(1, 1, Measure.Minutes)]
    //    public async Task Coof([Remainder] IGuildUser name)
    //    {
    //        var user = await Context.Guild.GetUserAsync(Context.User.Id).ConfigureAwait(false); ;
    //        if (user.RoleIds.Any(id => id == 672517021732438026))
    //        {
    //            var role = Context.Guild.GetRole(672517021732438026);
    //            var hearole = Context.Guild.GetRole(672759930666876991);
    //            if (name.RoleIds.Any(id => id == 672755435454988294))
    //            {
    //                await ReplyAsync($"{name.Username}'s hazmat suit is protecting them from the corona");
    //            }
    //            else
    //            {
    //                await name.AddRoleAsync(role).ConfigureAwait(false);
    //                await name.RemoveRoleAsync(hearole).ConfigureAwait(false);

    //                Console.WriteLine($"{Context.User.Username} has infected {name.Username}");

    //                await ReplyAsync($"{Context.User.Username} has infected {name.Username}");
    //                await ReplyAsync($"Corona has been cured for now haha yeah");
    //            }
    //        }
    //    }
    //    [Command("cure"), Ratelimit(3, 30, Measure.Minutes)]
    //    public async Task Cure([Remainder] IGuildUser name)
    //    {
    //        var user = await Context.Guild.GetUserAsync(Context.User.Id).ConfigureAwait(false); ;
    //        if ((user.RoleIds.Any(id => id == 672755435454988294)) || (user.RoleIds.Any(id => id == 672759930666876991)))
    //        {
    //            var role = Context.Guild.GetRole(672759930666876991);
    //            var infrole = Context.Guild.GetRole(672517021732438026);
    //            if (name.RoleIds.Any(id => id == 672785044699611139))
    //            {
    //                await ReplyAsync($"{name.Username} absolutely refuses to be vaccinated");
    //            }
    //            else
    //            {
    //                if (name.RoleIds.Any(id => id == 672517021732438026))
    //                {
    //                    await name.AddRoleAsync(role).ConfigureAwait(false);
    //                    await name.RemoveRoleAsync(infrole).ConfigureAwait(false);

    //                    Console.WriteLine($"{Context.User.Username} has cured {name.Username}");

    //                    await ReplyAsync($"{Context.User.Username} has vaccinated {name.Username}");
    //                }
    //                else
    //                {
    //                    await ReplyAsync($"{name.Username} is not infected??? are you super retarded???");
    //                }
    //            }
    //        }
    //    }
    //}

    //public class _ : ModuleBase
    //{
    //    [RequireRolePrecondition(AccessLevel.BotOwner)]
    //    [Command("embedtest")]
    //    public async Task Embedthing(bool inline, [Remainder]string text)
    //    {
    //        var BotUser = await Bot._bot.GetApplicationInfoAsync();
    //        EmbedBuilder embed = new EmbedBuilder();
    //        EmbedAuthorBuilder author = new EmbedAuthorBuilder();
    //        EmbedFooterBuilder footer = new EmbedFooterBuilder();
    //        //author stuff
    //        author.Name = BotUser.Name;
    //        author.IconUrl = BotUser.IconUrl;
    //        embed.Author = author;
    //        //footer stuff
    //        footer.Text = "Given at";
    //        embed.Footer = footer;
    //        //embed stuff
    //        embed.Timestamp = DateTime.Now;
    //        embed.AddInlineField("test 1", "test 1");
    //        embed.AddInlineField("test 2", "test 2");
    //        embed.AddField(x =>
    //        {
    //            x.IsInline = inline;
    //            x.Name = "Test Embed";
    //            x.Value = text;
    //        });
    //        await ReplyAsync("This is an embed test, I think I did it...", false, embed);
    //    }
    //}
}

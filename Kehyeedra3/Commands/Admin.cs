using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Kehyeedra3.Preconditions;
using Kehyeedra3.Services.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Kehyeedra3.Commands
{
    public class Admin : InteractiveBase ///////////////////////////////////////////////
    {
        [RequireRolePrecondition(AccessLevel.ServerAdmin)]
        [Command("adddelet"), Summary("Adds a delet this image to the bot from link or image (admin only)")]
        public async Task AddDelet() //Listens for attachments
        {
            var attachments = Context.Message.Attachments;//Gets attachments as var
            foreach (var item in attachments)
            {
                Uri link = new Uri(item.Url);
                using (WebClient _webclient = new WebClient())
                {
                    string location = Path.Combine(Environment.CurrentDirectory, "delet");
                    if (!Directory.Exists(location))
                        Directory.CreateDirectory(location);
                    location += "/" + item.Filename;
                    _webclient.DownloadFileAsync(link, location);
                }
                await ReplyAsync($"Delet added");
                break;
            }
        }
        [RequireRolePrecondition(AccessLevel.ServerAdmin)]
        [Command("adddelet"), Summary("Adds a delet this image to the bot from link or image (admin only)")]
        public async Task AddDelet(string url) //Listens for urls
        {
            Uri link = new Uri(url);
            using (WebClient _webclient = new WebClient())
            {
                string location = Path.Combine(Environment.CurrentDirectory, $"delet");
                if (!Directory.Exists(location))
                    Directory.CreateDirectory(location);
                location += "/" + Guid.NewGuid() + ".jpg";
                _webclient.DownloadFileAsync(link, location);
            }
            await ReplyAsync($"Delet added");
        }
        [RequireRolePrecondition(AccessLevel.ServerAdmin)]
        [Command("say"), Summary("Sends given message to given channel (admin only)")]
        public async Task Say(ITextChannel channel, [Remainder]string message)
        {
            await channel.SendMessageAsync(message);
        }
        [RequireRolePrecondition(AccessLevel.BotOwner)]
        [Command("modifybot"), Summary("I'm goonaa mooodifoo")]
        public async Task ModifyBot(string _name)
        {
            //reference current bot user
            var BotCurrUser = Bot._bot.CurrentUser;
            await BotCurrUser.ModifyAsync(x =>
            {
                //sets name
                x.Username = _name;
            });
            //reply
            await ReplyAsync($"Set name to {_name}");
        }
        [RequireRolePrecondition(AccessLevel.BotOwner)]
        [Command("vbi", RunMode = RunMode.Async)]
        public async Task VerifyBankIntegrity()
        {
            using var Database = new ApplicationDbContextFactory().CreateDbContext();

            List<User> users = Database.Users.OrderByDescending(user => user.Money).ToList();
            User bank = Database.Users.FirstOrDefault(x => x.Id == 0);

            int existing = 0;
            users.ForEach(x => existing += Convert.ToInt32(x.Money));
            int difference = 1000000 - existing;
            string content = "";
            if (difference >= 0)
            {
                content += $"Current stability is: {existing / 10000d}%\n";
            }
            else
            {
                content += $"Current stability is: {(1000000d / existing) * 100}%\n";
            }
            if (difference != 0)
            {
                content += "Do you want to stabilize existing economy?";
                await Context.Channel.SendMessageAsync($"{content}");
                var message = await NextMessageAsync();
                if (message != null && message.Content.ToLowerInvariant() == "yes")
                {
                    bank.Money += difference;
                    await Database.SaveChangesAsync();
                    if (difference > 0)
                    {
                        await Context.Channel.SendMessageAsync($"Economy has been stabilized by adding {difference / 10000d}% to bank");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"Economy has been stabilized by removing {0-difference / 10000d}% from bank");
                    }
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync($"{content}");
            }
        }
        /*
        [RequireRolePrecondition(AccessLevel.BotOwner)]
        [Command("getstamp")]
        public async Task YeedraStamp()
        {
            ulong stamp = DateTime.UtcNow.ToYeedraStamp();
            await ReplyAsync($"{Context.User.Mention} {stamp}");
        }
        [RequireRolePrecondition(AccessLevel.BotOwner)]
        [Command("savefile")]

        public async Task SaveFile(string fday, string fscore)
        {
            string location = Path.Combine(Environment.CurrentDirectory, "drawtasks");
            string tlocation = ($"{location}/days.txt");
            var attachments = Context.Message.Attachments;
            string fname = $"{fday}-{fscore}";
            if (File.Exists(location + "/days.txt"))
                {
                
                }
            foreach (var item in attachments)
            {
                Uri link = new Uri(item.Url);
                using (WebClient _webclient = new WebClient())
                {
                    if (!Directory.Exists(location))
                        Directory.CreateDirectory(location);
                    location += ($"/{fday}-{fscore}.jpg");
                    _webclient.DownloadFileAsync(link, location);
                }
                await ReplyAsync($"Post archived");
                break;
            }
        }*/
    }
}

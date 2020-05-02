using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Kehyeedra3.Preconditions;
using Kehyeedra3.Services.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
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
        [RequireRolePrecondition(AccessLevel.BotOwner)]
        [Command("modifymoney"), Alias("mm")]
        public async Task ModMoney(string type, int amount, IUser target = null)
        {
            User user;
            if (type.ToLowerInvariant() == "add" || type.ToLowerInvariant() == "set")
            {
                using var Database = new ApplicationDbContextFactory().CreateDbContext();
                string yuser = "";
                if (target == null)
                {
                    user = Database.Users.FirstOrDefault(x => x.Id == Context.User.Id);
                    yuser = $"{Context.User.Username}";
                }
                else
                {
                    user = Database.Users.FirstOrDefault(x => x.Id == target.Id);
                    yuser = $"{target.Username}";
                }
                if (type == "add")
                {
                    if (!user.GrantMoney(Database.Users.FirstOrDefault(x => x.Id == 0), amount))
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nBank has no money, convince someone to gamble");
                        return;
                    }
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nSet **{yuser}**'s money to **{user.Money / 10000d}%**.");
                    await Database.SaveChangesAsync();
                    return;
                }
                else if (type == "set")
                {
                    if (amount < 0)
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nCan't set to a negative.");
                        return;
                    }
                    else
                    {
                        if (!user.GrantMoney(Database.Users.FirstOrDefault(x => x.Id == 0), amount - user.Money))
                        {
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nBank has no money, convince someone to gamble.");
                            return;
                        }
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nSet **{yuser}**'s money to **{amount / 10000d}%**.");
                        await Database.SaveChangesAsync();
                        return;
                    }
                    
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nInvalid type.");
            }
        }

        //[Command("hello")]
        //public async Task Hello(int a, int b)
        //{
        //    int x = a - b;
        //    string message = "";
        //    if (x < 0)
        //    {
        //        message += "The result is negative!";
        //    }
        //    else if (x > 0)
        //    {
        //        message += "The result is positive!";
        //    }
        //    else
        //    {
        //        message += "The result is 0!";
        //    }
        //    await Context.Channel.SendMessageAsync($"The result is {x}\n{message}");
        //}
            // test commands
            //[Command("cbt", RunMode = RunMode.Async)]
            //public async Task CombatTest()
            //{
            //    string[] attackse = new string[]
            //    {
            //        "a bite",
            //        "a crowbar",
            //        "invasive odor",
            //        "an intense slap"
            //    };

            //    int hp = 100;
            //    int atk = 100;
            //    int bhp = 1000;
            //    int dmg = 0;
            //    int edg = 0;
            //    string cbta = "";
            //    int numatt = SRandom.Next(attackse.Length);
            //    string at1;
            //    string at2;
            //    string eattack;
            //    int cb;
            //    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nBattle against **Fishthot**\nChoose your battle fish:\n**Pistol Shrimp | Coomfish**");
            //    var message = await NextMessageAsync();
            //    if (message.Content.ToLowerInvariant() == "pistol shrimp")
            //    {
            //        cbta = "Pistol Shrimp";
            //        at1 = "Gun";
            //        at2 = "Crowbar";
            //        cb = 1;
            //        hp = 80;
            //        atk = 20;
            //    }
            //    else if (message.Content.ToLowerInvariant() == "coomfish")
            //    {
            //        cbta = "Coomfish";
            //        at1 = "Eruption";
            //        at2 = "Smack";
            //        cb = 2;
            //        hp = 160;
            //        atk = 10;
            //    }
            //    else
            //    {
            //        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nInvalid battle fish??? are you RETARDeDED??");
            //        return;
            //    }
            //    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou have chosen: **{cbta}**, your stats are HP: **{hp}** ATK: **{atk}**\n\nBegin battle?");
            //    message = await NextMessageAsync();
            //    if (message.Content.ToLowerInvariant() == "yes")
            //    {
            //        string ment = $"{Context.User.Mention}\n";
            //        while (bhp > 0 && hp > 0)
            //        {
            //            if (bhp > 0 && hp > 0)
            //            {
            //                edg = SRandom.Next(3, 16);
            //                dmg = SRandom.Next(atk, atk * 20);
            //                eattack = attackse[numatt];
            //                hp -= edg;
            //                await Context.Channel.SendMessageAsync($"{ment}**Fishthot** attacks with {eattack}, dealing **{edg}** damage.\n**{cbta}**'s **HP** drops to {hp}.");
            //                ment = "";
            //                if (hp <= 0)
            //                {
            //                    await Context.Channel.SendMessageAsync($"Oh dear! **{cbta}** has fallen in battle.\nChoose your last ditch effort.\n**Belt** | **Punch**");
            //                    message = await NextMessageAsync();
            //                    if (message.Content.ToLowerInvariant() == "belt")
            //                    {
            //                        dmg = SRandom.Next(20, 100);
            //                        bhp -= dmg;
            //                    }
            //                    else if (message.Content.ToLowerInvariant() == "punch")
            //                    {
            //                        dmg = SRandom.Next(40, 80);
            //                        bhp -= dmg;
            //                    }
            //                    await Context.Channel.SendMessageAsync($"Your last ditch effort dealt **{dmg}** damage, reducing **Fishthot**'s health to {bhp}.");
            //                }
            //                else
            //                {
            //                    bhp -= dmg;
            //                    await Context.Channel.SendMessageAsync($"What will you attack with?\n**{at1}** | **{at2}**");
            //                    message = await NextMessageAsync();
            //                    if (cb == 1 && message.Content.ToLowerInvariant() == "gun")
            //                    {
            //                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n**{cbta}** pulls out his trusty glock and shoots at **Fishthot**, dealing **{dmg}** damage.\n**Fishthot**'s HP has been reduced to **{bhp}**.");
            //                    }
            //                    else if (cb == 1 && message.Content.ToLowerInvariant() == "crowbar")
            //                    {
            //                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n**{cbta}** unsheathes a menacing looking crowbar and lands a nice smack on **Fishthot**, dealing **{dmg}** damage.\n**Fishthot**'s HP has been reduced to **{bhp}**.");
            //                    }
            //                    else if (cb == 2 && message.Content.ToLowerInvariant() == "eruption")
            //                    {
            //                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n**{cbta}** releases a massive ***CUM ERUPTION*** on **Fishthot**, dealing **{dmg}** damage.\n**Fishthot**'s HP has been reduced to **{bhp}**.");
            //                    }
            //                    else if (cb == 2 && message.Content.ToLowerInvariant() == "smack")
            //                    {
            //                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n**{cbta}** feeds **Fishthot** a nice knuckle sandwich with his **Power Arm**, dealing **{dmg}** damage.\n**Fishthot**'s HP has been reduced to **{bhp}**.");
            //                    }
            //                    else
            //                    {
            //                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n**{cbta}** did not understand your command.\n**{cbta}** is looking at you with disappointed eyes.\nYour turn is skipped, good job retard.");
            //                    }
            //                }

            //            }
            //            else
            //            {
            //                if (bhp <= 0 && hp > 0)
            //                {
            //                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n**You win!** Go buy **{cbta}** a beer or something for his great accomplishments.");
            //                    break;
            //                }
            //                else if (bhp <= 0 && hp < 0)
            //                {
            //                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n**{cbta}** is gone. But so is **Fishthot**. Make sure to boil him in a good broth, he would have deserved it.");
            //                    break;
            //                }
            //                else if (hp <= 0)
            //                {
            //                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n**{cbta}** is gone. Now nothing stands between **Fishthot** and your frail frame.");
            //                    break;
            //                }
            //                else
            //                {
            //                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nSomething went ***REALLY*** wrong");
            //                    break;
            //                }
            //            }
            //        }
            //    }
            //    else
            //    {
            //        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nyou afraid or something? loser lmao");
            //        return;
            //    }
            //}





            //[RequireRolePrecondition(AccessLevel.BotOwner)]
            //[Command("getstamp")]
            //public async Task YeedraStamp()
            //{
            //    ulong stamp = DateTime.UtcNow.ToYeedraStamp();
            //    await ReplyAsync($"{Context.User.Mention} {stamp}");
            //}
            //[RequireRolePrecondition(AccessLevel.BotOwner)]
            //[Command("savefile")]

            //public async Task SaveFile(string fday, string fscore)
            //{
            //    string location = Path.Combine(Environment.CurrentDirectory, "drawtasks");
            //    string tlocation = ($"{location}/days.txt");
            //    var attachments = Context.Message.Attachments;
            //    string fname = $"{fday}-{fscore}";
            //    if (File.Exists(location + "/days.txt"))
            //        {

            //        }
            //    foreach (var item in attachments)
            //    {
            //        Uri link = new Uri(item.Url);
            //        using (WebClient _webclient = new WebClient())
            //        {
            //            if (!Directory.Exists(location))
            //                Directory.CreateDirectory(location);
            //            location += ($"/{fday}-{fscore}.jpg");
            //            _webclient.DownloadFileAsync(link, location);
            //        }
            //        await ReplyAsync($"Post archived");
            //        break;
            //    }
            //}


        }
}

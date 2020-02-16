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

namespace Kehyeedra3
{
    //..[prefix]stats[group] ping[command]
    //..stats ping
    [Group]
    public class Stats : ModuleBase ///////////////////////////////////////////////
    {
        [Command("ping")]
        public async Task Pong()
        {
            await Context.Channel.TriggerTypingAsync();
            await ReplyAsync($"My current ping is {Bot._bot.GetShardFor(Context.Guild).Latency}ms");
        }
    }
    [Group]
    public class HelpModule : ModuleBase ///////////////////////////////////////////////
    {
        private CommandService _service;

        public HelpModule(CommandService service) //Create a constructor for the commandservice dependency
        {
            _service = service;
        }
        [Command("commands")]
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

        [Command("command")]
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
        }

        [Command("help")]
        public async Task HelpCommand()
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.AddField("help", "Shows this thing");
            embed.AddField("ping", "Shows ping to server");
            embed.AddField("commands", "Lists commands");
            embed.AddField("command", "Tells what parameters a command uses");
            embed.AddField("delet", "Posts a delet this image. Can be used on other channels.");
            embed.AddField("ratetrap", "Rates your or another person's trap potential as a percentage");
            embed.AddField("8b", "Gives a prediction like a generic 8ball command that every self respecting discord bot must have");
            embed.AddField("AIMLbot", "Mention me to talk with me (don't expect intelligence)");
            embed.AddField("math", "It's a calculator, that's what compooter do");
            embed.AddField("roll", "Rolls dice. Eg. 'roll d20'");
            embed.AddField("mine", "Mines %coins");
            embed.AddField("bet", "Gamble %coins in units of 0.0001%");
            embed.AddField("balance", "Displays the percentage of the total currency you own");
            embed.AddField("bank", "Displays the percentage of total currency the bank owns");
            embed.AddField("give", "Give a user money. Eg. 'give @user [amount]'");
            embed.AddField("say", "Sends given message to given channel (admin only)");
            embed.AddField("adddelet", "Adds a delet this image to the bot from link or image (admin only)");
            await ReplyAsync("Here's a list of commands and what they do", false, embed.Build());
        }
    }
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
    public class Stuff : ModuleBase ///////////////////////////////////////////////
    {
        //public DatabaseService dbService { get; set; }

        [Command("delet")]
        public async Task DeletThis()
        {
            string imgdirpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Delet");
            DirectoryInfo imgdir = new DirectoryInfo(imgdirpath);
            var files = imgdir.GetFiles();
            var item = files[Bot._rnd.Next(0, files.Length)];
            await Context.Channel.SendFileAsync(item.FullName);

        }
        [Command("delet")]
        public async Task DeletThis(ITextChannel channel)
        {
            string imgdirpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Delet");
            DirectoryInfo imgdir = new DirectoryInfo(imgdirpath);
            var files = imgdir.GetFiles();
            var item = files[Bot._rnd.Next(0, files.Length)];
            await channel.SendFileAsync(item.FullName);

        }

        [Command("ratetrap")]
        public async Task RateTrap()
        {
            Random rando = new Random();
            Random rando1 = new Random();
            int trapRating0 = rando.Next(0, 101);
            if (trapRating0 == 100)
            {
                int trapRating1 = rando1.Next(0, 1001);
                await Context.Channel.SendMessageAsync($"I'd say right now you're {trapRating1}% passable");
            }
            else
            {
                await Context.Channel.SendMessageAsync($"I'd say right now you're {trapRating0}% passable");
            }
        }
        [Command("ratetrap")]
        public async Task RateOtherTrap([Remainder] string name)
        {
            Random rando = new Random();
            Random rando1 = new Random();
            int trapRating0 = rando.Next(0, 101);
            if (trapRating0 == 100)
            {
                int trapRating1 = rando1.Next(0, 1001);
                await Context.Channel.SendMessageAsync($"I'd say right now {name} is {trapRating1}% passable");
            }
            else
            {
                await Context.Channel.SendMessageAsync($"I'd say right now {name} is {trapRating0}% passable");
            }
        }
        [Command("ratertrap")]
        public async Task RaterTrap()
        {
            await Context.Channel.SendMessageAsync("Please do not be like this man http://tinyurl.com/y7lj6nob");
        }
        [Command("8b")]
        public async Task VapeBall([Remainder] string input)
        {
            Random rando = new Random();
            string[] predictions = new string[]
            {
                "No but you're still gay",
                "I think so",
                "Mayhaps",
                "Yeah but you're still gay",
                "No kys"
            };
            int randomIndex = rando.Next(predictions.Length);
            string text = predictions[randomIndex];
            await ReplyAsync(text + " " + Context.User.Mention);
        }
        [Command("math")]
        public async Task Mathboi([Remainder] string input)
        {
            string result = new DataTable().Compute(input, null).ToString();
            await Context.Channel.SendMessageAsync($"{Context.User.Mention} {input} = {result}");
        }
        [Command("roll")]
        public async Task RollDice([Remainder] string input)
        {
            int dinput = int.Parse(input.Substring(input.IndexOf("d")).Replace("d", ""));
            Random rando = new Random();
            int output = rando.Next(dinput+1);
            await Context.Channel.SendMessageAsync("" + output);
        }
        [Command("remind")]
        public async Task Reminder(ulong d, ulong h, ulong m, [Remainder] string r)
        {
            DateTime dt = DateTime.UtcNow;
            
            ulong yeedraStamp = DateTime.UtcNow.ToYeedraStamp();

            var reminder = new Reminder
            {
                UserId = Context.User.Id,
                Message = ($"Ok dude so at about UTC{dt} you wanted me to remind you and I quote '{r}'"),
                Created = yeedraStamp,
                Send = ((d * 86400) + (h * 3600) + (m * 60)) + yeedraStamp
            };

            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                Database.Reminders.Add(reminder);

                await Database.SaveChangesAsync().ConfigureAwait(false);
            }
            await Context.Channel.SendMessageAsync($"Ok dude I'll remind you in {d}d {h}h {m}m");
        }
    }
    
    public class Money : ModuleBase<ICommandContext>
    {
        readonly string[] ores = new string[]
        {
                "**Gold**,",
                "**Platinum**,",
                "**Plastids**,",
                "a ticking **Time Bomb**,",
                "**Neural Sensors**,",
                "an **Amethyst**,",
                "**Germanium**,",
                "a **Hotdog**,",
                "**Corundum**,",
                "**Quartz**,",
                "**Lithium**,",
                "**Stone**,",
                "a lost **Tribe of Ethiopians**,",
                "**Beryllium**,",
                "**Gallium**,",
                "an **Amber**,",
                "**Bismuth**",
                "an **Emerald**,",
                "a lost **Sock**,",
                "**Tellurium**,",
                "**Ferrite**,",
                "a **Glass of Water**,",
                "**Redstone**,",
                "**Racism**,",
                "**Bronze Ore**,",
                "**Chlorophyte**,",
                "a **Mysterious Artifact of Great Power**,",
                "**Goblite**,",
                "**Ligmanite**,",
                "**Ramen's Friendship**,",
                "an unidentified **Skeleton**,",
                "a piece of **Gravel**,",
                "**Copper**,",
                "**Volatile Motes**,",
                "a **Diamond**,",
                "**Thorium**,",
                "a **Fresh Apple**,",
                "**Raid Shadow Legends** sponsorship money,",
                "a **Boot**,",
                "**Runite**,",
                "a **WinRAR license key**,",
                "a **Viet Cong Tunnel**,",
                "a single unit of several **Trees**,",
                "**1,000,000₩**,",
                "a **Rock Golem**,",
                "a piece of **Toast**,",
                "**Luminite**,",
                "a **Funky Lava Lamp**,",
                "the **Cum Chalice**, you raise a toast to Nick.",
                "a **#%**,",
                "the **Master Sword**,",
                "your **True Calling in Life**,",
                "the **Ocarina of Time**,",
                "**Phosphophyllite**,",
                "a **Brain**,",
                "**Tom's Penis**,",
                "**Oil**,",
                "a **Can of Peaches**,",
                "a **Used Deodorant Stick**,"
        };
        readonly string o = "<:ye:677089325208305665>";
        readonly string n = "<:no:677091514249248778>";
        readonly string ye = "<:ya:677179974154715146>";

        [Command("mine"), Ratelimit(6, 2, Measure.Minutes)]
        public async Task Mine()
        {
            ulong time = ulong.Parse(DateTime.Now.ToString("yyyyMMddHHmm"));
            ulong lastmine;
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                var user = Database.Users.FirstOrDefault(x => x.Id == Context.User.Id);
                lastmine = user.LastMine;
                user.LastMine = time;
                await Database.SaveChangesAsync();
            }
            if (lastmine < time)
            {
                int res1 = SRandom.Next(0, 101);
                int res2 = SRandom.Next(0, 101);
                int res3 = SRandom.Next(0, 101);
                int end = 0;
                string marks = $"{n}{n}{n}";
                int num = SRandom.Next(ores.Length);
                string ore = ores[num];
                if (res1 > 20)
                {
                    end = 1;
                    marks = $"{o}{n}{n}";
                    if (res2 > 50)
                    {
                        end = 2;
                        marks = $"{o}{o}{n}";
                        if (res3 > 80)
                        {
                            marks = $"{o}{o}{o}";
                            string bonus = "";
                            int res4 = SRandom.Next(0, 6) * 2;
                            int res5 = res4 / 2;
                            end = res4 + 3;
                            for (int i = 0; i < 5; i++)
                            {
                                if (i < res5)
                                {
                                    bonus += $"{ye}";
                                }
                                else
                                {
                                    bonus += $"{n}";
                                }
                            }
                            if (res4 == 0)
                            {
                                await Context.Channel.SendMessageAsync($"{marks} **+** {bonus}\n{Context.User.Mention} **Lucky strike!** Bonus: {ore} You earned {end / 10000d}%");
                            }
                            else
                            {
                                await Context.Channel.SendMessageAsync($"{marks} **+** {bonus}\n{Context.User.Mention} **Lucky strike!** Bonus: {res4}, You earned {end / 10000d}%");
                            }
                        }
                    }
                }

                if (end == 0)
                {
                    await Context.Channel.SendMessageAsync($"{marks}\n{Context.User.Mention} You have found {ore} you presume it is worthless and toss it away.");
                }
                else
                {
                    if (end < 3)
                    {
                        await Context.Channel.SendMessageAsync($"{marks}\n{Context.User.Mention} You found {end / 10000d}% while mining");
                    }
                }


                if (end != 0)
                {
                    using (var Database = new ApplicationDbContextFactory().CreateDbContext())
                    {
                        var user = Database.Users.FirstOrDefault(x => x.Id == Context.User.Id);

                        if (!user.GrantMoney(Database.Users.FirstOrDefault(x => x.Id == 0), end))
                        {
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention} Bank has no money, convince someone to gamble");
                        }

                        await Database.SaveChangesAsync();
                    }
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} wait 1 minute ok next minute yeah? yeah buddy?");
            }
        }
        [Command("balance")]
        public async Task Shekels([Remainder] IUser otherUser = null)
        {
            User user;
            User buser;
            User suser;

            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                user = Database.Users.FirstOrDefault(x => x.Id == (otherUser == null ? Context.User.Id : otherUser.Id));
                buser = Database.Users.FirstOrDefault(x => x.Id == 0);
                suser = Database.Users.FirstOrDefault(x => x.Id == 1);
            }

            if (otherUser == null)
            {
                if (user == null)
                {
                    using (var Database = new ApplicationDbContextFactory().CreateDbContext())
                    {
                        user = new User
                        {
                            Id = Context.User.Id,
                            Avatar = Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl(),
                            Username = Context.User.Username
                        };
                        Database.Users.Add(user);
                        await Database.SaveChangesAsync();
                    }
                }
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} You own {user.Money / 10000d}%\nWhich is {(user.Money * 100) / (1000000 - buser.Money - suser.Money)}% of the money in circulation");
            }
            else
            {
                if (user == null)
                {
                    using (var Database = new ApplicationDbContextFactory().CreateDbContext())
                    {
                        user = new User
                        {
                            Id = Context.User.Id,
                            Avatar = Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl(),
                            Username = Context.User.Username
                        };
                        Database.Users.Add(user);
                        await Database.SaveChangesAsync();
                    }
                }
                await Context.Channel.SendMessageAsync($"{otherUser.Mention} owns {user.Money / 10000d}%\nWhich is {(user.Money * 100) / (1000000 - buser.Money - suser.Money)}% of the money in circulation");
            }
        }

        [Command("bank")]
        public async Task BankBalance()
        {
            User user;
            User suser;
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                user = Database.Users.FirstOrDefault(x => x.Id == 0);
                suser = Database.Users.FirstOrDefault(x => x.Id == 1);
            }
            await Context.Channel.SendMessageAsync($"Bank has {user.Money/10000d}% left\nSkuld can currently sell a maximum of {suser.Money*64}₩ at 0.0001% = 64₩ exchange rate");
        }
        [Command("bet")]
        public async Task Gamble(int wager)
        {
            int res0 = SRandom.Next(0, 10000000);
            Random ran = new Random(res0);
            int res1 = ran.Next(0, 101);
            if (wager<0)
            {
                wager = 0;
            }
            int loss = wager;
            if (res1 == 100)
            {
                wager = wager*4;
            }
            else
            {
                if (res1 >= 95)
                {
                    wager = wager * 3;
                }
                else
                {
                    if (res1 == 77)
                    {
                        wager = wager * 7;
                    }
                    else
                    {
                        if (res1 < 60)
                        {
                            wager = 0;
                        }
                        else
                        {
                            wager = wager * 2;
                        }
                    }
                }
            }
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                var user = Database.Users.FirstOrDefault(x => x.Id == Context.User.Id);
                var buser = Database.Users.FirstOrDefault(x => x.Id == 0);
                if (user.Money < loss)
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention} You can't afford that, go back to the mines.");
                }
                else
                {
                    if (buser.Money > 100)
                    {
                        if (!user.GrantMoney(Database.Users.FirstOrDefault(x => x.Id == 0), (wager) - loss))
                        {
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention} Bank has no money, gamble more and lose please.");
                        }
                        await Database.SaveChangesAsync();

                        EmbedBuilder embed = new EmbedBuilder();
                        if (res1 == 77)
                        {
                            embed.AddField($"**Rolled: Lucky cat!**", $"Result: +{((wager) - loss) / 10000d}%\nBalance: {(user.Money) / 10000d}%");
                            await ReplyAsync($"{Context.User.Mention}", false, embed.Build());
                        }
                        else
                        {
                            if (((wager) - loss) > 0)
                            {
                                embed.AddField($"**Rolled: {res1}**", $"Result: +{((wager) - loss) / 10000d}%\nBalance: {(user.Money) / 10000d}%");
                                await ReplyAsync($"{Context.User.Mention}", false, embed.Build());
                            }
                            if (((wager) - loss) < 0)
                            {
                                embed.AddField($"**Rolled: {res1}**", $"Result: {((wager) - loss) / 10000d}%\nBalance: {(user.Money) / 10000d}%");
                                await ReplyAsync($"{Context.User.Mention}", false, embed.Build());
                            }
                        }
                    }
                    else
                    {
                        await ReplyAsync($"Hey, stop that.");
                    }
                }
            }
        }
        [Command("give")]
        public async Task GiveShekel(IGuildUser person, int amount)
        {
            if (amount > 0)
            {
                using (var Database = new ApplicationDbContextFactory().CreateDbContext())
                {
                    var user = Database.Users.FirstOrDefault(x => x.Id == Context.User.Id);
                    if (user.Money < amount)
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention} You don't have that much money??");
                    }
                    else
                    {
                        if (user.Id == person.Id)
                        {
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention} You have transferred your money to yourself???");
                        }
                        else
                        {
                            var transfer = amount - (amount * 2);
                            if (!user.GrantMoney(Database.Users.FirstOrDefault(x => x.Id == person.Id), transfer))
                            {
                                await Context.Channel.SendMessageAsync($"{Context.User.Mention} You can't afford that, go back to the mines.");
                            }
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention} **{amount / 10000d}%** has been transferred from your account.");
                            await Database.SaveChangesAsync();
                        }
                    }
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} That's not how this works??");
            }

        }
    }
    
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
    //                await ReplyAsync($"{name.Mention}'s hazmat suit is protecting them from the corona");
    //            }
    //            else
    //            {
    //                await name.AddRoleAsync(role).ConfigureAwait(false);
    //                await name.RemoveRoleAsync(hearole).ConfigureAwait(false);

    //                Console.WriteLine($"{Context.User.Username} has infected {name.Username}");

    //                await ReplyAsync($"{Context.User.Mention} has infected {name.Mention}");
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
    //                await ReplyAsync($"{name.Mention} absolutely refuses to be vaccinated");
    //            }
    //            else
    //            {
    //                if (name.RoleIds.Any(id => id == 672517021732438026))
    //                {
    //                    await name.AddRoleAsync(role).ConfigureAwait(false);
    //                    await name.RemoveRoleAsync(infrole).ConfigureAwait(false);

    //                    Console.WriteLine($"{Context.User.Username} has cured {name.Username}");

    //                    await ReplyAsync($"{Context.User.Mention} has vaccinated {name.Mention}");
    //                }
    //                else
    //                {
    //                    await ReplyAsync($"{name.Mention} is not infected??? are you super retarded???");
    //                }
    //            }
    //        }
    //    }
    //}

        public class Admin : ModuleBase ///////////////////////////////////////////////
        {
        [RequireRolePrecondition(AccessLevel.ServerAdmin)]
            [Command("adddelet")]
            public async Task AddDelet() //Listens for attachments
            {
                var attachments = Context.Message.Attachments;//Gets attachments as var
                foreach (var item in attachments)
                {
                    Uri link = new Uri(item.Url);
                    using (WebClient _webclient = new WebClient())
                    {
                        string location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "delet");
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
            [Command("adddelet")]
            public async Task AddDelet(string url) //Listens for urls
            {
                Uri link = new Uri(url);
                using (WebClient _webclient = new WebClient())
                {
                    string location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"delet");
                    if (!Directory.Exists(location))
                        Directory.CreateDirectory(location);
                    location += "/" + Guid.NewGuid() + ".jpg";
                    _webclient.DownloadFileAsync(link, location);
                }
                await ReplyAsync($"Delet added");
            }
            [RequireRolePrecondition(AccessLevel.ServerAdmin)]
            [Command("say")]
            public async Task Say(ITextChannel channel, [Remainder]string message)
            {
                await channel.SendMessageAsync(message);
            }
            [RequireRolePrecondition(AccessLevel.BotOwner)]
            [Command("modifybot")]
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
            [Command("savefile")]

            public async Task SaveFile(string fday, string fscore)
        {
            string location = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "drawtasks");
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
        }

    }

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

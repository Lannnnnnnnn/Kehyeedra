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

namespace Kehyeedra3
{
    [Group]
    public class Stats : ModuleBase ///////////////////////////////////////////////
    {
        [Command("ping")]
        public async Task Pong()
        {
            await Context.Channel.TriggerTypingAsync();
            await ReplyAsync($"My current ping is {Bot._bot.Latency}ms");
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
        [Command("commands"), Alias("coomands")]
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

        [Command("command"),Alias("coomand")]
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
            embed.AddField("ping", "Shows ping to server");
            embed.AddField("commands", "Lists commands");
            embed.AddField("command", "Tells what parameters a command uses");
            embed.AddField("help", "Shows this thing");

            embed.AddField("delet", "Posts a delet this image. Can be used on other channels.");
            embed.AddField("ratetrap", "Rates your or another person's trap potential as a percentage");
            embed.AddField("8b", "Gives a prediction like a generic 8ball command that every self respecting discord bot must have");
            embed.AddField("math", "It's a calculator, that's what compooter do");
            embed.AddField("roll", "Rolls dice. Eg. 'roll d20'");
            embed.AddField("remind","Reminds you in a given time. (days, hours, minutes) Eg. 'remind 1 2 3 wash hands' would remind you in 1 day, 2 hours, 3 minutes to wash your hands");
            embed.AddField("dab", "Dabs a person");

            embed.AddField("mine", "Mines %coins");
            embed.AddField("fish", "Cast your line into the abyss, see if something bites?");
            embed.AddField("inventory, inv", "Shows the fish you have currently. Might show other things in the distant future.");
            //embed.AddField("buy", " ");
            //embed.AddField("sell"," ");
            embed.AddField("balance, bal", "Displays the percentage of the total currency you own");
            embed.AddField("bank", "Displays the percentage of total currency the bank owns");
            embed.AddField("bet", "Gamble %coins in units of 0.0001%");
            embed.AddField("leaderboard, lb", "Shows the top 10 people in regards to % money as well as how much of the money in circulation they own");
            embed.AddField("give", "Give a user money. Eg. 'give @user [amount]'");

            embed.AddField("AIMLbot", "Mention me to talk with me (don't expect intelligence)");
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
    public class Stuff : InteractiveBase<SocketCommandContext> ///////////////////////////////////////////////
    {

        [Command("delet")]
        public async Task DeletThis()
        {
            string imgdirpath = Path.Combine(Environment.CurrentDirectory, "Delet");
            DirectoryInfo imgdir = new DirectoryInfo(imgdirpath);
            var files = imgdir.GetFiles();
            var item = files[Bot._rnd.Next(0, files.Length)];
            await Context.Channel.SendFileAsync(item.FullName);

        }
        [Command("delet")]
        public async Task DeletThis(ITextChannel channel)
        {
            string imgdirpath = Path.Combine(Environment.CurrentDirectory, "Delet");
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
            int output = SRandom.Next(dinput);
            await Context.Channel.SendMessageAsync("" + output+1);
        }
        [Command("remind")]
        public async Task Reminder(ulong d, ulong h, ulong m, [Remainder] string r)
        {
            DateTime dt = DateTime.UtcNow;

            string time = dt.ToString("dd/MM/yyyy HH:mm");
            
            ulong yeedraStamp = DateTime.UtcNow.ToYeedraStamp();

            var reminder = new Reminder
            {
                UserId = Context.User.Id,
                Message = ($"At **UTC {time}** you wanted me to remind you:\n**'{r}'**"),
                Created = yeedraStamp,
                Send = ((d * 86400) + (h * 3600) + (m * 60)) + yeedraStamp
            };

            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                Database.Reminders.Add(reminder);

                await Database.SaveChangesAsync().ConfigureAwait(false);
            }
            await Context.Channel.SendMessageAsync($"{Context.User.Mention} Ok, I'll remind you in {d}d {h}h {m}m");
        }
        [Command("grant")]
        public async Task Daycare(IGuildUser ouser)
        {
            var user = Context.Guild.GetUser(Context.User.Id);
            var drole = Context.Guild.GetRole(682109241363922965);
            if (user.Roles.Any(x => x.Id == 682109241363922965))
            {
                await user.RemoveRoleAsync(drole);
                await ouser.AddRoleAsync(drole);
                await Context.Channel.SendMessageAsync($"*{ouser.Mention} the power of daycare rests in the palm of your hands*");
            }
            else
            {
                await Context.Channel.SendMessageAsync($"HNNNNG you do not possess this power HNNNGGGG");
            }
        }
        [Command("dab")]
        public async Task Dab(IGuildUser user = null)
        {
            if (user == null)
            {
                await Context.Channel.SendMessageAsync($"You put a dab of creamy sauce on your delicious, crunchy fishstick.\nYou have gained +5 calories.");
            }
            else
            {
                await Context.Channel.SendMessageAsync($"You give your good friend {user.Mention} a dab of creamy sauce to enjoy with their delicious, crunchy fishstick.\n{user.Mention} has gained +5 calories.");
            }
        }
    }

    public class Economy : InteractiveBase<SocketCommandContext>
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
                "a **Girlfriend**,",
                "**Thorium**,",
                "a **Fresh Apple**,",
                "**Raid Shadow Legends** sponsorship money,",
                "a **Boot**,",
                "**Runite**,",
                "a **WinRAR license key**,",
                "a **Viet Cong Tunnel**,",
                "a single unit of several **Trees**,",
                "**1,000,000₩**,",
                "**Jas's Love**,",
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

        readonly string[] discards = new string[]
        {
                "you presume it is worthless and toss it away",
                "you drop it and lose it",
                "a vicious furry takes it from you",
                "it appears to have vanished",
                "but it seems like you were hallucinating",
                "but it is seized by the communists",
                "you mistake it for a chance to succeed in life and throw it away",
                "you get scared and curb stomp it, shattering it",
                "the **Goblins** claim rightful possession of it"
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
                int numd = SRandom.Next(discards.Length);
                string ore = ores[num];
                string discard = discards[numd];
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
                    await Context.Channel.SendMessageAsync($"{marks}\n{Context.User.Mention} You have found {ore} {discard}.");
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
        [Command("fish"), Ratelimit(6, 2, Measure.Minutes)]
        public async Task FishCommand()
        {
            ulong time = ulong.Parse(DateTime.Now.ToString("yyyyMMddHHmm"));
            ulong lastfish;
            ulong totalXp;
            ulong xp;
            ulong level;
            ulong lvlXp;
            Dictionary<FishSpecies, int[]> inv = new Dictionary<FishSpecies, int[]>();
            List<Fish> fishes = Fishing.GetFishList();

            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                var user = Database.Fishing.FirstOrDefault(x => x.Id == Context.User.Id);
                if (user == null)
                {
                    {
                        user = new Fishing
                        {
                            Id = Context.User.Id,
                        };
                        Database.Fishing.Add(user);
                    }
                }
                else
                {
                    inv = user.GetInventory();
                }
                level = user.Lvl;
                lastfish = user.LastFish;
                totalXp = user.TXp;
                lvlXp = user.Xp;

                await Database.SaveChangesAsync();
            }



            if (lastfish < time)
            {
                int rari = (SRandom.Next(0, 2001));
                int weight = SRandom.Next(10+Convert.ToInt32(level*5), 1501);
                ulong rarity;

                if (level < 100)
                {
                    rarity = level*10 + (ulong)rari;
                }
                else
                {
                    rarity = 1000 + (ulong)rari;
                }

                Fish fish;

                if (rarity == 2070 || rarity == 2770)
                {
                    rarity = Convert.ToUInt64(SRandom.Next(2007, 3001))+level*4;
                    if (rarity > 2500)
                    {
                        List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.Legendary).ToList();
                        fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                        xp = 10;
                    }
                    else
                    {
                        List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.Rare).ToList();
                        fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                        xp = 20;
                    }
                }

                else if (rarity > 1700)
                {
                    rarity = Convert.ToUInt64(SRandom.Next(1750, 2801));
                    List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.Uncommon).ToList();
                    fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                    xp = 10;
                    if (rarity > 2600)
                    {
                        possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.Rare).ToList();
                        fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                        xp = 20;
                    }
                }
                else
                {
                    List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.Common).ToList();
                    fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                    xp = 5;
                }

                FishSize size;

                if (fish.Rarity == FishRarity.Legendary)
                {
                    weight = 1000;
                }

                if (weight >= 750)
                {
                    size = FishSize.Medium;
                    if (weight >= (1000 - Convert.ToInt32(level*2)))
                    {
                        weight = SRandom.Next(10, 2001) + Convert.ToInt32(level*5);
                    }

                    if (fish.Rarity == FishRarity.Legendary)
                    {
                        weight = SRandom.Next(2000 + Convert.ToInt32(level*20), 40001);
                    }
                    double w = Convert.ToDouble(weight);
                    if (weight >= 1000)
                    {
                        xp = Convert.ToUInt64(Math.Round((xp * w / 1000), 0, MidpointRounding.ToEven));
                    }
                }
                else
                {
                    size = FishSize.Small;
                }

                if (weight >= 1500)
                {
                    size = FishSize.Large;
                }

                string lvlUp = "";

                using (var Database = new ApplicationDbContextFactory().CreateDbContext())
                {
                    var user = Database.Fishing.FirstOrDefault(x => x.Id == Context.User.Id);
                    user.LastFish = time;
                    await Database.SaveChangesAsync().ConfigureAwait(false);
                }

                if (rarity > 200)
                {
                    ulong toNextLvl = 0;
                    using (var Database = new ApplicationDbContextFactory().CreateDbContext())
                    {
                        var user = Database.Fishing.FirstOrDefault(x => x.Id == Context.User.Id);

                        inv = user.GetInventory();

                        int[] amounts;
                        if (!inv.TryGetValue(fish.Id, out amounts))
                        {
                            amounts = new int[] { 0, 0, 0 };
                            inv.Add(fish.Id, amounts);
                        }

                        int sizeIndex = (int)size;
                        amounts[sizeIndex]++;
                       
                        user.SetInventory(inv);

                        user.TXp += xp;

                        toNextLvl = user.Xp - user.TXp;
                        if (user.TXp >= user.Xp)
                        {
                            user.Lvl += 1;
                            lvlXp = 50;
                            for (ulong i = 0; i < user.Lvl; i++)
                            {
                                if (i <= user.Lvl)
                                {
                                    lvlXp += Convert.ToUInt64(Math.Round((lvlXp * 0.05d + 50d), 0, MidpointRounding.ToEven));
                                }
                            }
                            user.Xp = lvlXp;
                            toNextLvl = user.Xp - user.TXp;
                            level = user.Lvl;
                            lvlUp = $"**You leveled up!** You are now **Level {level}**";
                        }
                        else
                        {
                            lvlUp = $"You need **{toNextLvl}**xp more to reach Level **{level + 1}**";
                        }

                        await Database.SaveChangesAsync().ConfigureAwait(false); // :]
                    }

                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n {fish.Emote} You have caught a {weight / 100d}kg **{fish.Name}**, rarity: {fish.Rarity}\nYou gain **{xp}**xp.\n{lvlUp}");
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention} Your line snaps. Your disappointment is immeasurable, and your day is ruined.");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} arrrrr-right, ye scurby bastard, I know yer eager t' scour the seven seas but ye needs t' wait till the next minute t' pillage the booty'o'the depths, savvy?");
            }

        }
        [Command("inventory"), Alias("inv", "fishinv")]
        public async Task FishInventory([Remainder]IGuildUser user = null)
        {
            if (user == null)
                user = Context.User as IGuildUser;

            Fishing feeshUser;
            Dictionary<FishSpecies, int[]> inv = new Dictionary<FishSpecies, int[]>();
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                feeshUser = Database.Fishing.FirstOrDefault(x => x.Id == user.Id);
                if (feeshUser == null)
                {
                    feeshUser = new Fishing
                    {
                        Id = user.Id
                    };
                    
                    Database.Fishing.Add(feeshUser);
                }
                else
                {
                    inv = feeshUser.GetInventory();
                }

                await Database.SaveChangesAsync().ConfigureAwait(false);
            }

            if (inv.Any())
            {
                Dictionary<FishSpecies, int> small = new Dictionary<FishSpecies, int>();
                Dictionary<FishSpecies, int> med = new Dictionary<FishSpecies, int>();
                Dictionary<FishSpecies, int> large = new Dictionary<FishSpecies, int>();

                foreach (var entry in inv)
                {
                    if(entry.Value.Count() > 0)
                    {
                        if (entry.Value[0] > 0)
                        {
                            small.Add(entry.Key, entry.Value[0]);
                        }
                    }
                    if(entry.Value.Count() > 1)
                    {
                        if (entry.Value[1] > 0)
                        {
                            med.Add(entry.Key, entry.Value[1]);
                        }
                    }
                    if(entry.Value.Count() > 2)
                    {
                        if (entry.Value[2] > 0)
                        {
                            large.Add(entry.Key, entry.Value[2]);
                        }
                    }
                }

                if (user.Id != Context.User.Id)
                {
                    await Context.Channel.SendMessageAsync($"arr matey this be {user.Mention}'s locker");
                }

                if(small.Any())
                {
                    EmbedBuilder embed = new EmbedBuilder
                    {
                        Title = "small mateys"
                    };

                    foreach (var entry in small)
                    {
                        if (entry.Key == FishSpecies.LuckyCatfish)
                        {
                            embed.AddField("Lucky Catfish", $"{entry.Value} feeshie{(entry.Value > 1 ? "s" : "")}", true);
                        }
                        else
                        {
                            embed.AddField(entry.Key.ToString(), $"{entry.Value} feeshie{(entry.Value > 1 ? "s" : "")}", true);
                        }
                    }

                    await Context.Channel.SendMessageAsync($"", embed: embed.Build());
                }
                
                if(med.Any())
                {
                    EmbedBuilder embed = new EmbedBuilder
                    {
                        Title = "medium mateys"
                    };

                    foreach (var entry in med)
                    {
                        if (entry.Key == FishSpecies.LuckyCatfish)
                        {
                            embed.AddField("Lucky Catfish", $"{entry.Value} feeshie{(entry.Value > 1 ? "s" : "")}", true);
                        }
                        else
                        {

                            embed.AddField(entry.Key.ToString(), $"{entry.Value} feeshie{(entry.Value > 1 ? "s" : "")}", true);
                        }
                    }

                    await Context.Channel.SendMessageAsync($"", embed: embed.Build());
                }

                if(large.Any())
                {
                    EmbedBuilder embed = new EmbedBuilder
                    {
                        Title = "large mateys"
                    };

                    foreach (var entry in large)
                    {
                        if(entry.Key == FishSpecies.LuckyCatfish)
                        {
                            embed.AddField("Lucky Catfish", $"{entry.Value} feeshie{(entry.Value > 1 ? "s" : "")}", true);
                        }
                        else
                        {
                            embed.AddField(entry.Key.ToString(), $"{entry.Value} feeshie{(entry.Value > 1 ? "s" : "")}", true);
                        }
                    }

                    await Context.Channel.SendMessageAsync($"", embed: embed.Build());
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("Go fish nigger").ConfigureAwait(false);
            }
        }
        //[Command("tradebuy", RunMode = RunMode.Async)]
        //public async Task TradingBuy(int amount, string itemtype, int price, [Remainder] string item)
        //{
        //    using (var Database = new ApplicationDbContextFactory().CreateDbContext())
        //    {
        //        var KehUser = Database.Users.FirstOrDefault(x => x.Id == Context.User.Id);
        //        if (itemtype.ToLowerInvariant() == "fish")
        //        {
        //            FishSize size = 0;
        //            FishSpecies species = 0;
        //            List<Fish> fishes = Fishing.GetFishList();

        //            if (item.ToLowerInvariant().Contains("large"))
        //            {
        //                size = FishSize.Large;
        //            }
        //            else if (item.ToLowerInvariant().Contains("medium"))
        //            {
        //                size = FishSize.Medium;
        //            }
        //            else if (item.ToLowerInvariant().Contains("small"))
        //            {
        //                size = FishSize.Small;
        //            }
        //            else
        //            {
        //                await Context.Channel.SendMessageAsync($"{Context.User.Mention} Your size is not up to my standards.");
        //                return;
        //            }

        //            if (fishes.Any(z => item.ToLowerInvariant().Contains(z.Name.ToLowerInvariant())))
        //            {
        //                species = fishes.FirstOrDefault(z => item.ToLowerInvariant().Contains(z.Name.ToLowerInvariant())).Id;
        //            }
        //            else
        //            {
        //                await Context.Channel.SendMessageAsync($"{Context.User.Mention} The goo pool contains no such fish.");
        //                return;
        //            }

        //            if (Database.StoreFronts.Any(x=> x.StoreItemType == StoreItemType.Fish))
        //            {
        //                var stores = Database.StoreFronts.Where(x => x.StoreItemType == StoreItemType.Fish).ToList();

        //                stores.Shuffle();

        //                var store = stores.FirstOrDefault();

        //                if(store.Items.Any(x=>x.Item.ToLowerInvariant() == item.ToLowerInvariant()))
        //                {
        //                    var itm = store.Items.FirstOrDefault(x => x.Item.ToLowerInvariant() == item.ToLowerInvariant());

        //                    if(itm.Price * amount <= KehUser.Money)
        //                    {
        //                        if (itm.Amount >= amount)
        //                        {
        //                            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nTrade offer to buy item **{size} {species}** for **{price / 10000d}%**").ConfigureAwait(false);
        //                        }
        //                        else
        //                        {
        //                            await ReplyAsync("Whoa slow down there buckaroo, they ain't selling that much, go sit in the corner and think about what you've done").ConfigureAwait(false);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        await ReplyAsync("Nigger slow down, you aint got the cash monee to make that purchase smh frfr onjah").ConfigureAwait(false);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                await ReplyAsync("No one is selling, so you can't buy lmao, big stinky");
        //            }
        //        }
        //        else
        //        {
        //            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nInvalid trade type. Come back when the error command is fixed lmaoy").ConfigureAwait(false);
        //        }
        //    }
        //}
        //[Command("tradesell")]
        //public async Task TradingSell(int amount, string itemtype, int price, [Remainder] string item)
        //{
        //    string contents = "trade info";
        //    FishSize size = 0;
        //    FishSpecies species = 0;
        //    List<Fish> fishes = Fishing.GetFishList();
        //    if (itemtype.ToLowerInvariant() == "fish")
        //    {
        //        if (item.ToLowerInvariant().Contains("large"))
        //        {
        //            size = FishSize.Large;
        //        }
        //        else if (item.ToLowerInvariant().Contains("medium"))
        //        {
        //            size = FishSize.Medium;
        //        }
        //        else if (item.ToLowerInvariant().Contains("small"))
        //        {
        //            size = FishSize.Small;
        //        }
        //        else
        //        {
        //            await Context.Channel.SendMessageAsync($"{Context.User.Mention} Your size is not up to my standards.");
        //            return;
        //        }
        //        if (fishes.Any(z => item.ToLowerInvariant().Contains(z.Name.ToLowerInvariant())))
        //        {
        //            species = fishes.FirstOrDefault(z => item.ToLowerInvariant().Contains(z.Name.ToLowerInvariant())).Id;
        //        }
        //        else
        //        {
        //            await Context.Channel.SendMessageAsync($"{Context.User.Mention} The goo pool contains no such fish.");
        //            return;
        //        }
        //        contents += $"\ntype: sell\nitem: {size} {species}\namount: {amount}\nprice: {price / 10000d}%\n";

        //        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nTrade offer to sell item **{size} {species}** for **{price / 10000d}%**").ConfigureAwait(false);

        //        await Context.Channel.SendMessageAsync($"{contents}").ConfigureAwait(false);
        //    }
        //    else
        //    {
        //        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nInvalid trade type. Come back when the error command is fixed lmaoy").ConfigureAwait(false);
        //    }
        //}
        //[Command("tradeoffers")]
        //public async Task ShowOffers(bool localOffers = true)
        //{
        //    using (var database = new ApplicationDbContextFactory().CreateDbContext())
        //    {
        //        string message = "";
        //        if (localOffers)
        //        {

        //        }
        //    }
        //}

        [Command("balance"),Alias("bal","money")]
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
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} You own {user.Money / 10000d}%\nWhich is ~{Math.Round(((user.Money * 100d) / (1000000d - buser.Money - suser.Money)), 2, MidpointRounding.ToEven)}% of the money in circulation");
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
                await Context.Channel.SendMessageAsync($"{otherUser.Mention} owns {user.Money / 10000d}%\nWhich is ~{Math.Round(((user.Money * 100d) / (1000000d - buser.Money - suser.Money)), 2, MidpointRounding.ToEven)}% of the money in circulation");
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
            Random ran = new Random(SRandom.Next(0, 100000000) + int.Parse(Context.User.AvatarId + Context.User.Discriminator));
            int res1 = ran.Next(0, 11);
            if (res1 >= 5)
            {
                res1 = ran.Next(50, 101);
            }
            else
            {
                res1 = ran.Next(0, 50);
            }
            if (wager<0)
            {
                wager = 0;
            }
            int loss = wager;
            if (res1 == 100)
            {
                wager = wager*4;
            }
            else if (res1 >= 95)
            {
                wager = wager * 3;
            }
            else if (res1 == 77)
            {
                wager = wager * 7;
            }
            else if (res1 < 50)
            {
                wager = 0;
            }
            else
            {
                wager = wager * 2;
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
                        else if (((wager) - loss) > 0)
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
                    else
                    {
                        await ReplyAsync($"Hey, stop that.");
                    }
                }
            }
        }
        [Command("leaderboard"),Alias("top","lb")]
        public async Task Leaderboard()
        {
            List<User> users;
            User bank;
            User skuld;

            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                users = Database.Users.OrderByDescending(user => user.Money).ToList();
                bank = Database.Users.FirstOrDefault(x => x.Id == 0);
                skuld = Database.Users.FirstOrDefault(x => x.Id == 1);
            }
            users.Remove(bank);
            users.Remove(skuld);

            string leaderboardMessage = "**Top Ten Most Jewish Miners**:";
            for (int i = 0; i < 10; i++)
            {
                string percent = $"{ users[i].Money / 10000d }";
                string percentCirculating = $"{Math.Round(((users[i].Money * 100d) / (1000000d - bank.Money - skuld.Money)),2,MidpointRounding.ToEven)}";
                leaderboardMessage += $"\n**{users[i].Username}** : {percent}% ~ *{percentCirculating}% circulating*";
            }

            await Context.Channel.SendMessageAsync(leaderboardMessage);

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
            [Command("adddelet")]
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

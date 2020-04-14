using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Kehyeedra3.Services.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Kehyeedra3.Commands
{
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

        [Command("mine"), Ratelimit(6, 2, Measure.Minutes), Summary("Mines %coins")]
        public async Task Mine()
        {
            ulong time = ulong.Parse(DateTime.Now.ToString("yyyyMMddHHmm"));
            ulong lastmine;
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                var user = Database.Users.FirstOrDefault(x => x.Id == Context.User.Id);
                lastmine = user.LastMine;
                user.LastMine = time;
                user.Username = Context.User.Username;
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
        [Command("fish"), Ratelimit(6, 2, Measure.Minutes), Summary("Cast your line into the abyss, see if something bites?")]
        public async Task FishCommand()
        {
            ulong time = ulong.Parse(DateTime.Now.ToString("yyyyMMddHHmm"));
            ulong lastfish;
            ulong totalXp;
            ulong xp;
            ulong level;
            ulong lvlXp;
            int rod;
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
                rod = user.RodUsed;

                await Database.SaveChangesAsync();
            }



            if (lastfish < time)
            {
                int rari = (SRandom.Next(0, 2001));
                int weight = SRandom.Next(10 + Convert.ToInt32(level * 5), 1501);
                ulong rarity;

                if (level < 100)
                {
                    rarity = level * 10 + (ulong)rari;
                }
                else
                {
                    rarity = 1000 + (ulong)rari;
                }

                Fish fish;

                if (rarity == 777 || (rarity > 2060 && rarity <= 2070) || (rarity >= 2765 && rarity <= 2767))
                {
                    int tierRoll = SRandom.Next(0, 101);

                    if (rod >= 3 && tierRoll > 60)
                    {
                        List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T4Legendary).ToList();
                        fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                        xp = 25;
                    }
                    else if (rod >= 2 && tierRoll > 40)
                    {
                        List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T3Legendary).ToList();
                        fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                        xp = 20;
                    }
                    else if (rod >= 1 && tierRoll > 20)
                    {
                        List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T2Legendary).ToList();
                        fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                        xp = 15;
                    }
                    else
                    {
                        List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.Legendary).ToList();
                        fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                        xp = 10;
                    }
                    if (rarity == 777)
                    {
                        xp = 77;
                    }
                }

                else if (rarity > 1700)
                {
                    int tierRoll = SRandom.Next(0, 101);
                    rarity = Convert.ToUInt64(SRandom.Next(1750, 2801));
                    if (rod >= 3 && tierRoll > 60)
                    {
                        List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T4Uncommon).ToList();
                        fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                        xp = 20;
                    }
                    else if (rod >= 2 && tierRoll > 40)
                    {
                        List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T3Uncommon).ToList();
                        fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                        xp = 16;
                    }
                    else if (rod >= 1 && tierRoll > 20)
                    {
                        List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T2Uncommon).ToList();
                        fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                        xp = 13;
                    }
                    else
                    {
                        List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.Uncommon).ToList();
                        fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                        xp = 10;
                    }
                    if (rarity > 2600)
                    {
                        if (rod >= 3 && tierRoll > 60)
                        {
                            List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T4Rare).ToList();
                            fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                            xp = 35;
                        }
                        else if (rod >= 2 && tierRoll > 40)
                        {
                            List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T3Rare).ToList();
                            fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                            xp = 30;
                        }
                        else if (rod >= 1 && tierRoll > 20)
                        {
                            List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T2Rare).ToList();
                            fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                            xp = 25;
                        }
                        else
                        {
                            List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.Rare).ToList();
                            fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                            xp = 20;
                        }
                    }

                }
                else
                {
                    int tierRoll = SRandom.Next(0, 101);
                    if (rod >= 3 && tierRoll > 60)
                    {
                        List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T4Common).ToList();
                        fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                        xp = 10;
                    }
                    else if (rod >= 2 && tierRoll > 40)
                    {
                        List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T3Common).ToList();
                        fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                        xp = 8;
                    }
                    else if (rod >= 1 && tierRoll >= 20)
                    {
                        List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T2Common).ToList();
                        fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                        xp = 6;
                    }
                    else
                    {
                        List<Fish> possibleFishes = fishes.Where(f => (int)f.Rarity == (int)FishRarity.Common).ToList();
                        fish = possibleFishes[SRandom.Next(possibleFishes.Count)];
                        xp = 5;
                    }
                }

                FishSize size;

                if (fish.Rarity == FishRarity.Legendary || fish.Rarity == FishRarity.T2Legendary || fish.Rarity == FishRarity.T3Legendary || fish.Rarity == FishRarity.T4Legendary )
                {
                    weight = 1000;
                }

                if (weight >= 750)
                {
                    size = FishSize.Medium;
                    if (weight >= (1000 - Convert.ToInt32(level * 2)))
                    {
                        weight = SRandom.Next(10, 2001) + Convert.ToInt32(level * 5);
                    }

                    if (fish.Rarity == FishRarity.Legendary || fish.Rarity == FishRarity.T2Legendary || fish.Rarity == FishRarity.T3Legendary || fish.Rarity == FishRarity.T4Legendary)
                    {
                        weight = SRandom.Next(2000 + Convert.ToInt32(level * 20), 40001);
                    }
                    double w = Convert.ToDouble(weight);
                    if (weight >= 1000)
                    {
                        xp = Convert.ToUInt64(Math.Round((xp * w / 1000), 0, MidpointRounding.ToEven));
                        if (fish.Rarity == FishRarity.Legendary || fish.Rarity == FishRarity.T2Legendary || fish.Rarity == FishRarity.T3Legendary || fish.Rarity == FishRarity.T4Legendary)
                        {
                            if (xp < 100)
                            {
                                xp = 100;
                            }
                        }
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
                        int times = 0;
                        if (user.TXp >= user.Xp)
                        {
                            while (user.TXp >= user.Xp)
                            {
                                user.Lvl += 1;
                                times += 1;
                                lvlXp = 50;
                                for (ulong i = 0; i < user.Lvl; i++)
                                {
                                    if (i <= user.Lvl)
                                    {
                                        lvlXp += Convert.ToUInt64(Math.Round((lvlXp * 0.05d + 50d), 0, MidpointRounding.ToEven));
                                    }
                                }
                                user.Xp = lvlXp;
                            }
                            toNextLvl = user.Xp - user.TXp;
                            level = user.Lvl;
                            if (times > 1)
                            {
                                lvlUp = $"**You leveled up {times} times!** You are now **Level {level}.**";
                            }
                            else
                            {
                                lvlUp = $"**You leveled up!** You are now **Level {level}**";
                            }
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
        [Command("checkrod"),Summary("Displays what fishing rods you can use, as well as your currently equipped fishing rod")]
        public async Task CheckRod()
        {
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                var user = Database.Fishing.FirstOrDefault(x => x.Id == Context.User.Id);
                await Context.Channel.SendMessageAsync($"You have unlocked fishing rods up to **T{user.RodOwned+1}**\nYou have currently equipped a **T{user.RodUsed+1}** rod");
            }
        }
        [Command("setrod"),Summary("Set your fishing rod to the desired tier (for example: 'setrod 1' to set to default rod)")]
        public async Task SetRod(byte tier)
        {
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                var user = Database.Fishing.FirstOrDefault(x => x.Id == Context.User.Id);
                if (tier - 1 <= user.RodOwned)
                {
                    user.RodUsed = Convert.ToByte(tier - 1);
                    string rodtype = "";
                    if (tier == 1)
                    {
                        rodtype = "Basic";
                    }
                    else if (tier == 2)
                    {
                        rodtype = "Reinforced";
                    }
                    else if (tier == 3)
                    {
                        rodtype = "Spectral";
                    }
                    else if (tier == 4)
                    {
                        rodtype = "Cosmic";
                    }
                    else
                    {
                        rodtype = "Currently unobtainable";
                    }
                    await Context.Channel.SendMessageAsync($"You are now using a **{rodtype} (T{tier})** rod");
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"You don't have that rod. You own rods up to **T{user.RodOwned+1}**");
                }
                await Database.SaveChangesAsync().ConfigureAwait(false);

            }
        }
        [Command("inventory"), Alias("inv", "fishinv"), Summary("Shows the fish you have currently. Might show other things in the distant future.")]
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
                    if (entry.Value.Count() > 0)
                    {
                        if (entry.Value[0] > 0)
                        {
                            small.Add(entry.Key, entry.Value[0]);
                        }
                    }
                    if (entry.Value.Count() > 1)
                    {
                        if (entry.Value[1] > 0)
                        {
                            med.Add(entry.Key, entry.Value[1]);
                        }
                    }
                    if (entry.Value.Count() > 2)
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

                if (small.Any())
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

                if (med.Any())
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

                if (large.Any())
                {
                    EmbedBuilder embed = new EmbedBuilder
                    {
                        Title = "large mateys"
                    };

                    foreach (var entry in large)
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
            }
            else
            {
                await Context.Channel.SendMessageAsync("Go fish nigger").ConfigureAwait(false);
            }
        }
        [Command("tradebuy", RunMode = RunMode.Async), Summary("")]
        public async Task TradingBuy(int amount, string itemtype, int price, [Remainder] string item)
        {
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                var KehUser = Database.Users.FirstOrDefault(x => x.Id == Context.User.Id);
                if (itemtype.ToLowerInvariant() == "fish")
                {
                    FishSize size = 0;
                    FishSpecies species = 0;
                    List<Fish> fishes = Fishing.GetFishList();

                    if (item.ToLowerInvariant().Contains("large"))
                    {
                        size = FishSize.Large;
                    }
                    else if (item.ToLowerInvariant().Contains("medium"))
                    {
                        size = FishSize.Medium;
                    }
                    else if (item.ToLowerInvariant().Contains("small"))
                    {
                        size = FishSize.Small;
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention} Your size is not up to my standards.");
                        return;
                    }

                    if (fishes.Any(z => item.ToLowerInvariant().Contains(z.Name.ToLowerInvariant())))
                    {
                        species = fishes.FirstOrDefault(z => item.ToLowerInvariant().Contains(z.Name.ToLowerInvariant())).Id;
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention} The goo pool contains no such fish.");
                        return;
                    }

                    if (Database.StoreFronts.Any(x => x.StoreItemType == StoreItemType.Fish))
                    {
                        var stores = Database.StoreFronts.Where(x => x.StoreItemType == StoreItemType.Fish).ToList();

                        stores.Shuffle();

                        var store = stores.FirstOrDefault();

                        if (store.Items.Any(x => x.Item.ToLowerInvariant() == item.ToLowerInvariant()))
                        {
                            var itm = store.Items.FirstOrDefault(x => x.Item.ToLowerInvariant() == item.ToLowerInvariant());

                            if (itm.Price * amount <= KehUser.Money)
                            {
                                if (itm.Amount >= amount)
                                {
                                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nTrade offer to buy item **{size} {species}** for **{price / 10000d}%**").ConfigureAwait(false);
                                }
                                else
                                {
                                    await ReplyAsync("Whoa slow down there buckaroo, they ain't selling that much, go sit in the corner and think about what you've done").ConfigureAwait(false);
                                }
                            }
                            else
                            {
                                await ReplyAsync("Nigger slow down, you aint got the cash monee to make that purchase smh frfr onjah").ConfigureAwait(false);
                            }
                        }
                    }
                    else
                    {
                        await ReplyAsync("No one is selling, so you can't buy lmao, big stinky");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nInvalid trade type. Come back when the error command is fixed lmaoy").ConfigureAwait(false);
                }
            }
        }
        [Command("tradesell"), Summary("")]
        public async Task TradingSell(int amount, string itemtype, int price, [Remainder] string item)
        {
            string contents = "trade info";
            FishSize size = 0;
            FishSpecies species = 0;
            List<Fish> fishes = Fishing.GetFishList();
            if (itemtype.ToLowerInvariant() == "fish")
            {
                if (item.ToLowerInvariant().Contains("large"))
                {
                    size = FishSize.Large;
                }
                else if (item.ToLowerInvariant().Contains("medium"))
                {
                    size = FishSize.Medium;
                }
                else if (item.ToLowerInvariant().Contains("small"))
                {
                    size = FishSize.Small;
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention} Your size is not up to my standards.");
                    return;
                }
                if (fishes.Any(z => item.ToLowerInvariant().Contains(z.Name.ToLowerInvariant())))
                {
                    species = fishes.FirstOrDefault(z => item.ToLowerInvariant().Contains(z.Name.ToLowerInvariant())).Id;
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention} The goo pool contains no such fish.");
                    return;
                }
                contents += $"\ntype: sell\nitem: {size} {species}\namount: {amount}\nprice: {price / 10000d}%\n";

                await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nTrade offer to sell item **{size} {species}** for **{price / 10000d}%**").ConfigureAwait(false);

                await Context.Channel.SendMessageAsync($"{contents}").ConfigureAwait(false);
            }
            else
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nInvalid trade type. Come back when the error command is fixed lmaoy").ConfigureAwait(false);
            }
        }
        [Command("tradeoffers"), Summary("")]
        public async Task ShowOffers(bool localOffers = true)
        {
            using (var database = new ApplicationDbContextFactory().CreateDbContext())
            {
                StringBuilder message = new StringBuilder();
                if (localOffers)
                {
                    var stores = database.StoreFronts.Where(x => x.UserId == Context.User.Id);

                    foreach(var store in stores)
                    {
                        if(store.Offers.Any())
                        {
                            foreach(var offer in store.Offers)
                            {
                                message.AppendLine($"Offer found in **{store.StoreItemType} Store** from: <@{offer.BuyerId}> for **{store.Items.FirstOrDefault(x => x.InvId == offer.ItemId).Item}** @ **{offer.OfferAmount}**");
                            }
                        }
                        else
                        {
                            message.AppendLine($"**{store.StoreItemType} Store** has no offers currently");
                        }
                    }
                    await Context.Channel.SendMessageAsync($"{message}");
                }
            }
        }


        [Command("balance"), Alias("bal", "money"), Summary("Displays the percentage of the total currency you own")]
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
        [Command("bank"), Summary("Displays the percentage of total currency the bank owns")]
        public async Task BankBalance()
        {
            User user;
            User suser;
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                user = Database.Users.FirstOrDefault(x => x.Id == 0);
                suser = Database.Users.FirstOrDefault(x => x.Id == 1);
            }
            await Context.Channel.SendMessageAsync($"Bank has {user.Money / 10000d}% left\nSkuld can currently sell a maximum of {suser.Money * 64}₩ at 0.0001% = 64₩ exchange rate");
        }
        [Command("bet"), Summary("Gamble %coins in units of 0.0001%")]
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
            if (wager < 0)
            {
                wager = 0;
            }
            int loss = wager;
            if (res1 == 100)
            {
                wager = wager * 4;
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
        [Command("leaderboard"), Alias("top", "lb"), Summary("Shows the top 10 people in regards to % money as well as how much of the money in circulation they own")]
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
                string percentCirculating = $"{Math.Round(((users[i].Money * 100d) / (1000000d - bank.Money - skuld.Money)), 2, MidpointRounding.ToEven)}";
                leaderboardMessage += $"\n**{users[i].Username}** : {percent}% ~ *{percentCirculating}% circulating*";
            }

            await Context.Channel.SendMessageAsync(leaderboardMessage);

        }
        [Command("give"), Summary("Give a user money. Eg. 'give @user [amount]'")]
        public async Task GiveShekel(IGuildUser person, int amount)
        {
            if (amount > 0)
            {
                using (var Database = new ApplicationDbContextFactory().CreateDbContext())
                {
                    var user = Database.Users.FirstOrDefault(x => x.Id == Context.User.Id);
                    var pers = Database.Users.FirstOrDefault(x => x.Id == person.Id);
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
                            if (pers.GrantMoney(Database.Users.FirstOrDefault(x => x.Id == 0), amount) && user.GrantMoney(Database.Users.FirstOrDefault(x => x.Id == 0), -amount))
                            {
                                await Context.Channel.SendMessageAsync($"{Context.User.Mention} **{amount / 10000d}%** has been transferred from your account.");
                            }
                            else
                            {
                                await Context.Channel.SendMessageAsync($"{Context.User.Mention} You can't afford that, go back to the mines.");
                            }
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
        [Command("stats"),Summary("View a user's stats")]
        public async Task FishProfile(IUser otherUser = null)
        {
            using (var database = new ApplicationDbContextFactory().CreateDbContext())
            {
                if (otherUser == null)
                {
                    var user = database.Fishing.FirstOrDefault(x => x.Id == Context.User.Id);
                    var muser = database.Users.FirstOrDefault(x => x.Id == Context.User.Id);
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}'s stats\nFishing level: **{user.Lvl}**\nFishing xp: **{user.TXp}**\nBalance: **{muser.Money/10000d}%**");
                }
                else
                {
                    var user = database.Fishing.FirstOrDefault(x => x.Id == otherUser.Id);
                    var muser = database.Users.FirstOrDefault(x => x.Id == otherUser.Id);
                    await Context.Channel.SendMessageAsync($"{otherUser.Mention}'s stats\nFishing level: **{user.Lvl}**\nFishing xp: **{user.TXp}**\nBalance: **{muser.Money/10000d}%**");
                }
            }
        }
    }
}

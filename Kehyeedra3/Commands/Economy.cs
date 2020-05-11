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

        [Command("mine"), Ratelimit(6, 2, Measure.Minutes), Summary("Mines %coins.")]
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
                long end = 0;
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
                            end = (long)res4 + 3;
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
                                await Context.Channel.SendMessageAsync($"{marks} **+** {bonus}\n{Context.User.Mention} **Lucky strike!** Bonus: {ore} You earned {end.ToYeedraDisplay()}%");
                            }
                            else
                            {
                                await Context.Channel.SendMessageAsync($"{marks} **+** {bonus}\n{Context.User.Mention} **Lucky strike!** Bonus: {res4}, You earned {end.ToYeedraDisplay()}%");
                            }
                        }
                    }
                }

                if (end <= 0)
                {
                    await Context.Channel.SendMessageAsync($"{marks}\n{Context.User.Mention} You have found {ore} {discard}.");
                    return;
                }
                else if(end < 3)
                {
                    await Context.Channel.SendMessageAsync($"{marks}\n{Context.User.Mention} You found {end.ToYeedraDisplay()}% while mining");
                }

                using (var Database = new ApplicationDbContextFactory().CreateDbContext())
                {
                    var user = Database.Users.FirstOrDefault(x => x.Id == Context.User.Id);

                    if (!user.GrantMoney(Database.Users.FirstOrDefault(x => x.Id == 0), end))
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nBank has no money, convince someone to gamble.");
                        return;
                    }

                    await Database.SaveChangesAsync();
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
            int prestige;
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
                prestige = user.Prestige;

                await Database.SaveChangesAsync();
            }



            if (lastfish < time)
            {
                int rari = (SRandom.Next(0, 2001));
                int weigh = SRandom.Next(10, 1501+prestige*500);
                int tierRoll = SRandom.Next(0, 81+prestige*40);
                int dCatchRoll = SRandom.Next(0, 1000+prestige*5);
                int dcatch = 1;
                ulong rarity;
                int weight;

                if (dCatchRoll > 1000)
                {
                    int many = 995;
                    while (many+5 <= dCatchRoll)
                    {
                        dcatch += 1;
                        many += 5;
                    }
                }

                if (level < 100 && prestige == 0)
                {
                    rarity = level * 10 + (ulong)rari;
                    weight = (int)level * 5 + weigh; 
                }
                else
                {
                    rarity = 1000 + (ulong)rari;
                    weight = 500 + weigh;
                }

                Fish fish;

                if (rarity == 777 || (rarity > 2060 && rarity <= 2070) || rarity == 2777)
                {
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
                    if (rarity == 777 || rarity == 2777)
                    {
                        xp = 77+(77*Convert.ToUInt64(rod/2));
                    }
                }
                else if (rarity > 1700)
                {
                    rarity = Convert.ToUInt64(SRandom.Next(1700, 2801));
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
                    else if (rod >= 1 && tierRoll > 20)
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

                if (weight >= (1000 - Convert.ToInt32(level * 2)))
                {
                    weight = SRandom.Next(100, 2001) + Convert.ToInt32(level * 5 + (Convert.ToUInt64(prestige * 500)));
                }
                if (fish.Rarity == FishRarity.Legendary || fish.Rarity == FishRarity.T2Legendary || fish.Rarity == FishRarity.T3Legendary || fish.Rarity == FishRarity.T4Legendary)
                {
                    weight = SRandom.Next(2000 + Convert.ToInt32(level * 20), 40001 + prestige * 10000);
                }

                if (weight >= 1000)
                {
                    
                    if (weight >= 1500)
                    {
                        size = FishSize.Large;
                    }
                    else
                    {
                        size = FishSize.Medium;
                    }
                    
                    double w = Convert.ToDouble(weight);
                    xp = Convert.ToUInt64(Math.Round(xp * w / 1000, 0, MidpointRounding.ToEven))*(ulong)dcatch;

                    if (fish.Rarity == FishRarity.Legendary || fish.Rarity == FishRarity.T2Legendary || fish.Rarity == FishRarity.T3Legendary || fish.Rarity == FishRarity.T4Legendary)
                    {
                        if (xp < 100)
                        {
                            xp = 100;
                        }
                    }
                }
                else
                {
                    size = FishSize.Small;
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
                        amounts[sizeIndex] += dcatch;

                        user.SetInventory(inv);

                        user.TXp += xp;

                        toNextLvl = user.Xp - user.TXp;
                        int times = 0;
                        
                        if (user.TXp >= user.Xp)
                        {
                            ulong leXp = 50;
                            bool leTrig = false;
                            for (ulong i = 0; i < user.Lvl; i++)
                            {
                                if (i <= user.Lvl)
                                {
                                    leXp += Convert.ToUInt64(Math.Round((leXp * 0.05d + 50d), 0, MidpointRounding.ToEven));
                                }
                            }
                            if (leXp != user.Xp)
                            {
                                user.Lvl = 0;
                                user.Xp = 0;
                                leTrig = true;
                            }
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
                            if (leTrig)
                            {
                                lvlUp = $"**Your level was recalculated to match your xp.** You are now **Level {level}**";
                            }
                            else if (times > 1)
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
                    if (dcatch == 1)
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n {fish.Emote} You have caught a {weight / 100d}kg **{fish.Name}**, rarity: {fish.Rarity}\nYou gain **{xp}**xp.\n{lvlUp}");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n {fish.Emote} You have caught **{dcatch}** {weight / 100d}kg **{fish.Name}**, rarity: {fish.Rarity}\nYou gain **{xp}**xp.\n{lvlUp}");
                    }
                    
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYour line snaps. Your disappointment is immeasurable, and your day is ruined.");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention}\narrrrr-right, ye scurby bastard, I know yer eager t' scour the seven seas but ye needs t' wait till the next minute t' pillage the booty'o'the depths, savvy?");
            }

        }
        [Command("checkrod"),Summary("Displays what fishing rods you can use, as well as your currently equipped fishing rod.")]
        public async Task CheckRod()
        {
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                var user = Database.Fishing.FirstOrDefault(x => x.Id == Context.User.Id);
                if (user == null)
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou don't own any fishing rods. Try **fishing**.");
                    return;
                }
                await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou have unlocked fishing rods up to **T{user.RodOwned+1}**\nYou have currently equipped a **T{user.RodUsed+1}** rod");
            }
        }
        [Command("setrod"),Summary("Set your fishing rod to the desired tier (for example: 'setrod 1' to set to default rod.)")]
        public async Task SetRod(byte tier)
        {
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                var user = Database.Fishing.FirstOrDefault(x => x.Id == Context.User.Id);
                if (user == null)
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou don't own any fishing rods. Try **fishing**.");
                    return;
                }
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
        [Command("inventory"), Alias("inv", "fishinv"), Summary("Shows the fish you have currently. Variables: fish tier")]
        public async Task FishInventory(int? tier = null, IGuildUser user = null)
        {
            if (user == null)
            {
                user = Context.User as IGuildUser;
            }

            if (tier == null)
            {
                tier = 1;
            }

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

                List<Fish> fishes = Fishing.GetFishList();

                List<Fish> legfish = fishes.Where(f => (int)f.Rarity == (int)FishRarity.Legendary).ToList();
                List<Fish> rarfish = fishes.Where(f => (int)f.Rarity == (int)FishRarity.Rare).ToList();
                List<Fish> uncfish = fishes.Where(f => (int)f.Rarity == (int)FishRarity.Uncommon).ToList();
                List<Fish> comfish = fishes.Where(f => (int)f.Rarity == (int)FishRarity.Common).ToList();

                if (tier > 1 && tier < 5)
                {
                    switch (tier)
                    {
                        case 2:
                            {
                                legfish = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T2Legendary).ToList();
                                rarfish = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T2Rare).ToList();
                                uncfish = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T2Uncommon).ToList();
                                comfish = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T2Common).ToList();
                            }
                            break;
                        case 3:
                            {
                                legfish = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T3Legendary).ToList();
                                rarfish = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T3Rare).ToList();
                                uncfish = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T3Uncommon).ToList();
                                comfish = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T3Common).ToList();
                            }
                            break;
                        case 4:
                            {
                                legfish = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T4Legendary).ToList();
                                rarfish = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T4Rare).ToList();
                                uncfish = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T4Uncommon).ToList();
                                comfish = fishes.Where(f => (int)f.Rarity == (int)FishRarity.T4Common).ToList();
                            }
                            break;
                    }
                }
                else if (tier < 1 || tier > 4)
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nTier not available.");
                    return;
                }

                string legendary = "";
                string rare = "";
                string uncommon = "";
                string common = "";
                string fishmote = "";
                string fishtext = "";
                foreach (var entry in inv)
                {

                    fishmote = fishes.FirstOrDefault(x => x.Id == entry.Key).Emote;

                    if (!fishmote.Contains("><"))
                    {
                        fishmote += "<:emptyslot:709350723199959101>";
                    }
                    if (fishmote.Contains("missingLeg"))
                    {
                        fishmote = fishes.FirstOrDefault(x => x.Id == entry.Key).Name;
                    }

                    fishtext = $"{fishmote} [ **S**-{entry.Value[0]}  **M**-{entry.Value[1]}  **L**-{entry.Value[2]} ]\n";

                    if (legfish.Any( f => f.Id == entry.Key))
                    {
                        legendary += $"{fishtext}";
                    }
                    if (rarfish.Any(f => f.Id == entry.Key))
                    {
                        rare += $"{fishtext}";
                    }
                    if (uncfish.Any(f => f.Id == entry.Key))
                    {
                        uncommon += $"{fishtext}";
                    }
                    if (comfish.Any(f => f.Id == entry.Key))
                    {
                        common += $"{fishtext}";
                    }
                }
                string locker = $"";
                if (legendary != "")
                {
                    locker += $"{legendary}\n";
                }
                if (rare != "")
                {
                    locker += $"{rare}\n";
                }
                if (uncommon != "")
                {
                    locker += $"{uncommon}\n";
                }
                locker += $"{common}";
                if (locker == "")
                {
                    await Context.Channel.SendMessageAsync($"there be nothin' in this locker, cap'n");
                    return;
                }
                if (user.Id != Context.User.Id)
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n{user.Username}'s inventory\n{locker}");
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n{locker}");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("Go fish nigger").ConfigureAwait(false);
            }
        }
        [Command("tradebuy", RunMode = RunMode.Async), Summary("Unfinished command")]
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
                                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nTrade offer to buy item **{size} {species}** for **{((long)price).ToYeedraDisplay()}%**").ConfigureAwait(false);
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
        [Command("tradesell"), Summary("Unfinished command")]
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
                contents += $"\ntype: sell\nitem: {size} {species}\namount: {amount}\nprice: {((long)price).ToYeedraDisplay()}%\n";

                await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nTrade offer to sell item **{size} {species}** for **{((long)price).ToYeedraDisplay()}%**").ConfigureAwait(false);

                await Context.Channel.SendMessageAsync($"{contents}").ConfigureAwait(false);
            }
            else
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nInvalid trade type. Come back when the error command is fixed lmaoy").ConfigureAwait(false);
            }
        }
        [Command("tradeoffers"), Summary("Unfinished command")]
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
        [Command("balance"), Alias("bal", "money"), Summary("Displays the percentage of the total currency you own.")]
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
                await Context.Channel.SendMessageAsync($"{Context.User.Mention} You own {user.Money.ToYeedraDisplay()}%\nWhich is ~{Math.Round(((user.Money * 100d) / (1000000d - buser.Money - suser.Money)), 2, MidpointRounding.ToEven)}% of the money in circulation");
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
                await Context.Channel.SendMessageAsync($"{otherUser.Mention} owns {user.Money.ToYeedraDisplay()}%\nWhich is ~{Math.Round(((user.Money * 100d) / (1000000d - buser.Money - suser.Money)), 2, MidpointRounding.ToEven)}% of the money in circulation");
            }
        }
        [Command("bank"), Summary("Displays the percentage of total currency the bank owns.")]
        public async Task BankBalance()
        {
            User user;
            User suser;
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                user = Database.Users.FirstOrDefault(x => x.Id == 0);
                suser = Database.Users.FirstOrDefault(x => x.Id == 1);
            }
            await Context.Channel.SendMessageAsync($"Bank has {(suser.Money + user.Money).ToYeedraDisplay()}% left"/*\nSkuld can currently sell a maximum of {suser.Money * 64}₩ at 0.0001% = 64₩ exchange rate*/);
        }
        [Command("bet"), Summary("Gamble %coins in units of 0.0001%.")]
        public async Task Gamble(int wager)
        {
            int res1 = SRandom.Next(0, 101);
            int res2 = SRandom.Next(0, 101);
            int loss = wager;

            if (res1 > res2)
            {
                wager += wager;
            }
            else if (res1 < res2)
            {
                loss += wager;
            }

            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                var user = Database.Users.FirstOrDefault(x => x.Id == Context.User.Id);
                var buser = Database.Users.FirstOrDefault(x => x.Id == 0);
                if (user == null || user.Money < loss)
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n You can't afford that, go back to the mines.");
                    return;
                }
                else
                {
                    if (buser.Money > 100)
                    {
                        if (!user.GrantMoney(Database.Users.FirstOrDefault(x => x.Id == 0), wager - loss))
                        {
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nBank has no money, gamble more and lose please.");
                            return;
                        }
                        await Database.SaveChangesAsync();
                        string result = "";
                        if ((wager - loss) > 0)
                        {
                            result = $"Rolled: **{res1}** against **{res2}**\nResult: +{((long)(wager - loss)).ToYeedraDisplay()}%\nBalance: {user.Money.ToYeedraDisplay()}%";
                            await ReplyAsync($"{Context.User.Mention}\n{result}");
                        }
                        if ((wager - loss) < 0)
                        {
                            result = $"Rolled: **{res1}** against **{res2}**\nResult: {((long)(wager - loss)).ToYeedraDisplay()}%\nBalance: {user.Money.ToYeedraDisplay()}%";
                            await ReplyAsync($"{Context.User.Mention}\n{result}");
                        }
                    }
                    else
                    {
                        await ReplyAsync($"{Context.User.Mention}\nHey, stop that.");
                    }
                }
            }
        }
        [Command("leaderboard"), Alias("top", "lb"), Summary("Shows the top 10 people in a leaderboard, currently available leaderboards: 'f, fish', 'm, money'.")]
        public async Task Leaderboard(string type = null)
        {
            if (type == null)
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nPlease specify the leaderboard you want to view (fish or f for fish, m or money for money)");
            }
            else if (type == "m" || type == "money")
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

                int placing = 0;
                string leaderboardMessage = "**Top Ten Most Jewish Users**:";
                for (int i = 0; i < 10; i++)
                {
                    placing += 1;
                    string percent = $"{users[i].Money.ToYeedraDisplay()}";
                    string percentCirculating = $"{Math.Round(((users[i].Money * 100d) / (1000000d - bank.Money - skuld.Money)), 2, MidpointRounding.ToEven)}";
                    leaderboardMessage += $"\n**#{placing} : {users[i].Username}**\n{percent}% ~ *{percentCirculating}% circulating*";
                }
                await Context.Channel.SendMessageAsync(leaderboardMessage);
            }
            else if (type == "f" || type == "fish")
            {
                List<Fishing> users;
                using (var Database = new ApplicationDbContextFactory().CreateDbContext())
                {
                    users = Database.Fishing.OrderByDescending(user => user.TXp).ToList();
                    string leaderboardMessage = "**Top Ten Smelliest Fishermen**:";
                    int placing = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        placing += 1;
                        var user = Database.Users.FirstOrDefault(x => x.Id == users[i].Id);
                        string xp = $"{users[i].TXp}";
                        string level = $"{users[i].Lvl}";
                        if (users[i].Prestige > 0)
                        {
                            level += $" + {users[i].Prestige}P";
                        }
                        leaderboardMessage += $"\n**#{placing} : {user.Username}** Lvl : **{level}**\n*{xp}xp*";
                    }
                    await Context.Channel.SendMessageAsync(leaderboardMessage);
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nCan't find the leaderboard you were looking for. \nPlease type fish or f for fish leaderboard, m or money for money leaderboard.");
            }

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
                                await Context.Channel.SendMessageAsync($"{Context.User.Mention} **{((long)amount).ToYeedraDisplay()}%** has been transferred from your account.");
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
        public async Task StatProfile(IUser otherUser = null)
        {
            Dictionary<FishSpecies, int[]> inv = new Dictionary<FishSpecies, int[]>();
            Dictionary<FishSpecies, int> small = new Dictionary<FishSpecies, int>();
            Dictionary<FishSpecies, int> med = new Dictionary<FishSpecies, int>();
            Dictionary<FishSpecies, int> large = new Dictionary<FishSpecies, int>();
            Fishing feeshUser;

            int scount = 0;
            int mcount = 0;
            int lcount = 0;

            using (var database = new ApplicationDbContextFactory().CreateDbContext())
            {
                if (otherUser == null)
                {
                    feeshUser = database.Fishing.FirstOrDefault(x => x.Id == Context.User.Id);
                }
                else
                {
                    feeshUser = database.Fishing.FirstOrDefault(x => x.Id == otherUser.Id);
                }

                if (feeshUser == null)
                {
                    if (otherUser != null)
                    {
                        feeshUser = new Fishing
                        {
                            Id = otherUser.Id
                        };
                    }
                    else
                    {
                        feeshUser = new Fishing
                        {
                            Id = Context.User.Id
                        };
                    }
                    database.Fishing.Add(feeshUser);
                    await database.SaveChangesAsync();
                    if (otherUser != null)
                    {
                        await Context.Channel.SendMessageAsync($"**{otherUser.Username}** Was added to the database.");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou were added to database.");
                    }
                    return;
                }
                else
                {
                    inv = feeshUser.GetInventory();
                }

                foreach (var entry in inv)
                {
                    if (entry.Value.Count() > 0)
                    {
                        if (entry.Value[0] > 0)
                        {
                            scount += entry.Value[0];
                        }
                    }
                    if (entry.Value.Count() > 1)
                    {
                        if (entry.Value[1] > 0)
                        {
                            mcount += entry.Value[1];
                        }
                    }
                    if (entry.Value.Count() > 2)
                    {
                        if (entry.Value[2] > 0)
                        {
                            lcount += entry.Value[2];
                        }
                    }
                }

                if (otherUser == null)
                {
                    var user = database.Fishing.FirstOrDefault(x => x.Id == Context.User.Id);
                    var muser = database.Users.FirstOrDefault(x => x.Id == Context.User.Id);
                    string pres = "";
                    double cawe = 0;
                    if (user.Prestige > 0)
                    {
                        pres = $" +{user.Prestige}P";
                        cawe = 500;
                    }
                    else
                    {
                        cawe = user.Lvl * 5d + 10d;
                    }
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}'s stats\nFishing level: **{user.Lvl}{pres}**\nMax catch weight: **{(user.Lvl * 5 + 2000d + user.Prestige*500d) / 100}kg**\nMin catch weight: **{cawe /100}kg**\n" +
                        $"Fishing xp: **{user.TXp}**\nTotal fish: **{scount + mcount + lcount}** *(Large: {lcount} Medium: {mcount} Small: {scount})*\nBalance: **{muser.Money.ToYeedraDisplay()}%**");
                }
                else
                {
                    var user = database.Fishing.FirstOrDefault(x => x.Id == otherUser.Id);
                    var muser = database.Users.FirstOrDefault(x => x.Id == otherUser.Id);
                    string pres = "";
                    double cawe = 0;
                    if (user.Prestige > 0)
                    {
                        pres = $" +{user.Prestige}P";
                        cawe = 510;
                    }
                    else
                    {
                        cawe = user.Lvl * 5d + 10d;
                    }
                    await Context.Channel.SendMessageAsync($"{otherUser.Mention}'s stats\nFishing level: **{user.Lvl}{pres}**+P{user.Prestige}\nMax catch weight: **{(user.Lvl * 5 + 2000d + user.Prestige*500d) / 100}kg**\nMin catch weight: **{cawe / 100}kg**\n" +
                        $"Fishing xp: **{user.TXp}**\nTotal fish: **{scount + mcount + lcount}** *(Large: {lcount} Medium: {mcount} Small: {scount}*)\nBalance: **{muser.Money.ToYeedraDisplay()}%**");
                }
            }
        }
        [Command("prestige",RunMode = RunMode.Async),Summary("Sacrifice a load of XP for benefits and growth potential.")]
        public async Task PrestigeCommand()
        {
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                var user = Database.Fishing.FirstOrDefault(x => x.Id == Context.User.Id);
                if (user == null)
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYour first prestige is available at **Lvl 100** in **130503xp**.");
                    return;
                }
                else
                {
                    ulong presXp = 50;
                    if (user.Prestige > 0 || user.TXp >= 130503)
                    {
                        for (ulong i = 1; i < 100 + (ulong)user.Prestige*5; i++)
                        {
                            if (i <= 100 + (ulong)user.Prestige*5)
                            {
                                presXp += Convert.ToUInt64(Math.Round((presXp * 0.05d + 50d), 0, MidpointRounding.ToEven));
                            }
                        };
                        if (user.TXp >= presXp)
                        {
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n**Tier {user.Prestige+1}** is available! Type **confirm** to buy it for **{presXp}xp**.");
                            var message = await NextMessageAsync();
                            if (message.Content.ToLowerInvariant() == "confirm")
                            {
                                user.Prestige++;
                                user.TXp -= presXp;
                                user.Xp = 0;
                                user.Lvl = 0;
                                ulong lvlXp = 0;
                                while (user.TXp >= user.Xp)
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
                                }
                                await Database.SaveChangesAsync();
                                await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou've successfully upgraded to **P{user.Prestige}**.\nReadjusting level.");
                            }
                            else
                            {
                                await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nCome back when you change your mind.");
                            }
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYour next prestige is available at **Lvl {100+user.Prestige*5}** in **{presXp - user.TXp}xp**.");
                        }
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYour first prestige is available at **Lvl 100** in **{130503-user.TXp}xp**.");
                    }
                }
            }
        }
        [Command("xptolevel"),Alias("tolv", "xpto"),Summary("Displays how much xp you need to reach the given level.")]
        public async Task XpToNextLevl(ulong lvl, string xp = null)
        {
            ulong lvlXp = 50;
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                if (lvl > 1 && lvl <= 200 && xp == null)
                {
                    for (ulong i = 1; i < lvl; i++)
                    {
                        if (i <= lvl)
                        {
                            lvlXp += Convert.ToUInt64(Math.Round((lvlXp * 0.05d + 50d), 0, MidpointRounding.ToEven));
                        }
                    };
                }
                else if (lvl == 1)
                {
                    lvlXp = 50;
                }
                else if (lvl < 1)
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nThat's not really possible?");
                    return;
                }
                else if (lvl > 200 && xp == null)
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n**Lvl 200** is the maximum lvl");
                    return;
                }
                
                var user = Database.Fishing.FirstOrDefault(x => x.Id == Context.User.Id);
                if (xp != null && xp == "xp")
                {
                    if (user == null)
                    {
                        {
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n???");
                        }
                    }
                    else
                    {
                        if (user.TXp >= lvl)
                        {
                            await Context.Channel.SendMessageAsync($"XP since you reached **{lvl}xp : {user.TXp - lvl}**" +
                                                                   $"\nCurrent XP : **{user.TXp}**");
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync($"XP left until **{lvl}xp : {lvl - user.TXp}**" +
                                                                   $"\nCurrent XP : **{user.TXp}**" +
                                                                   $"\nProgress to goal : **~{Math.Round(((user.TXp * 100d) / lvl), 0, MidpointRounding.ToEven)}%**");
                        }
                    }
                }
                else
                {
                    if (user == null)
                    {
                        {
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nXP required for **Lvl {lvl} : {lvlXp}**");
                        }
                    }
                    else
                    {
                        if (user.Lvl >= lvl)
                        {
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nXP required for **Lvl {lvl} : {lvlXp}**" +
                                                                   $"\nXP since you reached **Lvl {lvl} : {user.TXp - lvlXp}**" +
                                                                   $"\nCurrent XP : **{user.TXp}**");
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nXP required for **Lvl {lvl} : {lvlXp}**" +
                                                                   $"\nXP left until **Lvl {lvl} : {lvlXp - user.TXp}**" +
                                                                   $"\nCurrent XP : **{user.TXp}**" +
                                                                   $"\nProgress to goal : **~{Math.Round(((user.TXp * 100d) / lvlXp), 0, MidpointRounding.ToEven)}%**");
                        }
                    }
                }
            }
        }
    }
}

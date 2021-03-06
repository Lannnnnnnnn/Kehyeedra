﻿using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Kehyeedra3.Preconditions;
using Kehyeedra3.Services.Models;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Enumeration;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Kehyeedra3.Commands
{
    public class Interactive : InteractiveBase<SocketCommandContext>
    {
        readonly string[] attacks = new string[]
        {
            "",
            ""
        };

        [Command("battlefish", RunMode = RunMode.Async),Alias("bf"),Summary("Type **bf help** or **bf h** for help with this command.")]
        public async Task BattleFish(string option = null, [Remainder]string sec = null)
        {
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                var user = Database.Users.FirstOrDefault(x => x.Id == Context.User.Id);
                var userfish = Database.Battlefish.Where(x => x.UserId == Context.User.Id);

                int attb = 0;
                int defb = 0;
                int hpb = 0;
                int apb = 0;
                int dgb = 0;

                if (!userfish.Any() && option == null)
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou don't have any battlefish.");
                    return;
                }
                else
                {
                    var fish = userfish.FirstOrDefault(x => x.FishType == user.CurrentBattlefish);
                    if (fish == null)
                    {
                        fish = userfish.FirstOrDefault();
                    }
                    switch (fish.FishType)
                    {
                        case Services.Models.BattleFish.Herring:
                            {
                                attb = 9;
                                defb = 9;
                                hpb = 9;
                                apb = 3;
                                dgb = 2;
                            }
                            break;
                        case Services.Models.BattleFish.Birgus:
                            {
                                attb = 10;
                                defb = 6;
                                hpb = 5;
                                apb = 9;
                                dgb = 4;
                            }
                            break;
                        case Services.Models.BattleFish.Abama:
                            {
                                attb = 4;
                                defb = 3;
                                hpb = 5;
                                apb = 18;
                                dgb = 8;
                            }
                            break;
                        case Services.Models.BattleFish.Pistashrimp:
                            {
                                attb = 15;
                                defb = 4;
                                hpb = 5;
                                apb = 6;
                                dgb = 6;
                            }
                            break;
                    }
                }


                string opt = "";

                BattleFish type = 0;
                if (option != null)
                {
                    opt = option.ToLowerInvariant();
                }
                
                string bfishlist = "";

                if (opt == "change" || opt == "c")
                {
                    string bfishlistname = "";
                    foreach (var fesh in userfish)
                    {
                        string prefix = "Hatchling";
                        string suffix = "";

                        if (fesh.Lvl >= 15)
                        {
                            prefix = "Young";
                        }
                        if (fesh.Lvl >= 30)
                        {
                            prefix = "Adolescent";
                        }
                        if (fesh.Lvl >= 50)
                        {
                            prefix = "Adult";
                        }

                        switch ((int)fesh.FishType)
                        {
                            case 1:
                                {
                                    if (fesh.Lvl >= 100)
                                    {
                                        bfishlistname += $"ton";
                                        suffix = $"Authentic Masculine";
                                    }
                                    bfishlistname = $"{prefix} Herring{suffix}";
                                }
                                break;
                            case 2:
                                {
                                    if (fesh.Lvl >= 100)
                                    {
                                        prefix = $"Great Sage";
                                    }
                                    bfishlistname = $"{prefix} Birgus";
                                }
                                break;
                            case 3:
                                {
                                    if (fesh.Lvl >= 100)
                                    {
                                        prefix = $"President";
                                    }
                                    bfishlistname = $"{prefix} Abama";
                                }
                                break;
                            case 4:
                                {
                                    if (fesh.Lvl >= 100)
                                    {
                                        suffix += $" XTREME";
                                        prefix = $"Hardboiled";
                                    }
                                    bfishlistname = $"{prefix} Pistashrimp{suffix}";
                                }
                                break;
                        }
                        bfishlist += $"{(byte)fesh.FishType} : LVL {fesh.Lvl} {bfishlistname}\n";
                        
                    }

                }

                if (option == null)
                {
                    var fish = userfish.FirstOrDefault(x => x.FishType == user.CurrentBattlefish);
                    if (fish == null)
                    {
                        fish = userfish.FirstOrDefault();
                    }

                    StringBuilder message = new StringBuilder($"{Context.User.Mention}\n");

                    string species = $"";
                    string attacks = $"";

                    double lvm = 20;
                    double lvmhp = 100;
                    int lvdf = 5;

                    for (int i = 0; i < fish.Lvl; i++)
                    {
                        lvm += Math.Round((Convert.ToDouble(lvm) * 0.01d), 0, MidpointRounding.ToEven) + 5;
                        lvmhp += Math.Round((Convert.ToDouble(lvmhp) * 0.01d), 0, MidpointRounding.ToEven) + 10;
                    }
                    int lvlm = Convert.ToInt32(lvm) / 10;
                    int lvlmhp = Convert.ToInt32(lvmhp) / 10;

                    int att = lvlm * attb;
                    int def = lvdf * defb;
                    int hp = lvlmhp * hpb;
                    int ap = lvlmhp * apb;
                    int dg = lvlm * dgb;                    

                    string prefix = "Hatchling";

                    if (fish.Lvl >= 15)
                    {
                        prefix = "Young";
                    }
                    if (fish.Lvl >= 30)
                    {
                        prefix = "Adolescent";
                    }
                    if (fish.Lvl >= 50)
                    {
                        prefix = "Adult";
                    }
                    switch ((int)fish.FishType)
                    {
                        case 0:
                            {
                                await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou don't have a battlefish.");
                            }
                            return;
                        case 1:
                            {
                                species = "Herring";
                                if (fish.Lvl >= 100)
                                {
                                    species += $"ton";
                                    prefix = $"Authentic Masculine";
                                }

                                attacks += $"1 : **Slap** - {species} slaps the opponent.";
                                if (fish.Lvl >= 5)
                                {
                                    attacks += $"\n2 : **Kick** - {species} kicks the opponent with his muscular fin.";
                                }
                                if (fish.Lvl >= 15)
                                {
                                    attacks += $"\n3 : **Flex** - {species} flexes his fin muscles, stunning the opponent.";
                                }
                                if (fish.Lvl >= 30)
                                {
                                    attacks += $"\n4 : **Restoration Wand** - {species} spins his wand to recover some health.";
                                }
                                if (fish.Lvl >= 50)
                                {
                                    attacks += $"\n5 : **Fairy Nightmare** - {species} executes a devastating ultimate attack.";
                                }

                            }
                            break;
                        case 2:
                            {
                                species = "Birgus";
                                if (fish.Lvl >= 100)
                                {
                                    species += $"";
                                    prefix = $"Great Sage";
                                }

                                attacks += $"1 : **Staff Slam** - {species} hits the enemy with its staff.";
                                if (fish.Lvl >= 5)
                                {
                                    attacks += $"\n2 : **Chitin Shards** - {species} summons a blast of magical shards at your opponent.";
                                }
                                if (fish.Lvl >= 15)
                                {
                                    attacks += $"\n3 : **Iron Shell** - {species} materializes a hard shell, increasing defense.";
                                }
                                if (fish.Lvl >= 30)
                                {
                                    attacks += $"\n4 : **Siphon Gaze** - {species} absorbs the enemy's life force with an enigmatic gaze.";
                                }
                                if (fish.Lvl >= 50)
                                {
                                    attacks += $"\n5 : **Ecletic Rift** - {species} summons portals to alternate dimensions to call forth an army of raving crabs.";
                                }

                            }
                            break;
                        case 3:
                            {
                                species = "Abama";
                                if (fish.Lvl >= 100)
                                {
                                    species += $"";
                                    prefix = $"President";
                                }

                                attacks += $"1 : **Tentacle Slap** - {species} slaps the opponent with its tentacle.";
                                if (fish.Lvl >= 5)
                                {
                                    attacks += $"\n2 : **Squirt Ink** - {species} shoots ink at the opponent, blinding them temporarily.";
                                }
                                if (fish.Lvl >= 15)
                                {
                                    attacks += $"\n3 : **Bind** - {species} holds down the opponent with its tentacles.";
                                }
                                if (fish.Lvl >= 30)
                                {
                                    attacks += $"\n4 : **Metabolism** - {species} increases metabolism to instantly remove all ailments.";
                                }
                                if (fish.Lvl >= 50)
                                {
                                    attacks += $"\n5 : **Ancestral Wrath** - {species} calls into the depths to unleash its true potential.";
                                }

                            }
                            break;
                        case 4:
                            {
                                species = "Pistashrimp";
                                if (fish.Lvl >= 100)
                                {
                                    species += $" XTREME";
                                    prefix = $"Hardboiled";
                                }

                                attacks += $"1 : **Pistolwhip** - {species} swings at the enemy with the back of its pistol.";
                                if (fish.Lvl >= 5)
                                {
                                    attacks += $"\n2 : **Fire** - {species} shoots a round from its pistol.";
                                }
                                if (fish.Lvl >= 15)
                                {
                                    attacks += $"\n3 : **Fedora Tip** - {species} tips its charming headwear, seducing the enemy.";
                                }
                                if (fish.Lvl >= 30)
                                {
                                    attacks += $"\n4 : **Water Jet** - {species} tosses aside its silly weapon and snaps with its real claw.";
                                }
                                if (fish.Lvl >= 50)
                                {
                                    attacks += $"\n5 : **Dual Jet** - {species} vaporizes the surrounding with its machine gun claws.";
                                }

                            }
                            break;
                        }
                        message.AppendLine($"LVL {fish.Lvl} **{prefix} {species}**\nName: **{fish.Name}**\nStats: **ATK : {att} DEF : {def}% HP : {hp} AP : {ap}**\nActions:\n{attacks}\n\n");
                    
                    await Context.Channel.SendMessageAsync(message.ToString());
                }
                else if (option == "name" && sec != null || option == "n" && sec != null)
                {
                    var fish = userfish.FirstOrDefault(x => x.FishType == user.CurrentBattlefish);
                    if (fish == null)
                    {
                        fish = userfish.FirstOrDefault();
                    }

                    if (fish == null)
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou can't name a fish you don't own!");
                        return;
                    }

                    if (sec.Length > 16)
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nThe maximum name length is 16 characters. \nStop jassing.");
                        return;
                    }
                    else if (fish.FishType != 0)
                    {
                        fish.Name = sec;
                        await Database.SaveChangesAsync();
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou have changed the name of your battlefish to **{sec}**.");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou can't manage a fish if you don't have one.");
                    }

                }
                else if (option == "buy" || option == "b")
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nHere is a list of available battlefish. Please choose one by its respective number. All battlefish cost 0.05%." +
                        $"\n1 : **Herring** - An unarmed melee fighter with well balanced stats. It is not very agile.\nModifiers: **ATK 9, DEF 9, HP 9, AP 2**" +
                        $"\n2 : **Birgus** - A crustacean mage with a focus on AP and DMG. It is slightly agile.\nModifiers: **ATK 10, DEF 6, HP 5, AP 9**" +
                        $"\n3 : **Abama** - A cephalopod rogue with a heavy focus on debuffs. It is very agile.\nModifiers: **ATK 4, DEF 3, HP 5, AP 18**" +
                        $"\n4 : **Pistashrimp** - A crustacean ranger with a heavy focus on DPS. It is somewhat agile.\nModifiers: **ATK 15, DEF 4, HP 5, AP 6**");
                    var reply = await NextMessageAsync();
                    BattleFish rep = (BattleFish)byte.Parse(reply.Content);
                    if (userfish.Any(x => x.FishType == rep))
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou already have one of those, please don't neglect it and make it sad.");
                        return;
                    }
                    if ((int)rep > 4 || (int)rep < 1)
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nWhat you're looking for does not exist.");
                        return;
                    }
                    else if (user.Money < 500)
                    {
                        await Context.Channel.SendMessageAsync($"Sorry **{Context.User.Mention}**, I can't give credit.\nCome back when you're a little, ***mmmmm***, richer.\n*You're missing {(500 - user.Money)/10000d}%.*");
                        return;
                    }
                    string species = "";
                    switch ((int)rep)
                    {
                        case 1:
                            {
                                species = "a Herring";
                            }
                            break;
                        case 2:
                            {
                                species = "a Birgus";
                            }
                            break;
                        case 3:
                            {
                                species = "an Abama";
                            }
                            break;
                        case 4:
                            {
                                species = "a Pistashrimp";
                            }
                            break;
                    }
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou want **{species}**? It's yours my friend, as long as you have enough **%**.\n *This cannot be reverted. Type 'confirm' to confirm.*");
                    reply = await NextMessageAsync();
                    if (reply.Content.ToLowerInvariant() == "confirm")
                    {
                        if (!user.GrantMoney(Database.Users.FirstOrDefault(x => x.Id == 0), -500))
                        {
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nBank has no money, convince someone to gamble.");
                            return;
                        }

                        var bfish = new User.BattleFishObject
                        {
                            FishType = rep,
                            UserId = Context.User.Id
                        };

                        Database.Battlefish.Add(bfish);

                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nThank you for your purchase.");
                        await Database.SaveChangesAsync();
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYour purchase is cancelled.");
                    }
                }
                else if (option == "info" || option == "i")
                {
                    string info = "";
                    if (sec.ToLowerInvariant() == "herring")
                    {
                        info = "Equipment type: Enhancing clothing.\nBonus info: Only males of this species exist. I don't know how they reproduce, and I might not want to know.";
                    }
                    else if (sec.ToLowerInvariant() == "birgus")
                    {
                        info = "Equipment type: Staves.\nBonus info: Exceptional Birguses may live for centuries, their powers increasing with age. While breeding, they part with much of their potential power, so few of them actually do so.";
                    }
                    else if (sec.ToLowerInvariant() == "abama")
                    {
                        info = "Equipment type: Enhancing clothing.\nBonus info: The tentacles are not just for show. Being highly agile and stealthy, Abamas are experts at grabbing things unnoticed.";
                    }
                    else if (sec.ToLowerInvariant() == "pistashrimp")
                    {
                        info = "Equipment type: Firearms.\nBonus info: Despite making up only 13% of the underwater wildlife, Pistashrimps account for 100% of the unarmed firearm robberies. Few of them are righteous, but the ones that are make for great detectives.";
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nNot a valid fish.");
                        return;
                    }
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n{info}");
                }
                else if (option == "help" || option == "h")
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}" +
                        $"\n**name or n** to name your fish." +
                        $"\n**buy or b** to buy a fish." +
                        $"\n**info or i** along with **name of bf** to get further info on a battlefish." +
                        $"\n**help or h** to view this." +
                        $"\n**change or c** to change battlefish to any owned fish." +
                        $"\nSpecify **no option** to view your battlefish stats.");
                }
                else if (option == "change" || option == "c")
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nPlease select your battlefish\n{bfishlist}");
                    var fens = await NextMessageAsync();
                    type = (BattleFish)byte.Parse(fens.Content);
                    if (userfish.Any(x => x.FishType == type))
                    {
                        user.CurrentBattlefish = type;
                        await Database.SaveChangesAsync();
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nBattlefish changed.");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou don't own that.");
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nAre you confused? Try **bf help** if you are having trouble with your bf.");
                }

            }
        }

        [Command("gstore", RunMode = RunMode.Async),Alias("gs")]
        public async Task GeneralStore(string input = null)
        {
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                Dictionary<Items, int[]> items = new Dictionary<Items, int[]>();
                Dictionary<FishSpecies, int[]> fishinv = new Dictionary<FishSpecies, int[]>();
                List<User.Item> itemlist = User.ListItems();
                List<Fish> fishes = Fishing.GetFishList();

                User.Item item;
                Fish fish;

                var fuser = Database.Fishing.FirstOrDefault(x => x.Id == Context.User.Id);
                var user = Database.Users.FirstOrDefault(x => x.Id == Context.User.Id);

                if (fuser == null)
                {
                    fuser = new Fishing
                    {
                        Id = user.Id
                    };

                    Database.Fishing.Add(fuser);
                    await Database.SaveChangesAsync();
                }
                else
                {
                    fishinv = fuser.GetInventory();
                }

                if (user.GeneralInventory == null || user.GeneralInventory.Length < 3 )
                {
                    user.GeneralInventory = "{}";
                    await Database.SaveChangesAsync();
                }

                string itemtxt = "";

                foreach (User.Item i in itemlist)
                {
                    itemtxt += $"{(int)i.Id} : {i.Name} for {((long)i.Price).ToYeedraDisplay()}%\n";
                }

                if (input != null)
                {
                    input = input.ToLowerInvariant();
                }

                if (input == null)
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nPlease specify 'b / buy' to buy items, or 's / sell' to sell items.");
                }
                else if (input == "b" || input == "buy")
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nSpecify the item.\n{itemtxt}");
                    var inp = await NextMessageAsync();

                    item = itemlist.FirstOrDefault(i => (int)i.Id == int.Parse(inp.Content));
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nSpecify the amount.");
                    inp = await NextMessageAsync();

                    items = user.GetGenInve();

                    int[] amount = { 0 };
                    
                    if (!items.TryGetValue(item.Id, out amount))
                    {
                        amount = new int[] { 0 };
                        items.Add(item.Id, amount);
                    }

                    if (int.Parse(inp.Content) * item.Price <= user.Money)
                    {
                        amount[0] += int.Parse(inp.Content);

                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nThis will cost you {ulong.Parse(inp.Content) * (ulong)item.Price}.\nType 'ok' to confirm.");
                        inp = await NextMessageAsync();
                        if (inp.Content.ToLowerInvariant() == "ok")
                        {
                            if (!user.GrantMoney(Database.Users.FirstOrDefault(x => x.Id == 0), -(amount[0] * item.Price)))
                            {
                                await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nBank has no money, convince someone to gamble.");
                                return;
                            }
                            user.SetGenInve(items);
                            await Database.SaveChangesAsync();
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nBought {int.Parse(inp.Content)} of {item.Name}.");
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nPlease come back when you feel like spending.");
                        }
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nCome back when you're a little, MMMMMMmmmm, richer.");
                    }
                }
                else if (input == "s" || input == "sell")
                {
                    int value = 0;
                    FishSize size;
                    if (fuser.Inventory.Length < 3)
                    {
                        await Context.Channel.SendMessageAsync($"Your inventory is empty. Try fishing more.");
                        return;
                    }
                    
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nSpecify the tier.\n");
                    var inp = await NextMessageAsync();
                    int tier = int.Parse(inp.Content);
                    
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
                    string fishtext = "";

                    foreach (var entry in fishinv)
                    {
                        fishtext = $"{(int)entry.Key} : {entry.Key} [";
                        if (entry.Value[0] > 0)
                        {
                            fishtext += $" **S**-{entry.Value[0]}";
                        }
                        if (entry.Value[1] > 0)
                        {
                            fishtext += $" **M**-{entry.Value[1]}";
                        }
                        if (entry.Value[2] > 0)
                        {
                            fishtext += $" **L**-{entry.Value[2]}";
                        }
                        fishtext += $" ]\n";

                        if (legfish.Any(f => f.Id == entry.Key))
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
                    
                    string locker = "";
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
                        await Context.Channel.SendMessageAsync($"You don't have anything to sell in this tier.");
                        return;
                    }

                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nSpecify the item.\n\n{locker}");
                    inp = await NextMessageAsync();
                    fish = fishes.FirstOrDefault(i => (int)i.Id == int.Parse(inp.Content));
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nSpecify the size. 0 = small, 1 = medium, 2 = large\n");
                    inp = await NextMessageAsync();
                    size = (FishSize)int.Parse(inp.Content);
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nSpecify the amount.\n");
                    inp = await NextMessageAsync();
                    int amount = int.Parse(inp.Content);
                    int amountcheck = fishinv.FirstOrDefault(f => f.Key == fish.Id).Value[(int)size];
                    if (amountcheck < amount)
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou don't have that many.\n");
                        return;
                    }

                    switch (fish.Rarity)
                    {
                        case FishRarity.Legendary:
                            {
                                value = 700;
                            }
                            break;
                        case FishRarity.Rare:
                            {
                                value = 6 * (int)(size+1);
                            }
                            break;
                        case FishRarity.Uncommon:
                            {
                                value = 2 * (int)(size+1);
                            }
                            break;
                        case FishRarity.Common:
                            {
                                value = (int)(size+1);
                            }
                            break;
                        case FishRarity.T2Legendary:
                            {
                                value = 750;
                            }
                            break;
                        case FishRarity.T2Rare:
                            {
                                value = 12 * (int)(size+1);
                            }
                            break;
                        case FishRarity.T2Uncommon:
                            {
                                value = 4 * (int)(size+1);
                            }
                            break;
                        case FishRarity.T2Common:
                            {
                                value = 2 * (int)(size+1);
                            }
                            break;
                        case FishRarity.T3Legendary:
                            {
                                value = 800;
                            }
                            break;
                        case FishRarity.T3Rare:
                            {
                                value = 18 * (int)(size+1);
                            }
                            break;
                        case FishRarity.T3Uncommon:
                            {
                                value = 6 * (int)(size+1);
                            }
                            break;
                        case FishRarity.T3Common:
                            {
                                value = 3 * (int)(size+1);
                            }
                            break;
                        case FishRarity.T4Legendary:
                            {
                                value = 850;
                            }
                            break;
                        case FishRarity.T4Rare:
                            {
                                value = 32 * (int)(size+1);
                            }
                            break;
                        case FishRarity.T4Uncommon:
                            {
                                value = 8 * (int)(size+1);
                            }
                            break;
                        case FishRarity.T4Common:
                            {
                                value = 4 * (int)(size+1);
                            }
                            break;
                    }
                    value *= amount;

                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou're about to sell **{amount} {fish.Name}** for **{((long)value).ToYeedraDisplay()}**.\nType 'ok' to confirm.");
                    inp = await NextMessageAsync();
                    if (inp.Content.ToLowerInvariant() == "ok")
                    {

                        int[] amounts;
                        if (!fishinv.TryGetValue(fish.Id, out amounts))
                        {
                            amounts = new int[] { 0, 0, 0 };
                            fishinv.Add(fish.Id, amounts);
                        }

                        int sizeIndex = (int)size;
                        amounts[sizeIndex] -= amount;

                        fuser.SetInventory(fishinv);

                        if (!user.GrantMoney(Database.Users.FirstOrDefault(x => x.Id == 0), value))
                        {
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nBank has no money, convince someone to gamble.");
                            return;
                        }
                        await Database.SaveChangesAsync();

                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nThanks, seaman, enjoy your moolah.");
                        
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nI guess I'm starving tonight. :[");
                    }
                }
            }
        }




    }
}

using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Kehyeedra3.Preconditions;
using Kehyeedra3.Services.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
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

        [RequireRolePrecondition(AccessLevel.BotOwner)]
        [Command("battlefish", RunMode = RunMode.Async),Alias("bf"),Summary("Type **bf help** or **bf h** for help with this command.")]
        public async Task BattleFish(string option = null, [Remainder]string sec = null)
        {
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                var user = Database.Users.FirstOrDefault(x => x.Id == Context.User.Id);
                if (user.BattleFish.Any() == false)
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou don't have a battlefish.");
                    return;
                }

                var fish = user.BattleFish.FirstOrDefault();

                string species = $"";
                string attacks = $"";
                int att = 0;
                int def = 0;
                int dg = 0;
                int hp = 0;
                int ap = 0;

                double lvm = 20;
                double lvmhp = 100;

                if (option == null)
                {
                    for (int i = 0; i < fish.Lvl; i++)
                    {
                        lvm += Math.Round((Convert.ToDouble(lvm) * 0.01d), 0, MidpointRounding.ToEven) + 5;
                        lvmhp += Math.Round((Convert.ToDouble(lvmhp) * 0.01d), 0, MidpointRounding.ToEven) + 10;
                    }
                    int lvlm = Convert.ToInt32(lvm) / 10;
                    int lvlmhp = Convert.ToInt32(lvmhp) / 10;
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
                    switch (fish.FishType)
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

                                att = 9 * lvlm;
                                def = 9 * lvlm;
                                hp = 9 * lvlmhp;
                                ap = 3 * lvlmhp;

                                dg = 2 * lvlm;
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

                                att = 10 * lvlm;
                                def = 6 * lvlm;
                                hp = 5 * lvlmhp;
                                ap = 9 * lvlmhp;

                                dg = 4 * lvlm;
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

                                att = 4 * lvlm;
                                def = 3 * lvlm;
                                hp = 5 * lvlmhp;
                                ap = 18 * lvlmhp;

                                dg = 8 * lvlm;
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

                                att = 15 * lvlm;
                                def = 4 * lvlm;
                                hp = 5 * lvlmhp;
                                ap = 6 * lvlmhp;

                                dg = 6 * lvlm;
                            }
                            break;
                    }
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}'s LVL {fish.Lvl} **{prefix} {species}**\nName: **{fish.Name}**\nStats: **ATK : {att} DEF : {def} HP : {hp} AP : {ap}**\nActions:\n{attacks}");

                }
                else if (option == "name" && sec != null|| option == "n" && sec != null)
                {
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
                    byte rep = byte.Parse(reply.Content);
                    if (rep > 4 || rep < 1)
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nWhat you're looking for does not exist.");
                        return;
                    }
                    else if (user.Money < 500)
                    {
                        await Context.Channel.SendMessageAsync($"Sorry **{Context.User.Mention}**, I can't give credit.\nCome back when you're a little, ***mmmmm***, richer.\n*You're missing {(500 - user.Money)/10000d}%.*");
                        return;
                    }
                    switch (rep)
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
                        //create fish here pls let there fish be
                        fish.FishType = rep;
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
                    
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nAre you confused? Try **bf help** if you are having trouble with your bf.");
                }

            }
        }
        [RequireRolePrecondition(AccessLevel.BotOwner)]
        [Command("gstore", RunMode = RunMode.Async),Alias("gs")]
        public async Task GeneralStore(string input = null)
        {
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                var fuser = Database.Fishing.FirstOrDefault(x => x.Id == Context.User.Id);
                var user = Database.Users.FirstOrDefault(x => x.Id == Context.User.Id);
                var inv = fuser.GetInventory();
                if (input != null)
                {
                    input = input.ToLowerInvariant();
                }
                if (input == null)
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nHere's a list of available items.");
                }
                else if (input == "b" || input == "buy")
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nSpecify the item.");
                    var inp = await NextMessageAsync();
                    switch (Convert.ToInt32(inp))
                    {
                        case 1:
                            {
                                
                            }
                            break;
                        case 2:
                            {
                                
                            }
                            break;
                        case 3:
                            {

                            }
                            break;
                        case 4:
                            {

                            }
                            break;
                        case 5:
                            {

                            }
                            break;
                    }

                }
                else if (input == "s" || input == "sell")
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nSpecify the item.");

                    var inp = await NextMessageAsync();

                }
            }
            

        }
        [RequireRolePrecondition(AccessLevel.BotOwner)]
        [Command("setbf")]
        public async Task SetBattleFish(byte type, IUser usar = null)
        {
            string species = "";
            switch (type)
            {
                case 0:
                    {
                        species = "None";
                    }
                    break;
                case 1:
                    {
                        species = "Herring";
                    }
                    break;
                case 2:
                    {
                        species = "Birgus";
                    }
                    break;
                case 3:
                    {
                        species = "Abama";
                    }
                    break;
                case 4:
                    {
                        species = "Pistashrimp";
                    }
                    break;
            }
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                if (usar == null)
                {
                    var user = Database.Users.FirstOrDefault(x => x.Id == Context.User.Id).BattleFish.FirstOrDefault();
                    user.FishType = type;
                    await Context.Channel.SendMessageAsync($"Changed **{Context.User.Username}**'s bf type to {species}.");
                }
                else
                {
                    var user = Database.Users.FirstOrDefault(x => x.Id == usar.Id).BattleFish.FirstOrDefault();
                    user.FishType = type;
                    await Context.Channel.SendMessageAsync($"Changed **{usar.Username}**'s bf type to {species}.");
                }
                await Database.SaveChangesAsync();
            }
        }
        [RequireRolePrecondition(AccessLevel.BotOwner)]
        [Command("setbflv")]
        public async Task SetBattleFishLevel(int lv, IUser usar = null)
        {
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                if (usar == null)
                {
                    var user = Database.Users.FirstOrDefault(x => x.Id == Context.User.Id).BattleFish.FirstOrDefault();
                    user.Lvl = lv;
                    await Context.Channel.SendMessageAsync($"Changed **{Context.User.Username}**'s bf lvl to {lv}.");
                }
                else
                {
                    var user = Database.Users.FirstOrDefault(x => x.Id == usar.Id).BattleFish.FirstOrDefault();
                    user.Lvl = lv;
                    await Context.Channel.SendMessageAsync($"Changed **{usar.Username}**'s bf lvl to {lv}.");
                }
                await Database.SaveChangesAsync();
            }
        }
    }
}

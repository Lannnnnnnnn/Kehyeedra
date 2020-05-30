using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Kehyeedra3.Services.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kehyeedra3.Commands
{
    public class Stuff : InteractiveBase<SocketCommandContext> ///////////////////////////////////////////////
    {

        [Command("delet"), Summary("Posts a delet this image. Can be used on other channels.")]
        public async Task DeletThis(ITextChannel channel = null)
        {
            string imgdirpath = Path.Combine(Environment.CurrentDirectory, "Delet");
            DirectoryInfo imgdir = new DirectoryInfo(imgdirpath);
            var files = imgdir.GetFiles();
            var item = files[Bot._rnd.Next(0, files.Length)];
            if (channel == null)
            {
                await Context.Channel.SendFileAsync(item.FullName);
            }
            else
            {
                await channel.SendFileAsync(item.FullName);
            }
        }

        [Command("ratetrap"), Summary("Rates your or another person's trap potential as a percentage")]
        public async Task RateTrap( string name = null)
        {
            if (name == null)
            {
                Random rando = new Random();
                Random rando1 = new Random();
                int trapRating0 = rando.Next(0, 101);
                if (trapRating0 == 100)
                {
                    int trapRating1 = rando1.Next(0, 1001);
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nI'd say right now you're {trapRating1}% passable");
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nI'd say right now you're {trapRating0}% passable");
                }
            }
            else
            {
                Random rando = new Random();
                Random rando1 = new Random();
                int trapRating0 = rando.Next(0, 101);
                if (trapRating0 == 100)
                {
                    int trapRating1 = rando1.Next(0, 1001);
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nI'd say right now {name} is {trapRating1}% passable");
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nI'd say right now {name} is {trapRating0}% passable");
                }
            }

        }

        [Command("ratertrap"), Summary("ratertrap")]
        public async Task RaterTrap()
        {
            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nPlease do not be like this man http://tinyurl.com/y7lj6nob");
        }
        [Command("8b"), Summary("Gives a prediction like a generic 8ball command that every self respecting discord bot must have")]
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
            await ReplyAsync($"{Context.User.Mention}\n{text}");
        }
        [Command("math"), Summary("It's a calculator, that's what compooter do")]
        public async Task Mathboi([Remainder] string input)
        {
            string result = new DataTable().Compute(input, null).ToString();
            await Context.Channel.SendMessageAsync($"{Context.User.Mention} {input} = {result}");
        }
        [Command("roll"), Summary("Rolls dice. Eg. 'roll d20'")]
        public async Task RollDice([Remainder] string input)
        {
            int dinput = int.Parse(input.Substring(input.IndexOf("d")).Replace("d", ""));
            int output = SRandom.Next(dinput);
            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\n{output+1}");
        }
        [Command("remind"), Summary("Reminds you in a given time. (days, hours, minutes) Eg. 'remind 1 2 3 wash hands' would remind you in 1 day, 2 hours, 3 minutes to wash your hands")]
        public async Task Reminder(ulong d, ulong h, ulong m, [Remainder] string r = null)
        {
            string remin = "";
            string refix = "";
            ulong sen = (d * 86400) + (h * 3600) + (m * 60);
            ulong yeedraStamp = DateTime.UtcNow.ToYeedraStamp();

            if (r == null)
            {
                r = "Fill the text field next time you make a reminder.";
            }

            while (m > 59)
            {
                h += 1;
                m -= 60;
            }
            while (h > 23)
            {
                d += 1;
                h -= 24;
            }

            if (d > 0)
            {
                remin += $" {d} day";
                if (d > 1)
                {
                    remin += $"s";
                }
            }
            if (h > 0)
            {
                remin += $" {h} hour";
                if (h > 1)
                {
                    remin += $"s";
                }
            }
            if (m > 0)
            {
                remin += $" {m} minute";
                if (m > 1)
                {
                    remin += $"s";
                }
            }

            if (d == 0 && h == 0 && m == 0)
            {
                refix += "right now";
            }
            else
            {
                refix += "in";
            }
            if (sen != 0)
            {
                if (sen < 63072000)
                {
                    var reminder = new Reminder
                    {
                        UserId = Context.User.Id,
                        Message = $"{r}",
                        Created = yeedraStamp,
                        Send = sen + yeedraStamp
                    };

                    using (var Database = new ApplicationDbContextFactory().CreateDbContext())
                    {
                        Database.Reminders.Add(reminder);

                        await Database.SaveChangesAsync().ConfigureAwait(false);
                    }
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nAre you sure you need a reminder 2 years in the future..? \nAre you stupid?");
                    return;
                }

                await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nOk, I'll remind you {refix}{remin}.");
            }
            else
            {
                var dmchannel = await Bot._bot.GetUser(Context.User.Id).GetOrCreateDMChannelAsync();
                await dmchannel.SendMessageAsync($"**You literally just told me to DM you:**\n\n{r}");
            }
        }

        [Command("reminders",RunMode = RunMode.Async), Summary("List reminders")]
        public async Task ListReminders(string manage = null)
        {
            ulong d = 0;
            ulong h = 0;
            ulong m = 0;
            string when = "";
            string remin = "";
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                string rlist = "";
                var user = Context.User;
                
                foreach (Reminder reminder in Database.Reminders)
                {
                    ulong yeedraStamp = DateTime.UtcNow.ToYeedraStamp();
                    remin = "";
                    ulong ago = yeedraStamp - reminder.Created;
                    while (ago > 59)
                    {
                        m += 1;
                        ago -= 60;
                    }
                    while (m > 59)
                    {
                        h += 1;
                        m -= 60;
                    }
                    while (h > 23)
                    {
                        d += 1;
                        h -= 24;
                    }

                    if (d > 0)
                    {
                        remin += $" {d} day";
                        if (d > 1)
                        {
                            remin += $"s";
                        }
                    }
                    if (h > 0)
                    {
                        remin += $" {h} hour";
                        if (h > 1)
                        {
                            remin += $"s";
                        }
                    }
                    if (m > 0)
                    {
                        remin += $" {m} minute";
                        if (m > 1)
                        {
                            remin += $"s";
                        }
                    }
                    if (d == 0 && h == 0 && m == 0)
                    {
                        when = " less than a minute ago.";
                    }
                    else
                    {
                        when = $"{remin} ago.";
                    }

                    m = 0;
                    h = 0;
                    d = 0;

                    if (reminder.UserId == Context.User.Id)
                    {
                        rlist += $"Reminder ID: {reminder.Id} From{when} Contents: *{reminder.Message}*\n";
                    }
                }
                if (rlist == "")
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou haven't set any reminders.");
                    return;
                }
                if (manage == null)
                {
                    await Context.Channel.SendMessageAsync(rlist);
                    return;
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nWhich reminder would you like to edit?\n\n{rlist}");
                    var reply = await NextMessageAsync();
                    ulong rep = ulong.Parse(reply.Content);

                    var rem = Database.Reminders.FirstOrDefault(r => r.Id == rep && r.UserId == Context.User.Id);

                    if (rem == null)
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nYou have no such reminder.");
                        return;
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nWhat would you like to do? (edit/remove)");

                        reply = await NextMessageAsync();

                        if (reply.Content == "edit")
                        {
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nInsert new reminder message.");
                            reply = await NextMessageAsync();
                            rem.Message = reply.Content;
                            await Database.SaveChangesAsync();
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nReminder updated.");
                        }
                        else if (reply.Content == "remove")
                        {
                            Database.Reminders.Remove(rem);
                            await Database.SaveChangesAsync();
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nReminder removed.");
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync($"{Context.User.Mention}\nInvalid action.");
                        }
                    }
                }
            }
        }
    }
}

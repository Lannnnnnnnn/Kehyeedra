using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Kehyeedra3.Services.Models;
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
        public async Task DeletThis()
        {
            string imgdirpath = Path.Combine(Environment.CurrentDirectory, "Delet");
            DirectoryInfo imgdir = new DirectoryInfo(imgdirpath);
            var files = imgdir.GetFiles();
            var item = files[Bot._rnd.Next(0, files.Length)];
            await Context.Channel.SendFileAsync(item.FullName);

        }
        [Command("delet"), Summary("Posts a delet this image. Can be used on other channels.")]
        public async Task DeletThis(ITextChannel channel)
        {
            string imgdirpath = Path.Combine(Environment.CurrentDirectory, "Delet");
            DirectoryInfo imgdir = new DirectoryInfo(imgdirpath);
            var files = imgdir.GetFiles();
            var item = files[Bot._rnd.Next(0, files.Length)];
            await channel.SendFileAsync(item.FullName);

        }

        [Command("ratetrap"), Summary("Rates your or another person's trap potential as a percentage")]
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
        [Command("ratetrap"), Summary("Rates your or another person's trap potential as a percentage")]
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
        [Command("ratertrap"), Summary("ratertrap")]
        public async Task RaterTrap()
        {
            await Context.Channel.SendMessageAsync("Please do not be like this man http://tinyurl.com/y7lj6nob");
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
            await ReplyAsync(text + " " + Context.User.Mention);
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
            await Context.Channel.SendMessageAsync("" + output + 1);
        }
        [Command("remind"), Summary("Reminds you in a given time. (days, hours, minutes) Eg. 'remind 1 2 3 wash hands' would remind you in 1 day, 2 hours, 3 minutes to wash your hands")]
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
        [Command("grant"), Summary("Bambi Sit janksie")]
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
        [Command("dab"), Summary("Dabs a person")]
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
}

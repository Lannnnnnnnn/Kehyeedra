using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Threading.Tasks;
using Kehyeedra3.Services.Models;

namespace Kehyeedra3.Services
{
    public class ReminderService
    {
        private static async Task SendReminderAsync(Reminder reminder)
        {
            var dmchannel = await Bot._bot.GetUser(reminder.UserId).GetOrCreateDMChannelAsync();
            if (dmchannel != null)
            {
                ulong m = (reminder.Send - reminder.Created) / 60;
                ulong h = 0;
                ulong d = 0;
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
                string remin = "";
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
                await dmchannel.SendMessageAsync($"**Reminder from{remin} ago:**\n\n''{reminder.Message}''");
            }
        }
        public async Task Tick()
        {
            while (true)
            {
                using (var Database = new ApplicationDbContextFactory().CreateDbContext())
                {
                    var reminders = Database.Reminders.ToList();
                    if(Database.Reminders.Any() && Bot._bot != null && Bot._bot.ConnectionState == Discord.ConnectionState.Connected)
                    {
                        bool hasChanged = false;
                        foreach (var x in reminders)
                        {
                            if (x.Send <= DateTime.UtcNow.ToYeedraStamp())
                            {
                                await SendReminderAsync(x).ConfigureAwait(false);
                                Database.Reminders.Remove(x);
                                hasChanged = true;
                            }
                        }

                        if (hasChanged)
                        {
                            await Database.SaveChangesAsync().ConfigureAwait(false);
                        }
                    }
                }
                await Task.Delay(250);
            }
        }
    }
}

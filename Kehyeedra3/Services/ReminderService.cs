using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kehyeedra3.Services.Models;

namespace Kehyeedra3.Services
{
    public class ReminderService
    {
        private static async Task SendReminderAsync(Reminder reminder)
        {
            var dmchannel = await Bot._bot.GetUser(reminder.UserId).GetOrCreateDMChannelAsync();
            await dmchannel.SendMessageAsync(reminder.Message);
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

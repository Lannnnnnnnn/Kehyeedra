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
            using (var Database = new ApplicationDbContextFactory().CreateDbContext())
            {
                List<Reminder> toRemove = new List<Reminder>();
                while (true)
                {
                    if(Database.Reminders.Any() && Bot._bot != null && Bot._bot.Shards.All(x=>x.ConnectionState == Discord.ConnectionState.Connected))
                    {
                        foreach(var x in Database.Reminders)
                        {
                            if (x.Send <= DateTime.UtcNow.ToYeedraStamp())
                            {
                                await SendReminderAsync(x).ConfigureAwait(false);
                                toRemove.Add(x);
                            }
                        }
                        Database.Reminders.RemoveRange(toRemove);
                        await Database.SaveChangesAsync().ConfigureAwait(false);
                        toRemove.Clear();
                    }
                    await Task.Delay(250);
                }
            }
        }
    }
}

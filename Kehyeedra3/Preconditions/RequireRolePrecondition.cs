using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Linq;

namespace Kehyeedra3.Preconditions
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RequireRolePrecondition : PreconditionAttribute
    {
        private AccessLevel Level;

        public RequireRolePrecondition(AccessLevel level)
        {
            Level = level;
        }

        public override async Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo 
            command, IServiceProvider map)
        {
            var access = await GetPermissionAsync(context).ConfigureAwait(false);

            if (access >= Level)
                return PreconditionResult.FromSuccess();
            else
                return PreconditionResult.FromError("Insufficient permission level.");
        }

        public async Task<AccessLevel> GetPermissionAsync(ICommandContext c)
        {
            if (c.User.IsBot)
                return AccessLevel.Blocked; 

            if ((Bot._bot.GetApplicationInfoAsync
                ().Result).Owner.Id == c.User.Id)
                return AccessLevel.BotOwner;

            var user = await c.Guild.GetUserAsync(c.User.Id, CacheMode.AllowDownload).ConfigureAwait(false);
            if (user != null)
            {
                if (c.Guild.OwnerId == user.Id)
                    return AccessLevel.ServerOwner; 

                if (user.GuildPermissions.Administrator || user.GuildPermissions.ManageGuild)
                    return AccessLevel.ServerAdmin; 

                if (Configuration.Load().BigBoys.Contains(c.User.Id) || (Bot._bot.GetApplicationInfoAsync().Result).Owner.Id == c.User.Id) //is a big boy
                {
                    Console.WriteLine(user.Id + "\t" + true);
                    return AccessLevel.BigBoy;
                }

                if (user.GuildPermissions.ManageMessages &&
                    user.GuildPermissions.BanMembers &&
                    user.GuildPermissions.KickMembers &&
                    user.GuildPermissions.ManageRoles)
                    return AccessLevel.ServerMod;
            }
            return AccessLevel.User; 
        }
    }
}

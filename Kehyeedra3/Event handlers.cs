using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace Kehyeedra3
{
    public class EventHandlers : Bot
    {
        public static void InstallEventHandlers()
        {
            _bot.Log += _bot_Log;
            _bot.UserVoiceStateUpdated += _bot_UserVoiceStateUpdated;
            _cmds.CommandExecuted += _cmds_CommandExecuted;
        }

        private static async Task _cmds_CommandExecuted(Optional<CommandInfo> commandInfo, ICommandContext context, IResult result)
        {
            if(result.IsSuccess)
            {

            }
            else
            {
                if (commandInfo.Value.Name == "coof")
                {
                    await context.Channel.SendMessageAsync("Coofing didn't work.");
                }
            }
        }

        //voice join/leave add/remove role
        static async Task _bot_UserVoiceStateUpdated(Discord.WebSocket.SocketUser arg1, Discord.WebSocket.SocketVoiceState arg2, Discord.WebSocket.SocketVoiceState arg3)
        {
            if (!arg1.IsBot)
            {
                if (arg2.VoiceChannel == null && arg3.VoiceChannel != null)
                {
                    IGuild guild = arg3.VoiceChannel.Guild;
                    if (guild.Id == 296739813380587521)
                    {
                        Console.WriteLine($"{arg1.Username} joined voice on Gulag");
                        var role = guild.GetRole(411185260819251211);
                        var user = await guild.GetUserAsync(arg1.Id);
                        await user.AddRoleAsync(role);
                    }
                }
                if (arg2.VoiceChannel != null && arg3.VoiceChannel == null)
                {
                    IGuild guild = arg2.VoiceChannel.Guild;
                    if (guild.Id == 296739813380587521)
                    {
                        Console.WriteLine($"{arg1.Username} left voice on Gulag");
                        var role = guild.GetRole(411185260819251211);
                        var user = await guild.GetUserAsync(arg1.Id);
                        await user.RemoveRoleAsync(role);
                    }
                }
            }
        }

        private static Task _bot_Log(LogMessage message)
        {
            if (message.Severity == LogSeverity.Info)
                Console.ForegroundColor = ConsoleColor.Cyan;
            if (message.Severity == LogSeverity.Warning)
                Console.ForegroundColor = ConsoleColor.Yellow;
            if (message.Severity == LogSeverity.Error)
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            if (message.Severity == LogSeverity.Critical)
                Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{message.ToString()}");
            Console.ForegroundColor = ConsoleColor.White;
            return null;
        }
        
    }
}

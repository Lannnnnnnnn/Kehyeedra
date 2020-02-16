using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;
using AIMLbot;
using System.IO;
using System.Data;
using Discord;

namespace Kehyeedra3
{
    //Set CommandHandler as partial class of Bot
    class CommandHandler : Bot
    {
        public static async Task KizunaAi(ICommandContext Context, String Message)
        {
            try
            {
                if (Message.Contains($"{Context.Client.CurrentUser.Id}"))
                {
                    var messagearr = Message.Split(' ');
                    Message = String.Join(" ", messagearr.Skip(1).ToArray());
                }
                if (Message.ToLowerInvariant().Contains("what is "))
                {
                    string fiNum = Message.Substring(8);
                    string result = new DataTable().Compute(fiNum, null).ToString();
                    await Context.Channel.SendMessageAsync($"{Context.User.Mention}, {fiNum} = {result}");
                }
                else
                {
                    bool triggeredphrase = false;
                    KeyValuePair<string, string> trigger = new KeyValuePair<string, string>("", "");
                    foreach (var phrase in Configuration.Load().TriggerPhrases)
                    {
                        if (Message.ToLowerInvariant().Contains(phrase.Key))
                        {
                            triggeredphrase = true;
                            trigger = phrase;
                            break;
                        }
                    }
                    if (!triggeredphrase)
                    {
                        ChatUser = new AIMLbot.User(Convert.ToString(Context.User.Id + ".dat"), ChatService);
                        if (!File.Exists(PathToUserData + "\\" + Context.User.Id + ".dat"))
                            ChatUser.Predicates.DictionaryAsXML.Save(PathToUserData + "\\" + Context.User.Id + ".dat");
                        ChatUser = new AIMLbot.User(Convert.ToString(Context.User.Id), ChatService);
                        ChatUser.Predicates.loadSettings(PathToUserData + "\\" + Context.User.Id + ".dat");
                        var r = new AIMLbot.Request(Message, ChatUser, ChatService);
                        var userresp = ChatService.Chat(r);
                        var response = userresp.Output;
                        ChatUser.Predicates.DictionaryAsXML.Save(PathToUserData + "\\" + Context.User.Id + ".dat");
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}, {response}");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync($"{Context.User.Mention}, {trigger.Value}");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
        public static async Task InstallCommands()
        {
            //adds HandleCommand to handle the commands from message received
            _bot.MessageReceived += HandleCommand;

            await InstallModules();
        }

        private static async Task HandleCommand(SocketMessage arg)
        {
            if (!arg.Author.IsBot)
            {
                using (var Database = new ApplicationDbContextFactory().CreateDbContext())
                {
                    if(!Database.Users.Any(x=>x.Id == arg.Author.Id))
                    {
                        Database.Users.Add(new Services.Models.User
                        {
                            Id = arg.Author.Id,
                            Avatar = arg.Author.GetAvatarUrl() ?? arg.Author.GetDefaultAvatarUrl(),
                            Username = arg.Author.Username
                        });
                        await Database.SaveChangesAsync();
                    }
                }

                var message = arg as SocketUserMessage;
                if (message == null) return;
                int argPos = 0;
                var context = new CommandContext(_bot, message);
                if (message.HasMentionPrefix(_bot.CurrentUser, ref argPos))
                {
                    await KizunaAi(context, message.Content);
                }
                if (message.Content.Contains("\uD83C\uDD71")) //B emoji detector
                {
                    await context.Channel.SendMessageAsync($"B emoji detected. Proceed to kill yourself, {context.User.Mention}");
                }
                var erole = context.Guild.GetRole(674970145268957194);
                var euser = await context.Guild.GetUserAsync(context.User.Id).ConfigureAwait(false);
                if (!euser.RoleIds.Any(id => id == 674970145268957194))
                {
                    if (message.Content.Contains("<@&674970145268957194>")) //everyone role
                    {
                        await euser.AddRoleAsync(erole);
                    }
                }
                if (message.Content.Contains("cock and ball torture is a sexual activity involving the torture of the male genitals")) //remove role
                {
                    await euser.RemoveRoleAsync(erole);
                }
                if (message.Content.ToLowerInvariant().Contains("jojo"))
                {
                    var jojoke = WeebClient.DownloadString("https://api.skuldbot.uk/fun/jojoke/?raw");
                    await context.Channel.SendMessageAsync($"{context.User.Mention} is that a fucksnifflerling {jojoke} reference?");
                }
                if (!(message.HasStringPrefix(Configuration.Load().Prefix, ref argPos))) return;
                {
                    var result = await _cmds.ExecuteAsync(context, argPos, _dmap);

                    if (result.IsSuccess)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Sent successfully");
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    else
                    {
                        await context.Channel.SendMessageAsync($"Command failed with the following error:\n{result.ErrorReason}\nPlease make sure your brain is plugged in and charging.");
                        Console.ForegroundColor = ConsoleColor.Red; //set text red
                        Console.WriteLine($"Something went wrong\n{result.ErrorReason}");
                        Console.ForegroundColor = ConsoleColor.White; //back to white
                    }
                }
            }
        }

        public static async Task InstallModules()
        {
            await _cmds.AddModulesAsync(Assembly.GetEntryAssembly(), _dmap);
        }
    }
}

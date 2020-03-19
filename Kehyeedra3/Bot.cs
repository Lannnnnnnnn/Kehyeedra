using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.IO;
using AIMLbot;
using Microsoft.Extensions.DependencyInjection;
using System.Timers;
using System.Net;
using System.Net.Http;
using Kehyeedra3.Services;
using System.Threading;

namespace Kehyeedra3
{
    public class Bot
    {
        /// Star Vars
        public static DiscordShardedClient _bot;
        public static System.Timers.Timer Clockboy;
        public static CommandService commands;
        public static AudioService AudioService;
        public static CommandService _cmds;
        public static IServiceProvider _dmap;
        public static CommandServiceConfig _cmdsconfig;
        public static int Shards = 0;
        public static Random _rnd = new Random();
        public static AIMLbot.Bot ChatService;
        public static AIMLbot.User ChatUser;
        public static string PathToUserData;
        public static WebClient WeebClient;
        public static ReminderService RmService;
        string[] rcsounds = new string[]
        {
                AppContext.BaseDirectory + @"Audio\goblin_death.wav",
                AppContext.BaseDirectory + @"Audio\cough1.wav",
                AppContext.BaseDirectory + @"Audio\cough2.wav",
                AppContext.BaseDirectory + @"Audio\wilhelmcough.wav",
                AppContext.BaseDirectory + @"Audio\trapcough1.wav",
                AppContext.BaseDirectory + @"Audio\trapcough2.wav",
                AppContext.BaseDirectory + @"Audio\h3h3cough1.wav",
                AppContext.BaseDirectory + @"Audio\h3h3cough2.wav",
                AppContext.BaseDirectory + @"Audio\h3h3cough3.wav",
                AppContext.BaseDirectory + @"Audio\h3h3cough4.wav",
                AppContext.BaseDirectory + @"Audio\h3h3cough5.wav",
                AppContext.BaseDirectory + @"Audio\sodocough.mp3",
                AppContext.BaseDirectory + @"Audio\dbzscream.wav",
                AppContext.BaseDirectory + @"Audio\healthycough1.wav",
                AppContext.BaseDirectory + @"Audio\poohcough1.wav",
                AppContext.BaseDirectory + @"Audio\jontronooh.wav",
                AppContext.BaseDirectory + @"Audio\deep.wav"
        };

        public async Task CreateBot()
        {
            var Config = Configuration.Load();

            Clockboy = new System.Timers.Timer();

            WeebClient = new WebClient();
            
            _bot = new DiscordShardedClient(new DiscordSocketConfig()
            {
                LogLevel = LogSeverity.Verbose,
                DefaultRetryMode = RetryMode.AlwaysRetry,
                HandlerTimeout = 10000,
                ConnectionTimeout = 10000,
                TotalShards = Config.Shards
            });

            AudioService = new AudioService();

            Shards = _bot.Shards.Count;

            _cmds = new CommandService();

            _cmdsconfig = new CommandServiceConfig
            {
                CaseSensitiveCommands = false
            };

            _dmap = new ServiceCollection()
                .AddSingleton(_bot)
                .AddSingleton(_cmds)
                .AddSingleton(AudioService)
                .BuildServiceProvider();

            await CommandHandler.InstallCommands();

            EventHandlers.InstallEventHandlers();
            InstallChatService();

            Clockboy.Elapsed += Clockboy_Elapsed;
            Clockboy.Interval = SRandom.Next(3600000, 14400000);
            //1200000 = 20 minutes

            RmService = new ReminderService();

            new Thread(
                async () =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    await RmService.Tick().ConfigureAwait(false);
                }
            ).Start();
        }

        private async void Clockboy_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach(var Guild in _bot.Guilds)
            {
                foreach(var VoiceChannel in Guild.VoiceChannels)
                {
                    if (VoiceChannel.Users.Count > 1)
                    {
                        try
                        {
                            await AudioService.JoinAudio(Guild, VoiceChannel);
                            var file = rcsounds[_rnd.Next(rcsounds.Length - 1)];

                            Console.WriteLine("File exists? " + File.Exists(file));

                            Console.WriteLine("Playing file: " + file);

                            await AudioService.SendAudioAsync(Guild, null, file);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex);
                        }
                        finally
                        {
                            await AudioService.LeaveAudio(Guild);
                        }
                    }
                }
            }
            Clockboy.Start();
        }

        public async Task StartBot()
        {
            try
            {
                await _bot.LoginAsync(TokenType.Bot, Configuration.Load().Token);
                await _bot.StartAsync();
                Clockboy.Start();
                //new Thread(async x =>
                //{
                //    await Bot.RmService.Tick();
                //}).Start();
                await Task.Delay(-1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(DateTime.Now + " Error, reason: " + ex.ToString());
            }
            finally
            {
                await _bot.StopAsync();
                await _bot.LogoutAsync();
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        public static void EnsureConfigExists()
        {
            string storage = AppDomain.CurrentDomain.BaseDirectory;
            if (!Directory.Exists(Path.Combine(storage, "storage")))
                Directory.CreateDirectory(Path.Combine(storage, "storage"));

            string configfile = Path.Combine(storage, "storage/configuration.json");

            if (!File.Exists(configfile))
            {
                var config = new Configuration();
                config.Save();
                Console.WriteLine($"The configuration file has been created at {configfile}.\n\nEdit the file with your details and restart");
                Console.ReadLine();
                Environment.Exit(0);
            }
        }
        public static void InstallChatService()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config",
                    "Settings.xml");
                if (File.Exists(path))
                {
                    ChatService = new AIMLbot.Bot();
                    ChatService.loadSettings(path);
                    ChatService.isAcceptingUserInput = false;
                    ChatService.loadAIMLFromFiles();
                    ChatService.isAcceptingUserInput = true;
                    PathToUserData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "aimlusers");
                    Console.WriteLine("Loaded: Chat Service");
                }
                else { }
            }
            catch(Exception ex)
            {
                Console.WriteLine (ex);
            }
        }
    }
}
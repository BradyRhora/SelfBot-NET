using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace SelfBot
{
    public class Bot
    {
        static void Main(string[] args) => new Bot().Run().GetAwaiter().GetResult();

        #region Vars
        public static DiscordSocketClient client;
        private CommandService commands;
        public static string[] lennyStats = File.ReadAllLines(@"Files\lennyStats.txt"); //0 - happy lennys 1 - sad lennys 2 - bennys
        public static bool quirk;
        public static bool lenny;
        public static string qTroll;

        public static bool learning = false;

        public static string[] lastMessage;
        public static IChannel[] channels;

        /*public static bool image = false;
        public static IChannel imgChan;
        public static string imgURL;*/
        #endregion

        public async Task Run()
        {
            Start:
            try
            {
                Console.WriteLine("Welcome, Brady. Initializing Selfbot...");
                client = new DiscordSocketClient();
                Console.WriteLine("Client Initialized.");
                commands = new CommandService();
                Console.WriteLine("Command Service Initialized.");
                string token = File.ReadAllLines(@"Constants\UserToken")[0];
                await InstallCommands();
                Console.WriteLine("Commands Installed, logging in.");
                await client.LoginAsync(TokenType.User, token);
                Console.WriteLine("Successfully logged in!");
                // Connect the client to Discord's gateway
                await client.StartAsync();
                Console.WriteLine("Selfbot successfully intialized");
                // Block this task until the program is exited.
                await Task.Delay(-1);
            }
            catch (Exception e)
            {
                Console.WriteLine("\n==========================================================================");
                Console.WriteLine("                                  ERROR                        ");
                Console.WriteLine("==========================================================================\n");
                Console.WriteLine($"Error occured in {e.Source}");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);

                Again:

                Console.WriteLine("Would you like to try reconnecting? [Y/N]");
                var input = Console.Read();

                if (input == 121) { Console.Clear(); goto Start; }
                else if (input == 110) Environment.Exit(0);

                Console.WriteLine("Invalid input.");
                goto Again;
            }
        }

        public async Task InstallCommands()
        {
            client.MessageReceived += HandleCommand;
            client.Ready += HandleReady;

            await commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        //Code that runs when bot recieves a message
        public async Task HandleCommand(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            int argPos = 0;

            #region Quirks
            if (quirk && !message.HasCharPrefix('+', ref argPos) && message.Author.Id == client.CurrentUser.Id)
            {
                string text = message.Content;
                if (qTroll == "kv") text = text.ToUpper();
                else if (qTroll == "am")
                {
                    text = text.ToLower();
                    text = text.Replace('o', '0');
                    text = text.Replace(".", "");
                    text = text.Replace(",", "");
                    text = text.Replace(":)", "0_0");
                    text = text.Replace(":(", "0_0");
                }
                else if (qTroll == "tn")
                {
                    char[] chars = text.ToCharArray();
                    for (int i = 0; i < chars.Count(); i++)
                    {
                        if (!char.IsUpper(chars[i])) chars[i] = char.ToUpper(chars[i]);
                        else chars[i] = char.ToLower(chars[i]);
                    }

                    text = new string(chars);

                    text = text.Replace('.', ',');
                    text = text.Replace(":)", "}:)");
                    text = text.Replace(";)", "};)");
                    text = text.Replace(":(", "}:(");
                }
                else if (qTroll == "sc")
                {
                    text = text.Replace('s', '2');
                    text = text.Replace('S', '2');
                    text = text.Replace("i", "ii");
                    text = text.Replace("I", "II");
                    text = text.Replace("to", "two");
                    text = text.Replace("too", "two");
                }
                else if (qTroll == "nl")
                {
                    text = ":33 < " + text;
                    text = text.Replace("ee", "33");
                    text = text.Replace("EE", "33");
                }
                else if (qTroll == "km")
                {
                    char[] chars = text.ToCharArray();

                    chars[0] = char.ToUpper(chars[0]);

                    for (int i = 0; i < chars.Count(); i++)
                    {
                        if (chars[i] == ' ') chars[i + 1] = char.ToUpper(chars[i + 1]);
                    }

                    text = new string(chars);
                }
                else if (qTroll == "tp")
                {
                    text = text.ToUpper();

                    text = text.Replace('A', '4');
                    text = text.Replace('E', '3');
                    text = text.Replace('I', '1');
                    text = text.Replace(" :", ">:");
                    text = text.Replace(":(", ":[");
                    text = text.Replace(":)", ":]");
                    text = text.Replace(";(", ";[");
                    text = text.Replace(":D", ":D");
                }
                else if (qTroll == "vs")
                {
                    text = text.Replace("great", "gr8");
                    text = text.Replace('b', '8');
                    text = text.Replace('B', '8');
                    text = text.Replace("ate", "8");
                    text = text.Replace("ait", "8");
                    text = text.Replace(":)", "::::)");
                    text = text.Replace(":(", "::::(");

                    text = text.Replace(";)", ";;;;)");
                    text = text.Replace(":D", "::::D");
                }
                else if (qTroll == "ez")
                {
                    text = "D--> " + text;
                    text = text.Replace("nay", "neigh");
                    text = text.Replace('x', '%');
                    text = text.Replace('X', '%');
                    text = text.Replace("blue", "b100");
                    text = text.Replace("loo", "100");
                    text = text.Replace("BLUE", "B100");
                    text = text.Replace("LOO", "100");
                }
                else if (qTroll == "gm")
                {
                    char[] chars = text.ToCharArray();
                    for (int i = 0; i < chars.Count(); i++)
                    {
                        if (i % 2 == 0) chars[i] = char.ToUpper(chars[i]);
                        else chars[i] = char.ToLower(chars[i]);
                    }
                    text = new string(chars);
                }
                else if (qTroll == "ea")
                {
                    text = text.Replace("w", "ww");
                    text = text.Replace("v", "vv");
                    text = text.Replace("W", "WW");
                    text = text.Replace("V", "VV");
                    text = text.Replace("ing", "in");
                }
                else if (qTroll == "fp")
                {
                    text = text.Replace("h", ")(");
                    text = text.Replace("H", ")(");
                    text = text.Replace("E", "-E");

                    text = text.Replace(" :", " 38");
                    text = text.Replace(":(", "8(");
                    text = text.Replace(":)", "8)");

                    text = text.Replace(":D", "8D");
                    text = text.Replace(":D", "8D");
                }
                else if (qTroll == "dp")
                {
                    text = text.Replace("a", "@");
                    text = text.Replace("e", "3");
                    text = text.Replace("i", "1");
                    text = text.Replace("o", "0");
                    text = text.Replace("y", "¥");
                    text = text.Replace("h", "#");
                    text = text.Replace("l", "£");
                    text = text.Replace("c", "€");
                    text = text.Replace("b", "8");
                    text = text.Replace("t", "%");
                    text = text.Replace("s", "$");
                    text = text.Replace("d", "|]");

                    text = text.Replace("A", "@");
                    text = text.Replace("E", "3");
                    text = text.Replace("I", "1");
                    text = text.Replace("O", "0");
                    text = text.Replace("Y", "¥");
                    text = text.Replace("H", "#");
                    text = text.Replace("L", "£");
                    text = text.Replace("C", "€");
                    text = text.Replace("B", "8");
                    text = text.Replace("T", "%");
                    text = text.Replace("S", "$");
                    text = text.Replace("D", "|]");

                }
                await message.ModifyAsync(x => x.Content = text);
            }
            #endregion
            #region Lenny Converter
            if (lenny && !message.HasCharPrefix('+', ref argPos) && message.Author.Id == client.CurrentUser.Id)
            {
                string text = message.Content;
                if (text.Contains("&l"))
                {
                    try
                    {
                        text = text.Replace("&l", "( ͡° ͜ʖ ͡°)");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    await message.ModifyAsync(x => x.Content = text);
                }
            }

            #endregion
            #region Lenny Stat Tracker

            if (!message.HasStringPrefix("+", ref argPos) && (message.Content.Contains("( ͡° ͜ʖ ͡°)") || message.Content.Contains("&l") || message.Content.Contains("( ͡° ʖ̯ ͡°)") || message.Content.Contains(":^)")))
            {


                if (message.Content.Contains("( ͡° ͜ʖ ͡°)") || message.Content.Contains("&l"))
                {
                    char[] msg = message.Content.ToCharArray();
                    for (int i = 0; i < msg.Count(); i++)
                    {
                        if (msg[i] == '(') if (message.Content.Substring(i, 11) == "( ͡° ͜ʖ ͡°)") lennyStats[0] = Convert.ToString(Convert.ToInt32(lennyStats[0]) + 1);
                    }
                }

                if (message.Content.Contains("( ͡° ʖ̯ ͡°)"))
                {
                    char[] msg = message.Content.ToCharArray();
                    for (int i = 0; i < msg.Count(); i++)
                    {
                        string sub = message.Content.Substring(i, 11);
                        if (msg[i] == '(') if (message.Content.Substring(i, 11) == "( ͡° ʖ̯ ͡°)") lennyStats[1] = Convert.ToString(Convert.ToInt32(lennyStats[1]) + 1);
                    }
                }

                if (message.Content.Contains(":^)"))
                {
                    char[] msg = message.Content.ToCharArray();
                    for (int i = 0; i < msg.Count(); i++)
                    {
                        string sub = message.Content.Substring(i, 3);
                        if (msg[i] == ':') if (message.Content.Substring(i, 3) == ":^)") lennyStats[2] = Convert.ToString(Convert.ToInt32(lennyStats[2]) + 1);
                    }
                }

                File.WriteAllLines(@"Files\lennyStats.txt", lennyStats);
            }
            #endregion
            #region Brady Tracker
            if (message.Content.ToLower().Contains("brady") && message.Author.Id != 108312797162541056 && !message.Author.IsBot)
            {
                var bradyChan = client.GetChannel(322978036465270784) as IMessageChannel;
                var user = message.Author as IGuildUser;
                var role = user.Guild.GetRole(user.RoleIds.ElementAtOrDefault(1));
                var footer = new EmbedFooterBuilder();
                footer.Text = $"Sent from {message.Channel.Name} in {user.Guild}";
                footer.IconUrl = user.Guild.IconUrl;
                var emb = new EmbedBuilder().WithCurrentTimestamp().WithTitle(message.Author.ToString()).WithDescription(message.Content.ToString()).WithThumbnailUrl(message.Author.GetAvatarUrl()).WithColor(role.Color).WithFooter(footer);
                await bradyChan.SendMessageAsync("", embed: emb);
            }
            #endregion
            #region Tatsumaki
            if (message.Author.Id == 172002275412279296)
            {
                if (message.Channel.Id == 277455823855419392)
                {
                    if (message.Content.Contains("daily"))
                    {
                        var dailyChan = client.GetChannel(263602576849633283) as IMessageChannel;
                        await dailyChan.SendMessageAsync("t!daily");
                        await dailyChan.SendMessageAsync("t!remindme daily in 1 day");
                        await dailyChan.SendMessageAsync("t!slots 200");
                    }
                }
                else if (message.Content.Contains("has given @Brady a repuation point"))
                {
                    string repUser = message.Content;
                    repUser = repUser.Replace(" has given @Brady a repuation point!", "");
                    repUser = repUser.Replace(":up:  |  ", "");

                    await message.Channel.SendMessageAsync($"t!rep {repUser}");
                }
            }
            #endregion
            
            #region AutoResponder
            if (message.Attachments.Count <= 0 && message.Content.Length < 200 && learning)
            {

                //So, we'll have to start with learning proper responses. Instead of just using my information, I'll try to use as much as I can from all the servers I'm on.


                string txt = message.Content;

                int chanID = -1;
                for (int i = 0; i < channels.Count(); i++)
                {
                    if (channels[i] == message.Channel)
                    {
                        chanID = i;
                        break;
                    }
                }

                //If the message has been seen before..
                if (!Funcs.ListExists(txt))
                {
                    var list = Funcs.NewList(txt);
                }

                if (lastMessage[chanID] != null) Funcs.AddResponse(lastMessage[chanID], txt);

                lastMessage[chanID] = message.Content;
            }
            #endregion


            if (message.HasCharPrefix('+', ref argPos) && (message.Author.Id == client.CurrentUser.Id || message.Author.Id == Constants.ZAIM))
            {

                var context = new CommandContext(client, message);
                var result = await commands.ExecuteAsync(context, argPos);
                if (!result.IsSuccess)
                    Console.WriteLine(result.ErrorReason);
            }
            else return;
        }

        //Code that runs when bot comes online
        public async Task HandleReady()
        {
            List<IChannel> chans = new List<IChannel>();

            foreach (IGuild guild in client.Guilds)
            {
                foreach (IChannel chan in await guild.GetChannelsAsync())
                {
                    chans.Add(chan);
                }
            }

            channels = chans.ToArray();

            lastMessage = new string[channels.Count()];
        }
    }


}

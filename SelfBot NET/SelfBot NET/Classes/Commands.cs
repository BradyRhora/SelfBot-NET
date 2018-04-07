using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace SelfBot
{
    public class Commands : ModuleBase
    {

        [Command("eval")]
        public async Task EvaluateCmd([Remainder] string expression)
        {
            IUserMessage msg = await ReplyAsync("Evaluating...");
            string result = await EvalService.EvaluateAsync(Context as CommandContext, expression);//
            var user = Context.User as IGuildUser;
            if (user.RoleIds.ToArray().Count() > 1)
            {
                var role = Context.Guild.GetRole(user.RoleIds.ElementAtOrDefault(1));
                var emb = new EmbedBuilder().WithColor(role.Color).WithDescription(result).WithTitle("Evaluated").WithCurrentTimestamp();
                await Context.Channel.SendMessageAsync("", embed: emb);
            }
            else
            {
                var emb = new EmbedBuilder().WithColor(new Color(147, 112, 219)).WithDescription(result).WithTitle("Evaluated").WithCurrentTimestamp();
                await Context.Channel.SendMessageAsync("", embed: emb);
            }

        }

        [Command("deed")]
        public async Task Deed(string name, int age, int skin, int hotness)
        {
            Random rdm = new Random();

            string fSense = "OK";
            string tone;
            string rect;
            string status;

            if (hotness > 8) status = "TAKEN";
            else if (hotness > 5) status = "UNKNOWN";
            else status = "SINGLE";

            if (age > 30 || hotness > 7 && skin > 3 && skin < 7) rect = "WIDE";
            else rect = "TIGHT";

            if (age < 13) { tone = "HIGH"; fSense = "CRAP"; }
            else if (skin < 5) tone = "MEDIUM";
            else if (skin > 7) tone = "LOW";
            else tone = "UNKNOWN";

            if (skin > 7 && age > 12) fSense = "BAD";
            else if (skin > 3 && age > 12) fSense = "GOOD";
            else if (age > 12) fSense = "OK";

            int rating = 100;


            int nLength;

            if (name != "?") nLength = name.Length;
            else nLength = rdm.Next(7) + 1;

            if (nLength != 1) rating -= 100 / nLength;
            else rating -= 50;
            char[] nameC = new char[100];
            nameC = name.ToCharArray();
            int cCount = 0;
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
            foreach (char chr in nameC)
            {
                foreach (char v in vowels) if (chr == v) cCount++;
            }
            rating -= age;
            if (skin < 3 || skin > 7) rating -= 5 * skin;
            else rating += 5 * skin;
            rating -= 3 * hotness;
            rating += 5 * cCount;
            if (rating < 0) rating = 0;
            else if (rating > 100) rating = 100;
            string message = "";
            message += ("```md\n########## FINAL DICK RESULTS ##########\n");
            message += ($"Results for: {name}\n");
            message += ($"STATUS: {status}. RECTUM DIAMETER: {rect}. VOCAL TONE: {tone}. FASHION SENSE: {fSense}.\n");
            message += ("DICK RATING: " + rating + "%```");
            await ReplyAsync(message);

            Console.WriteLine($"{name}'s DEED rating was calculated, returning {rating}%.");
        }

        [Command("quirk")]
        public async Task Quirk(string troll)
        {
            string[] trolls = { "kv", "am", "tn", "nl", "km", "vs", "sc", "tp", "ez", "gm", "ea", "fp", "dp" };
            if (!Bot.quirk)
            {
                if (troll == "kv" || troll == "am" || troll == "tn" || troll == "nl" || troll == "km" || troll == "vs" || troll == "sc" || troll == "tp" || troll == "ez" || troll == "gm" || troll == "ea" || troll == "fp" || troll == "dp")
                {
                    Bot.quirk = true;
                    Bot.qTroll = troll;
                }
                else if (troll == "list")
                {
                    var JEmb = new JEmbed()
                    {
                        ColorStripe = GetColor(Context.User),
                        Title = "Available Quirks",
                        Description = "[kv] - TEXT\n" +
                        "[am] - text. 0_0\n" +
                        "[tn] - tEXT,\n" +
                        "[nl] - :33 < text!\n" +
                        "[km] - Text.\n" +
                        "[vs] - Teeeeeeeext >::::)\n" +
                        "[sc] - text\n" +
                        "[tp] - T3XT\n" +
                        "[ez] - D--> Te%t.\n" +
                        "[gm] - mOtHeRfUcKiN tExT :o)\n" +
                        "[ea] - text.\n" +
                        "[fp] - T-EXT! 8D\n" +
                        "[dp] - ?!?!?!?!\n",
                        ThumbnailUrl = Context.User.AvatarId
                    };
                    await Context.Channel.SendMessageAsync("", embed: JEmb.Build());
                }
                else await Context.Channel.SendMessageAsync(":robot: Quirk doesn't exist!!?!?! :robot:");
            }
            else Bot.quirk = false;
        }

        [Command("fuse")]
        public async Task Fuse(string item)
        {
            var user = Context.User as IGuildUser;

            if (item == "unfuse")
            {
                await user.ModifyAsync(x => x.Nickname = "Brady");
                return;
            }


            string name;
            if (user.Nickname == null) name = user.Username;
            else name = user.Nickname;

            await Fuse(name, item);
        }

        [Command("fuse")]
        public async Task Fuse(string item1, string item2)
        {
            item1 = item1.ToLower();
            var user = await Context.Guild.GetUserAsync(108312797162541056) as IGuildUser;
            await Context.Channel.SendMessageAsync(":point_left:( ͡° ͜ʖ ͡°):point_left: ***FUUU...SION*** :point_right:( ͡° ͜ʖ ͡°):point_right:");
            System.Threading.Thread.Sleep(1000);
            await Context.Channel.SendMessageAsync(":point_right:( ͡° ͜ʖ ͡°):point_right::point_left:( ͡° ͜ʖ ͡°):point_left: ***HAAAAAA***");
            Random rdm = new Random();
            bool set = false;

            while (!set)
            {
                string temp1 = item2, temp2 = item1;
                int rando = rdm.Next(10);
                if (rando < 5)
                {
                    item2 = temp2;
                    item1 = temp1;
                }
                else
                {
                    item2 = temp1;
                    item1 = temp2;
                }

                int ID = -1;
                int rand = rdm.Next(10);
                char[] nameCheck = item1.ToCharArray();
                for (int i = 0; i < nameCheck.Count(); i++)
                {
                    if (nameCheck[i] == 'a' || nameCheck[i] == 'e' || nameCheck[i] == 'i' || nameCheck[i] == 'o' || nameCheck[i] == 'u' || nameCheck[i] == 'y')
                    {
                        ID = i;
                        if (rand < 5) break;
                    }
                }

                int ID2 = -1;
                char[] nameCheck2 = item2.ToLower().ToCharArray();


                for (int i = 0; i < nameCheck2.Count(); i++)
                {
                    if (nameCheck2[i] == 'a' || nameCheck2[i] == 'e' || nameCheck2[i] == 'i' || nameCheck2[i] == 'o' || nameCheck2[i] == 'u' || nameCheck2[i] == 'y')
                    {
                        ID2 = i;
                        if (rand > 5) break;
                    }
                }
                if (ID != -1)
                {
                    string nick = "";
                    string half, half2;
                    if (rand < 5)
                    {
                        half = item2.Substring(0, ID2);
                        half2 = item1.Substring(ID);
                        nick = half + half2;
                    }
                    else
                    {
                        half = item1.Substring(0, ID);
                        half2 = item2.Substring(ID2);
                    }
                    nick = char.ToUpper(half[0]) + half.Substring(1) + half2;

                    if (user.Nickname != nick)
                    {
                        set = true;
                        if (Context.User.Id == Constants.BRADY) await user.ModifyAsync(x => x.Nickname = nick);
                        else
                        {
                            await Context.Channel.SendMessageAsync($":robot: {nick} :robot:");
                            Console.WriteLine($"{Context.User.Username} has fused {item1} and {item2} and recieved {nick}.");
                        }
                    }
                }
                else await Context.Channel.SendMessageAsync(":robot: No ty :robot:");
            }
        }

        [Command("lenny")]
        public async Task Lenny()
        {
            if (!Bot.lenny) { await Context.Message.ModifyAsync(x => x.Content = "( ͡° ͜ʖ ͡°)"); Bot.lenny = true; }
            else { await Context.Message.ModifyAsync(x => x.Content = "( ͡° ʖ̯ ͡°)"); Bot.lenny = false; }
        }

        [Command("lenny")]
        public async Task Lenny(string arg)
        {
            if (arg == "stats")
            {
                await Context.Channel.SendMessageAsync($"```md\nLENNY STATS\n============\nLenny Count: {Bot.lennyStats[0]}" +
                    $"\nSad Lenny Count: {Bot.lennyStats[1]}\nBenny Count: {Bot.lennyStats[2]}" +
                    $"\nTotal Faces: {Convert.ToInt32(Bot.lennyStats[0]) + Convert.ToInt32(Bot.lennyStats[1]) + Convert.ToInt32(Bot.lennyStats[2])}```");
            }
            else if (arg == "stats+")
            {
                var created = Context.User.CreatedAt;
                var today = DateTime.Now;

                var span = today - created;
                int days = span.Days;

                var totalLens = Convert.ToInt32(Bot.lennyStats[0]) + Convert.ToInt32(Bot.lennyStats[1]) + Convert.ToInt32(Bot.lennyStats[2]);

                double lensPerDay = totalLens / days;

                await Context.Channel.SendMessageAsync($"```md\nLENNY STATS\n============\nLenny Count: {Bot.lennyStats[0]}" +
                    $"\nSad Lenny Count: {Bot.lennyStats[1]}\nBenny Count: {Bot.lennyStats[2]}" +
                    $"\nTotal Faces: {totalLens}" +
                    $"\nThis adds up to {lensPerDay} Lennys per day, starting from the creation of this account. ({days} days ago)```");
            }
        }

        [Command("amal")]
        public async Task Amalgamemetion(int amount, [Remainder] string arg)
        {
            bool lenny = false;
            if (arg.Contains("°"))
            {
                if (arg.Contains(" ͜"))
                {
                    arg = "( ͡° ͜ʖ ͡°)";
                }
                else if (arg.Contains("ʖ̯"))
                {
                    arg = "( ͡° ʖ̯ ͡°)";
                }

                lenny = true;
            }

            Random rdm = new Random();
            string final = "";
            string[] words;
            List<string> wordsL = new List<string>();
            if (!lenny)
            {
                words = arg.Split(' ');

                foreach (string item in words)
                {
                    if (item != "")//or null, idk
                    {
                        wordsL.Add(item);
                    }
                }
            }

            else wordsL.Add(arg);

            for (int i = 0; i < amount; i++)
            {
                if (rdm.Next(100) > 25) for (int a = 0; a < rdm.Next(3) + 1; a++) final += "*";

                string word = wordsL[rdm.Next(wordsL.Count())];

                int letters = rdm.Next(word.Length);
                int back = rdm.Next(100);

                if (back < 50) final += word.Substring(word.Length - letters);
                else final += word.Substring(0, letters);
            }
            await Context.Channel.SendMessageAsync(final);
            Console.WriteLine($"{arg} has been amalgmated at a size of {amount}.");
        }

        [Command("f")]
        public async Task Eff(string param)
        {
            if (param.ToLower() == "f")
            {
                await Context.Message.ModifyAsync(x => x.Content = ":regional_indicator_f::regional_indicator_f::regional_indicator_f:\n" +
                ":regional_indicator_f:\n" +
                ":regional_indicator_f::regional_indicator_f:\n" +
                ":regional_indicator_f:\n" +
                ":regional_indicator_f:");
            }
            else if (param == "pain")
            {
                await Context.Message.ModifyAsync(x => x.Content = ":bread::bread::bread:\n" +
                ":bread:\n" +
                ":bread::bread:\n" +
                ":bread:\n" +
                ":bread:");
            }
        }

        [Command("pain")]
        public async Task Pain([Remainder] string word)
        {
            if (word.Contains("|"))
            {
                foreach (string value in word.Split('|'))
                {
                    await Pain(value);
                }
            }
            else if (word.StartsWith("custom"))
            {
                word = word.Replace("custom", "");
                word = word.Replace("```", "");
                word = word.Replace("X", ":bread:");
                word = word.Replace("O", ":black_large_square:");
                await Context.Channel.SendMessageAsync(word);
            }
            else
            {
                if (word.Length > 6 && word != "( ͡° ͜ʖ ͡°)") await Context.Channel.SendMessageAsync(":robot: TOO MUCH BREAD!!! :robot:");
                else
                {
                    BreadBuilder bb = new BreadBuilder();
                    string output = bb.build(word);
                    await Context.Channel.SendMessageAsync(output);
                }
            }
        }

        [Command("waggle")]
        public async Task Waggle()
        {
            await Context.Message.DeleteAsync();
            await Context.Channel.SendFileAsync(@"Constants\knotting-maximum.gif");
        }

        [Command("help"), Summary("Displays commands and descriptions.")]
        public async Task Help()
        {
            JEmbed emb = new JEmbed();
            emb.Author.Name = "Commands";
            emb.ColorStripe = GetColor(Context.User);

            foreach (CommandInfo command in Bot.commands.Commands)
            {
                emb.Fields.Add(new JEmbedField(x =>
                {
                    string header = "?" + command.Name;
                    foreach (ParameterInfo parameter in command.Parameters)
                    {
                        header += " [" + parameter.Name + "]";
                    }
                    x.Header = header;
                    x.Text = command.Summary;
                }));
            }
            await Context.Channel.SendMessageAsync("", embed: emb.Build());
        }

        [Command("learn")]
        public async Task Learn()
        {
            Bot.learning = !Bot.learning;
            string msg;
            if (Bot.learning) msg = ":heavy_check_mark:";
            else msg = ":x:";
            await Context.Message.ModifyAsync(x => x.Content = ":robot:" + msg);
        }

        Color GetColor(IUser User)
        {
            var user = User as IGuildUser;
            if (user == null)
            {
                if (user.RoleIds.ToArray().Count() > 1)
                {
                    var role = Context.Guild.GetRole(user.RoleIds.ElementAtOrDefault(1));
                    return role.Color;
                }
                else return Constants.DEFAULT_COLOUR;
            }
            else return Constants.DEFAULT_COLOUR;
        }

        string[] GenerateSudoku()
        {
            string[] sudoku = new string[9];





            return sudoku;
        }

        /* Forget me
        #region Game Vars
        public static string[] words;
        public static int counter;
        public static CommandContext cont;
        #endregion

        [Command("game")]
        public async Task Game(int interval, [Remainder]string input)
        {
            words = input.Split(' ');
            counter = 0;
            cont = Context;
            Timer t = new Timer(TimerCallback, null, 0, interval);
            await Context.Channel.SendMessageAsync(":robot::ok_hand:");
        }

        async void TimerCallback(Object state)
        {
            try
            {
                var user = cont.User as ISelfUser;
                if (counter > words.Count() -1) counter = 0;

                

                await (cont.Client as Discord.WebSocket.DiscordSocketClient).SetGameAsync(words[counter]);
                counter++;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }*/


        /*WebClient webclient = new WebClient();
        [Command("image"), Alias("img")]
        public async Task Image(string command) { await Image(command, 100); }


        [Command("image"), Alias("img")]
        public async Task Image(string command, int amount)
        {
            if (command == "on") { Bot.image = true; Bot.imgChan = Context.Channel; await Context.Message.ModifyAsync(x => x.Content = ":robot: Image Processing: On :robot:"); }
            else
            {
                if (Bot.image && Context.Channel == Bot.imgChan)
                {
                    webclient.DownloadFileAsync(new Uri(Bot.imgURL), $@"Files\tempIMG.jpg");



                    /* Image Processor Code (Much better. Fucc)
                    ImageFactory fac = new ImageFactory();
                    fac.Load(@"Files\tempIMG.jpg");

                    if (command == "pixel") fac.Pixelate(amount);
                    else if (command == "quality") fac.Quality(amount);
                    else if (command == "rotate") fac.Rotate(amount);
                    else if (command == "sharp") fac.GaussianSharpen(amount);
                    else if (command == "bright") fac.Brightness(amount);
                    else if (command == "contrast") fac.Contrast(amount);
                    else if (command == "blur") fac.GaussianBlur(amount);
                    else if (command == "hue") fac.Hue(amount);
                    else if (command == "round") fac.RoundedCorners(amount);
                    //else if (command == "") fac.Tint();

                    fac.Save(@"Files\editedIMG.jpg");
                    
                    await Context.Channel.SendFileAsync(@"Files\editedIMG.jpg");

                }
            }
            
        }*/

        #region CLOSED UNTIL FURTHER NOTICE (Evolve, Game)
        /*[Command("evolve")]
        public async Task Evolve(string word)
        {
            //how do I want to make it "evolve"?
            //dont forget 'mutations'
            //multiple evolution types?

            Random rdm = new Random();
            int portion = Convert.ToInt32(Math.Round(Convert.ToDecimal(word.Length / 5))); //get portion of text (20%)
            int rand = 0;
            while (rand == 0) rand = rdm.Next(-1, 2);
            string sub = "";
            if (rand < 0)
            {
                sub = word.Substring(word.Length + rand, portion);
            }
            else if (rand > 0)
            {
                sub = word.Substring(portion);
            }

            Mutate(sub);

        }

        string Mutate(string input)
        {
            char[] chars = input.ToCharArray();
            Random chance = new Random();
            for (int i = 0; i < chars.Count(); i++)
            {
                if (chance.Next(100) < 5)
                {

                }
            }
            return input;
        }

        [Command("save", RunMode = RunMode.Async)]
        public async Task Save(int ID)
        {
            try
            {


                List<string> file = File.ReadAllLines(@"C:\Users\Owner\Documents\Visual Studio 2017\Projects\SelfBot\SelfBot\Files\saves.txt").ToList();
                if (msg.Attachments != null) file.Add($"{msg.Author.Username}|{msg.Content}|{msg.Author.GetAvatarUrl()}|{msg.Attachments}");
                else file.Add($"{msg.Author.Username}|{msg.Content}|{msg.Author.GetAvatarUrl()}");
                File.WriteAllLines(@"C:\Users\Owner\Documents\Visual Studio 2017\Projects\SelfBot\SelfBot\Files\saves.txt", file);
                await Context.Message.ModifyAsync(x => x.Content = "Saved!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        [Command("show", RunMode = RunMode.Async)]
        public async Task Show(int ID)
        {
            try { 
            var file = File.ReadAllLines(@"C:\Users\Owner\Documents\Visual Studio 2017\Projects\SelfBot\SelfBot\Files\saves.txt");
            string[] items = file[ID].Split('|');
            EmbedBuilder emb;
                if (items.Count() == 4) emb = new EmbedBuilder().WithTitle(items[0]).WithDescription(items[1]).WithThumbnailUrl(items[2]).WithImageUrl(items[3]);
                else emb = new EmbedBuilder().WithTitle(items[0]).WithDescription(items[1]).WithThumbnailUrl(items[2]);
            await Context.Message.DeleteAsync();
            await Context.Channel.SendMessageAsync("",embed: emb);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        */
        #endregion
    }

}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Net;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Text.RegularExpressions;

namespace SelfBot
{
    public class Commands : ModuleBase
    {
        WebClient web = new WebClient();
        Random rdm = new Random();

        [Command("eval"), Summary("Runs the given C# code and returns the output.")]
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

        [Command("deed"), Summary("Dick Extraction and Evaluation Device.")]
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

        [Command("quirk"), Summary("Text quirks.")]
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

        [Command("fuse"), Summary("Fuses the two inputted words/names.")]
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

        [Command("lenny"), Summary("Converts &l to lenny face, or displays stats.")]
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

        [Command("amal"), Summary("Amalgamates the given string.")]
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

        [Command("f"), Summary("F")]
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

        [Command("pain"), Summary("Spelling with bread!")]
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

        [Command("spell"), Summary("Spelling with other emotes!")]
        public async Task Spell(string emote, [Remainder]string word)
        {
            if (word.Contains("|"))
            {
                foreach (string value in word.Split('|'))
                {
                    await Spell(emote,value);
                }
            }
            else
            {
                if (word.Length > 6 && word != "( ͡° ͜ʖ ͡°)") await Context.Channel.SendMessageAsync(":robot: TOO MANY EMOTES!!! :robot:");
                else
                {
                    BreadBuilder bb = new BreadBuilder();
                    string output = bb.build(word).Replace(":bread:",emote) ;
                    await Context.Channel.SendMessageAsync(output);
                }
            }
        }

        [Command("waggle"), Summary(";)")]
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
                    string header = "+" + command.Name;
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
        
        [Command("subscript"), Summary("Convert text to subscript")]
        public async Task Subscript([Remainder] string text)
        {
            string msg = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (Constants.ALPHABET.Contains(text[i])) msg += Constants.SUBSCRIPT[Constants.ALPHABET.IndexOf(text[i])];
                else msg += text[i];
            }
            await Context.Message.ModifyAsync(x => x.Content = msg);
        }

        [Command("small"), Summary("Convert text to small")]
        public async Task Small([Remainder] string text)
        {
            foreach(IGuildUser u in (await Context.Guild.GetUsersAsync()))
            {
                await u.ModifyAsync(x => x.Nickname = null);
            }

            string msg = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (Constants.ALPHABET.Contains(text[i])) msg += Constants.SMALL[Constants.ALPHABET.IndexOf(text[i])];
                else msg += text[i];
            }
            await Context.Message.ModifyAsync(x => x.Content = msg);
        }

        [Command("superscript"), Summary("Convert text to superscript")]
        public async Task Superscript([Remainder] string text)
        {
            string msg = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (Constants.ALPHABET.Contains(text[i])) msg += Constants.SUPERSCRIPT[Constants.ALPHABET.IndexOf(text[i])];
                else msg += text[i];
            }
            await Context.Message.ModifyAsync(x => x.Content = msg);
        }
        
        [Command("brady"), Summary("Gets a random Brady quote.")]
        public async Task Brady()
        {
            var quotes = File.ReadAllLines("Files/bradyQuotes.txt");
            await ReplyAsync("```"+quotes[rdm.Next(quotes.Count())]+"```");
        }
        
        [Command("hey"), Summary("Sends random Skyrim guard quote.")]
        public async Task Hey()
        {
            var quotes = File.ReadAllLines("Files/guardQuotes.txt");
            string quote = quotes[rdm.Next(quotes.Count())];
            await Context.Message.ModifyAsync(x => x.Content = quote);
        }
        
        [Command("away"), Summary("Toggle whether I'm away or not.")]
        public async Task Away()
        {
            if (!Bot.away)
            {
                Bot.away = true;
                await ReplyAsync("Now away.");
            }
            else
            {
                Bot.away = false;
                await ReplyAsync("No longer away.");
            }
        }

        [Command("learn"), Summary("Toggle whether I'm learning or not.")]
        public async Task Learn()
        {
            if (!Bot.learning)
            {
                Bot.learning = true;
                await ReplyAsync("Now learning.");
            }
            else
            {
                Bot.learning = false;
                await ReplyAsync("No longer learning.");
            }
        }

        [Command("cleanup"), Summary("Cleanup learning data.")]
        public async Task Cleanup()
        {
            Console.WriteLine("Disabling Learning");
            bool wasLearning = Bot.learning;
            Bot.learning = false;

            var starters = File.ReadAllLines("Files/Learning/starters.txt");
            List<string> newStarters = new List<string>();
            string[] invalidPrefixs = { "/", "+", ";", "-", "!", ".", "#",">","_","$","%","(","^","@",",",".","`","["," ",")","'","{","}","~","=" };
            string[] invalidChars = { "+", ";", "-", "!", ".", "#",":", "_", "$", "%","(", "^", "@", ",", "`", "[", " ", ")", "'", "{", "}", "~", "=" };
            Console.Write("Confirming validity of starters... ");
            foreach (string s in starters)
            {
                bool valid = true;
                string newS = s.ToLower();
                if (!s.Contains("/>/")) valid = false;
                foreach (string chr in invalidPrefixs) if (s.StartsWith(chr)) valid = false;
                foreach (string chr in invalidChars) if (s.Contains(chr)) valid = false;
                if (newStarters.Where(x => x.StartsWith(newS + "/>/")).Count() > 0) valid = false;
                if (valid) newStarters.Add(newS);
            }
            Console.WriteLine("Done.");
            Console.Write("Saving new list... ");
            File.WriteAllLines("Files/Learning/starters.txt", newStarters);
            Console.WriteLine("Done.");

            var words = Directory.GetFiles("Files/Learning");
            foreach (string word in words)
            {
                if (word.EndsWith(".wrd"))
                    foreach (string chr in invalidPrefixs)
                    {
                        if (word.Split(new char[] { '/', '\\' })[2].StartsWith(chr))
                        {
                            File.Delete(word);
                            break;
                        }
                    }
            }

            words = Directory.GetFiles("Files/Learning");
            foreach (string word in words) if (word.EndsWith(".wrd"))
                {
                    Console.WriteLine("Cleaning: " + word);
                    var responses = File.ReadAllLines(word);
                    List<string> newResponses = new List<string>();

                    bool valid = true;
                    foreach(string r in responses)
                    {
                        foreach (string chr in invalidChars) if (r.Contains(chr)) valid = false;

                        if (valid) newResponses.Add(r);
                    }
                    File.WriteAllLines(word, newResponses);

                }

            Bot.learning = wasLearning;
            await ReplyAsync("Done");
        }

        [Command("mess"),Summary("Translates the inputted text a bunch of times.")]
        public async Task Mess([Remainder]string input)
        {
            //translate.googleapis.com/translate_a/single?client=gtx&sl=srcLanguage&tl=dstLanguage&dt=t&q=srcText
            string[] languages = {"af","sq","ar","az","eu","bn","be","bg","ca","zh-CN","zh-TW","hr","cs","da","nl","eo","et","tl","fi","fr","gl","ka","de","el","en","gu","ht","iw","hu","is","id",
                "ga","it","ja","kn","ko","la","lv","lt","mk","ms","mt","no","fa","pl","pt","ro","sr","sk","sl","es","sw","sv","ta","te","th","tr","uk","ur","vi","cy","yi" };

            string text = input;

            for (int i = rdm.Next(10,21); i >= 0; i--)
            {
                var lang = languages[rdm.Next(languages.Count())];
                if (i == 0 || i%2 == 0)  lang = "en";
                Console.WriteLine("Language used: " + lang);
                web.DownloadFile($"https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl={lang}&dt=t&q={text}", @"Files\translation.txt");
                text = File.ReadAllText(@"Files\translation.txt");
                int[] quotesIndex = new int[2];
                int aCount = 0;
                for (int o = 0; o < text.Length; o++)
                {
                    if (text[o] == '"')
                    {
                        quotesIndex[aCount] = o;
                        aCount++;
                        if (aCount == 2) break;
                    }
                }
                text = text.Substring(quotesIndex[0] + 1, quotesIndex[1] - 1 - quotesIndex[0]);
                Console.WriteLine("\t" + text);
            }

            await Context.Channel.SendMessageAsync(":robot::speech_balloon: " + text);



        }

        [Command("quote"), Summary("Shows the inputted message")]
        public async Task Quote(ulong messageID)
        {
            var msgs = await Context.Channel.GetMessagesAsync().Flatten();
            var match = msgs.Where(x => x.Id == messageID);
            var msg = match.FirstOrDefault();
            if (msg == null)
            {

            }
            else
            {
                JEmbed msgEmb = new JEmbed();
                msgEmb.Author = new JEmbedAuthor(x =>
                {
                    x.Name = GetName(msg.Author as IGuildUser);
                    x.IconUrl = msg.Author.GetAvatarUrl();
                });
                msgEmb.Description = msg.Content;
                msgEmb.ColorStripe = GetColor(msg.Author);
                msgEmb.Footer.Text = msg.Timestamp.LocalDateTime.AddHours(-4).ToString("dddd, MMM dd hh:mm tt");
                await Context.Message.ModifyAsync(x => { x.Content = ""; x.Embed = msgEmb.Build(); });
            }
        }

        [Command("party"), Summary("How's the party?")]
        public async Task Party()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("headless");
            IWebDriver chrome = new ChromeDriver(chromeOptions);

            string[] charIDs = { "3969285", "4678486","4528604","5517451","5625203"};


            foreach (string id in charIDs)
            {
                chrome.Navigate().GoToUrl("https://www.dndbeyond.com/characters/"+id);

                var wait = new WebDriverWait(chrome, new TimeSpan(0, 0, 10));
                wait.Until(ExpectedConditions.ElementExists(By.ClassName("ct-character-tidbits__avatar")));

                var imgElem = chrome.FindElement(By.ClassName("ct-character-tidbits__avatar"));
                var imgUrl = imgElem.GetCssValue("background-image").Replace("url(\"", "").Replace("\")", "");

                String pageSource = chrome.FindElement(By.TagName("body")).Text;

                var charData = pageSource.Split('\n');
                var name = charData[3].Trim('\r');
                var health = charData[9].Trim('\r');
                var GenderRaceLevel = charData[4].Trim('\r');
                var exp = charData[7].Trim('\r');
                var cLevel = charData[5].Trim('\r');
                var nLevel = charData[6].Trim('\r');

                JEmbed emb = new JEmbed();
                emb.Title = name;
                emb.ThumbnailUrl = imgUrl;
                var grl = Regex.Replace(GenderRaceLevel, "[A-Z]", " $&").Trim();
                emb.Description = grl;
                emb.Fields.Add(new JEmbedField(x =>
                {
                    x.Header = ":heart: Health";
                    x.Text = health;
                    x.Inline = true;
                }));
                emb.Fields.Add(new JEmbedField(x =>
                {
                    x.Header = ":arrow_up: EXP";
                    x.Text = cLevel + " -> " + exp + " -> " + nLevel;
                    x.Inline = true;
                }));
                emb.ColorStripe = new Color(rdm.Next(256), rdm.Next(256), rdm.Next(256));
                await ReplyAsync("", embed: emb.Build());
            }
            chrome.Close();
        }




        public static Color GetColor(IUser User)
        {
            var user = User as IGuildUser;
            if (user != null)
            {
                if (user.RoleIds.ToArray().Count() > 1)
                {
                    var role = user.Guild.GetRole(user.RoleIds.ElementAtOrDefault(1));
                    return role.Color;
                }
                else return Constants.DEFAULT_COLOUR;
            }
            else return Constants.DEFAULT_COLOUR;
        }
        string GetName(IGuildUser user)
        {
            if (user.Nickname != null) return user.Nickname;
            else return user.Username;
        }
        
    }

}

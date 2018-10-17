using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SelfBot
{
    public static class Brain
    {
        static string[] words;
        
        static Random rdm = new Random();



        static Brain()
        {
            FileStream file = null;
            if (!Directory.Exists("Files/Learning")) Directory.CreateDirectory("Files/Learning");
            if (file != null) file.Close();
        }

        public static void Read(string sentence)
        {
            words = sentence.Split(' ');
            for (int i = 0; i < words.Count(); i++) Learn(i);
        }

        static void Learn(int word)
        {
            if (word == 0) addStarter(words[word]);

            string[] wordData;
            if (!isKnown(words[word]))
            {
                var file = File.Create($"Files/Learning/{words[word]}.wrd");
                file.Close();
                wordData = new string[0];
            }
            else wordData = File.ReadAllLines($"Files/Learning/{words[word]}.wrd");
            
            
            

            bool found = false;
            if (word < words.Count()-1)
            {
                for (int i = 0; i < wordData.Count(); i++)
                { 
                    if (wordData[i].Split(new string[] { "/>/" }, StringSplitOptions.None)[0].ToLower() == words[word + 1].ToLower())
                    {
                        found = true;

                        var newWords = wordData[i].Split(new string[] { "/>/" }, StringSplitOptions.None);
                        newWords[1] = Convert.ToString(Convert.ToInt64(newWords[1]) + 1);
                        wordData[i] = $"{newWords[0]}/>/{newWords[1]}";
                        break;
                    }
                }

                if (!found)
                {
                    var dataList = wordData.ToList();
                    dataList.Add(words[word + 1] + "/>/1");
                    wordData = dataList.ToArray();
                }
            }
            else
            {
                for (int i = 0; i < wordData.Count(); i++)
                {
                    if (wordData[i].Split(new string[] { "/>/" }, StringSplitOptions.None)[0] == "[[[END]]]")
                    {
                        found = true;

                        var newWords = wordData[i].Split(new string[] { "/>/" }, StringSplitOptions.None);
                        newWords[1] = Convert.ToString(Convert.ToInt64(newWords[1]) + 1);
                        wordData[i] = $"{newWords[0]}/>/{newWords[1]}";

                        break;
                    }
                }

                if (!found)
                {
                    var dataList = wordData.ToList();
                    dataList.Add("[[[END]]]" + "/>/1");
                    wordData = dataList.ToArray();
                }
            }
            File.WriteAllLines($"Files/Learning/{words[word]}.wrd", wordData);
            
        }

        static void addStarter(string word)
        {
            var starters = File.ReadAllLines("Files/Learning/starters.txt");

            bool found = false;
            for(int i = 0; i < starters.Count();i++)
            {
                if (starters[i].ToLower().StartsWith(word.ToLower()))
                {
                    found = true;
                    var usage = Convert.ToInt64(starters[i].Split(new string[] { "/>/" },StringSplitOptions.None)[1]);
                    usage++;
                    starters[i] = $"{starters[i].Split(new string[] { "/>/" }, StringSplitOptions.None)[0]}/>/{usage}";
                    break;
                }
            }

            if (!found)
            {
                var newStarters = starters.ToList();
                newStarters.Add($"{word}/>/1");
                starters = newStarters.ToArray();
            }
            
            File.WriteAllLines("Files/Learning/starters.txt",starters);

        }
        
        static bool isKnown(string word)
        {
            return File.Exists($"Files/Learning/{word}.wrd");
        }
        
        public static string GenerateSentence()
        {
            string[] starters = File.ReadAllLines("Files/Learning/starters.txt");

            Dictionary<string, long> starterVals = new Dictionary<string, long>();
            foreach(string w in starters)
            {

                if (w.Contains("/>/"))
                {
                    var d = w.Split(new string[] { "/>/" }, StringSplitOptions.None);
                    try
                    {
                        starterVals.Add(d[0], Convert.ToInt64(d[1]));
                    }
                    catch (Exception e) { Console.WriteLine(e.Message); }
                }
                else Console.WriteLine("No divider.");
                
            }

            var starterValList = starterVals.ToList();
            var orderedStarters = starterValList.OrderByDescending(x=>x.Value);

            var starterIndex = rdm.Next(10);
            string starter = orderedStarters.ElementAt(starterIndex).Key;



            string response = starter;
            string word = starter;
            string lastWord = starter;
            do
            {
                try
                {
                    string[] nextWords = File.ReadAllLines($"Files/Learning/{word}.wrd");


                    Dictionary<string, long> wordVals = new Dictionary<string, long>();
                    foreach (string w in nextWords)
                    {
                        if (w.Contains("/>/"))
                        {
                            var d = w.Split(new string[] { "/>/" }, StringSplitOptions.None);
                            try
                            {
                                wordVals.Add(d[0], Convert.ToInt64(d[1]));
                            }
                            catch (Exception e) { Console.WriteLine(e.Message); }
                        }

                        else Console.WriteLine("No divider, skipped.");
                    }

                    var wordValList = wordVals.ToList();
                    var orderedWords = wordValList.OrderByDescending(x => x.Value);

                    string nextWord = "";
                    do
                    {
                        var wordIndex = rdm.Next(10);
                        nextWord = orderedWords.ElementAt(wordIndex).Key;
                    } while (nextWord == lastWord);
                    
                    if (nextWord == "[[[END]]]") break;
                    if (response.Split(' ').Count() > 30) break;
                    else
                    {
                        response += " " + nextWord;
                        if (response.Length > 1700) break;
                        word = nextWord;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            } while (true);
            return response;
        }
    }
}

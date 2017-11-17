using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
namespace SelfBot
{
    public class Funcs
    {
        public static List<string> GetList(string listName)
        {
            return File.ReadAllLines($@"Files\Lists\{listName}.list").ToList();
        }

        //file name max 172

        public static List<string> GetNums(string listName)
        {
            return File.ReadAllLines($@"Files\Lists\{listName}[NUMS].list").ToList();
        }

        public static void SaveList(List<string> list, string name)
        {
            File.WriteAllLines($@"Files\Lists\{name}.list",list);
        }

        public static bool ListExists(string listName)
        {
            if (File.Exists($@"Files\Lists\{listName}.list")) return true;
            else return false;
        }

        public static void AddToList(string listName, string data)
        {
            File.AppendAllText($@"Files\Lists\{listName}.list", data);
        }

        public static List<string> NewList(string listName)
        {
            var list = new List<string>();
            list.Add("NULL");
            SaveList(list, listName);

            var listNums = new List<string>();
            listNums.Add("0");
            SaveList(listNums, listName + "[NUMS]");

            return list;
        }

        public static void AddResponse(string listName,string response)
        {
            var list = GetList(listName);
            var listNums = GetNums(listName);
            bool added = false;
            for (int i = 1; i < list.Count(); i++)
            {
                if (Distance(list[i],response) < 4)
                {
                    listNums[i] = Convert.ToString(Convert.ToInt32(listNums[i]) + 1);
                    SaveList(listNums,listName+"[NUMS]");
                    added = true;
                    break;
                }
            }
            if (!added)
            {
                AddToList(listName, response);
                AddToList(listName + "[NUMS]", "1");
            }
        }

        /// <summary>
        /// Compute the distance between two strings.
        /// </summary>
        public static int Distance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }
    }
}

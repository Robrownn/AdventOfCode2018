using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace src
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"./input.txt");

            List<string> twoLetters = SelectDuplicates(lines, (x => x.Count() == 2));
            List<string> threeLetters = SelectDuplicates(lines, (x => x.Count() == 3));
            var both = from s1 in twoLetters
            join s2 in threeLetters on s1 equals s2
            select s2;

            var checksum = twoLetters.Count() * threeLetters.Count();
            Console.WriteLine(checksum);

            var differByOne = SelectDifferByOne(lines);
            foreach (var item in differByOne)
            {
                Console.WriteLine(ConstructCommon(item.Key, item.Value));
            }
        }

        static List<string> SelectDuplicates(string[] lines, Func<IGrouping<char, char>, bool> predicate)
        {
            List<string> result = new List<string>();

            foreach (string line in lines)
            {
                var query = line.GroupBy(x => x)
                    .Where(predicate)
                    .Any();

                if (query)
                    result.Add(line);
            }

            return result;
        }

        static string ConstructCommon(string source, string target)
        {
            List<char> memes = new List<char>();
            for (int i = 0; i < source.Length; i++)
            {
                if (source[i] == target[i])
                    memes.Add(source[i]);
            }
            return string.Concat(memes);
        }

        static Dictionary<string, string> SelectDifferByOne(string[] lines)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            for (int i = 0; i < lines.Length; i++)
            {
                var source = lines[i];
                for (int j = i + 1; j < lines.Length; j++)
                {
                    var target = lines[j];

                    if (CharacterDifference(source, target) == 1)
                        result.Add(source, target);
                }
            }

            return result;
        }

        static int CharacterDifference(string s1, string s2)
        {
            int result = 0;
            for (int i = 0; i < s1.Length; i++)
            {
                if (!(s1[i] == s2[i]))
                    result++;
            }

            return result;
        }
    }
}
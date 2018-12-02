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
        }

        static List<string> SelectDuplicates(string[] lines, Func<IGrouping<char, char>, bool> predicate)
        {
            List<string> result = new List<string>();

            foreach (string line in lines)
            {
                var wtf = line.GroupBy(x => x)
                    .Where(predicate)
                    .Any();

                if (wtf)
                    result.Add(line);

            }

            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace partOne
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern = "\\d+";
            string[] lines = File.ReadAllLines(@"./input.txt");
            List<ElfClaim> elfClaims = new List<ElfClaim>();
            foreach (var line in lines)
            {
                elfClaims.Add(LineToElfClaim(line, pattern));
            }
        }

        static ElfClaim LineToElfClaim(string line, string pattern)
        {
            var numbers = Regex.Matches(line, pattern, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(500));

            return new ElfClaim
            {
                id = Int32.Parse(numbers[0].Value),
                    fromLeft = Int32.Parse(numbers[1].Value),
                    fromTop = Int32.Parse(numbers[2].Value),
                    length = Int32.Parse(numbers[3].Value),
                    width = Int32.Parse(numbers[4].Value)
            };
        }
    }

    struct ElfClaim
    {
        public int id { get; set; }
        public int fromLeft { get; set; }
        public int fromTop { get; set; }
        public int length { get; set; }
        public int width { get; set; }
    }
}
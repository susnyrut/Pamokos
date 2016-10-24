using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileIO
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = ""; // nuskaityk failą į input kintamąjį

            FindSportsmenWithLongestFullname(input);
            FindSportsmenWithLongestFullnameLikeAPro(input);

            SortSportsmenByFullnameLength(input);
        }

        public static void FindSportsmenWithLongestFullname(string input)
        {
            //sukarpyk input į string[]
            //surask ilgiausią vardą
            //išvesk į failą longestNameV1.txt
        }

        public static void FindSportsmenWithLongestFullnameLikeAPro(string input)
        {
            //sukarpyk input į List<string>
            //surask ilgiausią vardą
            //išvesk į failą longestNameV2.txt
        }
        
        public static void SortSportsmenByFullnameLength(string input)
        {
            //sukarpyk input į List<string>
            //surūšiuok pagal vardo ilgį
            //išvesk į failą sortedNames.txt
        }
    }
}

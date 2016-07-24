using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NumberFinderLibrary
{
    /// <summary>
    /// Number File Generator Class
    /// </summary>
    public class NumberFileGenerator
    {
        private const int maxNumber = 1000;
        private const int minNumber = 0;
        private const int rngSeed = 1234;
        private static string _fileName = @"\RandomNumberFile.txt";

        public static String FileName { get { return Directory.GetCurrentDirectory() + _fileName; } }
        public static int MaxNumberRange { get { return maxNumber; } }
        public static int MinNumberRange {  get { return minNumber; } }

        /// <summary>
        /// Generates a file of random number on each line in the current working directory
        /// Returns the current working file name 
        /// </summary>
        /// <returns>String</returns>
        public static String GenerateRandomNumberFile()
        {
            //Generate and shuffle our list of ints
            List<int> numbersList = Enumerable.Range(minNumber, maxNumber).ToList();
            Shuffle(ref numbersList);

            //remove one from random in our list
            numbersList.RemoveAt(new Random(rngSeed).Next(minNumber, maxNumber));

            //Write out the file to current working directory
            using (StreamWriter myFile = File.CreateText(FileName))
            {
                foreach(int number in numbersList)
                {
                    myFile.WriteLine(number);
                }
            }

            return FileName;
        }
        
        //Fisher-Yates random shuffling
        protected internal static void Shuffle(ref List<int> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}

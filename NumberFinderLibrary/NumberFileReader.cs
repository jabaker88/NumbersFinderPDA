using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NumberFinderLibrary
{
    /// <summary>
    /// Input handling class for Number File Generator 
    /// </summary>
    public class NumberFileReader
    {
        private static List<int> _numList = new List<int>();
        public static List<int> NumbersList { get { return _numList; } }

        protected internal static List<int> GetNumberFileList(string file)
        {
            string input = String.Empty;
            int outInt;

            using (StreamReader sr = File.OpenText(file))
            {
                while (!sr.EndOfStream)
                {
                    input = sr.ReadLine();
                    bool failedToConvert = !int.TryParse(input, out outInt);

                    if (failedToConvert)
                        throw new InvalidCastException("Invalid Number: " + input);

                    _numList.Add(outInt);
                }             
            }

            if (!ValidateNumberList(_numList))
            {
                throw new Exception("Invalid Number Range Detected");
            }

            return _numList;
        }

        protected internal static bool ValidateNumberList(List<int> list)
        {
            bool valid = true;
            int max = list.Max();
            int min = list.Min();

            if (max > list.Count)
                valid = false;

            if (min < 0)
                valid = false;

            if (list.Count < NumberFileGenerator.MaxNumberRange - 1)
                valid = false;

            return valid;
        }

        protected internal static int FindMissingSequence(List<int> list)
        {
            //if missing number is not found right away, it's zero or the last number
            int missingNo = 0;
            list.Sort();

            if (list[0] != 0)
                return 0;

            if (list[list.Count - 1] != list.Count)
                return list.Count;

            for(int i = 0; i < list.Count; i++)
            {
                int num = list[i];

                if(num != i)
                {
                    missingNo = i;
                    break;
                }
            }

            return missingNo;
        }

        /// <summary>
        /// Opens and gets the missing number sequence from a file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static String OpenAndFindMissingSequence(string file)
        {
            string output = String.Empty;

            List<int> numbersList = GetNumberFileList(file);

            output = FindMissingSequence(numbersList).ToString();

            return output;
        }
    }
}

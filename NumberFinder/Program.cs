using System;
using NumberFinderLibrary;
using System.Collections.Generic;

namespace NumberFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo key;

            //Let's create a random number file for us to use
            string file = NumberFileGenerator.GenerateRandomNumberFile();

            do
            {
                string output = String.Empty;

                try
                {
                    output = NumberFileReader.OpenAndFindMissingSequence(file);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message + "\nTry again? (y)");
                    key = Console.ReadKey();
                    if(key.Key == ConsoleKey.Y)
                    {
                        Console.Clear();
                        NumberFileReader.NumbersList.Clear();
                        continue;
                    }

                    break;
                }

                List<int> numbersList = NumberFileReader.NumbersList;
                numbersList.Sort();

                Console.WriteLine("All numbers in sequence:\n");

                foreach (int number in numbersList)
                {
                    Console.WriteLine(number);
                }

                Console.WriteLine("Missing number is: " + output);
                Console.WriteLine("Go again? (y)");
                NumberFileReader.NumbersList.Clear();
                
                key = Console.ReadKey();

                Console.Clear();
            }
            while (key.Key == ConsoleKey.Y);
        }
    }
}

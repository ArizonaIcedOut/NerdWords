// Author: Eric Pu
// File Name: Program.cs
// Project Name: PASS3
// Creation Date: April 28, 2020
// Modified Date: April 28, 2020
// Description: A recursive solution to the "Nerd Words" problem.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace PASS3
{
    class Program
    {
        // Creates a new StopWatch object for timing
        static Stopwatch stopWatch = new Stopwatch();

        static void Main(string[] args)
        {
            // Sets read path to the designated input text file
            string readPath = "SimpleTest.txt";
            // Sets write path to the designated output text file
            string writePath = "Pu_E.txt";
            
            // Creates StreamReader for the input file
            StreamReader sr = new StreamReader(readPath);

            // Wipes the output file
            File.WriteAllText(writePath, "");

            // Creates StreamWriter for the output file
            using (StreamWriter sw = new StreamWriter(writePath))
            {
                // Resets and starts the stop watch
                stopWatch.Reset();
                stopWatch.Start();

                // Loops through every line in the input
                for (int i = 0; i < File.ReadAllLines(readPath).Length; ++i)
                {
                    // Creates string word as the current line in the input file
                    string word = sr.ReadLine();

                    // Writes the word and the result on the current line in the output file
                    if (!CheckWord(word)) sw.WriteLine("{0}:NO", word);
                    else sw.WriteLine("{0}:YES", word);

                    Console.WriteLine(i);
                }
            }
        
            // Stops the stop watch, and outputs the time
            stopWatch.Stop();
            Console.WriteLine(GetTimeOutput(stopWatch));

            Console.ReadLine();
        }
        
        /// <summary>
        /// Pre: string word as the input
        /// Post: Returns bool indicating if the word is a nerd word or not
        /// Description: Checks if the inputted word is a nerd word
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private static bool CheckWord(string word)
        {
            // If the word length is 1 and the word is 'X' (the only valid 1-letter word), returns true. Otherwise, returns false
            if (word.Length == 1)
            {
                if (word == "X") return true;
                else return false;
            }
            else
            {
                // Gets index of first occurrence of 'A', 'B', and 'Y'
                int aIndex = word.IndexOf('A');
                int bIndex = word.IndexOf('B');
                int yIndex = word.IndexOf('Y');

                // If the word has no characters (result of back-to-back Ys) returns false
                // Else, if the word has an A and B (indicated by index not equal to -1), checks other conditions
                // Else, if the word as a Y, the word is split up and CheckWord is run for each substring
                // Otherwise, returns false as all checks for validity have failed
                if (word.Length == 0) return false;
                else if (aIndex != -1 && bIndex != -1)
                {
                    // If the first A occurs after the first B, returns false as that is invalid
                    // Otherwise, CheckWord is run on the word without the first A and B
                    if (aIndex > bIndex) return false;
                    else
                    {
                        // Creates new subWord as the current word
                        string subWord = word;

                        // Removes first occurrences of A and B from subWord
                        subWord = subWord.Remove(aIndex, 1);
                        subWord = subWord.Remove(bIndex - 1, 1);

                        // Returns false if the subWord is invalid
                        if (!CheckWord(subWord)) return false;
                    }
                }
                else if (yIndex != -1)
                {
                    // Splits up the word by Ys and checks if each one is valid
                    foreach (string subWord in word.Split('Y'))
                    {
                        if (!CheckWord(subWord)) return false;
                    }
                }
                else return false;
            }

            // If none of the checks for the word being invalid have gone off, then returns true as it must be a nerd word
            return true;
        }

        public static string GetTimeOutput(Stopwatch timer)
        {
            TimeSpan ts = timer.Elapsed;
            int millis = ts.Milliseconds;
            int seconds = ts.Seconds;
            int minutes = ts.Minutes;
            int hours = ts.Hours;
            int days = ts.Days;
            return "Time- Days:Hours:Minutes:Seconds.Milliseconds:" + days + ":" +
            hours + ":" +
            minutes + ":" +
            seconds + "." +
            millis;
        }
    }
}

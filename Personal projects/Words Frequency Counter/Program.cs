// text in input.txt was generated using https://randomwordgenerator.com/paragraph.php
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordsFrequencyCounter
{
    class Program
    {
        static char[] FindSeparators(string inputFile)
        {
            List<char> separators = new List<char>();
            using (StreamReader reader = new StreamReader(inputFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    foreach (char cur in line)
                    {
                        if (!Char.IsLetter(cur) && !separators.Contains(cur) && cur != '\'')
                        {
                            separators.Add(cur);
                        }
                    }
                }
            }
            return separators.ToArray();
        }

        static SortedDictionary<string, int> CalculateWordFrequencies(string inputFile, char[] separators)
        {
            SortedDictionary<string, int> wordFrequencies = new SortedDictionary<string, int>();
            using (StreamReader reader = new StreamReader(inputFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] words = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string word in words)
                    {
                        string wordInLowerCase = string.Copy(word).ToLower();
                        if (wordFrequencies.ContainsKey(wordInLowerCase))
                        {
                            wordFrequencies[wordInLowerCase]++;
                        }
                        else
                        {
                            wordFrequencies.Add(wordInLowerCase, 1);
                        }
                    }
                }
            }
            return wordFrequencies;
        }

        static void PrintWordFrequencies(SortedDictionary<string, int> wordFrequencies)
        {
            Console.WriteLine(string.Format("{0, 17} {1, 11}", "Word", "Frequency"));
            List<KeyValuePair<string, int>> wordFrequenciesList = wordFrequencies.ToList();
            wordFrequenciesList.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            foreach (KeyValuePair<string, int> kvp in wordFrequenciesList)
            {
                Console.WriteLine("{0, 17} {1, 11}", kvp.Key, kvp.Value);
            }
        }

        static void Main(string[] args)
        {
            string inputFile = "input.txt";
            Console.WriteLine("The program analyzes frequencies based on the text in input.txt");
            Console.WriteLine("The program turns all words into lower case.");
            char[] separators = FindSeparators(inputFile);
            SortedDictionary<string, int> wordFrequencies = CalculateWordFrequencies(inputFile, separators);
            PrintWordFrequencies(wordFrequencies);
        }
    }
}

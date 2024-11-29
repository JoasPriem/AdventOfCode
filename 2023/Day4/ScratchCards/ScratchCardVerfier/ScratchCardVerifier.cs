using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CubeGameVerifier.GameVerifier
{
    public class ScratchCardVerifier
    {
        private readonly string _filePath;

        public ScratchCardVerifier(string filePath)
        {
            this._filePath = filePath;

        }

        public List<int> GetScracthCardPoints()
        {
            List<int> validGameIds = new List<int>();
            StreamReader sr = new StreamReader(_filePath);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                validGameIds.Add(CalculateScratchCardPoints(line));

            }

            return validGameIds;
        }

        public int CalculateScratchCardPoints(string line)
        {

            int numberOfMatches = CalculateNumberOfMatches(line);

            int points = CalculatePoints(numberOfMatches);

            return points;
        }

        private int CalculateNumberOfMatches(string line)
        {
            int numberOfMatches = 0;
            string[] splitLine = line.Split("|");
            List<string> winningNumbers = splitLine[0].Split(" ", StringSplitOptions.RemoveEmptyEntries).Skip(2).ToList();
            List<string> playingNumbers = splitLine[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (string playingNumber in playingNumbers)
            {
                if (winningNumbers.Contains(playingNumber))
                {
                    numberOfMatches++;
                }
            }
            return numberOfMatches;
        }

        public int CalculateNumberOfScratchedCards()
        {
            int numberOfScratchesCards = 0;
            Dictionary<int, (int, int)> dictionary = CreateScractchCardCopyDictionary();
            
            foreach ((int cardNumber,(int numberOfCopies, int numberOfMatches)) in dictionary)
            {
                for (int i = 1; i <= numberOfMatches; i++) 
                {
                    if(dictionary.TryGetValue((cardNumber + i), out (int,int) value))
                    {
                        value.Item1 += numberOfCopies;
                        dictionary[cardNumber + i] = (value.Item1, value.Item2);
                    } else
                    {
                        Console.WriteLine("Something went wrong getting card entry");
                    }
                }
                numberOfScratchesCards += numberOfCopies;
            }

            return numberOfScratchesCards;

        }

        private Dictionary<int, (int, int)> CreateScractchCardCopyDictionary()
        {
            Dictionary<int,(int, int)> dictionary = new Dictionary<int,(int, int)>();
            StreamReader sr = new StreamReader(_filePath);
            string line;
            int numberOfCopies = 1;
            while ((line = sr.ReadLine()) != null)
            {
                int cardNumber = ParseCardNumber(line);
                int numberOfMatches = CalculateNumberOfMatches(line);
                dictionary.Add(cardNumber, (numberOfCopies, numberOfMatches));
            }
            return dictionary;
        }

        private int ParseCardNumber(string line)
        {
            string firstSplit = line.Split(" ", StringSplitOptions.RemoveEmptyEntries)[1];
            string secondSplit = firstSplit.Split(":")[0];
            return Int32.Parse(secondSplit);
        }

        private int CalculatePoints(int numberOfMatches)
        {
            if (numberOfMatches == 0)
            {
                return 0;
            }
            return (int)Math.Pow(2, (numberOfMatches - 1));
        }


    }
}

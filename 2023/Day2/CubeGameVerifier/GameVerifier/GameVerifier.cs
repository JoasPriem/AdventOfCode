using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CubeGameVerifier.GameVerifier
{
    public class GameVerifier
    {
        private readonly string _filePath;
        private int numberOfRedCubes = 12;
        private int numberOfGreenCubes = 13;
        private int numberOfBlueCubes = 14;

        public GameVerifier(string filePath)
        {
            this._filePath = filePath;

        }

        public List<int> GetAllValidGameIds()
        {
            List<int> validGameIds = new List<int>();
            StreamReader sr = new StreamReader(_filePath);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                if (IsValidGame(line))
                {
                    validGameIds.Add(ParseGameId(line));
                }
            }

            return validGameIds;
        }

        public List<int> GetPowersOfMinimumGames()
        {
            List<int> powers = new List<int>();
            StreamReader sr = new StreamReader(_filePath);
            string line;
            while ((line = sr.ReadLine()) != null)
            {

                powers.Add(GetPowerOfMinimumGame(line));
            }
            return powers;
        }

        private bool IsValidGame(string line)
        {
            List<string> throwStats = ParseAllThrowStats(line);
            bool isValidGame = true;
            foreach (string throwStat in throwStats)
            {
                if (!IsValidThrow(throwStat))
                {
                    isValidGame = false;
                    break;
                }
            }
            return isValidGame;
        }

        private bool IsValidThrow(string throwStats)
        {
            return (ParseNumberOfCubes(throwStats, Colors.Red) <= numberOfRedCubes &&
                ParseNumberOfCubes(throwStats, Colors.Green) <= numberOfGreenCubes &&
                ParseNumberOfCubes(throwStats, Colors.Blue) <= numberOfBlueCubes);
        }

        private enum Colors
        {
            Red,
            Green,
            Blue
        }
        private int ParseNumberOfCubes(string throwStats, Colors color) 
        {
            string regexPattern;
            switch (color) { 
                case Colors.Red:
                    regexPattern = @"(\d+)(?=\s+red)";
                break;
                case Colors.Green:
                    regexPattern = @"(\d+)(?=\s+green)";
                break; 
                case Colors.Blue:
                    regexPattern = @"(\d+)(?=\s+blue)";
                break;
                default:
                    return 0;
                        
            }   

            Match match = Regex.Match(throwStats, regexPattern);
            if (match.Success) { 
                return Int32.Parse(match.Groups[1].Value);
            }
            return 0;
        }



        private List<string> ParseAllThrowStats(string line)
        {
            List<string> throwStats = new List<string>();
            string regexFilter = "[;:](.*?)(?=[;:]|$)";
            MatchCollection matches = Regex.Matches(line, regexFilter);

            foreach (Match match in matches)
            {
                throwStats.Add(match.Value);
            }
            return throwStats;
        }

        private int ParseGameId(string line)
        {
            string regexFilter = @"(?<=Game\s)(\d+)";
            string match = Regex.Match(line, regexFilter).Value;
            return Int32.Parse(match);
        }

        private int GetPowerOfMinimumGame(string game)
        {
            int minRed = 0;
            int minGreen = 0;
            int minBlue = 0;
          
            List<string> throwStats = ParseAllThrowStats(game);

            foreach (string throwStat in throwStats)
            {
                minRed = Math.Max(minRed, ParseNumberOfCubes(throwStat, Colors.Red));
                minGreen = Math.Max(minGreen, ParseNumberOfCubes(throwStat, Colors.Green));
                minBlue = Math.Max(minBlue, ParseNumberOfCubes(throwStat, Colors.Blue));
            }

            return minRed * minGreen * minBlue;
        }

    }
}

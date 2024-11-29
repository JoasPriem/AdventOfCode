using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ParseCalibrationValues.CallibrationValueParser
{
    public class CallibrationValueParser
    {
        readonly string callibrationValueFilePath;
        readonly Dictionary<string, int> _digitMap = new Dictionary<string, int>
            {
                { "zero", 0 },
                { "one", 1 },
                { "two", 2 },
                { "three", 3 },
                { "four", 4 },
                { "five", 5 },
                { "six", 6 },
                { "seven", 7 },
                { "eight", 8 },
                { "nine", 9 }
            };
        public CallibrationValueParser(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Path is: " + path);
            }
            callibrationValueFilePath = path;
        }
        public List<int> GetCallibrationValues(bool includedSpelledDigits)
        {
            List<int> callibrationValues = new List<int>();
            try
            {
                StreamReader reader = File.OpenText(callibrationValueFilePath);
                string regexFilter = GetRegexFilter(includedSpelledDigits);
                string? line;
                
                while ((line = reader.ReadLine()) != null)
                {
                    callibrationValues.Add(ParseCallibrationValueFromLine(line, regexFilter));
                }
            }
            catch (Exception exc)
            {
                Console.Error.WriteLine("An error occured while streaming lines from file: " + exc);
            }

            return callibrationValues;
        }

        private int ParseCallibrationValueFromLine(string line, string regexFilter)
        {
    
            var firstMatch = FindFirstMatchingDigits(line, regexFilter);
            int first = firstMatch != null ? MapToInt(firstMatch) : 0;
            
            var lastMatch = FindLastMatchingDigits(line, regexFilter);
            int last = lastMatch != null ? MapToInt(lastMatch) : 0;


            return 10 * first + last;
        }

        private string GetRegexFilter(bool includedSpelledDigits)
        {
            string matchSpelledNumbers = "zero|one|two|three|four|five|six|seven|eight|nine";
            string regexFilter;

            // Set up the regex pattern based on whether spelled digits are included
            if (includedSpelledDigits)
            {
                // Use a lookahead to allow for overlapping matches
                regexFilter = $@"[0-9]|(?=({matchSpelledNumbers}))({matchSpelledNumbers})";
            }
            else
            {
                regexFilter = @"[0-9]";
            }

            return regexFilter;
        }

        private string? FindLastMatchingDigits(string line, string regexFilter)
        {

            MatchCollection matches = Regex.Matches(line, regexFilter, RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
           
            return matches.Count() > 0 ? matches.First().ToString() : null ;
        }


        private string? FindFirstMatchingDigits(string line, string regexFilter)
        {

            MatchCollection matches = Regex.Matches(line, regexFilter, RegexOptions.IgnoreCase);

            return matches.Count() > 0 ? matches.First().ToString() : null;
        }

        private int MapToInt(string match)
        {
            int output;
            if (Int32.TryParse(match, out output)) { }
            else if (_digitMap.TryGetValue(match, out output)) { }
            else
            {
                Console.WriteLine("Mapping to int failed");
                output = 0;
            }

            return output;
        }
    }
}

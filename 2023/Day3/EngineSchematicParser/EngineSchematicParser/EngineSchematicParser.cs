using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CubeGameVerifier.GameVerifier
{
    public class EngineSchematicParser
    {
        private readonly string _filePath;
        private readonly List<string> _enginePartSchematic;
        public EngineSchematicParser(string filePath)
        {
            _filePath = filePath;
            _enginePartSchematic = ConvertTextFileToListOfString(_filePath);
        }

        public List<int> GetValidEnginePartNumbers()
        {
            List<int> validEnginePartNumbers = new List<int>();
            for (int i = 0; i < _enginePartSchematic.Count; i++)
            {
                string currentRow = _enginePartSchematic.ElementAt(i);
                string currentNumber = String.Empty;
                bool hasCurrentNumberAdjecentSymbol = false;

                for (int j = 0; j < currentRow.Length; j++)
                {
                    char currentChar = currentRow[j];
                    if (Char.IsNumber(currentChar))
                    {

                        currentNumber += currentChar;

                        if (HasAdjecentSymbol(i, j))
                        {
                            hasCurrentNumberAdjecentSymbol = true;

                        }
                    }
                    else if (!String.IsNullOrEmpty(currentNumber))
                    {
                        if (hasCurrentNumberAdjecentSymbol)
                        {
                            Console.WriteLine(currentNumber);
                            validEnginePartNumbers.Add(Int32.Parse(currentNumber));
                            hasCurrentNumberAdjecentSymbol = false;
                        }
                        currentNumber = String.Empty;
                    }
                }

                if (!String.IsNullOrEmpty(currentNumber))
                {
                    if (hasCurrentNumberAdjecentSymbol)
                    {
                        validEnginePartNumbers.Add(Int32.Parse(currentNumber));
                        hasCurrentNumberAdjecentSymbol = false;
                    }
                    currentNumber = String.Empty;
                }
            }
            return validEnginePartNumbers;
        }

        public List<int> GetGearRatios()
        {

            List<int> gearRatios = new List<int>();
            List<(int, int)> gearCoordinates = GetGearCoordinates();
            int gearRatio;
            foreach ((int iIndex, int jIndex) in gearCoordinates)
            {
                gearRatio = CalculateGearRatio(iIndex, jIndex);
                if (gearRatio > 0)
                {
                    gearRatios.Add(gearRatio);
                }
            }
            return gearRatios;
        }



        private List<string> ConvertTextFileToListOfString(string textFilePath)
        {
            StreamReader reader = new StreamReader(_filePath);

            string? line;
            List<string> engineSchematic = new List<string>();

            while ((line = reader.ReadLine()) != null)
            {
                engineSchematic.Add(line);
            }

            return engineSchematic;
        }

        private bool HasAdjecentSymbol(int iIndex, int jIndex)
        {

            for (int i = (iIndex - 1); i <= (iIndex + 1); i++)
            {
                for (int j = jIndex - 1; j <= (jIndex + 1); j++)
                {
                    try
                    {
                        char currentChar = _enginePartSchematic.ElementAt(i)[j];
                        if (!Char.IsDigit(currentChar) && currentChar != '.')
                        {
                            return true;
                        }
                    }
                    catch { }
                }
            }
            return false;
        }

        private List<(int, int)> GetGearCoordinates()
        {
            List<(int, int)> gearCoordinates = new List<(int, int)>();
            for (int i = 0; i < _enginePartSchematic.Count; i++)
            {
                string currentRow = _enginePartSchematic.ElementAt(i);

                for (int j = 0; j < currentRow.Length; j++)
                {
                    char currentChar = currentRow[j];
                    if (currentChar == '*')
                    {
                        gearCoordinates.Add((i, j));
                    }
                }
            }
            return gearCoordinates;
        }

        private int CalculateGearRatio(int iGearIndex, int jGearIndex)
        {
            int gearNumberOne = 0;
            (int, int) startIndexNumberOne = (-1, -1);
            int gearNumberTwo = 0;


            for (int i = (iGearIndex - 1); i <= (iGearIndex + 1); i++)
            {
                for (int j = jGearIndex - 1; j <= (jGearIndex + 1); j++)
                {
                    try
                    {
                        char currentChar = _enginePartSchematic.ElementAt(i)[j];
                        if (Char.IsDigit(currentChar))
                        {
                            (int parsedNumber, (int, int) startIndex) = parseFullNumber(i, j);
                            if (gearNumberOne == 0)
                            {
                                gearNumberOne = parsedNumber;
                                startIndexNumberOne = startIndex;
                            }
                            else if (gearNumberTwo == 0 && startIndex != startIndexNumberOne)
                            {
                                gearNumberTwo = parsedNumber;
                                return gearNumberOne * gearNumberTwo;
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        Console.Error.WriteLine(exc.Message);
                        Console.Error.WriteLine(exc.StackTrace);
                        Console.WriteLine(iGearIndex + "   " + jGearIndex + "    "+ _enginePartSchematic.First().Length);
                    }
                }
            }
            return 0;
        }

        private (int, (int, int)) parseFullNumber(int i, int j)
        {
            string rowOfNumber = _enginePartSchematic.ElementAt(i);
            int numberStartIndex = j;

            while (numberStartIndex > 0 && char.IsNumber(rowOfNumber[numberStartIndex - 1]))
            {
                numberStartIndex--;
            }

            char firstNumber = rowOfNumber[numberStartIndex];
            string fullNumber = firstNumber.ToString();
            int currentIndex = numberStartIndex + 1;


            while (currentIndex < rowOfNumber.Length && char.IsDigit(rowOfNumber[currentIndex]))
            {
                fullNumber += rowOfNumber[currentIndex];
                currentIndex++;
            }

            return (Int32.Parse(fullNumber), (i, numberStartIndex));
        }
    }
}

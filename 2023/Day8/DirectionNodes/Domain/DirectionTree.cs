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
    public class DirectionTree
    {
        readonly string filePath;
        private string directionInstructions;
        private Dictionary<string, (string, string)> directionNodesDict = new Dictionary<string, (string, string)>();
        public DirectionTree(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Path is: " + path);
            }
            filePath = path;
            InitializeDirectionTree();
        }

        public int GetNumberOfStepsToArriveAtEndLocation()
        {
            string currentNode = "AAA";
            int count = 0;
            int numberOfDirectionInstructions = this.directionInstructions.Length;
            char currentDirection;

            while (currentNode != "ZZZ")
            {
                currentDirection = this.directionInstructions.ElementAt(count % numberOfDirectionInstructions);
                if (currentDirection == 'L')
                {
                    currentNode = this.directionNodesDict[currentNode].Item1;
                }
                else if (currentDirection == 'R')
                {
                    currentNode = this.directionNodesDict[currentNode].Item2;
                }
                else
                {
                    Console.Error.WriteLine("Incorrect direction was given: " + currentDirection);
                    break;
                }

                count++;
            }
            return count;
        }


        public int GetNumberOfStepsToArriveAtEndLocationGhostEdition()
        {
            string[] currentNodes = GetAllNodesEndingWith('A');
            List<(int, int, int[])> loopStats = new List<(int, int, int[])>();

            foreach (string node in currentNodes)
            {
                loopStats.Add(GetLoopInformation(node, "Z"));
            }

            int[] loopLengths = loopStats.Select(kvp => kvp.Item2).ToArray();

            int lcm = LCMArray(loopLengths);

            return lcm;
        }


        private (int, int, int[]) GetLoopInformation(string startingNode, string endingString)
        {
            string currentNode = startingNode;
            int steps = 0;
            bool loopCompleted = false;
            int numberOfDirectionInstructions = this.directionInstructions.Length;
            char currentDirection;
            Dictionary<string, (int, int)> previousNodes = new Dictionary<string, (int, int)>() { { startingNode, (0,0) } };

            while (!loopCompleted)
            {
                int directionInstructionIndex = steps % numberOfDirectionInstructions;
                currentDirection = this.directionInstructions.ElementAt(directionInstructionIndex);


                if (currentDirection == 'L')
                {
                    currentNode = this.directionNodesDict[currentNode].Item1;
                }
                else if (currentDirection == 'R')
                {
                    currentNode = this.directionNodesDict[currentNode].Item2;
                }
                else
                {
                    Console.Error.WriteLine("Incorrect direction was given: " + currentDirection);
                    break;
                }

                steps++;
                if (previousNodes.ContainsKey(currentNode) && previousNodes[currentNode].Item2 == directionInstructionIndex)
                {
                    loopCompleted = true;   
                }
                else
                {
                    previousNodes.TryAdd(currentNode, (steps,directionInstructionIndex));
                }
            }

            int startOfLoop = previousNodes[currentNode].Item1;
           
            int lenghtOfLoop = steps - startOfLoop;
            int[] targetNodeIndexes = previousNodes
                .Where(kvp => kvp.Key.EndsWith(endingString))
                .Where(kvp => kvp.Value.Item1 >= startOfLoop)
                .Select(kvp => kvp.Value.Item1)
                .ToArray();

            return (startOfLoop, lenghtOfLoop, targetNodeIndexes);
        }


        private string[] GetAllNodesEndingWith(char endingChar)
        {
            List<string> nodes = new List<string>();
            foreach (string nodeName in this.directionNodesDict.Keys)
            {
                if (nodeName.EndsWith(endingChar))
                {
                    nodes.Add(nodeName);
                }


            }
            return nodes.ToArray();
        }

        private bool AllNodesEndWith(string[] nodes, char endingChar)
        {
            foreach (string nodeName in nodes)
            {
                if (!nodeName.EndsWith(endingChar))
                {
                    return false;
                }
            }
            return true;
        }


        private void InitializeDirectionTree()
        {
            StreamReader reader = File.OpenText(filePath);
            string? line;
            int count = 0;
            while ((line = reader.ReadLine()) != null)
            {
                if (count == 0)
                {
                    directionInstructions = line;
                }
                else if (!string.IsNullOrEmpty(line))
                {
                    AddNodeToDictionary(line);
                }
                count++;
            }
        }

        private void AddNodeToDictionary(string line)
        {
            string nodeName;
            (string, string) nodeDirections;

            string[] splittedLine = line.Split(" = ", StringSplitOptions.RemoveEmptyEntries);
            nodeName = splittedLine[0];

            string[] splittedNodeDirections = splittedLine[1].Split(",");

            nodeDirections = (splittedNodeDirections[0].Substring(1, 3), splittedNodeDirections[1].Substring(1, 3));

            this.directionNodesDict[nodeName] = nodeDirections;
        }

        private static int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return Math.Abs(a);
        }

        // Method to calculate LCM of two integers
        private static int LCM(int a, int b)
        {
            return Math.Abs(a * b) / GCD(a, b);
        }

        // Method to calculate LCM of an array of integers
        public static int LCMArray(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                throw new ArgumentException("Array must not be null or empty.");

            int lcm = numbers[0];
            for (int i = 1; i < numbers.Length; i++)
            {
                lcm = LCM(lcm, numbers[i]);
            }
            return lcm;
        }

    }
}


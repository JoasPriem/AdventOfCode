using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05_1
{
    public class CrateCrane
    {
        private readonly string _filePath;
        private List<Stack<char>> stacks = new List<Stack<char>>();
        public CrateCrane(string filePath)
        {
            if (File.Exists(filePath))
            {
                _filePath = filePath;
                InitializeStacks();
            }
            else
            {
                throw new Exception("File not found");
            }
        }

        private void InitializeStacks()
        {
            var reader = new StreamReader(_filePath);
            Stack<string> rows = new Stack<string>();
            string line;
            while ((line = reader.ReadLine()) != null)
            {

                ;
                if (line.Contains("1"))
                {
                    int numberOfStacks = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Count();
                    for (int i = 0; i < numberOfStacks; i++)
                    {
                        stacks.Add(new Stack<char>());
                    }
                }
                else
                {
                    rows.Push(line);
                }

                if (line == "")
                {
                    reader.Close();
                    break;
                }
            }

            foreach (var row in rows)
            {
                for (int i = 0; i < row.Length; i++)
                {
                    if (row[i] == '[')
                    {
                        int index = i / 4;
                        stacks[index].Push(row[i + 1]);
                    }
                }

            }
        }

        public string GetUpperCrateNames()
        {
            string upperCrates = "";
            foreach (Stack<char> stack in stacks)
            {
                upperCrates += stack.Pop();
            }
            return upperCrates;
        }


        public void FollowInstructions()
        {
            var reader = new StreamReader(_filePath);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Contains("move"))
                {
                    string[] instructions = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    int quantity = int.Parse(instructions[1]);
                    int fromStack = int.Parse(instructions[3]) - 1;
                    int toStack = int.Parse(instructions[5]) - 1;

                    for (int i = 0; i < quantity; i++)
                    {
                        char movingCrate = this.stacks[fromStack].Pop();
                        this.stacks[toStack].Push(movingCrate);
                    }

                }

            }
            reader.Close();
        }
    }
}

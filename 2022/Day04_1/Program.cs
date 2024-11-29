using System.Linq;
using System.Runtime.CompilerServices;

int numberOfRedundantElves = 0;

while (Console.ReadLine() is { } line)
{
    string[] assignments = line.Split(',', StringSplitOptions.TrimEntries);

    int[] rangeOne = Array.ConvertAll(assignments[0].Split("-", StringSplitOptions.TrimEntries), int.Parse);
    int[] rangeTwo = Array.ConvertAll(assignments[1].Split("-", StringSplitOptions.TrimEntries), int.Parse);

    if ((rangeOne[0] >= rangeTwo[0] && rangeOne[1] <= rangeTwo[1]) ||
        (rangeTwo[0] >= rangeOne[0] && rangeTwo[1] <= rangeOne[1])){
        
        numberOfRedundantElves++;
    }
}

Console.WriteLine(numberOfRedundantElves);
await Task.Delay(5000);
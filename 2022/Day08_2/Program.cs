using System.Linq;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
List<int[]> visibleTrees = new List<int[]>();
List<int[]> forest = new List<int[]>();
string line;
while ((line = Console.ReadLine()) != null)
{
    forest.Add(Array.ConvertAll(line.Select(c => c.ToString()).ToArray(), int.Parse));
    visibleTrees.Add(Enumerable.Repeat(0, line.Length).ToArray());
}



int CalculateScenicScore(int row, int col)
{
    int scenicScoreSouth = 0;
    int scenicScoreNorth = 0;
    int scenicScoreEast = 0;
    int scenicScoreWest = 0;
    int currentTreeHeight = forest[row][col];
    
    for (int i = row + 1; i < forest.Count; i++)
    {
        scenicScoreSouth++;
        if (forest[i][col] >= currentTreeHeight)
        {
            break;
        }

    }

    for (int i = row - 1; i >= 0; i--)
    {
        scenicScoreNorth++;
        if (forest[i][col] >= currentTreeHeight)
        {
            break;
        }

    }

    for (int i = col + 1; i < forest[0].Length; i++)
    {
        scenicScoreEast++;
        if (forest[row][i] >= currentTreeHeight)
        {
            break;
        }

    }

    for (int i = col - 1; i >= 0; i--)
    {
        scenicScoreWest++;
        if (forest[row][i] >= currentTreeHeight)
        {
            break;
        }

    }
    return scenicScoreNorth * scenicScoreEast * scenicScoreSouth * scenicScoreWest;
}


int highestScenicScore = 0;



for (int row = 0; row < forest.Count; row++)
{
    for (int col = 0; col < forest.Count; col++)
    {
        highestScenicScore = Math.Max(highestScenicScore, CalculateScenicScore(row, col));

    }
}




Console.WriteLine(highestScenicScore);
await Task.Delay(5000);
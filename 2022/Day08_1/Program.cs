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

for (int row = 0; row < forest.Count; row++)
{
    int highestTree = -1;
    for (int col = 0; col < forest[row].Length; col++)
    {
        if (forest[row][col] > highestTree)
        {
            visibleTrees[row][col] = 1;
            highestTree = forest[row][col];
        }
    }
}

for (int row = 0; row < forest.Count; row++)
{
    int highestTree = -1;
    for (int col = forest[0].Length - 1; col >= 0; col--)
    {
        if (forest[row][col] > highestTree)
        {
            visibleTrees[row][col] = 1;
            highestTree = forest[row][col];
        }
    }
}




for (int col = 0; col < forest[0].Length; col++)
{
    int highestTree = -1;
    for (int row = 0; row < forest.Count; row++)
    {
        if (forest[row][col] > highestTree)
        {
            visibleTrees[row][col] = 1;
            highestTree = forest[row][col];
        }
    }
}


for (int col = 0; col < forest[0].Length; col++)
{
    int highestTree = -1;

    for (int row = forest.Count - 1; row >= 0; row--)
    {

        if (forest[row][col] > highestTree)
        {
            visibleTrees[row][col] = 1;
            highestTree = forest[row][col];
        }
    }
}





int numberOfVisibleTrees = 0;

foreach (int[] row in visibleTrees)
{
    numberOfVisibleTrees += row.Sum();
}



Console.WriteLine(numberOfVisibleTrees);
await Task.Delay(5000);
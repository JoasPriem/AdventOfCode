
int[] topThree = {0,0,0};
int caloriesOfElf = 0;
while (Console.ReadLine() is { } line)
{
    if (!string.IsNullOrEmpty(line))
    {
        caloriesOfElf += int.Parse(line);
    } else
    {
        if (caloriesOfElf > topThree.Min())
        {
            int minIndex = Array.IndexOf(topThree, topThree.Min());
            topThree[minIndex] = caloriesOfElf;
        };

        caloriesOfElf = 0;
    }
}
Console.WriteLine(topThree.Sum());
await Task.Delay(5000);
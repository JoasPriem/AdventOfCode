using System.Linq;

int totalPriority = 0;
int elfCount = 0;

string[] elfRuckSacks = new string[3];
while (Console.ReadLine() is { } ruckSack)
{

    elfRuckSacks[elfCount] = ruckSack;
    elfCount++;

    if (elfCount == 3)
    {
        char sharedItem = elfRuckSacks[0].Where(chr => elfRuckSacks[1].Contains(chr) && elfRuckSacks[2].Contains(chr)).FirstOrDefault();
        int priority = 0;

        if (char.IsLower(sharedItem))
        {
            priority = 1 + sharedItem - 'a';
        }
        else
        {
            priority = 27 + sharedItem - 'A';
        }
        totalPriority += priority;

        elfCount = 0;
    }
    
}

Console.WriteLine(totalPriority);
await Task.Delay(5000);
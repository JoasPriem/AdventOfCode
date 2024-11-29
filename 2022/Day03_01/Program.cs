int totalPriority = 0;
while (Console.ReadLine() is { } ruckSack)
{
    int priority = 0;
    string compartmentOne = ruckSack.Substring(0, ruckSack.Length / 2 );
    string compartmentTwo = ruckSack.Substring(ruckSack.Length / 2 , ruckSack.Length / 2);
    char sharedItem = compartmentOne.Where(chr => compartmentTwo.Contains(chr)).FirstOrDefault();

    if (char.IsLower(sharedItem))
    {
        priority = 1 + sharedItem - 'a';
    }
    else
    {
        priority = 27 + sharedItem - 'A';
    }
    totalPriority += priority;
}

Console.WriteLine(totalPriority);
await Task.Delay(5000);
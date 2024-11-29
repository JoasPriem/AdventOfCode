using System.Linq;
using System.Runtime.CompilerServices;


string line = Console.ReadLine();
char[] chars = line.ToCharArray();
int numberOfStreamedChars = 0;
Queue<char> charQueue = new Queue<char>();

int answer = 0;

foreach (char c in chars)
{
    numberOfStreamedChars++;
    charQueue.Enqueue(c);

    if (charQueue.Distinct().Count() == 4)
    {
        answer = numberOfStreamedChars;
        break;
    }

    if (numberOfStreamedChars >= 4)
    {
        charQueue.Dequeue();
    }
}


Console.WriteLine(answer);
await Task.Delay(5000);
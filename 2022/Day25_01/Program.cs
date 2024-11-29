using System.Linq;
using System.Runtime.CompilerServices;

double getDecimalValue(char chr)
{
    switch (chr)
    {
        case '2':
            return 2d;
        case '1':
            return 1d;
        case '0':
            return 0d;
        case '-':
            return -1d;
        case '=':
            return -2d;
        default:
            throw new NotImplementedException();
    }
}

double GetDecimalValue(string snafu)
{
    double numberInDecimal = 0;
    var reversedLine = snafu.Reverse();

    for (int i = 0; i < reversedLine.Count(); i++)
    {
        numberInDecimal += Math.Pow(5, i) * getDecimalValue(reversedLine.ElementAt(i));
    }
    return numberInDecimal;
}

double totalSum = 0;
while (Console.ReadLine() is { } line)
{

    totalSum += GetDecimalValue(line); ;
}



Console.WriteLine(totalSum);
await Task.Delay(5000);
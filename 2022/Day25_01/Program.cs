using System.Linq;
using System.Runtime.CompilerServices;

double MapSnafuToDecimalDigit(char chr)
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
string MapDecimalToSnafuDigit(int value)
{
    switch (value)
    {
        case 2:
            return "2";
        case 1:
            return "1";
        case 0:
            return "0";
        case -1:
            return "-";
        case -2:
            return "=";
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
        numberInDecimal += Math.Pow(5, i) * MapSnafuToDecimalDigit(reversedLine.ElementAt(i));
    }
    return numberInDecimal;
}

double totalSum = 0;
while (Console.ReadLine() is { } line)
{
    totalSum += GetDecimalValue(line); ;
}

string GetSnafuValue(double number)
{
    string snafu = "";
    int length = CalculateMaxSnafuLength(number);
    
    snafu += GetSnafuDigetAt(number, length - 1);
    return snafu;
}

int CalculateMaxSnafuLength(double number)
{
    int index = 0;
    double maxValue = 2 * Math.Pow(5, index);

    while (maxValue < number)
    {
        index++;
        maxValue += 2 * Math.Pow(5, index);
    }

    return index + 1;
}

string GetSnafuDigetAt(double number, int index)
{
    if (index == 0)
    {
        return MapDecimalToSnafuDigit((int)number);
    }

    double bestResidualNumber = double.MaxValue;
    int selectedModifier = 0;

    for (int i = -2; i <= 2; i++)
    {
        double residualNumber = number - Math.Pow(5, index) * i;

        if (Math.Abs(residualNumber) < Math.Abs(bestResidualNumber))
        {
            bestResidualNumber = residualNumber;
            selectedModifier = i;
        }
    }

    return MapDecimalToSnafuDigit(selectedModifier) + GetSnafuDigetAt(bestResidualNumber, index - 1);


}

Console.WriteLine(GetSnafuValue(totalSum));





await Task.Delay(500440);
using Day02_1;
using System.Collections;

int totalScore = 0;

Dictionary<GameOutcome, int> gameScores = new Dictionary<GameOutcome, int>();
// Example usage
gameScores[GameOutcome.Win] = 6;
gameScores[GameOutcome.Loss] = 0;
gameScores[GameOutcome.Draw] = 3;

Dictionary<char, int> moveValues = new Dictionary<char, int>();
moveValues['A'] = 1;
moveValues['B'] = 2;
moveValues['C'] = 3;

static char CalculateRequiredMove(char opponentMove, GameOutcome gameOutcome)
{
    int normalizedCharValue = opponentMove - 'A';
    switch (gameOutcome)
    {
        case GameOutcome.Win:
            return (char)((normalizedCharValue + 1) % 3 + (int)'A');
        case GameOutcome.Loss:
            return (char)((normalizedCharValue + 2) % 3 + (int)'A');
        case GameOutcome.Draw:
            return opponentMove;
        default:
            return opponentMove;
    }
}

while (Console.ReadLine() is { } line)
{
    int gameScore = 0;
    char opponentMove = line.Split(" ")[0][0];
    char gameOutcome = line.Split(' ')[1][0];
    
    if (gameOutcome == 'X')
    {
        gameScore += gameScores[GameOutcome.Loss];
        gameScore += moveValues[CalculateRequiredMove(opponentMove, GameOutcome.Loss)];
    }
    else if (gameOutcome == 'Y')
    {
        gameScore += gameScores[GameOutcome.Draw];
        gameScore += moveValues[CalculateRequiredMove(opponentMove, GameOutcome.Draw)];
    }
    else if (gameOutcome == 'Z')
    {
        gameScore += gameScores[GameOutcome.Win];
        gameScore += moveValues[CalculateRequiredMove(opponentMove, GameOutcome.Win)];

    }
    else
    {
        Console.WriteLine("switch case catch failed for: " + gameOutcome);
    }


    totalScore += gameScore;
}
Console.WriteLine(totalScore);
await Task.Delay(15000);
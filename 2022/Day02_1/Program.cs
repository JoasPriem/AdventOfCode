using Day02_1;

int totalScore = 0;

Dictionary<GameOutcome, int> gameScores = new Dictionary<GameOutcome, int>();
// Example usage
gameScores[GameOutcome.Win] = 6;
gameScores[GameOutcome.Loss] = 0;
gameScores[GameOutcome.Draw] = 3;


while (Console.ReadLine() is { } line)
{
    int gameScore = 0;
    char opponentMove = line.Split(" ")[0][0];
    char myMove = line.Split(' ')[1][0];

    switch (opponentMove - myMove)
    {
        case -23:
            gameScore += gameScores[GameOutcome.Draw];
            break;
        case -24:
        case -21:
            gameScore += gameScores[GameOutcome.Win];
            break;
        case -25:
        case -22:
            gameScore += gameScores[GameOutcome.Loss];
            break;
        default:
            throw new Exception("Failed to assign gameOutcome");
    }

    switch (myMove)
    {
        case 'X':
            gameScore += 1;
            break;
        case 'Y':
            gameScore += 2;
            break;
        case 'Z':
            gameScore += 3;
            break;
        default:
            throw new Exception("Failed to assign gameOutcome");
    }
    totalScore += gameScore;
}
Console.WriteLine(totalScore);
await Task.Delay(5000);
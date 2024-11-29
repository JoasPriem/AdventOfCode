List<(int, int)> knots = new List<(int, int)>();
HashSet<(int, int)> tailLocationHistory = new HashSet<(int, int)>() { (0, 0) };
int numberOfKnots = 9;


for (int i = 0; i < numberOfKnots; i++)
{
    knots.Add((0, 0));
}

void MoveHead(string direction)
{
    var head = knots[0];
    switch (direction)
    {
        case "U":
            head.Item2 += 1;
            break;
        case "D":
            head.Item2 -= 1;
            break;
        case "R":
            head.Item1 += 1;
            break;
        case "L":
            head.Item1 -= 1;
            break;
        default:
            throw new NotImplementedException();
    }
    knots[0] = head;
}

void MoveKnot(int index)
{
    var tail = knots[index];
    var head = knots[index-1];
    if (head.Item1 - tail.Item1 == 2)
    {
        tail.Item1 = head.Item1 - 1;
        tail.Item2 = head.Item2;
    }
    else if (head.Item1 - tail.Item1 == -2)
    {
        tail.Item1 = head.Item1 + 1;
        tail.Item2 = head.Item2;
    }
    else if (head.Item2 - tail.Item2 == 2)
    {
        tail.Item2 = head.Item2 - 1;
        tail.Item1 = head.Item1;
    }
    else if (head.Item2 - tail.Item2 == -2)
    {
        tail.Item2 = head.Item2 + 1;
        tail.Item1 = head.Item1;
    }

    knots[index] = tail;
    knots[index - 1] = head;
}

string line;

while ((line = Console.ReadLine()) != null)
{
    string[] splittedLine = line.Split(' ');
    string direction = splittedLine[0];
    int steps = int.Parse(splittedLine[1]);

    for (int i = 0; i < steps; i++)
    {
        for (int knotIndex = 0; knotIndex < knots.Count; knotIndex++)
        {
            if (knotIndex == 0)
            {
                MoveHead(direction);
            } else {
                MoveKnot(knotIndex);
            }
        }
        tailLocationHistory.Add(knots.Last());
    }
}

Console.WriteLine(tailLocationHistory.Count);
await Task.Delay(5000);
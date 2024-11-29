using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Pipe
{
    private Pipe NextPipe { get; set; }
    private (int, int) Location { get; set; }
    public Pipe(char[][] pipeMap, (int, int) location)
    {
        Location = location;

        (int, int) nextPipeLocation = FindFirstConnectedPipeLocation(pipeMap, location);

        NextPipe = new Pipe(pipeMap, nextPipeLocation, this, this);
    }

    private Pipe(char[][] pipeMap, (int, int) location, Pipe previousPipe, Pipe startPipe)
    {

        Location = location;
        (int, int) nextPipeLocation = FindNextConnectedPipeLocation(pipeMap, location, previousPipe);

        if (nextPipeLocation != startPipe.Location)
        {
            NextPipe = new Pipe(pipeMap, nextPipeLocation, this, startPipe);
        } else
        {
            NextPipe = startPipe;
        }

        

    }

    private (int, int) FindFirstConnectedPipeLocation(char[][] pipeMap, (int, int) location)
    {
        try
        {

            char leftPipe = pipeMap[location.Item1][location.Item2 - 1];

            if (leftPipe == '-' || leftPipe == 'L' || leftPipe == 'F')
            {
                return (location.Item1, location.Item2 - 1);
            }

        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.ToString());
        }



        try
        {
            char rightPipe = pipeMap[location.Item1][location.Item2 + 1];

            if (rightPipe == '-' || rightPipe == 'J' || rightPipe == '7')
            {
                return (location.Item1, location.Item2 + 1);
            }

        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.ToString());
        }



        try
        {
            char topPipe = pipeMap[location.Item1 - 1][location.Item2];

            if (topPipe == '|' || topPipe == 'F' || topPipe == '7')
            {
                return (location.Item1 - 1, location.Item2);
            }

        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.ToString());
        }




        try
        {
            char bottomPipe = pipeMap[location.Item1 + 1][location.Item2];

            if (bottomPipe == '|' || bottomPipe == 'L' || bottomPipe == 'J')
            {
                return (location.Item1 + 1, location.Item2);
            }

        }
        catch (Exception exc)
        {
            Console.WriteLine(exc.ToString());

        }

        Console.WriteLine("No connections found for: " + location.Item1, location.Item2);
        throw new Exception();
    }

    private (int, int) FindNextConnectedPipeLocation(char[][] pipeMap, (int, int) location, Pipe previousPipe)
    {

        if ((location.Item1, location.Item2 - 1) != previousPipe.Location)
        {
            try
            {

                char leftPipe = pipeMap[location.Item1][location.Item2 - 1];

                if (leftPipe == '-' || leftPipe == 'L' || leftPipe == 'F' || leftPipe == 'S')
                {
                    return (location.Item1, location.Item2 - 1);
                }

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }
        }

        if ((location.Item1, location.Item2 + 1) != previousPipe.Location)
        {
            try
            {
                char rightPipe = pipeMap[location.Item1][location.Item2 + 1];

                if (rightPipe == '-' || rightPipe == 'J' || rightPipe == '7' || rightPipe == 'S')
                {
                    return (location.Item1, location.Item2 + 1);
                }

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }
        }

        if ((location.Item1 - 1, location.Item2) != previousPipe.Location)
        {
            try
            {
                char topPipe = pipeMap[location.Item1 - 1][location.Item2];

                if (topPipe == '|' || topPipe == 'F' || topPipe == '7' || topPipe == 'S')
                {
                    return (location.Item1 - 1, location.Item2);
                }

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }
        }


        if ((location.Item1 + 1, location.Item2) != previousPipe.Location)
        {
            try
            {
                char bottomPipe = pipeMap[location.Item1 + 1][location.Item2];

                if (bottomPipe == '|' || bottomPipe == 'L' || bottomPipe == 'J' || bottomPipe == 'S')
                {
                    return (location.Item1 + 1, location.Item2);
                }

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }
        }


        Console.WriteLine("No connections found for: " + location.Item1 + location.Item2);
        throw new Exception();
    }


    public int GetDistanceOfLoop()
    {
        int numberOfPipes = 1;
        return NextPipe.GetDistanceOfLoop(numberOfPipes, this);
    }

    private int GetDistanceOfLoop(int numberOfPipes, Pipe startPipe)
    {
        if (this == startPipe){
            return numberOfPipes;
        }

        return NextPipe.GetDistanceOfLoop(numberOfPipes + 1, this);



    }
}



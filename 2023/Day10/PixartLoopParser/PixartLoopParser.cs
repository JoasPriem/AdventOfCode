using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


public class PixartLoopParser
{
    readonly string filePath;
    private char[][] pipeMap;
    private Pipe startPipe;


    public PixartLoopParser(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Path is: " + path);
        }
        filePath = path;
        InitializePipeMap();
        (int, int) startPipeLocation = FindStartPipeLocation();
        startPipe = new Pipe(pipeMap, startPipeLocation);
    }

    private void InitializePipeMap()
    {
        StreamReader sr = new StreamReader(filePath);
        string line;
        List<char[]> pipeMapCharArrays = new List<char[]>();
        while ((line = sr.ReadLine()) != null)
        {
            pipeMapCharArrays.Add(line.ToCharArray());
        }

        this.pipeMap = pipeMapCharArrays.ToArray();
    }

    private (int, int) FindStartPipeLocation()
    {
        for (int i = 0; i < pipeMap.Length; i++)
        {
            for (int j = 0; j < pipeMap[i].Length; j++)
            {
                if (pipeMap[i][j] == 'S')
                {
                    return (i, j);
                }
            }
        }

        Console.WriteLine("S not found");
        return (0, 0);
    }
    public int CalculateMaxDistanceWithinLoop()
    {
        return startPipe.GetDistanceOfLoop()/2;
    }

}



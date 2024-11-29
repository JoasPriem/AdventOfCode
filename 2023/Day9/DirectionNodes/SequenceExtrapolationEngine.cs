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

public class SequenceExtrapolationEngine
{
    readonly string filePath;
    private string directionInstructions;
    private Dictionary<string, (string, string)> directionNodesDict = new Dictionary<string, (string, string)>();
    public SequenceExtrapolationEngine(string path)
    {
        if (!File.Exists(path))
        {
            throw new FileNotFoundException("Path is: " + path);
        }
        filePath = path;
    }


    public int[] GetRightSidedExtrapolatedValues()
    {
        StreamReader sr = new StreamReader(filePath);
        string line;
        List<int> values = new List<int>();
        while ((line = sr.ReadLine()) != null)
        {
            int extrapolatedValue = ExtrapolateToRightFromLine(line);
            values.Add(extrapolatedValue);
        }

        return values.ToArray();
    }


    public int[] GetLeftSidedExtrapolatedValues()
    {
        StreamReader sr = new StreamReader(filePath);
        string line;
        List<int> values = new List<int>();
        while ((line = sr.ReadLine()) != null)
        {
            int extrapolatedValue = ExtrapolateToLeftFromLine(line);
            values.Add(extrapolatedValue);
        }

        return values.ToArray();
    }

    private int ExtrapolateToRightFromLine(string inputLine)
    {
        string[] splittedLine = inputLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        int[] startValues = Array.ConvertAll(splittedLine, int.Parse);
        int extrapolatedValue = ExtrapolateToRightFromArray(startValues);
        return extrapolatedValue;
    }

    private int ExtrapolateToLeftFromLine(string inputLine)
    {
        string[] splittedLine = inputLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        int[] startValues = Array.ConvertAll(splittedLine, int.Parse);
        int extrapolatedValue = ExtrapolateToLeftFromArray(startValues);
        return extrapolatedValue;
    }

    private int ExtrapolateToRightFromArray(int[] startValues)
    {
        if (startValues.Where(x => x != 0).Count() == 0)
        {
            return 0;
        }

        int[] nextValues = GetDifferenceArray(startValues);
        return ExtrapolateToRightFromArray(nextValues) + startValues.Last();
    }

    private int ExtrapolateToLeftFromArray(int[] startValues)
    {
        if (startValues.Where(x => x != 0).Count() == 0)
        {
            return 0;
        }

        int[] nextValues = GetDifferenceArray(startValues);
        return startValues.First() - ExtrapolateToLeftFromArray(nextValues);
    }

    private int[] GetDifferenceArray(int[] startValues)
    {
        int[] differenceArray = new int[startValues.Length - 1];
        for (int i = 0; i < differenceArray.Length; i++)
        {
            differenceArray[i] = startValues[i + 1] - startValues[i];
        }
        return differenceArray;
    }
}



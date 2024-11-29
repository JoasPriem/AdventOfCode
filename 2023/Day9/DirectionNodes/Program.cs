// See https://aka.ms/new-console-template for more information


Console.WriteLine("Hello, World!");
SequenceExtrapolationEngine sequenceExtrapolationEngine;
try
{
    string relativePath = "input.txt"; // No leading backslash
    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

    sequenceExtrapolationEngine = new SequenceExtrapolationEngine(fullPath);
    int[] extrapolatedValues = sequenceExtrapolationEngine.GetLeftSidedExtrapolatedValues();
    Console.WriteLine("sum of values: " + extrapolatedValues.Sum());
}
catch (FileNotFoundException exc)
{
    Console.Error.WriteLine("File not found: " + exc);
}

Console.Read();
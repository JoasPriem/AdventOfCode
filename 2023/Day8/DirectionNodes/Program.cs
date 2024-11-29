// See https://aka.ms/new-console-template for more information
using ParseCalibrationValues.CallibrationValueParser;

Console.WriteLine("Hello, World!");
DirectionTree directionTree;
try
{
    string relativePath = "input.txt"; // No leading backslash
    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

    directionTree = new DirectionTree(fullPath);
    int numberOfSteps = directionTree.GetNumberOfStepsToArriveAtEndLocationGhostEdition();
    Console.WriteLine("Number Of Steps: " + numberOfSteps);
}
catch (FileNotFoundException exc)
{
    Console.Error.WriteLine("File not found: " + exc);
}

Console.Read();
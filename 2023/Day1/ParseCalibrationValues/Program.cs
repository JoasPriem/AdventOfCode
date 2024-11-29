// See https://aka.ms/new-console-template for more information
using ParseCalibrationValues.CallibrationValueParser;

Console.WriteLine("Hello, World!");
CallibrationValueParser parser;
try
{
    string relativePath = "callibrationValues.txt"; // No leading backslash
    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

    parser = new CallibrationValueParser(fullPath);
    List<int> callibrationValues = parser.GetCallibrationValues(true);
    int total = callibrationValues.Sum();
    Console.WriteLine("Total: " + total);
}
catch (FileNotFoundException exc)
{
    Console.Error.WriteLine("File not found: " + exc);
}

Console.Read();
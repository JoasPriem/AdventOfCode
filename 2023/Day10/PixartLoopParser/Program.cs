// See https://aka.ms/new-console-template for more information


Console.WriteLine("Hello, World!");
PixartLoopParser pixartLoopParser;
try
{
    string relativePath = "input.txt"; // No leading backslash
    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), relativePath);

    pixartLoopParser = new PixartLoopParser(fullPath);
    int maxDistance = pixartLoopParser.CalculateMaxDistanceWithinLoop();
    Console.WriteLine("max distance: " + maxDistance);
}
catch (FileNotFoundException exc)
{
    Console.Error.WriteLine("File not found: " + exc);
}

Console.Read();
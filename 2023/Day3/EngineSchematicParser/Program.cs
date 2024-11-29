// See https://aka.ms/new-console-template for more information
using CubeGameVerifier.GameVerifier;

Console.WriteLine("Hello, World!");

string filePath = Path.Combine(Environment.CurrentDirectory ,"input.txt");

var engineSchematicParser = new EngineSchematicParser(filePath);

List<int> validPartNumbers = engineSchematicParser.GetGearRatios();
Console.WriteLine(validPartNumbers.Sum());

Console.ReadLine();
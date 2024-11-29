// See https://aka.ms/new-console-template for more information
using CubeGameVerifier.GameVerifier;

Console.WriteLine("Hello, World!");

string filePath = Path.Combine(Environment.CurrentDirectory ,"input.txt");

var verifier = new GameVerifier(filePath);

List<int> validGameIds = verifier.GetPowersOfMinimumGames();
Console.WriteLine(validGameIds.Sum());

Console.ReadLine();
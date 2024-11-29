// See https://aka.ms/new-console-template for more information
using CubeGameVerifier.GameVerifier;

Console.WriteLine("Hello, World!");

string filePath = Path.Combine(Environment.CurrentDirectory ,"input.txt");

var mapper = new SeedLocationMapper(filePath);

List<int> lowestLocation = mapper.GetLowestMappedLocation();
Console.WriteLine(lowestLocation);

Console.ReadLine();
// See https://aka.ms/new-console-template for more information
using CubeGameVerifier.GameVerifier;

Console.WriteLine("Hello, World!");

string filePath = Path.Combine(Environment.CurrentDirectory ,"input.txt");

var verifier = new ScratchCardVerifier(filePath);

int numberOfScracthedCards = verifier.CalculateNumberOfScratchedCards();
Console.WriteLine(numberOfScracthedCards);

Console.ReadLine();
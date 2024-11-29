using Day05_1;
using System.Linq;
using System.Runtime.CompilerServices;

int numberOfRedundantElves = 0;

string fileName = "input.txt";
string filePath = Path.Combine(Environment.CurrentDirectory, fileName);

var crateCrane = new CrateCrane(filePath);
crateCrane.FollowInstructions();
string answer = crateCrane.GetUpperCrateNames();


Console.WriteLine(answer);
await Task.Delay(5000);
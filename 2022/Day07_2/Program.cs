using System.Linq;
using System.Runtime.CompilerServices;
using Day07_1;
double totalFileSize = 0;
string line;
string lastCommand = "";

string newDirName = "";
ElfDirectory currentDir = null;
while ((line = Console.ReadLine()) != null)
{
    string[] splittedLine = line.Split(' ');
    if (splittedLine[0] == "$")
    {
        string currentCommand = splittedLine[1];
        
        if (currentCommand == "cd")
        {
            if (splittedLine[2] == "..")
            {
                currentDir = currentDir.GetParentDirectory();
            }
            else
            {
                newDirName = splittedLine[2];

                if (currentDir == null)
                {
                    currentDir = new ElfDirectory(null, newDirName);
                }
                else
                {
                    var newDirectory = new ElfDirectory(currentDir, newDirName);
                    currentDir.AddDirectory(newDirectory);
                    currentDir = newDirectory;
                }
            }
        }

        lastCommand = currentCommand;
    }
    else
    {
        if (lastCommand == "ls")
        {
            if (splittedLine[0] != "dir")
            {
                currentDir.AddFile(double.Parse(splittedLine[0]));
            }
        }
    }
}
var rootDir = currentDir.GetRootDirectory();
double freeSpaceNeeded = 30000000;
double currentFreeSpace = 70000000 - rootDir.GetTotalFileSize();
double smallestDirectoryThatCanBeDeleted = double.MaxValue;

foreach(ElfDirectory dir in rootDir.GetAllDescendants())
{
    double fileSize = dir.GetTotalFileSize();

    if (fileSize + currentFreeSpace > freeSpaceNeeded)
    {
        smallestDirectoryThatCanBeDeleted = Math.Min(fileSize, smallestDirectoryThatCanBeDeleted);
    }

}

Console.WriteLine(smallestDirectoryThatCanBeDeleted);
await Task.Delay(5000);
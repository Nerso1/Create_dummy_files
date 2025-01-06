using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Text.Json;


internal class Program
{
    public static async Task Main(string[] args)
    //static async Task Main(string[] args) // Main is now async
    {
        string directoryPath = @"C:\Users\domin\Downloads\Folder";
        bool genericOrRandom = false;
        var files = new List<FileCreationRules>
        {
            new FileCreationRules(10, 1.234, ".rtc", DateTime.Now),
            new FileCreationRules(5, 0.987, ".log", DateTime.Now.AddDays(-1)),
            new FileCreationRules(20, 0.123, ".txt", DateTime.Now.AddDays(-2))
        };

        FileCreationRules.PrintAll(files);

        Console.WriteLine();

        FileCreationRules.CalculateTotalFilesSize(files);

        Console.WriteLine();

        Console.WriteLine("Amount of files to create: " + FileCreationRules.HowManyFilesToCreate(files));

        Console.WriteLine();

        var filesWithNames = await FilePreparer.CombineFileNamesAndData(files, genericOrRandom);
        FilePreparer.PrintPreparedFiles(filesWithNames);

        Console.WriteLine();

        FileCreator.CreateFiles(filesWithNames, directoryPath);
    }
}

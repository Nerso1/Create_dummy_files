using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Text.Json;


internal class Program
{
    public static async Task Main(string[] args)
    {
        string directoryPath = @"C:\Users\domin\Downloads\Folder";
        string filePath = "files.json";
        bool genericOrRandom = false;
        var files = await JsonFileHandler.DeserializeFromJsonFileAsync<FileCreationRules>(filePath);
        foreach (var file in files)
        {
            Console.WriteLine($"Amount: {file.Amount}, SizeMB: {file.SizeMB}, Extension: {file.Extension}, CreationDate: {file.CreationDate}");
        }

        //var files = new List<FileCreationRules>
        //{
        //    new FileCreationRules(10, 1.234, ".rtc", DateTime.Now),
        //    new FileCreationRules(5, 0.987, ".log", DateTime.Now.AddDays(-1)),
        //    new FileCreationRules(20, 0.123, ".txt", DateTime.Now.AddDays(-2))
        //};
        //await JsonFileHandler.SerializeToJsonFileAsync(filePath, files);

        FileCreationRules.PrintAll(files);

        Console.WriteLine();

        Console.WriteLine("Amount of files to create: " + FileCreationRules.HowManyFilesToCreate(files));

        Console.WriteLine();

        FileCreationRules.CalculateTotalFilesSize(files);

        Console.WriteLine();

        if (YesOrNoQuestion("Do you want to create files? (y/N) "))
        {
            Console.WriteLine();

            var filesWithNames = await FilePreparer.CombineFileNamesAndData(files, genericOrRandom);
            FilePreparer.PrintPreparedFiles(filesWithNames);

            Console.WriteLine();

            FileCreator.CreateFiles(filesWithNames, directoryPath);
        }
        
    }

    public static bool YesOrNoQuestion(string question)
    {
        Console.Write(question);
        ConsoleKeyInfo response = Console.ReadKey();
        if (response.KeyChar == 'Y' || response.KeyChar == 'y') return true;
        else return false;
    }
}

public static class JsonFileHandler
{
    public static async Task SerializeToJsonFileAsync<T>(string filePath, List<T> data)
    {
        // Convert the list to a JSON string
        var options = new JsonSerializerOptions { WriteIndented = true }; // Makes the JSON readable
        string jsonString = JsonSerializer.Serialize(data, options);

        // Write the JSON string to the file
        await File.WriteAllTextAsync(filePath, jsonString);
    }

    public static async Task<List<T>> DeserializeFromJsonFileAsync<T>(string filePath)
    {
        // Read the JSON string from the file
        string jsonString = await File.ReadAllTextAsync(filePath);

        // Deserialize the JSON string into a list
        return JsonSerializer.Deserialize<List<T>>(jsonString) ?? new List<T>();
    }

}

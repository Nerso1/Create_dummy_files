//using System;
//using System.IO;

//internal class Program
//{
//    static void Main(string[] args)
//    {
//        string directoryPath = @"C:\Users\domin\Downloads\Nowy folder";

//        string fileName = "MyLargeFile.bin"; // Choose your extension
//        string fullPath = Path.Combine(directoryPath, fileName);
//        long fileSizeInBytes = 1024 * 1024; // Example: 1 MB
//        DateTime customCreationDate = new DateTime(2023, 1, 1); // Set your desired creation date here

// Create the file with the specified size
//using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
//{
//    fs.SetLength(fileSizeInBytes);
//}

//// Set the creation date of the file
//File.SetCreationTime(fullPath, customCreationDate);

//        // Optional: Log confirmation
//        Console.WriteLine($"File of size {fileSizeInBytes} bytes created at: {fullPath}");
//        Console.WriteLine($"Creation date set to: {customCreationDate}");
//    }
//}

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
        bool genericOrRandom = false;
        var files = new List<FilesToCreate>
        {
            new FilesToCreate(10, 1.234, ".rtc", DateTime.Now),
            new FilesToCreate(5, 0.987, ".log", DateTime.Now.AddDays(-1)),
            new FilesToCreate(20, 0.123, ".txt", DateTime.Now.AddDays(-2))
        };

        FilesToCreate.PrintAll(files);

        Console.WriteLine();

        FilesToCreate.SizeOfAllFiles(files);

        Console.WriteLine();

        Console.WriteLine("Ammount of files to create: " + FilesToCreate.HowManyFilesToCreate(files));

        Console.WriteLine();

        // Get file names asynchronously
        //var fileNames = await FileNameGenerator.GetNames(FilesToCreate.HowManyFilesToCreate(files), genericOrRandom);
        //FileNameGenerator.PrintNames(fileNames);

        Console.WriteLine();

        var filesWithNames = await FileCombiner.CombineFileNamesAndData(files, genericOrRandom);
        FileCombiner.PrintCombinedFiles(filesWithNames);

        Console.WriteLine();

        FileCreator.CreateFiles(filesWithNames);
    }
}


public class FilesToCreate
{
    private int _ammount;
    private double _sizeMB;
    private string _extension = "test";
    private DateTime _creationDate;

    public int Ammount
    {
        get { return _ammount; }
        set
        {
            if (value < 0)
            {
                _ammount = 0;
            }
            if (value > 200)
            {
                _ammount = 200;
            }
            _ammount = value;
        }
    }

    public double SizeMB
    {
        get => _sizeMB;
        set
        {
            if (value < 0.00001)
            {
                _sizeMB = 0.00001;
            }
            else
            {
                _sizeMB = Math.Round(value, 3); // Round to 3 decimal places
            }
        }
    }

    public string Extension
    {
        get { return _extension; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                _extension = "test";
            }
            _extension = value;
        }
    }

    public DateTime CreationDate
    {
        get => _creationDate;
        set => _creationDate = value > DateTime.Now ? DateTime.Now : value; // Defaults to now if future date
    }

    public FilesToCreate(int ammount, double sizeMB, string extension, DateTime creationDate)
    {
        Ammount = ammount;
        SizeMB = sizeMB;
        Extension = extension;
        CreationDate = creationDate;
    }
    public void PrintValues()
    {
        Console.WriteLine($"Ammount: {Ammount}");
        Console.WriteLine($"Size (MB): {SizeMB}");
        Console.WriteLine($"Extension: {Extension}");
        Console.WriteLine($"Creation Date: {CreationDate}");
    }

    public static void PrintAll(List<FilesToCreate> files)
    {
        foreach (var file in files)
        {
            Console.WriteLine("---------- File ----------");
            file.PrintValues();
        }
    }
    public static void SizeOfAllFiles(List<FilesToCreate> files)
    {
        double sizeOfAllFiles = 0;
        string formattedSize;

        foreach (var file in files)
        {
            sizeOfAllFiles += file.Ammount * file.SizeMB;
        }

        if (sizeOfAllFiles >= 1024) // Convert to GB if >= 1024 MB
        {
            formattedSize = $"{Math.Round(sizeOfAllFiles / 1024, 2)} GB";
        }
        else if (sizeOfAllFiles < 1) // Convert to KB if < 1 MB
        {
            formattedSize = $"{Math.Round(sizeOfAllFiles * 1024, 2)} KB";
        }
        else // Keep in MB otherwise
        {
            formattedSize = $"{Math.Round(sizeOfAllFiles, 2)} MB";
        }

        Console.WriteLine("Sum of file sizes will be: " + formattedSize);
    }
    public static int HowManyFilesToCreate(List<FilesToCreate> files)
    {
        int count = 0;
        foreach (var file in files)
        {
            count += file.Ammount;
        }
        return count;
    }
}

public class FileNameGenerator
{
    //public static async List<string> GetNames(int ammount, bool genericOrRandom)
    public static async Task<List<string>> GetNames(int ammount, bool genericOrRandom)

    {
        var fileNames = new List<string>();
        if (genericOrRandom)
        {
            for (int i = 1; i <= ammount; i++)
            {
                fileNames.Add($"file{i}");
            }
        }
        else
        {
            // Await the asynchronous method to get the list of random words
            fileNames = await RandomWordGenerator.GetRandomWordList(ammount);
        }
        return fileNames;
    }

    public static void PrintNames(List<string> names)
    {
        foreach (var name in names)
        {
            Console.WriteLine(name);
        }
    }
}

public class RandomWordGenerator
{
    public static async Task<string> GetRandomWords(int ammountOfFiles)
    {
        string ammountOfFilesString = ammountOfFiles.ToString();
        string apiUrlWithoutNumber = "https://random-word-api.herokuapp.com/word?number=";
        string apiUrl = apiUrlWithoutNumber + ammountOfFilesString;

        using (HttpClient client = new HttpClient())
        {
            try
            {
                string response = await client.GetStringAsync(apiUrl); // Await the API call
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching words: " + ex.Message);
                return $"Error fetching words: {ex.Message}";
            }
        }
    }

    public static async Task<List<string>> GetRandomWordList(int ammountOfFiles)
    {
        string jsonResponse = await GetRandomWords(ammountOfFiles);
        try
        {
            // Deserialize JSON array into a List<string>
            var wordsList = JsonSerializer.Deserialize<List<string>>(jsonResponse);
            return wordsList ?? new List<string>(); // Return empty list if deserialization fails
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error parsing words: " + ex.Message);
            return new List<string>(); // Return empty list if parsing fails
        }

    }
}

public class FileToCreateWithName
{
    public string FileName { get; set; }
    public int Ammount { get; set; }
    public double SizeMB { get; set; }
    public string Extension { get; set; }
    public DateTime CreationDate { get; set; }

    public FileToCreateWithName(string fileName, int ammount, double sizeMB, string extension, DateTime creationDate)
    {
        FileName = fileName;
        Ammount = ammount;
        SizeMB = sizeMB;
        Extension = extension;
        CreationDate = creationDate;
    }
}

public class FileCombiner
{
    public static async Task<List<FileToCreateWithName>> CombineFileNamesAndData(List<FilesToCreate> files, bool genericOrRandom)
    {
        // Get the total number of files
        var fileNames = await FileNameGenerator.GetNames(FilesToCreate.HowManyFilesToCreate(files), genericOrRandom);

        // Create a list to store the combined data
        var combinedFiles = new List<FileToCreateWithName>();

        int index = 0;
        foreach (var file in files)
        {
            // Ensure the number of file names matches the amount
            for (int i = 0; i < file.Ammount; i++)
            {
                string fileName = fileNames[index++];  // Get the file name
                combinedFiles.Add(new FileToCreateWithName(fileName, file.Ammount, file.SizeMB, file.Extension, file.CreationDate));
            }
        }

        return combinedFiles;
    }
    public static void PrintCombinedFiles(List<FileToCreateWithName> combinedFiles)
    {
        foreach (var file in combinedFiles)
        {
            Console.WriteLine();
            Console.Write(file.FileName);
            Console.Write(file.Extension + ", ");
            Console.Write(file.SizeMB + " MB, ");
            Console.Write("creation date: " + file.CreationDate);
        }
    }
}

public class FileCreator
{
    public static void CreateFiles(List<FileToCreateWithName> combinedFiles)
    {
        string directoryPath = @"C:\Users\domin\Downloads\Nowy folder";
        foreach (var file in combinedFiles)
        {
            string fullPath = Path.Combine(directoryPath, file.FileName + file.Extension);
            using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                //long SizeInBytes = Convert.ToInt64(file.SizeMB * 1024 * 1024);
                long SizeInBytes = (long)(file.SizeMB * 1024 * 1024);
                fs.SetLength(SizeInBytes);
            }
            File.SetCreationTime(fullPath, file.CreationDate);
        }

    }

}
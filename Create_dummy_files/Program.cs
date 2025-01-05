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

//        // Create the file with the specified size
//        using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
//        {
//            fs.SetLength(fileSizeInBytes);
//        }

//        // Set the creation date of the file
//        File.SetCreationTime(fullPath, customCreationDate);

//        // Optional: Log confirmation
//        Console.WriteLine($"File of size {fileSizeInBytes} bytes created at: {fullPath}");
//        Console.WriteLine($"Creation date set to: {customCreationDate}");
//    }
//}

using System;
using System.Net.Http;
using System.Threading.Tasks;

internal class Program
{
    //static void Main(string[] args)
    static async Task Main(string[] args) // Main is now async
    {
        // Explicitly block on the asynchronous method
        //Task.Run(async () => await ChangeRandomWordsToAnArray()).GetAwaiter().GetResult();
        await ChangeRandomWordsToAnArray(); // Await the asynchronous method
    }

    static async Task ChangeRandomWordsToAnArray()
    {
        string words = await GetRandomWords(); // Await the asynchronous call
        Console.WriteLine(words); // Print the response
    }

    private static async Task<string> GetRandomWords()
    {
        string howManyFileNames = "10";
        string apiUrlWithoutNumber = "https://random-word-api.herokuapp.com/word?number=";
        string apiUrl = apiUrlWithoutNumber + howManyFileNames; // URL for fetching 10 random words

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
}


public class FileNameGenerator
{
    //public static async List<string> GetNames(int amount, bool genericOrRandom)
    public static async Task<List<string>> GetNames(int amount, bool genericOrRandom)

    {
        var fileNames = new List<string>();
        if (genericOrRandom)
        {
            for (int i = 1; i <= amount; i++)
            {
                fileNames.Add($"file{i}");
            }
        }
        else
        {
            // Await the asynchronous method to get the list of random words
            fileNames = await RandomWordGenerator.GetRandomWordList(amount);
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
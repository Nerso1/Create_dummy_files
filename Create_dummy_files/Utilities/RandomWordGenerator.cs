using System.Text.Json;

public class RandomWordGenerator
{
    public static async Task<string> GetRandomWords(int amountOfFiles)
    {
        string amountOfFilesString = amountOfFiles.ToString();
        string apiUrlWithoutNumber = "https://random-word-api.herokuapp.com/word?number=";
        string apiUrl = apiUrlWithoutNumber + amountOfFilesString;

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

    public static async Task<List<string>> GetRandomWordList(int amountOfFiles)
    {
        string jsonResponse = await GetRandomWords(amountOfFiles);
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
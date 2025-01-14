using System.Net.Http;
using System.Text.Json;
using MyProject.Services.Interfaces;

namespace MyProject.Services
{
    public class WordApiClient : IWordApiClient
    {
        public async Task<List<string>> GetRandomWordsAsync(int amount)
        {
            string apiUrl = $"https://random-word-api.herokuapp.com/word?number={amount}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string response = await client.GetStringAsync(apiUrl);
                    return JsonSerializer.Deserialize<List<string>>(response) ?? new List<string>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error fetching words: " + ex.Message);
                    return new List<string>();
                }
            }
        }
    }
}

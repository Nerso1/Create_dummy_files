using MyProject.Services.Interfaces;

namespace MyProject.Utilities
{
    public class FileNameGenerator
    {
        private readonly IWordApiClient _wordApiClient;

        public FileNameGenerator(IWordApiClient wordApiClient)
        {
            _wordApiClient = wordApiClient;
        }

        public async Task<List<string>> GetNamesAsync(int amount, bool genericOrRandom)
        {
            if (genericOrRandom)
            {
                var names = new List<string>();
                for (int i = 1; i <= amount; i++)
                {
                    names.Add($"file{i}");
                }
                return names;
            }
            else
            {
                return await _wordApiClient.GetRandomWordsAsync(amount);
            }
        }
    }
}

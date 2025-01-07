public interface IWordApiClient
{
    Task<List<string>> GetRandomWordsAsync(int amount);
}

//namespace MyProject.Services.Interfaces
//{
//    public interface IWordApiClient
//    {
//        Task<List<string>> GetRandomWordsAsync(int amount);
//    }
//}
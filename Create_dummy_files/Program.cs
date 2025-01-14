using MyProject.Services;
using MyProject.Models;
using MyProject.Utilities;

namespace CreateDummyFiles
{
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
}




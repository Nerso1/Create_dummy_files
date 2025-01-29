using MyProject.Services;
using MyProject.Models;
using MyProject.Utilities;

namespace CreateDummyFiles
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            string filePath = "files.json"; //C:\Users\domin\Downloads\Folder

            string directoryPath = GetDirectoryPath(args);
            bool genericOrRandom = !YesOrNoQuestion("Do you want your files to have random names? (y/N) ");
           
            Console.Clear();

            var files = await LoadOrCreateFileRules(filePath);

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

        private static string GetDirectoryPath (string[] args)
        {
            string? directoryPath;
            if (args.Length > 0)
            {
                directoryPath = args[0];
                Console.WriteLine($"Using provided directory path: {directoryPath}");
            }
            else
            {
                Console.WriteLine("Provide path to folder that you want to create dummy files in: ");
                directoryPath = Console.ReadLine();
                if (directoryPath == null)
                {
                    throw new Exception("Path cannot be null");
                } 
            }
            return directoryPath;
        }

        private static async Task<List<FileCreationRules>> LoadOrCreateFileRules(string filePath)
        {
            List<FileCreationRules> files;
            if (File.Exists(filePath))
            {
                files = await JsonFileHandler.DeserializeFromJsonFileAsync<FileCreationRules>(filePath);
                //foreach (var file in files)
                //{
                //    Console.WriteLine($"Amount: {file.Amount}, SizeMB: {file.SizeMB}, Extension: {file.Extension}, CreationDate: {file.CreationDate}");
                //}
                return files;
            }
            else
            {
                files = new List<FileCreationRules>
                {
                    new FileCreationRules(10, 1.234, ".rtc", DateTime.Now),
                    new FileCreationRules(5, 0.987, ".log", DateTime.Now.AddDays(-1)),
                    new FileCreationRules(20, 0.123, ".txt", DateTime.Now.AddDays(-2))
                };
                await JsonFileHandler.SerializeToJsonFileAsync(filePath, files);
                return files;
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




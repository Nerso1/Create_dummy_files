public class FilePreparer
{
    public static async Task<List<FileToCreate>> CombineFileNamesAndData(List<FileCreationRules> files, bool genericOrRandom)
    {
        // Step 1: Generate file names
        var fileNames = await GenerateFileNames(files, genericOrRandom);

        // Step 2: Combine names with metadata
        return CombineWithMetadata(files, fileNames);
    }

    public static async Task<List<string>> GenerateFileNames(List<FileCreationRules> files, bool genericOrRandom)
    {
        int totalFiles = FileCreationRules.HowManyFilesToCreate(files);
        return await FileNameGenerator.GetNames(totalFiles, genericOrRandom);
    }

    public static List<FileToCreate> CombineWithMetadata(List<FileCreationRules> files, List<string> fileNames)
    {
        var combinedFiles = new List<FileToCreate>();
        int index = 0;

        foreach (var file in files)
        {
            for (int i = 0; i < file.Amount; i++)
            {
                string fileName = fileNames[index++];
                combinedFiles.Add(new FileToCreate(fileName, file.Amount, file.SizeMB, file.Extension, file.CreationDate));
            }
        }

        return combinedFiles;
    }

    public static void PrintPreparedFiles(List<FileToCreate> combinedFiles)
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
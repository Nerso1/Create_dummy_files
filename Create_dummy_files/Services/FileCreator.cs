using MyProject.Models;

namespace MyProject.Services
{ 
    public class FileCreator
    {
        public static void CreateFiles(List<FileToCreate> combinedFiles, string directoryPath)
        {
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
}
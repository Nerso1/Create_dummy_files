namespace MyProject.Models
{
    public class FileToCreate
    {
        public string FileName { get; set; }
        public int Amount { get; set; }
        public double SizeMB { get; set; }
        public string Extension { get; set; }
        public DateTime CreationDate { get; set; }

        public FileToCreate(string fileName, int amount, double sizeMB, string extension, DateTime creationDate)
        {
            FileName = fileName;
            Amount = amount;
            SizeMB = sizeMB;
            Extension = extension;
            CreationDate = creationDate;
        }
    }
}

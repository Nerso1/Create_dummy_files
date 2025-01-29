namespace MyProject.Models
{
    public class FileCreationRules
    {
        private int _amount;
        private double _sizeMB;
        private string _extension = "test";
        private DateTime _creationDate;

        public int Amount
        {
            get { return _amount; }
            set
            {
                if (value < 0)
                {
                    _amount = 0;
                }
                if (value > 200)
                {
                    _amount = 200;
                }
                _amount = value;
            }
        }

        public double SizeMB
        {
            get => _sizeMB;
            set
            {
                if (value < 0.00001)
                {
                    _sizeMB = 0.00001;
                }
                else
                {
                    _sizeMB = Math.Round(value, 3); // Round to 3 decimal places
                }
            }
        }

        public string Extension
        {
            get { return _extension; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    _extension = "test";
                }
                _extension = value;
            }
        }

        public DateTime CreationDate
        {
            get => _creationDate;
            set => _creationDate = value > DateTime.Now ? DateTime.Now : value; // Defaults to now if future date
        }

        public FileCreationRules(int amount, double sizeMB, string extension, DateTime creationDate)
        {
            Amount = amount;
            SizeMB = sizeMB;
            Extension = extension;
            CreationDate = creationDate;
        }
        public void PrintValues()
        {
            Console.WriteLine($"Amount: {Amount}");
            Console.WriteLine($"Size (MB): {SizeMB}");
            Console.WriteLine($"Extension: {Extension}");
            Console.WriteLine($"Creation Date: {CreationDate}");
        }

        public static void PrintAll(List<FileCreationRules> files)
        {
            foreach (var file in files)
            {
                Console.WriteLine("-------- Group of files --------");
                file.PrintValues();
            }
        }
        public static void CalculateTotalFilesSize(List<FileCreationRules> files)
        {
            double sizeOfAllFiles = 0;
            string formattedSize;

            foreach (var file in files)
            {
                sizeOfAllFiles += file.Amount * file.SizeMB;
            }

            if (sizeOfAllFiles >= 1024) // Convert to GB if >= 1024 MB
            {
                formattedSize = $"{Math.Round(sizeOfAllFiles / 1024, 2)} GB";
            }
            else if (sizeOfAllFiles < 1) // Convert to KB if < 1 MB
            {
                formattedSize = $"{Math.Round(sizeOfAllFiles * 1024, 2)} KB";
            }
            else // Keep in MB otherwise
            {
                formattedSize = $"{Math.Round(sizeOfAllFiles, 2)} MB";
            }

            Console.WriteLine("Sum of file sizes will be: " + formattedSize);
        }
        public static int HowManyFilesToCreate(List<FileCreationRules> files)
        {
            //int count = 0;
            //foreach (var file in files)
            //{
            //    count += file.Amount;
            //}
            //return count;
            return files.Sum(file => file.Amount);
        }
    }
}

**Dummy File Generator**

A console application built with .NET 6.0 that creates dummy files with customizable properties for testing purposes. Perfect for developers who need to generate test files with specific sizes, extensions, and creation dates.

**Features**

- Flexible File Creation: Generate multiple groups of files with different specifications
- Smart Naming Options: Choose between random word-based names (via API) or generic sequential names
- Size Control: Specify exact file sizes in MB with precision up to 3 decimal places
- Custom Extensions: Set any file extension for your test files
- Date Management: Control file creation dates (prevents future dates)
- JSON Configuration: Save and load file creation rules from a JSON configuration file
- Input Validation: Built-in validation for file amounts (0-200), sizes, and dates
- Size Calculation: Automatically calculates total disk space requirements with smart unit conversion (KB/MB/GB)

**How It Works**

Configuration Loading: The app loads file creation rules from files.json or creates default rules if the file doesn't exist
File Planning: Displays planned file groups, total count, and estimated disk usage
Name Generation: Fetches random words from an external API or generates sequential names
File Creation: Creates actual files with specified sizes and sets their creation timestamps

**Usage**

Specify target directory as argument
_dotnet run "C:\path\to\target\folder"_

Or run without arguments and enter path when prompted
_dotnet run

**Sample Configuration (files.json)**
[
  {
    "Amount": 10,
    "SizeMB": 1.234,
    "Extension": ".rtc",
    "CreationDate": "2024-06-10T10:30:00"
  },
  {
    "Amount": 5,
    "SizeMB": 0.987,
    "Extension": ".log",
    "CreationDate": "2024-06-09T15:45:00"
  }
]




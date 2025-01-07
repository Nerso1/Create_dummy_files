using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public static class JsonFileHandler
{
    public static async Task SerializeToJsonFileAsync<T>(string filePath, List<T> data)
    {
        // Convert the list to a JSON string
        var options = new JsonSerializerOptions { WriteIndented = true }; // Makes the JSON readable
        string jsonString = JsonSerializer.Serialize(data, options);

        // Write the JSON string to the file
        await File.WriteAllTextAsync(filePath, jsonString);
    }

    public static async Task<List<T>> DeserializeFromJsonFileAsync<T>(string filePath)
    {
        // Read the JSON string from the file
        string jsonString = await File.ReadAllTextAsync(filePath);

        // Deserialize the JSON string into a list
        return JsonSerializer.Deserialize<List<T>>(jsonString) ?? new List<T>();
    }

}
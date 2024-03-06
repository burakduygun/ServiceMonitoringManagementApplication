using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Shared.Services
{
    public static class FileAccess
    {
        public static List<ServiceSettings> LoadServiceSettings(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                string jsonContent = reader.ReadToEnd();
                return JsonSerializer.Deserialize<List<ServiceSettings>>(jsonContent);
            }
            //string jsonContent = File.ReadAllText(filePath);
            //return JsonSerializer.Deserialize<List<ServiceSettings>>(jsonContent);
        }

        public static void SaveServiceSettings(List<ServiceSettings> serviceSettings, string filePath)
        {
            //string jsonString = JsonSerializer.Serialize(serviceSettings);
            //File.WriteAllText(filePath, jsonString);

            string jsonString = JsonSerializer.Serialize(serviceSettings);
            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.Write(jsonString);
            }
        }
    }
}

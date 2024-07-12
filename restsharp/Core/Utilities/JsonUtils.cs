using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace Core.Utilities
{
    public class JsonUtils
    {
        public static Dictionary<string, T> ReadDictionaryJson<T>(string filepath)
        {
            var jsonData = File.ReadAllText(filepath);
            var data = JsonSerializer.Deserialize<Dictionary<string, T>>(jsonData);
            return data ?? new Dictionary<string, T>();
        }

        public static string ReadJsonFile(string path)
        {
            path = Path.Combine(DirectorUtility.GetCurrentDirectoryPath(), path);
            if (!File.Exists(path))
            {
                throw new Exception("Can't file path " + path);
            }
            return File.ReadAllText(path);
        }
    }
}
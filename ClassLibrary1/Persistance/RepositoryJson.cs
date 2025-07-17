using System.Text.Json;
using Contracts;
using UnitsNet;

namespace Calc.Persistance
{
    public class RepositoryJson : IRepository
    {
        public RepositoryJson(List<MathLog> memory = null)
        {
            if (memory == null) { Memory = new List<MathLog>(); } else { Memory = memory; }
        }
        public List<MathLog> Memory { get; }
        public int MemoryPosition { get; set; }

        public void SaveMemory(string filePath)
        {
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (Memory.Any(m => m.IQuantityResult == null && m.ResultDouble == null))
            {
                throw new JsonException("Result cannot be null");
            }
            var options = new JsonSerializerOptions { WriteIndented = true };

            var json = JsonSerializer.Serialize(Memory.ToEntities(), options);
            File.WriteAllText(filePath, json);
        }

        public void LoadMemory(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File Not Found", filePath);
            }

            var json = File.ReadAllText(filePath);
            List<MathLog> deserializedMemory = JsonSerializer.Deserialize<List<MathLogEntity>>(json)
                .Select(entity => entity.FromEntity()).ToList();

            Memory.Clear();
            Memory.AddRange(deserializedMemory);
            MemoryPosition = 0;
        }
    }
}

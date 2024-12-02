using System.Text.Json;

namespace ClassLibrary1
{
    public class Repository
    {
        public List<MathLog> Memory = new List<MathLog>();
        public int MemoryPosition { get; set; }
        public void SaveMemory(string filePath)
        {
            if (Memory.Any(m => m.Result == null))
            {
                throw new JsonException("Result cannot be null");
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            options.Converters.Add(new QuantityJsonConverter());
            var json = JsonSerializer.Serialize(Memory, options);
            File.WriteAllText(filePath, json);
        }

        public void LoadMemory(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File Not Found", filePath);
            }

            var json = File.ReadAllText(filePath);
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            options.Converters.Add(new QuantityJsonConverter());
            var deserializedMemory = JsonSerializer.Deserialize<List<MathLog>>(json, options);
            Memory.Clear();
            foreach (var item in deserializedMemory)
            {
                Memory.Add(item);
            }
            MemoryPosition = 0;
        }
    }
}

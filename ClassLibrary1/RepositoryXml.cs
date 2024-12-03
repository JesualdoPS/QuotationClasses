using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace ClassLibrary1
{
    public class RepositoryXml : IRepository
    {
        public List<MathLog> Memory { get; set; } = new List<MathLog>();

        public int MemoryPosition { get; set; }

        public void SaveMemory(string filePath)
        {
            if (Memory.Any(m => m.Result == null))
            {
                throw new XmlException("Result cannot be null");
            }

            var entities = Memory.ToEntities();

            var options = new XmlWriterSettings();
            options.Indent = true;

            var xmlSerializer = new XmlSerializer(typeof(List<MathLogEntity>));
            using(var writer = new StreamWriter(filePath))
            {
                xmlSerializer.Serialize(writer, entities);
            }
        }

        public void LoadMemory(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File Not Found", filePath);
            }

            var xmlDeserializer = new XmlSerializer(typeof(List<MathLogEntity>));
            using (var reader = new StreamReader(filePath))
            {
                var entities = (List<MathLogEntity>)xmlDeserializer.Deserialize(reader);
            }
        }
    }
}

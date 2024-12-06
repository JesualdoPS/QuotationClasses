using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;

namespace Calc.Persistance
{
    public class RepositoryXml : IRepository
    {
        public RepositoryXml(List<MathLog> memory = null)
        {
            if (memory == null) { Memory = new List<MathLog>(); } else { Memory = memory; }
        }
        public List<MathLog> Memory { get; }

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
            using (var writer = XmlWriter.Create(filePath, options))
            {
                xmlSerializer.Serialize(writer, entities);
            }
        }

        public void LoadMemory(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File Not Found");
            }

            var xmlDeserializer = new XmlSerializer(typeof(List<MathLogEntity>));
            using (var reader = new StreamReader(filePath))
            {
                var entities = (List<MathLogEntity>)xmlDeserializer.Deserialize(reader);
            }
        }
    }
}

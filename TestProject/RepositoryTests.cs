using System.Text.Json;
using System.Xml;
using Calc.Persistance;
using FluentAssertions;
using UnitsNet;

namespace TestProject
{
    [TestClass]
    public class RepositoryTests
    {
        [TestMethod]
        public void ShouldSerializeMathLogToJson()
        {
            // Arrange
            var repository = new RepositoryJson();
            var mathLogs = new List<MathLog>
            {
                new MathLog { Math = "15 m + 5 m", Result = Length.FromMeters(20) },
                new MathLog { Math = "10 m * 2 m", Result = Area.FromSquareMeters(20) },
                new MathLog { Math = "5 mm - 3 mm", Result = Length.FromMillimeters(2) },
                new MathLog { Math = "3 mm * 3 mm", Result = Area.FromSquareMillimeters(9) }
            };
            repository.Memory = mathLogs;
            var filePath = @"D:\Material de aula\Aula de Programação\curso_C#\Aulas\QuotationFactory\Calculator.json";

            // Act
            repository.SaveMemory(filePath);
            var indented = new JsonSerializerOptions();
            indented.WriteIndented = true;
            var loadedRepository = new RepositoryJson();
            loadedRepository.LoadMemory(filePath);

            // Assert
            repository.Memory.Count.Should().Be(mathLogs.Count);
            for (int i = 0; i < mathLogs.Count; i++)
            {
                loadedRepository.Memory[i].Math.Should().Be(mathLogs[i].Math);
                loadedRepository.Memory[i].Result.ToString().Should().Be(mathLogs[i].Result.ToString());
            }
        }

        [TestMethod]
        public void ShouldSerializeMathLogToSQL()
        {
            // Arrange
            var repository = new RepositorySQL();
            var mathLogs = new List<MathLog>
            {
                new MathLog { Math = "15 m + 5 m", Result = Length.FromMeters(20) },
                new MathLog { Math = "10 m * 2 m", Result = Area.FromSquareMeters(20) },
                new MathLog { Math = "5 mm - 3 mm", Result = Length.FromMillimeters(2) },
                new MathLog { Math = "3 mm * 3 mm", Result = Area.FromSquareMillimeters(9) }
            };
            repository.Memory = mathLogs;
            var filePath = "";

            // Act
            repository.SaveMemory(filePath);
            var loadedRepository = new RepositorySQL();
            loadedRepository.LoadMemory(filePath);

            // Assert
            loadedRepository.Memory.Count.Should().Be(4);
            for (int i = 0; i < mathLogs.Count; i++)
            {
                loadedRepository.Memory[i].Math.Should().Be(mathLogs[i].Math);
                loadedRepository.Memory[i].Result.ToString().Should().Be(mathLogs[i].Result.ToString());
            }
        }

        [TestMethod]
        public void ShouldThrowFileNotFoundException()
        {
            // Arrange
            var repository = new RepositoryJson();
            var filePath = @"C:\temp\Calcultor_DoesNotExist.json";

            // Act
            Action action = () => repository.LoadMemory(filePath);

            // Assert
            action.Should().Throw<FileNotFoundException>("File Not Found", filePath);
        }

        [TestMethod]
        public void ShouldThrowJsonException_WhenSerializingInvalidMathLog()
        {
            // Arrange
            var repository = new RepositoryJson();
            var mathLogs = new List<MathLog>
            {
                new MathLog { Math = "15 x + 5 x", Result = null }
            };
            repository.Memory = mathLogs;
            var filePath = @"D:\Material de aula\Aula de Programação\curso_C#\Aulas\QuotationFactory\Calculator.json";

            // Act
            Action action = () => repository.SaveMemory(filePath);

            // Assert
            action.Should().Throw<JsonException>();
        }

        [TestMethod]
        public void ShouldThrowFileNotFoundWhenSerializeToXML()
        {
            // Arrange
            var repository = new RepositoryXml();
            string filePath = @"C:\temp\Calcultor_DoesNotExist.xml";

            // Act
            Action action = () => repository.LoadMemory(filePath);

            // Assert
            action.Should().Throw<FileNotFoundException>();
        }

        [TestMethod]
        public void ShouldSerializeMathLogToXml()
        {
            // Arrange
            var repository = new RepositoryXml();
            var mathLogs = new List<MathLog>
            {
                new MathLog { Math = "15 m + 5 m", Result = Length.FromMeters(20)},
                new MathLog { Math = "10 m * 2 m", Result = Area.FromSquareMeters(20)},
                new MathLog { Math = "5 mm - 3 mm", Result = Length.FromMillimeters(2)},
                new MathLog { Math = "3 mm * 3 mm", Result = Area.FromSquareMillimeters(9)}
            };
            repository.Memory = mathLogs;
            var filePath = @"D:\Material de aula\Aula de Programação\curso_C#\Aulas\QuotationFactory\Calculator.xml";

            // Act
            repository.SaveMemory(filePath);
            var loadedRepository = new RepositoryXml();
            loadedRepository.LoadMemory(filePath);

            // Assert
            repository.Memory.Count.Should().Be(mathLogs.Count);
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenResultIsNull()
        {
            // Arrange
            var repository = new RepositoryXml();
            var mathLogs = new List<MathLog>
            {
                new MathLog { Math = "15 m + 5 m", Result = Length.FromMeters(20)},
                new MathLog { Math = "10 m * 2 m", Result = null},
            };
            var filePath = @"D:\Material de aula\Aula de Programação\curso_C#\Aulas\QuotationFactory\Calculator.xml";
            repository.Memory = mathLogs;

            // Act
            Action action = () => repository.SaveMemory(filePath);

            // Assert
            action.Should().Throw<XmlException>();
        }

    }

}


using System.Text.Json;
using System.Xml;
using Calc.Persistance;
using Contracts;
using FluentAssertions;
using UnitsNet;

namespace TestProject
{
    [TestClass]
    public class RepositoryTests
    {
        private static readonly string _directory =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data");

        [TestMethod]
        public void ShouldSerializeMathLogToJson()
        {
            // Arrange            
            var mathLogs = new List<MathLog>
            {
                new MathLog { Math = "15 m + 5 m", IQuantityResult = Length.FromMeters(20) },
                new MathLog { Math = "10 m * 2 m", IQuantityResult = Area.FromSquareMeters(20) },
                new MathLog { Math = "5 mm - 3 mm", IQuantityResult = Length.FromMillimeters(2) },
                new MathLog { Math = "3 mm * 3 mm", IQuantityResult = Area.FromSquareMillimeters(9) }
            };

            var repository = new RepositoryJson(mathLogs);
            var filePath = Path.Combine(_directory, "ShouldSerializeMathLogToJson.json");

            // Act
            repository.SaveMemory(filePath);
            var loadedRepository = new RepositoryJson();
            loadedRepository.LoadMemory(filePath);

            // Assert
            repository.Memory.Count.Should().Be(mathLogs.Count);
            for (int i = 0; i < mathLogs.Count; i++)
            {
                loadedRepository.Memory[i].Math.Should().Be(mathLogs[i].Math);
                loadedRepository.Memory[i].IQuantityResult.ToString().Should().Be(mathLogs[i].IQuantityResult.ToString());
            }
        }

        [TestMethod]
        public void ShouldSaveDivisionToJson()
        {
            //Arrange
            var mathLogs = new List<MathLog>
            {
                new MathLog { Math = "15m/5m", ResultDouble = 3},
                new MathLog { Math = "20m/4m", ResultDouble = 5}
            };
            var repository = new RepositoryJson(mathLogs);
            var filePath = Path.Combine(_directory, "ShouldSerializeMathLogToJson.json");

            //Act
            repository.SaveMemory(filePath);
            var loadedRepository = new RepositoryJson();
            loadedRepository.LoadMemory(filePath);

            //Assert
            repository.Memory.Count.Should().Be(2);
            loadedRepository.Memory[0].Math.Should().Be("15m/5m");
            loadedRepository.Memory[1].Math.Should().Be("20m/4m");
            loadedRepository.Memory[0].ResultDouble.Should().Be(3);
            loadedRepository.Memory[1].ResultDouble.Should().Be(5);
        }

        [TestMethod]
        public void ShouldSaveDivisionAndMultiplication()
        {
            //Arrange
            var mathLogs = new List<MathLog>
            {
                new MathLog { Math = "15m/5m", ResultDouble = 3},
                new MathLog { Math = "20m*5m", IQuantityResult = Area.FromSquareMeters(100)}
            };
            var repository = new RepositoryJson(mathLogs);
            var filePath = Path.Combine(_directory, "ShouldSerializeMathLogToJson.json");

            //Act
            repository.SaveMemory(filePath);
            var loadedRepository = new RepositoryJson();
            loadedRepository.LoadMemory(filePath);

            //Assert
            loadedRepository.Memory.Count.Should().Be(2);
            loadedRepository.Memory[0].Math.Should().Be("15m/5m");
            loadedRepository.Memory[1].Math.Should().Be("20m*5m");
            loadedRepository.Memory[0].ResultDouble.Should().Be(3);
            loadedRepository.Memory[1].IQuantityResult.Should().Be(Area.FromSquareMeters(100));
        }

        //[TestMethod]
        public void ShouldSerializeMathLogToSQL()
        {
            // Arrange
            var repository = new RepositorySQL();
            var mathLogs = new List<MathLog>
            {
                new MathLog { Math = "15 m + 5 m", IQuantityResult = Length.FromMeters(20) },
                new MathLog { Math = "10 m * 2 m", IQuantityResult = Area.FromSquareMeters(20) },
                new MathLog { Math = "5 mm - 3 mm", IQuantityResult = Length.FromMillimeters(2) },
                new MathLog { Math = "3 mm * 3 mm", IQuantityResult = Area.FromSquareMillimeters(9) }
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
                loadedRepository.Memory[i].IQuantityResult.ToString().Should().Be(mathLogs[i].IQuantityResult.ToString());
            }
        }

        //[TestMethod]
        public void ShouldSerializeMathLogWithDivisionToSQL()
        {
            // Arrange
            var repository = new RepositorySQL();
            var mathLogs = new List<MathLog>
            {
                new MathLog { Math = "15m/5m", ResultDouble = 3},
                new MathLog { Math = "20m*5m", IQuantityResult = Area.FromSquareMeters(100)}
            };
            repository.Memory = mathLogs;
            var filePath = "";

            // Act
            repository.SaveMemory(filePath);
            var loadedRepository = new RepositorySQL();
            loadedRepository.LoadMemory(filePath);

            // Assert
            loadedRepository.Memory.Count.Should().Be(2);
            loadedRepository.Memory[0].Math.Should().Be("15m/5m");
            loadedRepository.Memory[1].Math.Should().Be("20m*5m");
            loadedRepository.Memory[0].ResultDouble.Should().Be(3);
            loadedRepository.Memory[1].IQuantityResult.Should().Be(Area.FromSquareMeters(100));
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
            var mathLogs = new List<MathLog>
            {
                new MathLog { Math = "15 x + 5 x", IQuantityResult = null }
            };
            var repository = new RepositoryJson(mathLogs);
            var filePath = Path.Combine(_directory, "ShouldSerializeMathLogToJson.json");

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
            var mathLogs = new List<MathLog>
            {
                new MathLog { Math = "15 m + 5 m", IQuantityResult = Length.FromMeters(20)},
                new MathLog { Math = "10 m * 2 m", IQuantityResult = Area.FromSquareMeters(20)},
                new MathLog { Math = "5 mm - 3 mm", IQuantityResult = Length.FromMillimeters(2)},
                new MathLog { Math = "3 mm * 3 mm", IQuantityResult = Area.FromSquareMillimeters(9)}
            };
            var repository = new RepositoryXml(mathLogs);
            var filePath = Path.Combine(_directory, "ShouldSerializeMathLogToJson.json");

            // Act
            repository.SaveMemory(filePath);
            var loadedRepository = new RepositoryXml();
            loadedRepository.LoadMemory(filePath);

            // Assert
            loadedRepository.Memory.Count.Should().Be(4);
        }

        [TestMethod]
        public void ShouldSaveDivisionAndMultiplicationToXML()
        {
            //Arrange
            var mathLogs = new List<MathLog>
            {
                new MathLog { Math = "15m/5m", ResultDouble = 3},
                new MathLog { Math = "20m*5m", IQuantityResult = Area.FromSquareMeters(100)}
            };
            var repository = new RepositoryXml(mathLogs);
            var filePath = Path.Combine(_directory, "ShouldSerializeMathLogToJson.json");

            //Act
            repository.SaveMemory(filePath);
            var loadedRepository = new RepositoryXml();
            loadedRepository.LoadMemory(filePath);

            //Assert
            loadedRepository.Memory.Count.Should().Be(2);
            loadedRepository.Memory[0].Math.Should().Be("15m/5m");
            loadedRepository.Memory[1].Math.Should().Be("20m*5m");
            loadedRepository.Memory[0].ResultDouble.Should().Be(3);
            loadedRepository.Memory[1].IQuantityResult.Should().Be(Area.FromSquareMeters(100));
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenResultIsNull()
        {
            // Arrange

            var mathLogs = new List<MathLog>
            {
                new MathLog { Math = "10 m * 2 m", IQuantityResult = null, ResultDouble = null},
            };
            var filePath = @"D:\Material de aula\Aula de Programação\curso_C#\Aulas\QuotationFactory\Storage\Calculator.xml";
            var repository = new RepositoryXml(mathLogs);

            // Act
            Action action = () => repository.SaveMemory(filePath);

            // Assert
            action.Should().Throw<XmlException>();
        }

    }

}


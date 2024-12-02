using System.Text.Json;
using ClassLibrary1;
using FluentAssertions;
using UnitsNet;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void ShouldAddTwoNumbers()
        {
            //Arrange
            var calculator = new Calculator();


            //Act
            var result = calculator.Add(1, 1);


            //Assert
            result.Should().Be(2);
        }

        [TestMethod]
        public void ShouldAddAStringWithALengthBasedUnitOfMeasure()
        {
            //Arrange
            var calculator = new Calculator();


            //Act
            var result = calculator.Calculate("12 m + 2 m");

            //Assert
            result.Result.Should().Be(Length.FromMeters(14));
        }

        [TestMethod]
        public void ShouldSubtractAStringWithALengthBasedUnitOfMeasure()
        {
            //Arrange
            var calculator = new Calculator();


            //Act
            var result = calculator.Calculate("12 m - 2 m");

            //Assert
            result.Result.Should().Be(Length.FromMeters(10));
        }

        [TestMethod]
        public void ShouldMultiplyAStringWithALengthBasedUnitOfMeasure()
        {
            //Arrange
            var calculator = new Calculator();


            //Act
            var result = calculator.Calculate("12 m * 2 m");

            //Assert
            result.Result.Should().Be(Area.FromSquareMeters(24));
        }

        [TestMethod]
        public void ShouldAddAStringWithALengthBasedUnitOfMeasureInMillimeters()
        {
            //Arrange
            var calculator = new Calculator();


            //Act
            var result = calculator.Calculate("12 mm + 2 mm");

            //Assert
            result.Result.Should().Be(Length.FromMillimeters(14));
        }

        [TestMethod]
        public void ShouldSubtractAStringWithALengthBasedUnitOfMeasureInMillimeters()
        {
            //Arrange
            var calculator = new Calculator();


            //Act
            var result = calculator.Calculate("12 mm - 2 mm");

            //Assert
            result.Result.Should().Be(Length.FromMillimeters(10));
        }

        [TestMethod]
        public void ShouldMultiplyAStringWithALengthBasedUnitOfMeasureInMillimeters()
        {
            //Arrange
            var calculator = new Calculator();


            //Act
            var result = calculator.Calculate("12 mm * 2 mm");

            //Assert
            ((Area)result.Result).SquareMillimeters.Should().Be(24);
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenUnsupportedOperatorSymbolIsUsed()
        {
            // Arrange
            var calculator = new Calculator();

            // Act e Assert
            Assert.ThrowsException<FormatException>(() => calculator.Calculate("12 m : 2 m "));
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenUnsupportedOperatorSymbolIsUsed_Millimeter()
        {
            // Arrange
            var calculator = new Calculator();

            // Act e Assert
            Assert.ThrowsException<FormatException>(() => calculator.Calculate("12 mm : 2 mm"));
        }
        [TestMethod]
        public void ShouldThrowExceptionWhenUnsupportedOperatorSymbolIsUsed_NotationWithoutSpaces()
        {
            // Arrange
            var calculator = new Calculator();

            // Act e Assert
            Assert.ThrowsException<FormatException>(() => calculator.Calculate("12m:2m"));
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenUnsupportedOperatorSymbolIsUsed_Millimeter_NotationWithoutSpaces()
        {
            // Arrange
            var calculator = new Calculator();

            // Act e Assert
            Assert.ThrowsException<FormatException>(() => calculator.Calculate("12mm:2mm"));
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenExpressionDoesNotHaveAllParts()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            Action result = () => calculator.Calculate("15m+15");

            // Assert
            result.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void ShouldAddAStringWithALengthBasedUnitOfMeasure_NotationWithoutSpaces()
        {
            //Arrange
            var calculator = new Calculator();


            //Act
            var result = calculator.Calculate("12m+2m");

            //Assert
            result.Result.Should().Be(Length.FromMeters(14));
        }

        [TestMethod]
        public void ShouldAddTwoLenghts()
        {
            //Arrange
            var calculator = new Calculator();
            var length1 = Length.FromMeters(1);
            var length2 = Length.FromMeters(1);

            //Act
            var result = calculator.Add(length1, length2);


            //Assert
            result.Meters.Should().Be(2);
        }

        [TestMethod]
        public void ShouldMultiplyTwoLenghts()
        {
            //Arrange
            var calculator = new Calculator();
            var length1 = Length.FromMeters(1);
            var length2 = Length.FromMeters(1);


            //Act
            var result = calculator.Multiply(length1, length2);


            //Assert
            result.SquareMillimeters.Should().Be(1000000.0);
        }

        [TestMethod]
        public void ShouldMultiplyThreeLenghts()
        {
            //Arrange
            var calculator = new Calculator();
            var lenght1 = Length.FromMeters(1);
            var lenght2 = Length.FromMeters(1);
            var lenght3 = Length.FromMeters(1);


            //Act
            var result = calculator.Multiply(lenght1, lenght2, lenght3);


            //Assert
            result.CubicMeters.Should().Be(1);
        }

        [TestMethod]
        public void ShouldCalculateWeightOfWater()
        {
            //Arrange
            var waterWeight = new Calculator();
            var waterVolume = Volume.FromCubicMeters(1);

            //Act
            var result = waterWeight.CalculateWeight(waterVolume, Materials.Water);


            //Assert
            result.Kilograms.Should().Be(1000);
        }

        [TestMethod]
        public void ShouldCalculateWeightOfSteel()
        {
            //Arrange
            var steelWeight = new Calculator();
            var steelVolume = Volume.FromCubicMeters(1);

            //Act
            var result = steelWeight.CalculateWeight(steelVolume, Materials.Steel);


            //Assert
            result.Kilograms.Should().Be(7850);
        }

        [TestMethod]
        public void ShouldCalculateWeightOfAluminum()
        {
            //Arrange
            var aluminumWeight = new Calculator();
            var aluminumVolume = Volume.FromCubicMeters(1);

            //Act
            var result = aluminumWeight.CalculateWeight(aluminumVolume, Materials.Aluminum);


            //Assert
            result.Kilograms.Should().Be(2600);
        }

        [TestMethod]
        public void ShouldMemorizeACalculation()
        {
            //Arrange
            var calculator = new Calculator();

            //Act
            calculator.Calculate("12 mm - 2 mm");

            //Assert
            calculator.Memory.Should().HaveCount(1);
        }

        [TestMethod]
        public void ShouldMemorizeEachCalculation()
        {
            //Arrange
            var calculator = new Calculator();

            //Act
            calculator.Calculate("12 mm - 2 mm");
            calculator.Calculate("12 mm - 2 mm");

            //Assert
            calculator.Memory.Should().HaveCount(2);
        }

        [TestMethod]
        public void ShouldMemorizeCurrentMemoryPosition()
        {
            //Arrange
            var calculator = new Calculator();

            //Act
            calculator.Calculate("12 mm - 2 mm");
            calculator.Calculate("12 mm - 2 mm");

            //Assert
            calculator.MemoryPosition.Should().Be(1);
        }

        [TestMethod]
        public void ShouldShowPreviousCalculation()
        {
            //Arrange
            var calculator = new Calculator();

            //Act
            calculator.Calculate("15 mm - 10 mm");
            calculator.Calculate("35 mm - 20 mm");
            calculator.Calculate("50 mm - 30 mm");
            var result = calculator.Calculate("previous");

            //Assert
            result.Math.Should().Be("35 mm - 20 mm");
            result.Result.Should().Be(Length.FromMillimeters(15));
        }

        [TestMethod]
        public void ShouldShowNextCalculation()
        {
            //Arrange
            var calculator = new Calculator();

            //Act
            calculator.Calculate("15 mm - 10 mm");
            calculator.Calculate("35 mm - 20 mm");
            calculator.Calculate("50 mm - 30 mm");
            calculator.Calculate("previous");
            calculator.Calculate("previous");
            var result = calculator.Calculate("next");

            //Assert
            result.Math.Should().Be("35 mm - 20 mm");
            result.Result.Should().Be(Length.FromMillimeters(15));
        }

        [TestMethod]
        public void ShouldNotDoNextIfOnLastItemInMemory()
        {
            //Arrange
            var calculator = new Calculator();

            //Act
            calculator.Calculate("15 mm - 10 mm");
            calculator.Calculate("35 mm - 20 mm");
            var result = calculator.Calculate("next");

            //Assert
            result.Math.Should().Be("35 mm - 20 mm");
            result.Result.Should().Be(Length.FromMillimeters(15));
        }

        [TestMethod]
        public void ShouldNotDoPreviousIfOnFirstItemInMemory()
        {
            //Arrange
            var calculator = new Calculator();

            //Act
            calculator.Calculate("35 mm - 20 mm");
            var result = calculator.Calculate("previous");

            //Assert
            result.Math.Should().Be("35 mm - 20 mm");
            result.Result.Should().Be(Length.FromMillimeters(15));
        }

        [TestMethod]
        public void ShouldCalculaterAStringWithMetersAndMillimeterBasedUnitOfMeasure()
        {
            //Arrange
            var calculator = new Calculator();


            //Act
            var result1 = calculator.Calculate("15 m + 5 mm");
            var result2 = calculator.Calculate("15 m - 5 mm");
            var result3 = calculator.Calculate("15 m * 5 mm");

            //Assert
            result1.Result.Should().Be(Length.FromMeters(15.005));
            result2.Result.Should().Be(Length.FromMeters(14.995));
            result3.Result.Should().Be(Area.FromSquareMeters(0.075));
        }

        [TestMethod]
        public void ShouldSerializeMathLogToJson()
        {
            // Arrange
            var calculator = new Calculator();
            var mathLogs = new List<MathLog>
            {
                new MathLog { Math = "15 m + 5 m", Result = Length.FromMeters(20) },
                new MathLog { Math = "10 m * 2 m", Result = Area.FromSquareMeters(20) },
                new MathLog { Math = "5 mm - 3 mm", Result = Length.FromMillimeters(2) },
                new MathLog { Math = "3 mm * 3 mm", Result = Area.FromSquareMillimeters(9) }
            };
            calculator.Memory = mathLogs;
            var filePath = @"D:\Material de aula\Aula de Programação\curso_C#\Aulas\QuotationFactory\Calculator.json";

            // Act
            calculator.SaveMemory(filePath);
            var indented = new JsonSerializerOptions();
            indented.WriteIndented = true;
            var loadedCalculator = new Calculator();
            loadedCalculator.LoadMemory(filePath);

            // Assert
            for (int i = 0; i < mathLogs.Count; i++)
            {
                loadedCalculator.Memory[i].Math.Should().Be(mathLogs[i].Math);
                loadedCalculator.Memory[i].Result.ToString().Should().Be(mathLogs[i].Result.ToString());
            }
        }

        [TestMethod]
        public void ShouldThrowFileNotFoundException()
        {
            // Arrange
            var calculator = new Calculator();
            var filePath = @"C:\temp\Calcultor_DoesNotExist.json";

            // Act
            Action action = () => calculator.LoadMemory(filePath);

            // Assert
            action.Should().Throw<FileNotFoundException>("File Not Found", filePath);
        }

        [TestMethod]
        public void ShouldThrowJsonException_WhenSerializingInvalidMathLog()
        {
            // Arrange
            var calculator = new Calculator();
            var mathLogs = new List<MathLog>
            {
                new MathLog { Math = "15 x + 5 x", Result = null }
            };
            calculator.Memory = mathLogs;
            var filePath = @"D:\Material de aula\Aula de Programação\curso_C#\Aulas\QuotationFactory\Calculator.json";

            // Act
            Action action = () => calculator.SaveMemory(filePath);

            // Assert
            action.Should().Throw<JsonException>();
        }
    }
}
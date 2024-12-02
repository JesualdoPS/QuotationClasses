using System.Text.Json;
using ClassLibrary1;
using FluentAssertions;
using UnitsNet;

namespace TestProject
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void ShouldAddTwoNumbers()
        {
            //Arrange
            var repository = new Repository();
            var calculator = new Calculator(repository);

            //Act
            var result = calculator.Add(1, 1);


            //Assert
            result.Should().Be(2);
        }

        [TestMethod]
        public void ShouldAddAStringWithALengthBasedUnitOfMeasure()
        {
            //Arrange
            var repository = new Repository();
            var calculator = new Calculator(repository);

            //Act
            var result = calculator.Calculate("12 m + 2 m");

            //Assert
            result.Result.Should().Be(Length.FromMeters(14));
        }

        [TestMethod]
        public void ShouldSubtractAStringWithALengthBasedUnitOfMeasure()
        {
            //Arrange
            var repository = new Repository();
            var calculator = new Calculator(repository);

            //Act
            var result = calculator.Calculate("12 m - 2 m");

            //Assert
            result.Result.Should().Be(Length.FromMeters(10));
        }

        [TestMethod]
        public void ShouldMultiplyAStringWithALengthBasedUnitOfMeasure()
        {
            //Arrange
            var repository = new Repository();
            var calculator = new Calculator(repository);

            //Act
            var result = calculator.Calculate("12 m * 2 m");

            //Assert
            result.Result.Should().Be(Area.FromSquareMeters(24));
        }

        [TestMethod]
        public void ShouldAddAStringWithALengthBasedUnitOfMeasureInMillimeters()
        {
            //Arrange
            var repository = new Repository();
            var calculator = new Calculator(repository);

            //Act
            var result = calculator.Calculate("12 mm + 2 mm");

            //Assert
            result.Result.Should().Be(Length.FromMillimeters(14));
        }

        [TestMethod]
        public void ShouldSubtractAStringWithALengthBasedUnitOfMeasureInMillimeters()
        {
            //Arrange
            var repository = new Repository();
            var calculator = new Calculator(repository);

            //Act
            var result = calculator.Calculate("12 mm - 2 mm");

            //Assert
            result.Result.Should().Be(Length.FromMillimeters(10));
        }

        [TestMethod]
        public void ShouldMultiplyAStringWithALengthBasedUnitOfMeasureInMillimeters()
        {
            //Arrange
            var repository = new Repository();
            var calculator = new Calculator(repository);

            //Act
            var result = calculator.Calculate("12 mm * 2 mm");

            //Assert
            ((Area)result.Result).SquareMillimeters.Should().Be(24);
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenUnsupportedOperatorSymbolIsUsed()
        {
            // Arrange
            var repository = new Repository();
            var calculator = new Calculator(repository);

            // Act e Assert
            Assert.ThrowsException<FormatException>(() => calculator.Calculate("12 m : 2 m "));
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenUnsupportedOperatorSymbolIsUsed_Millimeter()
        {
            // Arrange
            var repository = new Repository();
            var calculator = new Calculator(repository);

            // Act e Assert
            Assert.ThrowsException<FormatException>(() => calculator.Calculate("12 mm : 2 mm"));
        }
        [TestMethod]
        public void ShouldThrowExceptionWhenUnsupportedOperatorSymbolIsUsed_NotationWithoutSpaces()
        {
            // Arrange
            var repository = new Repository();
            var calculator = new Calculator(repository);

            // Act e Assert
            Assert.ThrowsException<FormatException>(() => calculator.Calculate("12m:2m"));
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenUnsupportedOperatorSymbolIsUsed_Millimeter_NotationWithoutSpaces()
        {
            // Arrange
            var repository = new Repository();
            var calculator = new Calculator(repository);

            // Act e Assert
            Assert.ThrowsException<FormatException>(() => calculator.Calculate("12mm:2mm"));
        }

        [TestMethod]
        public void ShouldThrowExceptionWhenExpressionDoesNotHaveAllParts()
        {
            // Arrange
            var repository = new Repository();
            var calculator = new Calculator(repository);

            // Act
            Action result = () => calculator.Calculate("15m+15");

            // Assert
            result.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void ShouldAddAStringWithALengthBasedUnitOfMeasure_NotationWithoutSpaces()
        {
            //Arrange
            var repository = new Repository();
            var calculator = new Calculator(repository);

            //Act
            var result = calculator.Calculate("12m+2m");

            //Assert
            result.Result.Should().Be(Length.FromMeters(14));
        }

        [TestMethod]
        public void ShouldAddTwoLenghts()
        {
            //Arrange
            var repository = new Repository();
            var calculator = new Calculator(repository);
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
            var repository = new Repository();
            var calculator = new Calculator(repository);
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
            var repository = new Repository();
            var calculator = new Calculator(repository);
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
            var repository = new Repository();
            var waterWeight = new Calculator(repository);
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
            var repository = new Repository();
            var steelWeight = new Calculator(repository);
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
            var repository = new Repository();
            var aluminumWeight = new Calculator(repository);
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
            var repository = new Repository();
            var calculator = new Calculator(repository);

            //Act
            calculator.Calculate("12 mm - 2 mm");

            //Assert
            repository.Memory.Should().HaveCount(1);
        }

        [TestMethod]
        public void ShouldMemorizeEachCalculation()
        {
            //Arrange
            var repository = new Repository();
            var calculator = new Calculator(repository);

            //Act
            calculator.Calculate("12 mm - 2 mm");
            calculator.Calculate("12 mm - 2 mm");

            //Assert
            repository.Memory.Should().HaveCount(2);
        }

        [TestMethod]
        public void ShouldMemorizeCurrentMemoryPosition()
        {
            //Arrange
            var repository = new Repository();
            var calculator = new Calculator(repository);

            //Act
            calculator.Calculate("12 mm - 2 mm");
            calculator.Calculate("12 mm - 2 mm");

            //Assert
            repository.MemoryPosition.Should().Be(1);
        }

        [TestMethod]
        public void ShouldShowPreviousCalculation()
        {
            //Arrange
            var repository = new Repository();
            var calculator = new Calculator(repository);

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
            var repository = new Repository();
            var calculator = new Calculator(repository);

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
            var repository = new Repository();
            var calculator = new Calculator(repository);

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
            var repository = new Repository();
            var calculator = new Calculator(repository);

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
            var repository = new Repository();
            var calculator = new Calculator(repository);


            //Act
            var result1 = calculator.Calculate("15 m + 5 mm");
            var result2 = calculator.Calculate("15 m - 5 mm");
            var result3 = calculator.Calculate("15 m * 5 mm");

            //Assert
            result1.Result.Should().Be(Length.FromMeters(15.005));
            result2.Result.Should().Be(Length.FromMeters(14.995));
            result3.Result.Should().Be(Area.FromSquareMeters(0.075));
        }
    }
}
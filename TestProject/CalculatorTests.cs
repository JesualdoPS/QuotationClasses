using Calc.BusinessLogic;
using Calc.Persistance;
using Contracts;
using FluentAssertions;
using UnitsNet;

namespace TestProject
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public async Task ShouldAddTwoNumbers()
        {
            //Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            //Act
            var result = await calculator.Add(1, 1);


            //Assert
            result.Should().Be(2);
        }

        [TestMethod]
        public async Task ShouldCalculateWithRepetitiveNumbers()
        {
            //Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            //Act
            var result = await calculator.Calculate("3m*30m");

            //Assert
            result.Result.Should().Be(Area.FromSquareMeters(90));
        }

        [TestMethod]
        public async Task ShouldSubtractTwoNumbers()
        {
            //Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            //Act
            var result = await calculator.Subtract(2, 1);


            //Assert
            result.Should().Be(1);
        }

        [TestMethod]
        public async Task ShouldMultiplyTwoNumbers()
        {
            //Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            //Act
            var result = await calculator.Multiply(2, 3);


            //Assert
            result.Should().Be(6);
        }

        [TestMethod]
        public async Task ShouldDivideTwoNumbers()
        {
            //Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            //Act
            var result = await calculator.Divide(10, 2);


            //Assert
            result.Should().Be(5);
        }

        [TestMethod]
        public async Task ShouldAddAStringWithALengthBasedUnitOfMeasure()
        {
            //Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            //Act
            var result = await calculator.Calculate("12 m + 2 m");

            //Assert
            result.Result.Should().Be(Length.FromMeters(14));
        }

        [TestMethod]
        public async Task ShouldSubtractAStringWithALengthBasedUnitOfMeasure()
        {
            //Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            //Act
            var result = await calculator.Calculate("12 m - 2 m");

            //Assert
            result.Result.Should().Be(Length.FromMeters(10));
        }

        [TestMethod]
        public async Task ShouldMultiplyAStringWithALengthBasedUnitOfMeasure()
        {
            //Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            //Act
            var result = await calculator.Calculate("12 m * 2 m");

            //Assert
            result.Result.Should().Be(Area.FromSquareMeters(24));
        }

        [TestMethod]
        public async Task ShouldAddAStringWithALengthBasedUnitOfMeasureInMillimeters()
        {
            //Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            //Act
            var result = await calculator.Calculate("12 mm + 2 mm");

            //Assert
            result.Result.Should().Be(Length.FromMillimeters(14));
        }

        [TestMethod]
        public async Task ShouldSubtractAStringWithALengthBasedUnitOfMeasureInMillimeters()
        {
            //Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            //Act
            var result = await calculator.Calculate("12 mm - 2 mm");

            //Assert
            result.Result.Should().Be(Length.FromMillimeters(10));
        }

        [TestMethod]
        public async Task ShouldMultiplyAStringWithALengthBasedUnitOfMeasureInMillimeters()
        {
            //Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            //Act
            var result = await calculator.Calculate("12 mm * 2 mm");

            //Assert
            ((Area)result.Result).SquareMillimeters.Should().Be(24);
        }

        [TestMethod]
        public async Task ShouldThrowExceptionWhenUnsupportedOperatorSymbolIsUsed()
        {
            // Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            // Act e Assert
            await Assert.ThrowsExceptionAsync<FormatException>(() => calculator.Calculate("12 m : 2 m "));
        }

        [TestMethod]
        public async Task ShouldThrowExceptionWhenUnsupportedOperatorSymbolIsUsed_Millimeter()
        {
            // Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            // Act e Assert
            await Assert.ThrowsExceptionAsync<FormatException>(() => calculator.Calculate("12 mm : 2 mm"));
        }

        [TestMethod]
        public async Task ShouldThrowExceptionWhenUnsupportedOperatorSymbolIsUsed_NotationWithoutSpaces()
        {
            // Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            // Act e Assert
            await Assert.ThrowsExceptionAsync<FormatException>(() => calculator.Calculate("12m:2m"));
        }

        [TestMethod]
        public async Task ShouldThrowExceptionWhenUnsupportedOperatorSymbolIsUsed_Millimeter_NotationWithoutSpaces()
        {
            // Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            // Act e Assert
            await Assert.ThrowsExceptionAsync<FormatException>(() => calculator.Calculate("12mm:2mm"));
        }

        [TestMethod]
        public async Task ShouldThrowExceptionWhenExpressionDoesNotHaveAllParts()
        {
            // Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            // Act
            Func<Task> result = async () => await calculator.Calculate("15m+15");

            // Assert
            await result.Should().ThrowAsync<ArgumentException>();
        }

        [TestMethod]
        public async Task ShouldAddAStringWithALengthBasedUnitOfMeasure_NotationWithoutSpaces()
        {
            //Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            //Act
            var result = await calculator.Calculate("12m+2m");

            //Assert
            result.Result.Should().Be(Length.FromMeters(14));
        }
        
        [TestMethod]
        public async Task ShouldMultiplyThreeLenghts()
        {
            //Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);
            var lenght1 = Length.FromMeters(1);
            var lenght2 = Length.FromMeters(1);
            var lenght3 = Length.FromMeters(1);


            //Act
            var result = await calculator.MultiplyVolume(lenght1, lenght2, lenght3);


            //Assert
            result.Should().Be(Volume.FromCubicMeters(1));
        }

        [TestMethod]
        public async Task ShouldCalculateWeightOfWater()
        {
            //Arrange
            var repository = new RepositoryJson();
            var waterWeight = new Calculator(repository);
            var waterVolume = Volume.FromCubicMeters(1);

            //Act
            var result = await waterWeight.CalculateWeight(waterVolume, Materials.Water);


            //Assert
            result.Should().Be(Mass.FromKilograms(1000));
        }

        [TestMethod]
        public async Task ShouldCalculateWeightOfSteel()
        {
            //Arrange
            var repository = new RepositoryJson();
            var steelWeight = new Calculator(repository);
            var steelVolume = Volume.FromCubicMeters(1);

            //Act
            var result = await steelWeight.CalculateWeight(steelVolume, Materials.Steel);


            //Assert
            result.Should().Be(Mass.FromKilograms(7850));
        }

        [TestMethod]
        public async Task ShouldCalculateWeightOfAluminum()
        {
            //Arrange
            var repository = new RepositoryJson();
            var aluminumWeight = new Calculator(repository);
            var aluminumVolume = Volume.FromCubicMeters(1);

            //Act
            var result = await aluminumWeight.CalculateWeight(aluminumVolume, Materials.Aluminum);


            //Assert
            result.Should().Be(Mass.FromKilograms(2600));
        }

        [TestMethod]
        public void ShouldMemorizeACalculation()
        {
            //Arrange
            var repository = new RepositoryJson();
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
            var repository = new RepositoryJson();
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
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            //Act
            calculator.Calculate("12 mm - 2 mm");
            calculator.Calculate("12 mm - 2 mm");

            //Assert
            repository.MemoryPosition.Should().Be(1);
        }

        [TestMethod]
        public async Task ShouldShowPreviousCalculation()
        {
            //Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            //Act
            await calculator.Calculate("15 mm - 10 mm");
            await calculator.Calculate("35 mm - 20 mm");
            await calculator.Calculate("50 mm - 30 mm");
            var result = await calculator.Calculate("previous");

            //Assert
            result.Math.Should().Be("35 mm - 20 mm");
            result.Result.Should().Be(Length.FromMillimeters(15));
        }

        [TestMethod]
        public async Task ShouldShowNextCalculation()
        {
            //Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            //Act
            await calculator.Calculate("15 mm - 10 mm");
            await calculator.Calculate("35 mm - 20 mm");
            await calculator.Calculate("50 mm - 30 mm");
            await calculator.Calculate("previous");
            await calculator.Calculate("previous");
            var result = await calculator.Calculate("next");

            //Assert
            result.Math.Should().Be("35 mm - 20 mm");
            result.Result.Should().Be(Length.FromMillimeters(15));
        }

        [TestMethod]
        public async Task ShouldNotDoNextIfOnLastItemInMemory()
        {
            //Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            //Act
            await calculator.Calculate("15 mm - 10 mm");
            await calculator.Calculate("35 mm - 20 mm");
            var result = await calculator.Calculate("next");

            //Assert
            result.Math.Should().Be("35 mm - 20 mm");
            result.Result.Should().Be(Length.FromMillimeters(15));
        }

        [TestMethod]
        public async Task ShouldNotDoPreviousIfOnFirstItemInMemory()
        {
            //Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);

            //Act
            await calculator.Calculate("35 mm - 20 mm");
            var result = await calculator.Calculate("previous");

            //Assert
            result.Math.Should().Be("35 mm - 20 mm");
            result.Result.Should().Be(Length.FromMillimeters(15));
        }

        [TestMethod]
        public async Task ShouldCalculaterAStringWithMetersAndMillimeterBasedUnitOfMeasure()
        {
            //Arrange
            var repository = new RepositoryJson();
            var calculator = new Calculator(repository);


            //Act
            var result1 = await calculator.Calculate("15 m + 5 mm");
            var result2 = await calculator.Calculate("15 m - 5 mm");
            var result3 = await calculator.Calculate("15 m * 5 mm");

            //Assert
            result1.Result.Should().Be(Length.FromMeters(15.005));
            result2.Result.Should().Be(Length.FromMeters(14.995));
            result3.Result.Should().Be(Area.FromSquareMeters(0.075));
        }
    }
}
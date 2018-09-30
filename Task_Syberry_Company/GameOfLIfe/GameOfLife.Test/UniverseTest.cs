using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace GameOfLife.Test
{
    public class UniverseTest
    {
        [Fact]
        public void CheckNotNullWhenInitializeWithTrueArg()
        {
            // Arrange
            var rows = 12;
            var columns = 14;
            var population = 10;

            // Act
            var universe = new Universe(rows, columns, population);

            // Assert
            Assert.NotNull(universe);
        }

        [Fact]
        public void CheckNotNullWhenInitializeWithFalseRowsArg()
        {
            // Arrange
            var rows = 9;
            var columns = 14;
            var population = 10;

            // Act
            try
            {
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var universe = new Universe(rows, columns, population);
                });
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Assert
                Assert.Equal("Number of rows must be at least 10 and not more than 40", ex.Message);
            }           
        }

        [Fact]
        public void CheckNotNullWhenInitializeWithFalseColumnsArg()
        {
            // Arrange
            var rows = 15;
            var columns = 41;
            var population = 10;

            // Act
            try
            {
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var universe = new Universe(rows, columns, population);
                });
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Assert
                Assert.Equal("Number of columns must be at least 10 and not more than 40", ex.Message);
            }
        }

        [Fact]
        public void CheckNotNullWhenInitializeWithFalsePopulationArg()
        {
            // Arrange
            var rows = 15;
            var columns = 15;
            var population = 1;

            // Act
            try
            {
                Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    var universe = new Universe(rows, columns, population);
                });
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Assert
                Assert.Equal("The population must be at least 3 and not more than 10", ex.Message);
            }
        }

        [Fact]
        public void SetBackground_CheckBackground()
        {
            // Arrange
            var background = ConsoleColor.Black;

            // Act
            var universe = new Universe(10, 10, 5);

            // Assert
            Assert.True(Console.BackgroundColor == background);
        }

        [Theory]
        [InlineData(10, 10, 3)]
        [InlineData(40, 40, 10)]
        [InlineData(40, 10, 5)]
        public void CheckTheoryInitialize(int row, int col, int pop)
        {
            // Act
            var universe = new Universe(row, col, pop);

            // Assert
            Assert.NotNull(universe);
        }
    }
}

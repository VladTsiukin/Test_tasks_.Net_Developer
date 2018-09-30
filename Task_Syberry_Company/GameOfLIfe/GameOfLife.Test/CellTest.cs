using System;
using Xunit;

namespace GameOfLife.Test
{
    public class CellTest
    {
        [Fact]
        public void CheckStateWhenInitialize()
        {
            // Arrange
            var cell = new Cell();

            // Act
            var state = State.Dead;
            var gen = 0;

            // Assert
            Assert.Equal(state, cell.State);
            Assert.Equal(gen, cell.Generation);
        }

        [Fact]
        public void CheckStateWhenInitializeWithArg()
        {
            // Arrange
            var cell = new Cell(State.Alive);

            // Act
            var state = State.Alive;
            var gen = 1;

            // Assert
            Assert.Equal(state, cell.State);
            Assert.Equal(gen, cell.Generation);
        }

        [Fact]
        public void UpdateState_CheckStateWithoutArg()
        {
            // Arrange
            var cell = new Cell();

            // Act
            cell.UpdateState();
            var state = State.Dead;
            var gen = 0;

            // Assert
            Assert.Equal(state, cell.State);
            Assert.Equal(gen, cell.Generation);
        }

        [Fact]
        public void UpdateState_CheckStateWithArg()
        {
            // Arrange
            var cell = new Cell();

            // Act
            cell.UpdateState(State.Alive);
            var state = State.Alive;
            var gen = 1;

            // Assert
            Assert.Equal(state, cell.State);
            Assert.Equal(gen, cell.Generation);
        }
    }
}

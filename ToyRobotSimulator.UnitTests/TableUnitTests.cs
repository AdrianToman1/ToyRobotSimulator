using Xunit;

namespace ToyRobotSimulator.UnitTests
{
    public class TableUnitTests
    {
        [Fact]
        public void IsValidLocation_FirstQuadrant_True()
        {
            // Arrange
            var table = new Table();

            // Act & Assert
            Assert.True(table.IsValidLocation(1, 1));
        }

        [Fact]
        public void IsValidLocation_SecondQuadrant_False()
        {
            // Arrange
            var table = new Table();

            // Act & Assert
            Assert.False(table.IsValidLocation(-1, 1));
        }

        [Fact]
        public void IsValidLocation_ThirdQuadrant_False()
        {
            // Arrange
            var table = new Table();

            // Act & Assert
            Assert.False(table.IsValidLocation(-1, -1));
        }

        [Fact]
        public void IsValidLocation_ForthQuadrant_False()
        {
            // Arrange
            var table = new Table();

            // Act & Assert
            Assert.False(table.IsValidLocation(1, -1));
        }

        [Fact]
        public void IsValidLocation_Origin_True()
        {
            // Arrange
            var table = new Table();

            // Act & Assert
            Assert.True(table.IsValidLocation(0, 0));
        }

        [Fact]
        public void IsValidLocation_x0_True()
        {
            // Arrange
            var table = new Table();

            // Act & Assert
            Assert.True(table.IsValidLocation(0, 1));
        }

        [Fact]
        public void IsValidLocation_y0_True()
        {
            // Arrange
            var table = new Table();

            // Act & Assert
            Assert.True(table.IsValidLocation(1, 0));
        }

        [Fact]
        public void IsValidLocation_x4_True()
        {
            // Arrange
            var table = new Table();

            // Act & Assert
            Assert.True(table.IsValidLocation(4, 1));
        }

        [Fact]
        public void IsValidLocation_y4_True()
        {
            // Arrange
            var table = new Table();

            // Act & Assert
            Assert.True(table.IsValidLocation(1, 4));
        }

        [Fact]
        public void IsValidLocation_x4y4_True()
        {
            // Arrange
            var table = new Table();

            // Act & Assert
            Assert.True(table.IsValidLocation(4, 4));
        }

        [Fact]
        public void IsValidLocation_x5_False()
        {
            // Arrange
            var table = new Table();

            // Act & Assert
            Assert.False(table.IsValidLocation(5, 1));
        }

        [Fact]
        public void IsValidLocation_y5_False()
        {
            // Arrange
            var table = new Table();

            // Act & Assert
            Assert.False(table.IsValidLocation(1, 5));
        }

        [Fact]
        public void IsValidLocation_x5y5_False()
        {
            // Arrange
            var table = new Table();

            // Act & Assert
            Assert.False(table.IsValidLocation(5, 5));
        }
    }
}

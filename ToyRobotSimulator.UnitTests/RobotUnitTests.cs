using System;
using Xunit;

namespace ToyRobotSimulator.UnitTests
{
    public class RobotUnitTests
    {
        [Fact]
        public void InstantiateRobot_OK()
        {
            // Arrange & Act
            var robot = new Robot();

            // Assert
            Assert.NotNull(robot);
            Assert.Null(robot.X);
            Assert.Null(robot.Y);
            Assert.Null(robot.Heading);
            Assert.Null(robot.Table);
        }

        [Fact]
        public void PlaceOnTable_TableNull_ThrowsArgumentNullException()
        {
            // Arrange
            var robot = new Robot();

            // Act & Assert
            Assert.Throws<ArgumentNullException>("table", () => robot.PlaceOnTable(null, 0, 0, 0));
        }

        [Fact]
        public void PlaceOnTable_InvalidLocation_ThrowsArgumentException()
        {
            // Arrange
            var robot = new Robot();

            // Act & Assert
            Assert.Throws<ArgumentException>( () => robot.PlaceOnTable(new Table(), -1, -1, 0));
        }

        [Fact]
        public void PlaceOnTable_OK()
        {
            // Arrange
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 1;
            var heading = 0;

            // Act
            robot.PlaceOnTable(table, x, y, heading);

            // Assert
            Assert.Equal(x, robot.X);
            Assert.Equal(y, robot.Y);
            Assert.Equal(heading, robot.Heading);
            Assert.Same(table, robot.Table);
        }

        [Fact]
        public void PlaceOnTable_Heading360_HeadingZero()
        {
            // Arrange
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 1;
            var heading = 360;

            // Act
            robot.PlaceOnTable(table, x, y, heading);

            // Assert
            Assert.Equal(0, robot.Heading);
        }

        [Fact]
        public void PlaceOnTable_Heading90_Heading90()
        {
            // Arrange
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 1;
            var heading = 90;

            // Act
            robot.PlaceOnTable(table, x, y, heading);

            // Assert
            Assert.Equal(heading, robot.Heading);
        }

        [Fact]
        public void PlaceOnTable_HeadingNegative90_Heading90()
        {
            // Arrange
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 1;
            var heading = -90;

            // Act
            robot.PlaceOnTable(table, x, y, heading);

            // Assert
            Assert.Equal(90, robot.Heading);
        }

        [Fact]
        public void PlaceOnTable_Heading450_Heading90()
        {
            // Arrange
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 1;
            var heading = 450;

            // Act
            robot.PlaceOnTable(table, x, y, heading);

            // Assert
            Assert.Equal(90, robot.Heading);
        }

        [Fact]
        public void IsOnTheTable_PlacedOnTable_True()
        {
            // Arrange
            var table = new Table();

            var robot = new Robot();
            robot.PlaceOnTable(table, 0, 0, 0);

            // Act & Assert
            Assert.True(robot.IsOnTheTable());
        }

        [Fact]
        public void IsOnTheTable_NeverPlacedOnTable_False()
        {
            // Arrange
            var robot = new Robot();

            // Act & Assert
            Assert.False(robot.IsOnTheTable());
        }
    }
}
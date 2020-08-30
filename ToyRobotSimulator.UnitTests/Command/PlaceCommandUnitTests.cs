using System;
using ToyRobotSimulator.Commands;
using Xunit;

namespace ToyRobotSimulator.UnitTests.Command
{
    public class PlaceCommandUnitTests
    {
        [Fact]
        public void InstantiatePlaceCommand_OK()
        {
            // Arrange
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 0;
            var heading = CompassPoint.North;

            // Act
            var placeCommand = new PlaceCommand(robot, table, x, y, heading);

            // Assert
            Assert.NotNull(placeCommand);
            Assert.Equal(x, placeCommand.X);
            Assert.Equal(y, placeCommand.Y);
            Assert.Equal(heading, placeCommand.Heading);
            Assert.Same(table, placeCommand.Table);
        }

        [Fact]
        public void InstantiateReportCommand_TableNull_ThrowsArgumentNullException()
        {
            // Arrange
            var robot = new Robot();
            var x = 0;
            var y = 0;
            var heading = CompassPoint.North;

            // Act & Assert
            Assert.Throws<ArgumentNullException>("table", () => new PlaceCommand(robot, null, x, y, heading));
        }

        [Fact]
        public void InstantiateReportCommand_LocationInvalid_ThrowsArgumentException()
        {
            // Arrange
            var robot = new Robot();
            var table = new Table();
            var x = 5;
            var y = 5;
            var heading = CompassPoint.North;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => new PlaceCommand(robot, table, x, y, heading));
        }

        [Fact]
        public void Execute_RobotYetPlacedOnTable_OK()
        {
            // Arrange
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 0;
            var heading = CompassPoint.North;
            var placeCommand = new PlaceCommand(robot, table, x, y, heading);

            // Act
            placeCommand.Execute();

            // Assert
            Assert.NotNull(placeCommand);
            Assert.Equal(x, robot.X);
            Assert.Equal(y, robot.Y);
            Assert.Equal(heading, (CompassPoint) robot.Heading);
            Assert.Same(table, robot.Table);
        }

        [Fact]
        public void Execute_AlreadyPlacedOnTable_OK()
        {
            // Arrange
            // Place robot on table
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 0;
            var heading = CompassPoint.North;
            var placeCommand = new PlaceCommand(robot, table, x, y, heading);
            placeCommand.Execute();

            // Act
            // Replace robot on table in different location and heading.
            x = 1;
            y = 1;
            heading = CompassPoint.West;
            var replaceCommand = new PlaceCommand(robot, table, x, y, heading);
            replaceCommand.Execute();

            // Assert
            Assert.NotNull(placeCommand);
            Assert.Equal(x, robot.X);
            Assert.Equal(y, robot.Y);
            Assert.Equal(heading, (CompassPoint) robot.Heading);
            Assert.Same(table, robot.Table);
        }

        [Fact]
        public void Execute_PlacedDifferentTable_OK()
        {
            // Arrange
            // Place robot on table1
            var robot = new Robot();
            var table1 = new Table();
            var x1 = 0;
            var y1 = 0;
            var heading1 = CompassPoint.North;
            var placeCommand = new PlaceCommand(robot, table1, x1, y1, heading1);
            placeCommand.Execute();

            // Act
            // Replace robot on table2
            var table2 = new Table();
            var x2 = 0;
            var y2 = 0;
            var heading2 = CompassPoint.North;
            var replaceCommand = new PlaceCommand(robot, table2, x2, y2, heading2);
            replaceCommand.Execute();

            // Assert
            Assert.NotNull(placeCommand);
            Assert.Equal(x2, robot.X);
            Assert.Equal(y2, robot.Y);
            Assert.Equal(heading2, (CompassPoint) robot.Heading);
            Assert.Same(table2, robot.Table);
        }
    }
}

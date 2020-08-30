using ToyRobotSimulator.Commands;
using Xunit;

namespace ToyRobotSimulator.UnitTests.Command
{
    public class RightCommandUnitTests
    {
        [Fact]
        public void InstantiateRightCommand_OK()
        {
            // Arrange
            var robot = new Robot();

            // Act
            var rightCommand = new RightCommand(robot);

            // Assert
            Assert.NotNull(rightCommand);
        }

        [Fact]
        public void Execute_RobotNotPlacedOnTable_NoEffect()
        {
            // Arrange
            var robot = new Robot();
            var rightCommand = new RightCommand(robot);

            // Act
            rightCommand.Execute();

            // Assert
            Assert.Null(robot.Heading);
            Assert.Null(robot.X);
            Assert.Null(robot.Y);
        }

        [Fact]
        public void Execute_RobotFacingNorth_FacingEast()
        {
            // Arrange
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 0;
            var heading = (int) CompassPoint.North;
            robot.PlaceOnTable(table, x, y, heading);

            var rightCommand = new RightCommand(robot);

            // Act
            rightCommand.Execute();

            // Assert
            Assert.Equal(x, robot.X);
            Assert.Equal(y, robot.Y);
            Assert.Equal((int) CompassPoint.East, robot.Heading);
            Assert.Same(table, robot.Table);
        }

        [Fact]
        public void Execute_RobotFacingSouth_FacingWest()
        {
            // Arrange
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 0;
            var heading = (int) CompassPoint.South;
            robot.PlaceOnTable(table, x, y, heading);

            var rightCommand = new RightCommand(robot);

            // Act
            rightCommand.Execute();

            // Assert
            Assert.Equal(x, robot.X);
            Assert.Equal(y, robot.Y);
            Assert.Equal((int) CompassPoint.West, robot.Heading);
            Assert.Same(table, robot.Table);
        }

        [Fact]
        public void Execute_RobotFacingEast_FacingSouth()
        {
            // Arrange
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 0;
            var heading = (int) CompassPoint.East;
            robot.PlaceOnTable(table, x, y, heading);

            var rightCommand = new RightCommand(robot);

            // Act
            rightCommand.Execute();

            // Assert
            Assert.Equal(x, robot.X);
            Assert.Equal(y, robot.Y);
            Assert.Equal((int) CompassPoint.South, robot.Heading);
            Assert.Same(table, robot.Table);
        }

        [Fact]
        public void Execute_RobotFacingWest_FacingNorth()
        {
            // Arrange
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 0;
            var heading = (int) CompassPoint.West;
            robot.PlaceOnTable(table, x, y, heading);

            var rightCommand = new RightCommand(robot);

            // Act
            rightCommand.Execute();

            // Assert
            Assert.Equal(x, robot.X);
            Assert.Equal(y, robot.Y);
            Assert.Equal((int) CompassPoint.North, robot.Heading);
            Assert.Same(table, robot.Table);
        }
    }
}

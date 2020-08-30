using ToyRobotSimulator.Commands;
using Xunit;

namespace ToyRobotSimulator.UnitTests.Command
{
    public class MoveCommandUnitTests
    {
        [Fact]
        public void Execute_RobotNotPlacedOnTable_NoEffect()
        {
            // Arrange
            var robot = new Robot();
            var moveCommand = new MoveCommand();

            // Act
            moveCommand.Execute(robot);

            // Assert
            Assert.Null(robot.Heading);
            Assert.Null(robot.X);
            Assert.Null(robot.Y);
        }

        [Fact]
        public void Execute_MoveFacingNorth_IncreaseY()
        {
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 0;
            var heading = (int) CompassPoint.North;
            robot.PlaceOnTable(table, x, y, heading);

            var moveCommand = new MoveCommand();

            // Act
            moveCommand.Execute(robot);

            // Assert
            Assert.Equal(x, robot.X);
            Assert.Equal(y + 1, robot.Y);
            Assert.Equal(heading, robot.Heading);
            Assert.Same(table, robot.Table);
        }

        [Fact]
        public void Execute_MoveFacingSouth_DecreaseY()
        {
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 1;
            var heading = (int) CompassPoint.South;
            robot.PlaceOnTable(table, x, y, heading);

            var moveCommand = new MoveCommand();

            // Act
            moveCommand.Execute(robot);

            // Assert
            Assert.Equal(x, robot.X);
            Assert.Equal(y - 1, robot.Y);
            Assert.Equal(heading, robot.Heading);
            Assert.Same(table, robot.Table);
        }

        [Fact]
        public void Execute_MoveFacingEast_IncreaseX()
        {
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 0;
            var heading = (int) CompassPoint.East;
            robot.PlaceOnTable(table, x, y, heading);

            var moveCommand = new MoveCommand();

            // Act
            moveCommand.Execute(robot);

            // Assert
            Assert.Equal(x + 1, robot.X);
            Assert.Equal(y, robot.Y);
            Assert.Equal(heading, robot.Heading);
            Assert.Same(table, robot.Table);
        }

        [Fact]
        public void Execute_MoveFacingWest_DecreaseX()
        {
            var robot = new Robot();
            var table = new Table();
            var x = 1;
            var y = 0;
            var heading = (int) CompassPoint.West;
            robot.PlaceOnTable(table, x, y, heading);

            var moveCommand = new MoveCommand();

            // Act
            moveCommand.Execute(robot);

            // Assert
            Assert.Equal(x - 1, robot.X);
            Assert.Equal(y, robot.Y);
            Assert.Equal(heading, robot.Heading);
            Assert.Same(table, robot.Table);
        }

        [Fact]
        public void Execute_MoveFacingNorthOnTableEdge_NoEffect()
        {
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 4;
            var heading = (int) CompassPoint.North;
            robot.PlaceOnTable(table, x, y, heading);

            var moveCommand = new MoveCommand();

            // Act
            moveCommand.Execute(robot);

            // Assert
            Assert.Equal(x, robot.X);
            Assert.Equal(y, robot.Y);
            Assert.Equal(heading, robot.Heading);
            Assert.Same(table, robot.Table);
        }

        [Fact]
        public void Execute_MoveFacingSouthOnTableEdge_NoEffect()
        {
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 0;
            var heading = (int) CompassPoint.South;
            robot.PlaceOnTable(table, x, y, heading);

            var moveCommand = new MoveCommand();

            // Act
            moveCommand.Execute(robot);

            // Assert
            Assert.Equal(x, robot.X);
            Assert.Equal(y, robot.Y);
            Assert.Equal(heading, robot.Heading);
            Assert.Same(table, robot.Table);
        }

        [Fact]
        public void Execute_MoveFacingEastOnTableEdge_NoEffect()
        {
            var robot = new Robot();
            var table = new Table();
            var x = 4;
            var y = 0;
            var heading = (int) CompassPoint.East;
            robot.PlaceOnTable(table, x, y, heading);

            var moveCommand = new MoveCommand();

            // Act
            moveCommand.Execute(robot);

            // Assert
            Assert.Equal(x, robot.X);
            Assert.Equal(y, robot.Y);
            Assert.Equal(heading, robot.Heading);
            Assert.Same(table, robot.Table);
        }

        [Fact]
        public void Execute_MoveFacingWestOnTableEdge_NoEffect()
        {
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 0;
            var heading = (int) CompassPoint.West;
            robot.PlaceOnTable(table, x, y, heading);

            var moveCommand = new MoveCommand();

            // Act
            moveCommand.Execute(robot);

            // Assert
            Assert.Equal(x, robot.X);
            Assert.Equal(y, robot.Y);
            Assert.Equal(heading, robot.Heading);
            Assert.Same(table, robot.Table);
        }
    }
}

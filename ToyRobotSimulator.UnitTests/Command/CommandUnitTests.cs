using System;
using Xunit;

namespace ToyRobotSimulator.UnitTests.Command
{
    public class CommandUnitTests
    {
        [Fact]
        public void Execute_RobotNull_ThrowsArgumentNullException()
        {
            // Arrange
            var command = new TestCommand();

            // Act & Assert
            Assert.Throws<ArgumentNullException>("robot", () => command.Execute(null));
        }

        [Fact]
        public void Execute_OK()
        {
            // Arrange
            var robot = new Robot();
            var command = new TestCommand();

            // Act
            command.Execute(robot);

            // Assert
            Assert.Equal(1, robot.X);
            Assert.Equal(-1, robot.Y);
        }

        private class TestCommand : Commands.Command
        {
            protected override void DoExecute(Robot robot)
            {
                robot.X = 1;
                robot.Y = -1;
            }
        }
    }
}

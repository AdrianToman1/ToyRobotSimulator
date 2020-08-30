using System;
using Xunit;

namespace ToyRobotSimulator.UnitTests.Command
{
    public class CommandUnitTests
    {
        [Fact]
        public void InstantiateCommand_OK()
        {
            // Arrange
            var robot = new Robot();

            // Act
            var command = new TestCommand(robot);

            // Assert
            Assert.NotNull(command);
            Assert.Same(robot, command.Robot);
        }

        [Fact]
        public void InstantiateCommand_RobotNull_ThrowsArgumentNullException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>("robot", () => new TestCommand(null));
        }

        [Fact]
        public void Execute_OK()
        {
            // Arrange
            var robot = new Robot();
            var command = new TestCommand(robot);

            // Act
            command.Execute();

            // Assert
            Assert.Equal(1, robot.X);
            Assert.Equal(-1, robot.Y);
        }

        private class TestCommand : Commands.Command
        {
            public TestCommand(Robot robot) : base(robot)
            {
            }

            protected override void DoExecute(Robot robot)
            {
                robot.X = 1;
                robot.Y = -1;
            }
        }
    }
}

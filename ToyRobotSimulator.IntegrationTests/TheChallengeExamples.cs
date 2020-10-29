using System.Collections.Generic;
using System.IO;
using ToyRobotSimulator.Commands;
using Xunit;

namespace ToyRobotSimulator.IntegrationTests
{
    /// <remarks>
    ///     These tests are the three examples from The Challenge
    /// </remarks>
    public class TheChallengeExamples
    {
        [Fact]
        public void Example_A()
        {
            // Arrange
            var simulation = new Simulation();
            var textWriter = new StringWriter();
            var commands = new []
            {
                "PLACE 0,0,North",
                "MOVE",
                "REPORT"
            };

            // Act
            foreach (var command in commands)
            {
                simulation.Execute(Command.Parse(command, textWriter));
            }

            // Assert
            Assert.Equal("0,1,North", textWriter.ToString().TrimEnd());
        }

        [Fact]
        public void Example_B()
        {
            // Arrange
            var simulation = new Simulation();
            var textWriter = new StringWriter();
            var commands = new[]
            {
                "PLACE 0,0,North",
                "LEFT",
                "REPORT"
            };

            // Act
            foreach (var command in commands)
            {
                simulation.Execute(Command.Parse(command, textWriter));
            }

            // Assert
            Assert.Equal("0,0,West", textWriter.ToString().TrimEnd());
        }

        [Fact]
        public void Example_C()
        {
            // Arrange
            var simulation = new Simulation();
            var textWriter = new StringWriter();
            var commands = new[]
            {
                "PLACE 1,2,East",
                "MOVE",
                "MOVE",
                "LEFT",
                "MOVE",
                "REPORT"
            };

            // Act
            foreach (var command in commands)
            {
                simulation.Execute(Command.Parse(command, textWriter));
            }

            // Assert
            Assert.Equal("3,3,North", textWriter.ToString().TrimEnd());
        }
    }
}

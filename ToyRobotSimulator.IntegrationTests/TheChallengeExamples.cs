using System.Collections.Generic;
using System.IO;
using ToyRobotSimulator.Commands;
using Xunit;

namespace ToyRobotSimulator.SimulationTests
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
            var table = new Table();
            var textWriter = new StringWriter();
            var commands = new List<Commands.Command>
            {
                new PlaceCommand(table, 0, 0, CompassPoint.North),
                new MoveCommand(),
                new ReportCommand(textWriter)
            };

            // Act
            simulation.Execute(commands);

            // Assert
            Assert.Equal("0,1,North", textWriter.ToString().TrimEnd());
        }

        [Fact]
        public void Example_B()
        {
            // Arrange
            var simulation = new Simulation();
            var table = new Table();
            var textWriter = new StringWriter();
            var commands = new List<Commands.Command>
            {
                new PlaceCommand(table, 0, 0, CompassPoint.North),
                new LeftCommand(),
                new ReportCommand(textWriter)
            };

            // Act
            simulation.Execute(commands);

            // Assert
            Assert.Equal("0,0,West", textWriter.ToString().TrimEnd());
        }

        [Fact]
        public void Example_C()
        {
            // Arrange
            var simulation = new Simulation();
            var table = new Table();
            var textWriter = new StringWriter();
            var commands = new List<Commands.Command>
            {
                new PlaceCommand(table, 1, 2, CompassPoint.East),
                new MoveCommand(),
                new MoveCommand(),
                new LeftCommand(),
                new MoveCommand(),
                new ReportCommand(textWriter)
            };

            // Act
            simulation.Execute(commands);

            // Assert
            Assert.Equal("3,3,North", textWriter.ToString().TrimEnd());
        }
    }
}

using System;
using System.Collections.Generic;
using ToyRobotSimulator.Commands;
using Xunit;

namespace ToyRobotSimulator.UnitTests
{
    public class SimulationUnitTests
    {
        [Fact]
        public void InstantiateSimulation_OK()
        {
            // Arrange & Act
            var simulation = new Simulation();

            // Assert
            Assert.NotNull(simulation);
            Assert.NotNull(simulation.Robot);
        }

        [Fact]
        public void Execute_CommandNull_ThrowsArgumentNullException()
        {
            // Arrange
            var simulation = new Simulation();
            Commands.Command command = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>("command", () => simulation.Execute(command));
        }

        [Fact]
        public void Execute_Command_OK()
        {
            // Arrange
            var simulation = new Simulation();
            var table = new Table();
            var x = 1;
            var y = 2;
            var heading = CompassPoint.North;
            var placeCommand = new PlaceCommand(table, x, y, heading);

            // Act
            simulation.Execute(placeCommand);

            // Assert
            Assert.NotNull(simulation.Robot);
            Assert.Equal(x, simulation.Robot.X);
            Assert.Equal(y, simulation.Robot.Y);
            Assert.Equal(heading, (CompassPoint) simulation.Robot.Heading);
            Assert.Same(table, simulation.Robot.Table);
        }

        [Fact]
        public void Execute_CommandsNull_ThrowsArgumentException()
        {
            // Arrange
            var simulation = new Simulation();
            List<Commands.Command> commands = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>("commands", () => simulation.Execute(commands));
        }

        [Fact]
        public void Execute_NoCommands_NoChange()
        {
            // Arrange
            var simulation = new Simulation();
            var commands = new List<Commands.Command>();
            
            // Act
            simulation.Execute(commands);

            // Assert
            Assert.NotNull(simulation.Robot);
            Assert.Null(simulation.Robot.X);
            Assert.Null(simulation.Robot.Y);
            Assert.Null(simulation.Robot.Heading);
            Assert.Null(simulation.Robot.Table);
        }

        [Fact]
        public void Execute_SingleCommands_OK()
        {
            // Arrange
            var simulation = new Simulation();
            var table = new Table();
            var x = 1;
            var y = 2;
            var heading = CompassPoint.North;
            var commands = new List<Commands.Command>
            {
                new PlaceCommand(table, x, y, heading)
            };

            // Act
            simulation.Execute(commands);

            // Assert
            Assert.NotNull(simulation.Robot);
            Assert.Equal(x, simulation.Robot.X);
            Assert.Equal(y, simulation.Robot.Y);
            Assert.Equal(heading, (CompassPoint)simulation.Robot.Heading);
            Assert.Same(table, simulation.Robot.Table);
        }

        [Fact]
        public void Execute_MultipleCommands_OK()
        {
            // Arrange
            var simulation = new Simulation();
            var table = new Table();
            var commands = new List<Commands.Command>
            {
                new PlaceCommand(table, 0, 0, CompassPoint.North),
                new RightCommand(),
                new MoveCommand()
            };

            // Act
            simulation.Execute(commands);

            // Assert
            Assert.NotNull(simulation.Robot);
            Assert.Equal(1, simulation.Robot.X);
            Assert.Equal(0, simulation.Robot.Y);
            Assert.Equal(CompassPoint.East, (CompassPoint)simulation.Robot.Heading);
            Assert.Same(table, simulation.Robot.Table);
        }
    }
}

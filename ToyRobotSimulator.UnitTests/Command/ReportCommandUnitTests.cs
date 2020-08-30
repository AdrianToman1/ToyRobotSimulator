using System;
using System.IO;
using ToyRobotSimulator.Commands;
using Xunit;

namespace ToyRobotSimulator.UnitTests.Command
{
    public class ReportCommandUnitTests
    {
        [Fact]
        public void InstantiateReportCommand_OK()
        {
            // Arrange
            var textWriter = new StringWriter();

            // Act
            var reportCommand = new ReportCommand(textWriter);

            // Assert
            Assert.NotNull(reportCommand);
            Assert.Same(textWriter, reportCommand.TextWriter);
        }

        [Fact]
        public void InstantiateReportCommand_TextWriterNull_ThrowsArgumentNullException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>("textWriter", () => new ReportCommand(null));
        }

        [Fact]
        public void Execute_RobotNotPlacedOnTable_OK()
        {
            // Arrange
            var robot = new Robot();
            var textWriter = new StringWriter();
            var reportCommand = new ReportCommand(textWriter);

            // Act
            reportCommand.Execute(robot);

            // Assert
            Assert.Null(robot.Heading);
            Assert.Null(robot.X);
            Assert.Null(robot.Y);
            Assert.Equal("Not roaming upon a table", textWriter.ToString().TrimEnd());
        }

        [Fact]
        public void Execute_RobotPlacedOnTable_OK()
        {
            var robot = new Robot();
            var table = new Table();
            var x = 0;
            var y = 1;
            var heading = (int) CompassPoint.North;
            robot.PlaceOnTable(table, x, y, heading);
            var textWriter = new StringWriter();

            var reportCommand = new ReportCommand(textWriter);

            // Act
            reportCommand.Execute(robot);

            // Assert
            Assert.Equal(x, robot.X);
            Assert.Equal(y, robot.Y);
            Assert.Equal((int) CompassPoint.North, robot.Heading);
            Assert.Same(table, robot.Table);
            Assert.Equal("0,1,North", textWriter.ToString().TrimEnd());
        }
    }
}

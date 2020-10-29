using System;
using System.IO;
using ToyRobotSimulator.Commands;
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

        [Fact]
        public void Parse_sNull_ThrowsArgumentNullException()
        {
            // Arrange
            var textWriter = new StringWriter();

            // Act & Assert
            Assert.Throws<ArgumentNullException>("s", () => Commands.Command.Parse(null, textWriter));
        }

        [Fact]
        public void Parse_TextWriterNull_ThrowsArgumentNullException()
        {
            // Arrange
            const string commandText = "LEFT";

            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>("textWriter", () => Commands.Command.Parse(commandText, null));
        }

        [Fact]
        public void Parse_sEmpty_ThrowsArgumentException()
        {
            // Arrange
            var commandText = string.Empty;
            var textWriter = new StringWriter();

            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>("s", () => Commands.Command.Parse(commandText, textWriter));
        }

        [Fact]
        public void Parse_sWhitespace_ThrowsArgumentException()
        {
            // Arrange
            const string commandText = "   ";
            var textWriter = new StringWriter();

            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>("s", () => Commands.Command.Parse(commandText, textWriter));
        }

        [Fact]
        public void Parse_Left_OK()
        {
            // Arrange
            const string commandText = "LEFT";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<LeftCommand>(command);
        }

        [Fact]
        public void Parse_Left_CaseInsensitive_OK()
        {
            // Arrange
            const string commandText = "lEFt";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<LeftCommand>(command);
        }

        [Fact]
        public void Parse_Left_ExtraParameters_OK()
        {
            // Arrange
            const string commandText = "LEFT FOO";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<LeftCommand>(command);
        }

        [Fact]
        public void Parse_Left_Whitespace_OK()
        {
            // Arrange
            const string commandText = "    LEFT    ";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<LeftCommand>(command);
        }

        [Fact]
        public void Parse_Right_OK()
        {
            // Arrange
            const string commandText = "RIGHT";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<RightCommand>(command);
        }

        [Fact]
        public void Parse_Right_CaseInsensitive_OK()
        {
            // Arrange
            const string commandText = "rIGHt";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<RightCommand>(command);
        }

        [Fact]
        public void Parse_Right_ExtraParameters_OK()
        {
            // Arrange
            const string commandText = "RIGHT FOO";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<RightCommand>(command);
        }

        [Fact]
        public void Parse_Right_Whitespace_OK()
        {
            // Arrange
            const string commandText = "   RIGHT   ";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<RightCommand>(command);
        }

        [Fact]
        public void Parse_Move_OK()
        {
            // Arrange
            const string commandText = "MOVE";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<MoveCommand>(command);
        }

        [Fact]
        public void Parse_Move_CaseInsensitive_OK()
        {
            // Arrange
            const string commandText = "mOVe";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<MoveCommand>(command);
        }

        [Fact]
        public void Parse_Move_ExtraParameters_OK()
        {
            // Arrange
            const string commandText = "MOVE FOO";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<MoveCommand>(command);
        }

        [Fact]
        public void Parse_Move_Whitespace_OK()
        {
            // Arrange
            const string commandText = "   MOVE   ";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<MoveCommand>(command);
        }

        [Fact]
        public void Parse_Report_OK()
        {
            // Arrange
            const string commandText = "REPORT";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<ReportCommand>(command);
        }

        [Fact]
        public void Parse_Report_CaseInsensitive_OK()
        {
            // Arrange
            const string commandText = "rEPORt";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<ReportCommand>(command);
        }

        [Fact]
        public void Parse_Report_ExtraParameters_OK()
        {
            // Arrange
            const string commandText = "REPORT FOO";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<ReportCommand>(command);
        }

        [Fact]
        public void Parse_Report_Whitespace_OK()
        {
            // Arrange
            const string commandText = "   REPORT   ";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<ReportCommand>(command);
        }

        [Fact]
        public void Parse_Place_OK()
        {
            // Arrange
            const string commandText = "PLACE 1,2,EAST";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<PlaceCommand>(command);
            Assert.Equal(1, ((PlaceCommand)command).X);
            Assert.Equal(2, ((PlaceCommand)command).Y);
            Assert.Equal(CompassPoint.East, ((PlaceCommand)command).Heading);
        }

        [Fact]
        public void Parse_Place_CaseInsensitive_OK()
        {
            // Arrange
            const string commandText = "pLACe 1,2,eASt";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<PlaceCommand>(command);
            Assert.Equal(1, ((PlaceCommand)command).X);
            Assert.Equal(2, ((PlaceCommand)command).Y);
            Assert.Equal(CompassPoint.East, ((PlaceCommand)command).Heading);
        }

        [Fact]
        public void Parse_Place_ExtraParameters_OK()
        {
            // Arrange
            const string commandText = "PLACE 1,2,EAST,FOO";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<PlaceCommand>(command);
            Assert.Equal(1, ((PlaceCommand)command).X);
            Assert.Equal(2, ((PlaceCommand)command).Y);
            Assert.Equal(CompassPoint.East, ((PlaceCommand)command).Heading);
        }

        [Fact]
        public void Parse_Place_Whitespace_OK()
        {
            // Arrange
            const string commandText = "   PLACE   1  ,  2  ,  EAST    ";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<PlaceCommand>(command);
            Assert.Equal(1, ((PlaceCommand)command).X);
            Assert.Equal(2, ((PlaceCommand)command).Y);
            Assert.Equal(CompassPoint.East, ((PlaceCommand)command).Heading);
        }

        [Fact]
        public void Parse_Place_InsufficientParameters_OK()
        {
            // Arrange
            const string commandText = "PLACE FOO";
            var textWriter = new StringWriter();

            // Act & Assert
            Assert.Throws<Exception>(() => Commands.Command.Parse(commandText, textWriter));
        }

        [Fact]
        public void Parse_Place_FirstParameterWrongType_ThrowsException()
        {
            // Arrange
            const string commandText = "PLACE FOO,2,EAST";
            var textWriter = new StringWriter();

            // Act & Assert
            Assert.Throws<Exception>(() => Commands.Command.Parse(commandText, textWriter));
        }

        [Fact]
        public void Parse_Place_SecondParameterWrongType_ThrowsException()
        {
            // Arrange
            const string commandText = "PLACE 1,FOO,EAST";
            var textWriter = new StringWriter();

            // Act & Assert
            Assert.Throws<Exception>(() => Commands.Command.Parse(commandText, textWriter));
        }

        [Fact]
        public void Parser_Place_ThirdParameterUnknown_ThrowsException()
        {
            // Arrange
            const string commandText = "PLACE 1,2,FOO";
            var textWriter = new StringWriter();

            // Act & Assert
            Assert.Throws<Exception>(() => Commands.Command.Parse(commandText, textWriter));
        }

        [Fact]
        public void Parse_UnknownCommandToken_ThrowsException()
        {
            // Arrange
            const string commandText = "UNKNOWN";
            var textWriter = new StringWriter();

            // Act & Assert
            Assert.Throws<Exception>(() => Commands.Command.Parse(commandText, textWriter));
        }

        [Fact]
        public void Parse_WithMultipleCommands_LineSeparated_Unsuccessful()
        {
            // Arrange
            const string commandText = "PLACE 1,2,EAST\r\nMOVE,1\r\nREPORT";
            var textWriter = new StringWriter();

            // Act
            var command = Commands.Command.Parse(commandText, textWriter);

            // Assert
            Assert.Null(command);
        }

        [Fact]
        public void Parse_WithMultipleCommands_SpaceSeparated_TokensMalformed()
        {
            // Arrange
            const string commandText = "PLACE 1,2,EAST MOVE,1 REPORT";
            var textWriter = new StringWriter();

            // Act & Assert
            Assert.Throws<Exception>(() => Commands.Command.Parse(commandText, textWriter));
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

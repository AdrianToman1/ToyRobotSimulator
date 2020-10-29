using System;
using System.IO;
using ToyRobotSimulator.Commands;
using Xunit;

namespace ToyRobotSimulator.UnitTests.Command
{
    public class CommandInterpreterUnitTests
    {
        [Fact]
        public void Lexer_sNull_ThrowsArgumentNullException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>("s", () => CommandInterpreter.Lexer(null));
        }

        [Fact]
        public void Lexer_sEmpty_ThrowsArgumentException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>("s", () => CommandInterpreter.Lexer(string.Empty));
        }

        [Fact]
        public void Lexer_sWhitespace_ThrowsArgumentException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>("s", () => CommandInterpreter.Lexer("  "));
        }

        [Fact]
        public void Lexer_WithNoParams_OK()
        {
            // Arrange
            const string s = "LEFT";

            // Act 
            var lexerResult = CommandInterpreter.Lexer(s);

            // Assert
            Assert.NotNull(lexerResult);
            Assert.Equal("LEFT", lexerResult.CommandToken);
            Assert.NotNull(lexerResult.ParameterTokens);
            Assert.Empty(lexerResult.ParameterTokens);
        }

        [Fact]
        public void Lexer_WithOneParam_OK()
        {
            // Arrange
            const string s = "MOVE 1";

            // Act 
            var lexerResult = CommandInterpreter.Lexer(s);

            // Assert
            Assert.NotNull(lexerResult);
            Assert.Equal("MOVE", lexerResult.CommandToken);
            Assert.NotNull(lexerResult.ParameterTokens);
            Assert.Single(lexerResult.ParameterTokens);
            Assert.Equal("1", lexerResult.ParameterTokens[0]);
        }

        [Fact]
        public void Lexer_WithMultipleParams_OK()
        {
            // Arrange
            const string s = "PLACE 1,2,EAST";

            // Act 
            var lexerResult = CommandInterpreter.Lexer(s);

            // Assert
            Assert.NotNull(lexerResult);
            Assert.Equal("PLACE", lexerResult.CommandToken);
            Assert.NotNull(lexerResult.ParameterTokens);
            Assert.Equal(3, lexerResult.ParameterTokens.Length);
            Assert.Equal("1", lexerResult.ParameterTokens[0]);
            Assert.Equal("2", lexerResult.ParameterTokens[1]);
            Assert.Equal("EAST", lexerResult.ParameterTokens[2]);
        }

        [Fact]
        public void Lexer_BadCommand_Unsuccessful()
        {
            // Arrange
            const string s = "LE+FT";

            // Act 
            var lexerResult = CommandInterpreter.Lexer(s);

            // Assert
            Assert.Null(lexerResult);
        }

        [Fact]
        public void Lexer_BadParams_OK()
        {
            // Arrange
            const string s = "MOVE +1";

            // Act 
            var lexerResult = CommandInterpreter.Lexer(s);

            // Assert
            Assert.Null(lexerResult);
        }

        [Fact]
        public void Lexer_WhitespaceWithNoParams_OK()
        {
            // Arrange
            const string s = "   LEFT    ";

            // Act 
            var lexerResult = CommandInterpreter.Lexer(s);

            // Assert
            Assert.NotNull(lexerResult);
            Assert.Equal("LEFT", lexerResult.CommandToken);
            Assert.NotNull(lexerResult.ParameterTokens);
            Assert.Empty(lexerResult.ParameterTokens);
        }

        [Fact]
        public void Lexer_WhitespaceWithOneParam_OK()
        {
            // Arrange
            const string s = "   MOVE    1   ";

            // Act 
            var lexerResult = CommandInterpreter.Lexer(s);

            // Assert
            Assert.NotNull(lexerResult);
            Assert.Equal("MOVE", lexerResult.CommandToken);
            Assert.NotNull(lexerResult.ParameterTokens);
            Assert.Single(lexerResult.ParameterTokens);
            Assert.Equal("1", lexerResult.ParameterTokens[0]);
        }

        [Fact]
        public void Lexer_WhitespaceWithMultipleParams_OK()
        {
            // Arrange
            const string s = "    PLACE   1  ,  2  ,  EAST   ";

            // Act 
            var lexerResult = CommandInterpreter.Lexer(s);

            // Assert
            Assert.NotNull(lexerResult);
            Assert.Equal("PLACE", lexerResult.CommandToken);
            Assert.NotNull(lexerResult.ParameterTokens);
            Assert.Equal(3, lexerResult.ParameterTokens.Length);
            Assert.Equal("1", lexerResult.ParameterTokens[0]);
            Assert.Equal("2", lexerResult.ParameterTokens[1]);
            Assert.Equal("EAST", lexerResult.ParameterTokens[2]);
        }

        [Fact]
        public void Lexer_CaseInsensitive_OK()
        {
            // Arrange
            const string s = "pLACe 1,2,EasT";

            // Act 
            var lexerResult = CommandInterpreter.Lexer(s);

            // Assert
            Assert.NotNull(lexerResult);
            Assert.Equal("pLACe", lexerResult.CommandToken);
            Assert.NotNull(lexerResult.ParameterTokens);
            Assert.Equal(3, lexerResult.ParameterTokens.Length);
            Assert.Equal("1", lexerResult.ParameterTokens[0]);
            Assert.Equal("2", lexerResult.ParameterTokens[1]);
            Assert.Equal("EasT", lexerResult.ParameterTokens[2]);
        }

        [Fact]
        public void Lexer_UnknownCommand_OK()
        {
            // Arrange
            const string s = "FOO BAR";

            // Act 
            var lexerResult = CommandInterpreter.Lexer(s);

            // Assert
            Assert.NotNull(lexerResult);
            Assert.Equal("FOO", lexerResult.CommandToken);
            Assert.NotNull(lexerResult.ParameterTokens);
            Assert.Single(lexerResult.ParameterTokens);
            Assert.Equal("BAR", lexerResult.ParameterTokens[0]);
        }

        [Fact]
        public void Lexer_MalformedCommand_OK()
        {
            // Arrange
            const string s = "PLACE EAST";

            // Act 
            var lexerResult = CommandInterpreter.Lexer(s);

            // Assert
            Assert.NotNull(lexerResult);
            Assert.Equal("PLACE", lexerResult.CommandToken);
            Assert.NotNull(lexerResult.ParameterTokens);
            Assert.Single(lexerResult.ParameterTokens);
            Assert.Equal("EAST", lexerResult.ParameterTokens[0]);
        }

        [Fact]
        public void Lexer_WithMultipleCommands_LineSeparated_Unsuccessful()
        {
            // Arrange
            const string s = "PLACE 1,2,EAST\r\nMOVE,1\r\nREPORT";

            // Act 
            var lexerResult = CommandInterpreter.Lexer(s);

            // Assert
            Assert.Null(lexerResult);
        }

        [Fact]
        public void Lexer_WithMultipleCommands_SpaceSeparated_TokensMalformed()
        {
            // Arrange
            const string s = "PLACE 1,2,EAST MOVE,1 REPORT";

            // Act 
            var lexerResult = CommandInterpreter.Lexer(s);

            // Assert
            Assert.NotNull(lexerResult);
            Assert.Equal("PLACE", lexerResult.CommandToken);
            Assert.NotNull(lexerResult.ParameterTokens);
            Assert.Equal(4, lexerResult.ParameterTokens.Length);
            Assert.Equal("1", lexerResult.ParameterTokens[0]);
            Assert.Equal("2", lexerResult.ParameterTokens[1]);
            Assert.Equal("EAST MOVE", lexerResult.ParameterTokens[2]);
            Assert.Equal("1 REPORT", lexerResult.ParameterTokens[3]);
        }

        [Fact]
        public void Parser_LexerResultNull_ThrowsArgumentNullException()
        {
            // Arrange
            var textWriter = new StringWriter();

            // Act & Assert
            Assert.Throws<ArgumentNullException>("lexerResult", () => CommandInterpreter.Parser(null, textWriter));
        }

        [Fact]
        public void Parser_TextWriterNull_ThrowsArgumentNullException()
        {
            // Arrange
            var lexerResult = new LexerResult();

            // Act & Assert
            Assert.Throws<ArgumentNullException>("textWriter", () => CommandInterpreter.Parser(lexerResult, null));
        }

        [Fact]
        public void Parser_LexerResultCommandTokenNull_ThrowsArgumentException()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = null,
                ParameterTokens = new string[0]
            };
            var textWriter = new StringWriter();

            // Act & Assert
            Assert.Throws<ArgumentException>("lexerResult", () => CommandInterpreter.Parser(lexerResult, textWriter));
        }

        [Fact]
        public void Parser_LexerResultCommandTokenEmpty_ThrowsArgumentException()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = string.Empty,
                ParameterTokens = new string[0]
            };
            var textWriter = new StringWriter();

            // Act & Assert
            Assert.Throws<ArgumentException>("lexerResult", () => CommandInterpreter.Parser(lexerResult, textWriter));
        }

        [Fact]
        public void Parser_LexerResultParametersNull_ThrowsArgumentException()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "LEFT",
                ParameterTokens = null
            };
            var textWriter = new StringWriter();

            // Act & Assert
            Assert.Throws<ArgumentException>("lexerResult", () => CommandInterpreter.Parser(lexerResult, textWriter));
        }

        [Fact]
        public void Parser_Left_OK()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "LEFT",
                ParameterTokens = new string[0]
            };
            var textWriter = new StringWriter();

            // Act
            var command = CommandInterpreter.Parser(lexerResult, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<LeftCommand>(command);
        }

        [Fact]
        public void Parser_Left_CaseInsensitive_OK()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "lEFt",
                ParameterTokens = new string[0]
            };
            var textWriter = new StringWriter();

            // Act
            var command = CommandInterpreter.Parser(lexerResult, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<LeftCommand>(command);
        }

        [Fact]
        public void Parser_Left_ExtraParameters_OK()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "LEFT",
                ParameterTokens = new[]
                {
                    "FOO"
                }
            };
            var textWriter = new StringWriter();

            // Act
            var command = CommandInterpreter.Parser(lexerResult, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<LeftCommand>(command);
        }

        [Fact]
        public void Parser_Right_OK()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "RIGHT",
                ParameterTokens = new string[0]
            };
            var textWriter = new StringWriter();

            // Act
            var command = CommandInterpreter.Parser(lexerResult, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<RightCommand>(command);
        }

        [Fact]
        public void Parser_Right_CaseInsensitive_OK()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "rIGHt",
                ParameterTokens = new string[0]
            };
            var textWriter = new StringWriter();

            // Act
            var command = CommandInterpreter.Parser(lexerResult, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<RightCommand>(command);
        }

        [Fact]
        public void Parser_Right_ExtraParameters_OK()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "RIGHT",
                ParameterTokens = new[]
                {
                    "FOO"
                }
            };
            var textWriter = new StringWriter();

            // Act
            var command = CommandInterpreter.Parser(lexerResult, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<RightCommand>(command);
        }

        [Fact]
        public void Parser_Move_OK()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "MOVE",
                ParameterTokens = new string[0]
            };
            var textWriter = new StringWriter();

            // Act
            var command = CommandInterpreter.Parser(lexerResult, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<MoveCommand>(command);
        }

        [Fact]
        public void Parser_Move_CaseInsensitive_OK()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "mOVe",
                ParameterTokens = new string[0]
            };
            var textWriter = new StringWriter();

            // Act
            var command = CommandInterpreter.Parser(lexerResult, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<MoveCommand>(command);
        }

        [Fact]
        public void Parser_Move_ExtraParameters_OK()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "MOVE",
                ParameterTokens = new[]
                {
                    "FOO"
                }
            };
            var textWriter = new StringWriter();

            // Act
            var command = CommandInterpreter.Parser(lexerResult, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<MoveCommand>(command);
        }

        [Fact]
        public void Parser_Report_OK()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "REPORT",
                ParameterTokens = new string[0]
            };
            var textWriter = new StringWriter();

            // Act
            var command = CommandInterpreter.Parser(lexerResult, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<ReportCommand>(command);
            Assert.Same(textWriter, ((ReportCommand)command).TextWriter);
        }

        [Fact]
        public void Parser_Report_CaseInsensitive_OK()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "rEPORt",
                ParameterTokens = new string[0]
            };
            var textWriter = new StringWriter();

            // Act
            var command = CommandInterpreter.Parser(lexerResult, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<ReportCommand>(command);
            Assert.Same(textWriter, ((ReportCommand)command).TextWriter);
        }

        [Fact]
        public void Parser_Report_ExtraParameters_OK()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "REPORT",
                ParameterTokens = new[]
                {
                    "FOO"
                }
            };
            var textWriter = new StringWriter();

            // Act
            var command = CommandInterpreter.Parser(lexerResult, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<ReportCommand>(command);
        }

        [Fact]
        public void Parser_Place_OK()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "PLACE",
                ParameterTokens = new[]
                {
                    "1",
                    "2",
                    "EAST"
                }
            };
            var textWriter = new StringWriter();

            // Act
            var command = CommandInterpreter.Parser(lexerResult, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<PlaceCommand>(command);
            Assert.Equal(1, ((PlaceCommand)command).X);
            Assert.Equal(2, ((PlaceCommand)command).Y);
            Assert.Equal(CompassPoint.East, ((PlaceCommand)command).Heading);
        }

        [Fact]
        public void Parser_Place_CaseInsensitive_OK()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "pLACe",
                ParameterTokens = new[]
                {
                    "1",
                    "2",
                    "eASt"
                }
            };
            var textWriter = new StringWriter();

            // Act
            var command = CommandInterpreter.Parser(lexerResult, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<PlaceCommand>(command);
            Assert.Equal(1, ((PlaceCommand)command).X);
            Assert.Equal(2, ((PlaceCommand)command).Y);
            Assert.Equal(CompassPoint.East, ((PlaceCommand)command).Heading);
        }

        [Fact]
        public void Parser_Place_ExtraParameters_OK()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "PLACE",
                ParameterTokens = new[]
                {
                    "1",
                    "2",
                    "eASt",
                    "FOO"
                }
            };
            var textWriter = new StringWriter();

            // Act
            var command = CommandInterpreter.Parser(lexerResult, textWriter);

            // Assert
            Assert.NotNull(command);
            Assert.IsType<PlaceCommand>(command);
            Assert.Equal(1, ((PlaceCommand)command).X);
            Assert.Equal(2, ((PlaceCommand)command).Y);
            Assert.Equal(CompassPoint.East, ((PlaceCommand)command).Heading);
        }

        [Fact]
        public void Parser_Place_InsufficientParameters_ThrowsException()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "PLACE",
                ParameterTokens = new[]
                {
                    "FOO"
                }
            };
            var textWriter = new StringWriter();

            // Act & Assert
            Assert.Throws<Exception>(() => CommandInterpreter.Parser(lexerResult, textWriter));
        }

        [Fact]
        public void Parser_Place_FirstParameterWrongType_ThrowsException()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "PLACE",
                ParameterTokens = new[]
                {
                    "FOO",
                    "2",
                    "EAST"
                }
            };
            var textWriter = new StringWriter();

            // Act & Assert
            Assert.Throws<Exception>(() => CommandInterpreter.Parser(lexerResult, textWriter));
        }

        [Fact]
        public void Parser_Place_SecondParameterWrongType_ThrowsException()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "PLACE",
                ParameterTokens = new[]
                {
                    "1",
                    "FOO",
                    "EAST"
                }
            };
            var textWriter = new StringWriter();

            // Act & Assert
            Assert.Throws<Exception>(() => CommandInterpreter.Parser(lexerResult, textWriter));
        }

        [Fact]
        public void Parser_Place_ThirdParameterUnknown_ThrowsException()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "PLACE",
                ParameterTokens = new[]
                {
                    "1",
                    "2",
                    "FOO"
                }
            };
            var textWriter = new StringWriter();

            // Act & Assert
            Assert.Throws<Exception>(() => CommandInterpreter.Parser(lexerResult, textWriter));
        }

        [Fact]
        public void Parser_UnknownCommandToken_ThrowsException()
        {
            // Arrange
            var lexerResult = new LexerResult
            {
                CommandToken = "FOO",
                ParameterTokens = new string[0]
            };
            var textWriter = new StringWriter();

            // Act & Assert
            Assert.Throws<Exception>(() => CommandInterpreter.Parser(lexerResult, textWriter));
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ToyRobotSimulator.Commands
{
    /// <summary>
    ///     Provides functionality to interpret a line of command text into <seealso cref="Command" /> object.
    /// </summary>
    public static class CommandInterpreter
    {
        /// <summary>
        ///     Performs tokenization of the provided line of command text.
        /// </summary>
        /// <param name="s">The line of command text</param>
        /// <exception cref="ArgumentNullException"><c>s</c> is null</exception>
        /// <exception cref="ArgumentException"><c>s</c> is empty or whitespace</exception>
        /// <returns>
        ///     A <see cref="LexerResult" /> containing the tokens for the provided line of command text if lexing with
        ///     successful, otherwise <c>null</c> if the lexing was unsuccessful.
        /// </returns>
        /// <remarks>
        ///     Lexing is case insensitive.
        ///     The lexer will only tokenize the command text, and will do no validation or verification that the tokens represents
        ///     an known and complete command.
        ///     It is assumed that the provided line of command text represents exactly one discrete command. There is no command
        ///     separator defined.
        ///     Attempting to provide a string with multiple commands separated by new line and/or carriage return characters will
        ///     result in lexing being unsuccessful and a <c>null</c> return value.
        ///     Attempting to provide a string with multiple commands separated by space(s) will potentially result in the tokens
        ///     within the returned <see cref="LexerResult" /> being malformed.
        /// </remarks>
        public static LexerResult Lexer(string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException($"{nameof(s)} must have a value", nameof(s));
            }

            // Regex expression to matches the command text as command (group 1) and optional param list (group 2).
            // The comma separated params list requires further processing to be broken down into individual tokens.

            var match = Regex.Match(s.Trim(), "^([\\w]+) ?([\\w, ]*)$");

            if (!match.Success)
            {
                return null;
            }

            var parameters = new string[0];

            if (match.Groups.Count > 2 && !string.IsNullOrWhiteSpace(match.Groups[2].Value))
            {
                parameters = match.Groups[2].Value.Split(",");
            }

            return new LexerResult
                {CommandToken = match.Groups[1].Value, ParameterTokens = parameters.Select(i => i.Trim()).ToArray()};
        }

        /// <summary>
        ///     Converts the tokens contain in the provided <see cref="LexerResult" /> into a <see cref="Command" />.
        /// </summary>
        /// <param name="lexerResult">The lexer result.</param>
        /// <param name="textWriter">The report output destination to use for <seealso cref="ReportCommand" />.</param>
        /// <exception cref="ArgumentNullException"><c>lexerResult</c> is null</exception>
        /// <exception cref="ArgumentNullException"><c>textWriter</c> is null</exception>
        /// <exception cref="ArgumentException"><c>lexerResult.CommandToken</c> is null, empty or whitespace</exception>
        /// <exception cref="ArgumentException"><c>lexerResult.ParameterTokens</c> is null</exception>
        /// <returns>An instance of <see cref="Command" /> for the provided <see cref="LexerResult" /> that was parsed.</returns>
        /// <remarks>
        ///     If <c>token</c> contains any superfluous parameters then they will be ignored.
        ///     Parsing of tokens is case insensitive.
        ///     It is assume that token don't include any unnecessary whitespace.
        /// </remarks>
        public static Command Parser(LexerResult lexerResult, TextWriter textWriter)
        {
            if (lexerResult == null)
            {
                throw new ArgumentNullException(nameof(lexerResult));
            }

            if (textWriter == null)
            {
                throw new ArgumentNullException(nameof(textWriter));
            }

            if (string.IsNullOrWhiteSpace(lexerResult.CommandToken))
            {
                throw new ArgumentException($"{nameof(lexerResult.CommandToken)} must have a value", nameof(lexerResult));
            }

            if (lexerResult.ParameterTokens == null)
            {
                throw new ArgumentException($"{nameof(lexerResult.ParameterTokens)} can not be null", nameof(lexerResult));
            }

            if (lexerResult.CommandToken.Equals("LEFT", StringComparison.InvariantCultureIgnoreCase))
            {
                return new LeftCommand();
            }

            if (lexerResult.CommandToken.Equals("MOVE", StringComparison.InvariantCultureIgnoreCase))
            {
                return new MoveCommand();
            }

            if (lexerResult.CommandToken.Equals("PLACE", StringComparison.InvariantCultureIgnoreCase))
            {
                if (lexerResult.ParameterTokens.Length < 3)
                {
                    throw new Exception("Expecting at least 3 parameters");
                }

                if (!int.TryParse(lexerResult.ParameterTokens[0], out var x))
                {
                    throw new Exception("Parameter 1: expecting an integer value");
                }

                if (!int.TryParse(lexerResult.ParameterTokens[1], out var y))
                {
                    throw new Exception("Parameter 1: expecting an integer value");
                }

                if (!Enum.TryParse(typeof(CompassPoint), lexerResult.ParameterTokens[2], true, out var heading))
                {
                    throw new Exception("Parameter 1: expecting either North, South, East or West");
                }

                return new PlaceCommand(new Table(), x, y, (CompassPoint)heading);
            }

            if (lexerResult.CommandToken.Equals("REPORT", StringComparison.InvariantCultureIgnoreCase))
            {
                return new ReportCommand(textWriter);
            }

            if (lexerResult.CommandToken.Equals("RIGHT", StringComparison.InvariantCultureIgnoreCase))
            {
                return new RightCommand();
            }

            throw new Exception($"Unknown command ${lexerResult.CommandToken}");
        }
    }
}

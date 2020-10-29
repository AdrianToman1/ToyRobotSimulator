using System;
using System.IO;

namespace ToyRobotSimulator.Commands
{
    /// <summary>
    ///     A base class for an commands.
    /// </summary>
    /// <remarks>
    ///     There could be an ICommand interface, but the simplest implementation is just an abstract class.
    /// </remarks>
    public abstract class Command
    {
        /// <summary>
        ///     Executes the command on the current robot.
        /// </summary>
        /// <param name="robot">The robot that is the command will be executed for</param>
        /// <exception cref="ArgumentNullException"><c>robot</c> is null</exception>
        public void Execute(Robot robot)
        {
            if (robot == null)
            {
                throw new ArgumentNullException(nameof(robot));
            }

            DoExecute(robot);
        }

        /// <summary>
        ///     Override this method to handle command execution for the provided robot.
        /// </summary>
        /// <param name="robot">The robot.</param>
        /// <remarks>
        ///     It's OK to assume that the Robot parameter contains a non-null value.
        /// </remarks>
        protected abstract void DoExecute(Robot robot);

        /// <summary>
        ///     Converts the the provided line of command text into a <see cref="Command" />.
        /// </summary>
        /// <param name="s">The line of command text</param>
        /// <param name="textWriter">The report output destination to use for <seealso cref="ReportCommand" />.</param>
        /// <exception cref="ArgumentNullException"><c>s</c> is null</exception>
        /// <exception cref="ArgumentException"><c>s</c> is empty or whitespace</exception>
        /// <exception cref="ArgumentNullException"><c>textWriter</c> is null</exception>
        /// <returns>An instance of <see cref="Command" /> for the provided line of command text that was parsed.</returns>
        /// <remarks>
        ///     Parsing is case insensitive.
        ///     It is assumed that the provided line of command text represents exactly one discrete command. There is no command
        ///     separator defined. Attempting to provide a string with multiple commands could either result in commands after the
        ///     first being ignored, or the parsing failing and <c>null</c> result being returned.
        ///     If the command text contains any superfluous parameters then they will be ignored.
        ///     This method orchestrates the parsing, the actual lexing and parsing logic is contained in
        ///     <see cref="CommandInterpreter" />.
        /// </remarks>
        public static Command Parse(string s, TextWriter textWriter)
        {
            if (s == null)
            {
                throw new ArgumentNullException(nameof(s));
            }

            if (textWriter == null)
            {
                throw new ArgumentNullException(nameof(textWriter));
            }

            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException($"{nameof(s)} must have a value", nameof(s));
            }

            var token = CommandInterpreter.Lexer(s);

            if (token == null)
            {
                return null;
            }

            return CommandInterpreter.Parser(token, textWriter);
        }
    }
}

using System;
using System.IO;

namespace ToyRobotSimulator.Commands
{
    /// <summary>
    ///     The command to announce the `x`,`y` and `facing` of the robot.
    /// </summary>
    /// <inheritdoc />
    public class ReportCommand : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <seealso cref="ReportCommand" /> class.
        /// </summary>
        /// <param name="textWriter">The report output destination</param>
        /// <exception cref="ArgumentNullException"><c>textWriter</c> is null.</exception>
        /// <inheritdoc />
        public ReportCommand(TextWriter textWriter)
        {
            TextWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
        }

        /// <summary>
        ///     Gets and the report output destination. 
        /// </summary>
        public TextWriter TextWriter { get; set; }

        /// <summary>
        ///     Handles the report command execution for the provided robot
        /// </summary>
        /// <inheritdoc />
        protected override void DoExecute(Robot robot)
        {
            if (robot.IsOnTheTable())
            {
                TextWriter.WriteLine($"{robot.X},{robot.Y},{(CompassPoint) robot.Heading}");
            }
            else
            {
                TextWriter.WriteLine("Not roaming upon a table");
            }
        }
    }
}

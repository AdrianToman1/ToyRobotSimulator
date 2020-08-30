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
        /// <inheritdoc />
        public ReportCommand(Robot robot, TextWriter textWriter) : base(robot)
        {
            TextWriter = textWriter ?? throw new ArgumentNullException(nameof(textWriter));
        }

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

using System;

namespace ToyRobotSimulator.Commands
{
    /// <summary>
    ///     The command to put the toy robot on the table in position `X`,`Y` and facing `NORTH`, `SOUTH`, `EAST` or `WEST`.
    /// </summary>
    /// <inheritdoc />
    public class PlaceCommand : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <seealso cref="PlaceCommand" /> class.
        /// </summary>
        /// <inheritdoc />
        public PlaceCommand(Table table, int x, int y, CompassPoint heading)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table));
            }

            if (!table.IsValidLocation(x, y))
            {
                throw new ArgumentException(
                    $"The location described by the values in {nameof(x)} and {nameof(y)} isn't valid");
            }

            Table = table;
            X = x;
            Y = y;
            Heading = heading;
        }

        /// <summary>
        ///     The current location of the robot on the x-axis.
        /// </summary>
        /// <remarks>
        ///     This value is null when the robot isn't placed on the table.
        /// </remarks>
        public int X { get; }

        /// <summary>
        ///     The current location of the robot on the y-axis.
        /// </summary>
        public int Y { get; }

        /// <summary>
        ///     The current heading of the robot in degrees from the x-axis.
        /// </summary>
        public CompassPoint Heading { get; }

        /// <summary>
        ///     The current table that the robot is roaming upon.
        /// </summary>
        public Table Table { get; }

        /// <summary>
        ///     Handles the place command execution for the provided robot
        /// </summary>
        /// <inheritdoc />
        // Second place?
        protected override void DoExecute(Robot robot)
        {
            robot.PlaceOnTable(Table, X, Y, (int) Heading);
        }
    }
}

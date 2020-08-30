using System;

namespace ToyRobotSimulator
{
    /// <summary>
    ///     Represents a toy robot that can roam upon a table's surface.
    /// </summary>
    public class Robot
    {
        /// <summary>
        ///     Initializes a new instance of the <seealso cref="Robot" /> class.
        /// </summary>
        /// <remarks>
        ///     By default the robot isn't placed on the table.
        /// </remarks>
        public Robot()
        {
            X = null;
            Y = null;
            Heading = null;
            Table = null;
        }

        /// <summary>
        ///     The current location of the robot on the x-axis.
        /// </summary>
        /// <remarks>
        ///     This value is null when the robot isn't placed on the table.
        /// </remarks>
        public int? X { get; set; }

        /// <summary>
        ///     The current location of the robot on the y-axis.
        /// </summary>
        /// <remarks>
        ///     This value is null when the robot isn't placed on the table.
        /// </remarks>
        public int? Y { get; set; }

        /// <summary>
        ///     The current heading of the robot in degrees from the x-axis.
        /// </summary>
        /// <remarks>
        ///     This value is null when the robot isn't placed on the table.
        /// </remarks>
        public int? Heading { get; set; }

        /// <summary>
        ///     The current table that the robot is roaming upon.
        /// </summary>
        /// <remarks>
        ///     This value is null when the robot isn't placed on the table.
        /// </remarks>
        public Table Table { get; private set; }

        /// <summary>
        ///     Places the robot on the provided table at the location described by the provided x & y values and with provided
        ///     heading.
        /// </summary>
        /// <exception cref="ArgumentNullException"><c>table</c> is null.</exception>
        /// <exception cref="ArgumentException">
        ///     The location on the provided table described by the provided x & y values is
        ///     invalid.
        /// </exception>
        /// <param name="table">The table.</param>
        /// <param name="x">The position on the x-axis.</param>
        /// <param name="y">The position on the y-axis.</param>
        /// <param name="heading">The heading.</param>
        /// <remarks>
        ///     The value for heading will be normalized to be within the range of 0-359 degrees.
        /// </remarks>
        public void PlaceOnTable(Table table, int x, int y, int heading)
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
            Heading = heading % 360;

            if (Heading < 0)
            {
                Heading = 360 + Heading;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether the robot is currently placed on a table.
        /// </summary>
        /// <returns><c>true</c> if the robot is currently on a table, otherwise <c>false</c>.</returns>
        public bool IsOnTheTable()
        {
            return Table != null;
        }
    }
}

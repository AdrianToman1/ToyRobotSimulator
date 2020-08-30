using System;

namespace ToyRobotSimulator
{
    /// <summary>
    ///     Represents a toy robot that can roam upon a table's surface.
    /// </summary>
    public class Robot
    {
        private int? _heading;

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
        ///     Heading will be normalized to be within the range 0 to 359 degrees.
        /// </remarks>
        public int? Heading
        {
            get => _heading;
            set
            {
                // Normalize heading to the range -359 to 359
                _heading = value % 360;

                // Translate negative headings to the range 0 to 359
                if (_heading < 0)
                {
                    _heading = 360 + _heading;
                }
            }
        }

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
            Heading = heading;
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

namespace ToyRobotSimulator
{
    /// <summary>
    ///     Represents the surface that the toy robot roams upon.
    /// </summary>
    /// <remarks>
    ///     The table is a 5 x 5 square, and the origin (0,0) can be considered to be the `SOUTH WEST` most corner.
    ///     The table has no knowledge of the robot or it's location.
    /// </remarks>
    public class Table
    {
        /// <summary>
        ///     The surface width and length
        /// </summary>
        private const int dimensions = 5;

        /// <summary>
        ///     Gets a value indicating whether the location described by the x and y is valid location on the underlying table
        ///     surface.
        /// </summary>
        /// <param name="x">The position on the x-axis</param>
        /// <param name="y">The position on the y-axis</param>
        /// <returns><c>true</c> is the location described by x and y values is valid, otherwise <c>false</c>.</returns>
        public bool IsValidLocation(int x, int y)
        {
            return x >= 0 && x < dimensions && y >= 0 && y < dimensions;
        }
    }
}

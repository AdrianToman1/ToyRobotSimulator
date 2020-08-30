namespace ToyRobotSimulator.Commands
{
    /// <summary>
    ///     The command to rotate the robot 90 degrees to the left without changing the position of the robot.
    /// </summary>
    /// <inheritdoc />
    public class LeftCommand : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <seealso cref="LeftCommand" /> class.
        /// </summary>
        /// <inheritdoc />
        public LeftCommand(Robot robot) : base(robot)
        {
        }

        /// <summary>
        ///     Handles the left command execution for the provided robot
        /// </summary>
        /// <inheritdoc />
        protected override void DoExecute(Robot robot)
        {
            // Ignore left command until the robot is on a table.
            if (robot.IsOnTheTable())
            {
                robot.Heading = robot.Heading + 90;

                // TODO: Refactor to a rotation command that accepts degrees.
            }
        }
    }
}

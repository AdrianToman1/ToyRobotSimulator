namespace ToyRobotSimulator.Commands
{
    /// <summary>
    ///     The command to rotate the robot 90 degrees to the right without changing the position of the robot.
    /// </summary>
    /// <inheritdoc />
    public class RightCommand : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <seealso cref="RightCommand" /> class.
        /// </summary>
        /// <inheritdoc />
        public RightCommand(Robot robot) : base(robot)
        {
        }

        /// <summary>
        ///     Handles the right command execution for the provided robot
        /// </summary>
        /// <inheritdoc />
        protected override void DoExecute(Robot robot)
        {
            // Ignore right command until the robot is on a table.
            if (!robot.IsOnTheTable())
            {
                return;
            }

            robot.Heading = (robot.Heading.Value - 90) % 360;

            if (robot.Heading < 0)
            {
                robot.Heading = 360 + robot.Heading;
            }

            // TODO: Refactor to a rotation command that accepts degrees.
            // TODO: Refactor Robot.Heading to handle the normalization of heading to the range of 0-359.
        }
    }
}

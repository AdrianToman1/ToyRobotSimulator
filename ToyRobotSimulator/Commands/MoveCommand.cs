using System;

namespace ToyRobotSimulator.Commands
{
    /// <summary>
    ///     The command to move the robot 1 unit forward n the direction it is currently facing.
    /// </summary>
    /// <inheritdoc />
    public class MoveCommand : Command
    {
        /// <summary>
        ///     Initializes a new instance of the <seealso cref="MoveCommand" /> class.
        /// </summary>
        /// <inheritdoc />
        public MoveCommand(Robot robot) : base(robot)
        {
        }

        /// <summary>
        ///     Handles the move command execution for the provided robot
        /// </summary>
        /// <inheritdoc />
        protected override void DoExecute(Robot robot)
        {
            // Ignore moved command until the robot is on a table.
            if (robot.IsOnTheTable())
            {
                // Get the x and y component of the vector to move the robot in.

                // The magnitude of the vector is 1.
                // Since the heading will be a multiple of 90 degrees the sine, cosine values will either be 0, 1 or -1.
                var xMove = (int) Math.Cos(Math.PI / 180 * robot.Heading.Value);
                var yMove = (int) Math.Sin(Math.PI / 180 * robot.Heading.Value);

                // The robots x and y values together can be considered a vector.
                // Therefore vector addition can be used to determine the robots new location.

                var proposedX = robot.X.Value + xMove;
                var proposedY = robot.Y.Value + yMove;

                if (robot.Table.IsValidLocation(proposedX, proposedY))
                {
                    robot.X = proposedX;
                    robot.Y = proposedY;
                }

                // TODO: Refactor to a method on Robot that handles the valid location check.
            }
        }
    }
}

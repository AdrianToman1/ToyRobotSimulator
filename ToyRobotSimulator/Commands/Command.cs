using System;

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
        ///     Initializes a new instance of the <seealso cref="Command" /> class.
        /// </summary>
        /// <param name="robot">The robot that is the command will be executed for</param>
        /// <exception cref="ArgumentNullException"><c>robot</c> is null</exception>
        protected Command(Robot robot)
        {
            Robot = robot ?? throw new ArgumentNullException(nameof(robot));
        }

        /// <summary>
        ///     Gets the robot that is the command will be executed for.
        /// </summary>
        /// <remarks>
        ///     Keeping the robot immutable simplifies the implementation while still meeting all the requirements.
        /// </remarks>
        public Robot Robot { get; }

        /// <summary>
        ///     Executes the command on the current robot.
        /// </summary>
        public void Execute()
        {
            // Urgh. Untestable. A command shouldn't ever be in this state.
            if (Robot == null)
            {
                throw new Exception("Robot is null");
            }

            DoExecute(Robot);
        }

        /// <summary>
        ///     Override this method to handle command execution for the provided robot.
        /// </summary>
        /// <param name="robot">The robot.</param>
        /// <remarks>
        ///     It's OK to assume that the Robot parameter contains a non-null value.
        /// </remarks>
        protected abstract void DoExecute(Robot robot);
    }
}

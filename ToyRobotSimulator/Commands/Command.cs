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
        ///     Executes the command on the current robot.
        /// </summary>
        /// <param name="robot">The robot that is the command will be executed for</param>
        /// <exception cref="ArgumentNullException"><c>robot</c> is null</exception>
        public void Execute(Robot robot)
        {
            if (robot == null)
            {
                throw new ArgumentNullException(nameof(robot));
            }

            DoExecute(robot);
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

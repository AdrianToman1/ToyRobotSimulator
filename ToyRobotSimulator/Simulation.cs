using System;
using System.Collections.Generic;
using ToyRobotSimulator.Commands;

namespace ToyRobotSimulator
{
    /// <summary>
    ///     Represents the current simulation context.
    /// </summary>
    /// <remarks>
    ///     By default, the simulation starts with a toy robot the isn't yet placed on any table.
    ///
    ///     I decided to keep the stored state a minimal as possible to reduce the requirement to
    ///     check if the state is consistent. A reference to Robot is held only by Simulation class.
    ///     Robot is passed to Commands as a parameter to the Execute method. A reference to the Table
    ///     provided by the place commands and is held by the robot itself.
    /// </remarks>
    public class Simulation
    {
        /// <summary>
        ///     Initializes a new instance of the <seealso cref="Simulation" /> class.
        /// </summary>
        public Simulation()
        {
            Robot = new Robot();
        }

        /// <summary>
        ///     Gets the current Toy Robot.
        /// </summary>
        public Robot Robot { get; }

        /// <summary>
        ///     Executes a command on the simulation.
        /// </summary>
        /// <param name="command">The command</param>
        /// <exception cref="ArgumentNullException"><c>command</c> is null</exception>
        /// <remarks>
        ///     Commands that place the simulation in an invalid state will be ignored.
        /// </remarks>
        public void Execute(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.Execute(Robot);
        }

        /// <summary>
        ///     Executes a batch of commands on the simulation.
        /// </summary>
        /// <param name="commands">The collection of commands.</param>
        /// <remarks>
        ///     Commands will be execute on the simulation in turn in the order they appear within the collection.
        ///     Commands that place the simulation in an invalid state will be ignored.
        /// </remarks>
        public void Execute(IEnumerable<Command> commands)
        {
            if (commands == null)
            {
                throw new ArgumentNullException(nameof(commands));
            }

            foreach (var command in commands)
            {
                Execute(command);
            }
        }
    }
}

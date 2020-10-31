using System;
using System.IO;
using ToyRobotSimulator.Commands;

namespace ToyRobotSimulator.ConsoleApp
{
    /// <summary>
    ///     The Toy Robot Simulator console app.
    /// </summary>
    /// <remarks>
    ///     The Toy Robot Simulator console app will execute commands on the Toy Robot Simulator from a provided text file.
    /// </remarks>
    internal class Program
    {
        /// <summary>
        ///     The Console App entry point.
        /// </summary>
        /// <param name="args">
        ///     The command-line arguments supplied when the application is launched. The first parameter must be
        ///     the location of the text file the contains the commands to execute on the Toy Robot Simulator.
        /// </param>
        private static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No command file provided");
                return;
            }

            string[] lines = null;

            try
            {
                lines = File.ReadAllLines(args[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (lines != null)
            {
                var simulation = new Simulation();

                foreach (var command in lines)
                {
                    simulation.Execute(Command.Parse(command, Console.Out));
                }

                Console.WriteLine();
                Console.WriteLine("Simulation Complete");
            }

            Console.WriteLine("Press any key to close this window. . .");
            Console.ReadKey();
        }
    }
}

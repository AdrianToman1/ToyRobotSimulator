using System;
using System.IO;
using System.Linq;
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

            var simulation = new Simulation();

            try
            {
                BoundedMode(args[0], simulation, Console.Out);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Press any key to close this window. . .");
            Console.ReadKey();
        }

        /// <summary>
        ///     Executes commands from the file at the provided filePath on the provided simulation. The simulation ends once all the commands in the file have been executed.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <param name="simulation">The simulation.</param>
        /// <param name="textWriter">The output destination.</param>
        private static void BoundedMode(string filePath, Simulation simulation, TextWriter textWriter)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"{nameof(filePath)} must have a value", nameof(filePath));
            }

            if (simulation == null)
            {
                throw new ArgumentNullException(nameof(simulation));
            }

            if (textWriter == null)
            {
                throw new ArgumentNullException(nameof(textWriter));
            }

            var lines = File.ReadAllLines(filePath).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            if (!lines.Any())
            {
                textWriter.WriteLine("The file contains no commands");
            }

            foreach (var command in lines)
            {
                simulation.Execute(Command.Parse(command, textWriter));
            }

            textWriter.WriteLine();
            textWriter.WriteLine("Simulation Complete");
        }
    }
}

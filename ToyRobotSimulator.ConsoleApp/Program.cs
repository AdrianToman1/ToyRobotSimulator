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
        public static void Main(string[] args)
        {
            try
            {
                if (args.Any())
                {
                    BoundedMode(args[0], Console.Out);

                    Console.WriteLine();
                    Console.WriteLine("Simulation Complete");
                }
                else
                {
                    Console.WriteLine("Simulation Ready");
                    Console.WriteLine();

                    FreeMode(Console.In, Console.Out);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        /// <summary>
        ///     Runs a simulation and executes commands on it from the file at the provided filePath. The simulation will be completed once all the commands in the file have been executed.
        /// </summary>
        /// <param name="filePath">The path to the file.</param>
        /// <param name="standardOutput">Standard output.</param>
        /// <exception cref="ArgumentNullException"><c>filePath</c> is null</exception>
        /// <exception cref="ArgumentNullException"><c>textWriter</c> is null</exception>
        /// <exception cref="ArgumentException"><c>filePath</c> is empty or whitespace</exception>
        private static void BoundedMode(string filePath, TextWriter standardOutput)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException($"{nameof(filePath)} must have a value", nameof(filePath));
            }

            if (standardOutput == null)
            {
                throw new ArgumentNullException(nameof(standardOutput));
            }

            var lines = File.ReadAllLines(filePath).Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            if (!lines.Any())
            {
                standardOutput.WriteLine("The file contains no commands");
            }

            var simulation = new Simulation();
            
            foreach (var command in lines)
            {
                simulation.Execute(Command.Parse(command, standardOutput));
            }
        }

        /// <summary>
        ///     Runs a simulation and executes commands on it from standard input until the user chooses the end the simulation.
        /// </summary>
        /// <param name="standardInput">Standard input.</param>
        /// <param name="standardOutput">Standard output.</param>
        /// <exception cref="ArgumentNullException"><c>textWriter</c> is null</exception>
        private static void FreeMode(TextReader standardInput, TextWriter standardOutput)
        {
            if (standardInput == null)
            {
                throw new ArgumentNullException(nameof(standardInput));
            }

            if (standardOutput == null)
            {
                throw new ArgumentNullException(nameof(standardOutput));
            }

            var simulation = new Simulation();

            do
            {
                var command = standardInput.ReadLine()?.Trim();

                if (!string.IsNullOrWhiteSpace(command))
                {
                    simulation.Execute(Command.Parse(command, standardOutput));
                }
            } while (true);
        }
    }
}
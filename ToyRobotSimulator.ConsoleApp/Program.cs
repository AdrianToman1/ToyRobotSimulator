using System;
using System.Collections.Generic;
using ToyRobotSimulator.Commands;

namespace ToyRobotSimulator.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var simulation = new Simulation();

            var commands = new List<Command>
            {
                new PlaceCommand(new Table(), 0, 0, CompassPoint.North),
                new MoveCommand(),
                new ReportCommand(Console.Out),
            };

            simulation.Execute(commands);
        }
    }
}

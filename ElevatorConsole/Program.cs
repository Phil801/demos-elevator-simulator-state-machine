using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateObjects;
using StateMachines;

namespace ElevatorConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            int int_elevators, int_floors;
            string elevators, floors;

            Console.WriteLine("Enter Number of Elevators:");
            elevators = Console.ReadLine();
            Console.WriteLine("Enter Number of Floors:");
            floors = Console.ReadLine();

            int.TryParse(elevators, out int_elevators);
            int.TryParse(floors, out int_floors);

            int_floors = int_floors != 0 ? int_floors : 2;
            int_elevators = int_elevators != 0 ? int_elevators : 1;

            Console.WriteLine("Creating building with {0} Floors and {1} Elevators.", int_floors, int_elevators);

            var Store = new Building(int_floors, int_elevators, new ConsoleOutput());
            Console.Read();
        }
    }
}

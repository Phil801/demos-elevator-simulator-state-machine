using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachines;
using StateObjects;

namespace StateObjects
{
    public class Building
    {
        public Building(int floors, int elevators, IOutput output)
        {
            this.Output = output;
            this.FloorCount = floors;
            this.ElevatorCount = elevators;
            Output.OutputLine("**********  Initializing Building  **********");
            BuildFloors(floors);
            Elevators = new ElevatorBank("Building Elevators", elevators, this, this.Output);
            Output.OutputLine("**********  Building Initialized  **********");
            User = new Rider("User", Output, this);
            User.OnFloor = Floors.First.ListNode;            
        }
        public void StartElevators()
        {
            Elevators.StartElevators();
        }
        public Rider User { get; set; }
        public int FloorCount { get; set; }
        public int ElevatorCount { get; set; }
        private IOutput Output { get; set; }
        public DoubleLinkedList<Floor> Floors { get; set; }
        public ElevatorBank Elevators{get;set;}
        private void  BuildFloors(int floors)
        {
            Output.OutputLine("*** Creating Floors ***");
            this.Floors = new DoubleLinkedList<Floor>();
            
            for (int i = 1; i < floors+1; i++)
            {
                string name = string.Format("Floor {0}", i);
                var floor = new Floor(name, string.Format("Welcome to {0}", name), string.Format("Leaving {0}", name), this.Output, i);
                Floors.Add(floor);
            }

        }

    }
}

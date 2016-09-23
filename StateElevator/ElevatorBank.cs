using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachines;

namespace StateObjects
{
    public enum ElevatorBankStatus { online, offline }
    public delegate void ElevatorBankChangedEventHandler(object sender, StateChangeEventArgs<ElevatorBankStatus> e);
    public class ElevatorBank : StateMachine<ElevatorBankStatus>
    {
        public ElevatorBank(string name, int numberOfElevators, Building building, IOutput output)
        {
            this.Name = name;
            this.Output = output;
            this.BuildingManager = building;

            this.Transitions.Add(new Transition<ElevatorBankStatus>(ElevatorBankStatus.online, ElevatorBankStatus.offline, new baseAction<ElevatorBankStatus>(ElevatorBankStatus.offline)), ElevatorBankStatus.offline);
            this.Transitions.Add(new Transition<ElevatorBankStatus>(ElevatorBankStatus.offline, ElevatorBankStatus.online, new baseAction<ElevatorBankStatus>(ElevatorBankStatus.online)), ElevatorBankStatus.online);
            this.CurrentState = ElevatorBankStatus.offline;
            this.PreviousState = ElevatorBankStatus.offline;
            InitializeElevators(numberOfElevators, building.FloorCount);
            this.BringOnline();
        }

        public event ElevatorBankChangedEventHandler Changed;
        public override void OnStateChanged(StateChangeEventArgs<ElevatorBankStatus> e)
        {
            if (Changed != null)
                Changed(this, e);
        }
        public Dictionary<string, Elevator> Elevators { get; set; }
        private Building BuildingManager { get; set; }

        public void StartElevators()
        {
            int i = 1;
            foreach (var item in this.Elevators)
            {
                Elevator elv = item.Value;
                switch (i)
                {
                    case 1:
                        elv.CurrentFloor = BuildingManager.Floors.First;
                        elv.Direction.CurrentState = ElevatorDirectionStates.Up;
                        break;
                    case 2:
                        elv.CurrentFloor = BuildingManager.Floors.GetLastNode();
                        elv.Direction.CurrentState = ElevatorDirectionStates.Up;
                        break;
                    case 3:
                        elv.CurrentFloor = BuildingManager.Floors.GetNodeAt(i, BuildingManager.Floors.First);
                        elv.Direction.CurrentState = ElevatorDirectionStates.Down;
                        break;
                    default:
                        break;
                }                                
                elv.CurrentState = ElevatorStates.Inactive;                
                elv.ChangeStateTo(ElevatorStates.Starting);
                i++;
            }
        }
        private void InitializeElevators(int numberOfElevators, int floors)
        {
            this.Output.OutputLine("*** Bringing Elevators Online ***");
            this.Elevators = new Dictionary<string, Elevator>();
            for (int i = 1; i < numberOfElevators + 1; i++)
            {
                var elevator = new Elevator(string.Format("Elevator {0}", i), this.Output, floors, i);
                Elevators.Add(elevator.Name, elevator);
            }
        }

    }
}

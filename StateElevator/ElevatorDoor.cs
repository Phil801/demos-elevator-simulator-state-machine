using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachines;

namespace StateObjects
{
    public enum ElevatorDoorStates { Opening, Open, Closing, Closed}
    public delegate void ElevatorDoorStateChangedEventHandler(object sender, StateChangeEventArgs<ElevatorDoorStates> e);
    public class ElevatorDoor: StateMachine<ElevatorDoorStates>
    {
        static async Task<Boolean> DelayResult(TimeSpan delay)
        {
            await Task.Delay(delay);
            return true;
        }

        public event ElevatorDoorStateChangedEventHandler Changed;
        public override async void OnStateChanged(StateChangeEventArgs<ElevatorDoorStates> e)
        {
            if (Changed != null)
            {
                Changed(this, e);

                switch (e.NewState)
                {
                    case ElevatorDoorStates.Opening:
                        await DelayResult(new TimeSpan(0, 0, Globals.STANDARD_TRANSITION_TIMEOUT));
                        ChangeStateTo(ElevatorDoorStates.Open);
                        break;
                    case ElevatorDoorStates.Open:
                        await DelayResult(new TimeSpan(0, 0, Globals.DOOR_TRANSITION_TIMEOUT));
                        ChangeStateTo(ElevatorDoorStates.Closing);
                        break;
                    case ElevatorDoorStates.Closing:
                        await DelayResult(new TimeSpan(0, 0, Globals.STANDARD_TRANSITION_TIMEOUT));
                        this.ChangeStateTo(ElevatorDoorStates.Closed);
                        break;
                    case ElevatorDoorStates.Closed:
                        break;
                    default:
                        break;
                }
            }
        }

        public ElevatorDoor(string name, IOutput output)
        {
            this.Output = output;
            this.Name = name;

            this.Transitions.Add(new Transition<ElevatorDoorStates>(ElevatorDoorStates.Closed, ElevatorDoorStates.Opening, new baseAction<ElevatorDoorStates>(ElevatorDoorStates.Opening)), ElevatorDoorStates.Opening);
            this.Transitions.Add(new Transition<ElevatorDoorStates>(ElevatorDoorStates.Opening, ElevatorDoorStates.Open, new baseAction<ElevatorDoorStates>(ElevatorDoorStates.Open)), ElevatorDoorStates.Open);
            this.Transitions.Add(new Transition<ElevatorDoorStates>(ElevatorDoorStates.Open, ElevatorDoorStates.Closing, new baseAction<ElevatorDoorStates>(ElevatorDoorStates.Closing)), ElevatorDoorStates.Closing);
            this.Transitions.Add(new Transition<ElevatorDoorStates>(ElevatorDoorStates.Closing, ElevatorDoorStates.Closed, new baseAction<ElevatorDoorStates>(ElevatorDoorStates.Closed)), ElevatorDoorStates.Closed);
            this.Transitions.Add(new Transition<ElevatorDoorStates>(ElevatorDoorStates.Closing, ElevatorDoorStates.Opening, new baseAction<ElevatorDoorStates>(ElevatorDoorStates.Opening)), ElevatorDoorStates.Opening);
            this.CurrentState = ElevatorDoorStates.Closed;
            this.PreviousState = ElevatorDoorStates.Closed;
            BringOnline();
        }

        public void Open()
        {
            this.ChangeStateTo(ElevatorDoorStates.Opening);
        }

        public void Close()
        {
            this.ChangeStateTo(ElevatorDoorStates.Closing);
        }
    }
}

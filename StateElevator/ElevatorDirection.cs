using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachines;

namespace StateObjects
{
    public enum ElevatorDirectionStates { Up, Down }
    public delegate void ElevatorDirectionChangedEventHandler(object sender, StateChangeEventArgs<ElevatorDirectionStates> e);
    public class ElevatorDirection : StateMachine<ElevatorDirectionStates>
    {
        public event ElevatorDirectionChangedEventHandler Changed;
        public ElevatorDirection(string name, IOutput output)
        {
            this.Name = name;
            this.Output = output;
            this.Transitions.Add(new Transition<ElevatorDirectionStates>(ElevatorDirectionStates.Up, ElevatorDirectionStates.Down, new baseAction<ElevatorDirectionStates>(ElevatorDirectionStates.Down)), ElevatorDirectionStates.Down);
            this.Transitions.Add(new Transition<ElevatorDirectionStates>(ElevatorDirectionStates.Down, ElevatorDirectionStates.Up, new baseAction<ElevatorDirectionStates>(ElevatorDirectionStates.Up)), ElevatorDirectionStates.Up);
            this.CurrentState = ElevatorDirectionStates.Up;
            this.PreviousState = ElevatorDirectionStates.Up;
            BringOnline();
        }

        public override void OnStateChanged(StateChangeEventArgs<ElevatorDirectionStates> e)
        {
            if (Changed != null)
                Changed(this, e);
        }

        public void ChangeDirection()
        {
            if (CurrentState == ElevatorDirectionStates.Up)
            {
                this.ChangeStateTo(ElevatorDirectionStates.Down);
            }
            else
            {
                if (CurrentState == ElevatorDirectionStates.Down)
                {
                    this.ChangeStateTo(ElevatorDirectionStates.Up);
                }
            }
        }
    }
}

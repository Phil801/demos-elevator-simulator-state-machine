using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachines;

namespace StateObjects
{
    public class ElevatorStateChangedEventArgs<T> : StateChangeEventArgs<T> where T : struct, IConvertible
    {
        public ElevatorStateChangedEventArgs(StateChangeEventArgs<T> oldE, Elevator elevator)
        {
            this.Name = oldE.Name;
            this.NewState = oldE.NewState;
            this.PreviousState = oldE.PreviousState;
            this.StateManager = oldE.StateManager;
            this.Elevator = elevator;
        }


        public Elevator Elevator { get; set; }
    }
}

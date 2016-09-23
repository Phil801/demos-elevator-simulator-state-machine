using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachines
{
    public class Transition<T> where T :struct, IConvertible 
    {
        public Transition(T currentState, T nextState, IAction<T> action)
        {
            this.CurrentState = currentState;
            this.NextState = nextState;
            this.Action = action;
        }

        public Transition(T currentState, T nextState)
        {
            this.CurrentState = currentState;
            this.NextState = nextState;
            this.Action = null;
        }

        public T CurrentState { get; set; }
        public T NextState { get; set; }        
        public IAction<T> Action { get; set; }

        public override bool Equals(object obj)
        {
            Transition<T> other = obj as Transition<T>;
            return other != null && this.CurrentState.Equals(other.CurrentState) && this.NextState.Equals(other.NextState);
        }
        public override int GetHashCode()
        {
            return 5 + 41 * this.CurrentState.GetHashCode() + 79 * this.NextState.GetHashCode();
        }
    }
}


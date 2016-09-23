//***************************************************************//
// State Transition Pattern using a Dictionary of Enums 
// derived from this github project:
// https://github.com/MarcoMig/Finite-State-Machine-FSM
//***************************************************************//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace StateMachines
{    
    public abstract class StateMachine<T> where T : struct, IConvertible
    {
        public StateMachine()
        {
            this.Transitions = new Dictionary<Transition<T>, T>();
            if (!typeof(T).IsEnum)
            {
                throw new Exception("Must initialize with a valid Enum");
            }      
        }
        protected void BringOnline()
        {
            this.Output.OutputLine(string.Format("{0} Online, Initialized to {1}", this.Name, this.CurrentState.ToString()));
        }
        public abstract void OnStateChanged(StateChangeEventArgs<T> e);          
        public string Name { get; set; }
        public Dictionary<Transition<T>, T> Transitions { get; set; }
        public T CurrentState { get; set; }
        public T PreviousState { get; set; }
        public IOutput Output { get; set; }
        public T ChangeStateTo(T changeToState)
        {            
            var newState = new Transition<T>(this.CurrentState, changeToState);

            if (CanReachState(newState))
            {
                newState = this.Transitions.Keys.Where(t => t.Equals(newState)).First();
                newState.Action.Invoke(this);
            }           
            return this.CurrentState;
        }

        public bool CanReachState(T stateToCheck)
        {
            Transition<T> checkState = new Transition<T>(this.CurrentState, stateToCheck);
            return CanReachState(checkState);
        }

        public bool CanReachState(Transition<T> stateToCheck)
        {
            return this.Transitions.Keys.Where(t => t.Equals(stateToCheck)).Count() > 0;
        }
    }
}

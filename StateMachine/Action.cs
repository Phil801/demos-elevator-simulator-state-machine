using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachines
{
    public class baseAction<T> : IAction<T> where T : struct, IConvertible
    {
        private T CurrentState;
        private T TransToState;

        public baseAction(T actionToState)
        {
            this.TransToState = actionToState;
        }
        public bool BeginTransistion()
        {
            return true;
        }

        public bool CompleteTransistion()
        {
            return true;
        }

        public bool DuringTransition()
        {
            return true;
        }

        public bool Invoke(StateMachine<T> manager)
        {
            this.CurrentState = manager.CurrentState;
            if (this.BeginTransistion())
            {
                if (this.DuringTransition())
                {
                    if (this.CompleteTransistion())
                    {
                        manager.PreviousState = manager.CurrentState;
                        manager.CurrentState = this.TransToState;
                        manager.Output.OutputLine(string.Format("{2} changed State from {0} To {1}", manager.PreviousState, manager.CurrentState, manager.Name));


                        manager.OnStateChanged(new StateChangeEventArgs<T>
                        {
                            Name = manager.Name,
                            NewState = manager.CurrentState,
                            PreviousState = manager.PreviousState,
                            StateManager = manager
                        });
                        return true;
                    }
                }
            }
            manager.Output.OutputLine(string.Format("Unable to change State from {0} To {1}", manager.CurrentState, this.TransToState));
            return false;
        }
    }
}

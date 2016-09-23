using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachines
{    
    public class StateChangeEventArgs<T>:EventArgs where T :struct, IConvertible
    {
        public T NewState { get; set; }
        public T PreviousState { get; set; }
        public string Name { get; set; }
        public StateMachine<T> StateManager { get; set; }
    }
}

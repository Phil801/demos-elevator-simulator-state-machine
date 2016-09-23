using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachines
{
    public class ElevatorStateChangedEventArgs<T>: StateChangeEventArgs<T> where T : struct, IConvertible
    {
        
    }
}

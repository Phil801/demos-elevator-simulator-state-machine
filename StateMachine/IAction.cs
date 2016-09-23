using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachines
{
   public interface IAction<T>where T : struct, IConvertible
    {
        bool Invoke(StateMachine<T>manager);
        bool BeginTransistion();
        bool DuringTransition();
        bool CompleteTransistion();
    }
}

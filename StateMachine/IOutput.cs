using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachines
{
    public interface IOutput
    {
        void OutputLine(string line);
        void OutputObject(object obj);
    }
}

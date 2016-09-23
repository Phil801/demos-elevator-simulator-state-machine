using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachines
{
    public class ConsoleOutput : IOutput
    {
        public void OutputLine(string line)
        {
            Console.WriteLine(line);
        }

        public void OutputObject(object obj)
        {
            throw new NotImplementedException();
        }
    }
}

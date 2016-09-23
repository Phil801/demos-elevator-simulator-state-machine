using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StateMachines;


namespace StateObjects
{
    public class Bot: Rider
    {
        public Bot(string name, IOutput output, Building buildingMgr)
            : base(name, output, buildingMgr)
        { }
    }
}

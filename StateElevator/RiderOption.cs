using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateObjects
{
    public class RiderOption
    {
        public string DisplayName { get; set; }
        public ActionInvokeHandler Callback{get; set;}
        public RiderOptionTypes RiderOptionType{ get; set; }
    }
}

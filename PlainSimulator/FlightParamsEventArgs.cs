using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlainSimulator
{
    class FlightParamsEventArgs : EventArgs
    {
        public Plain Plain;
        public Pilot Pilot;
    }
}

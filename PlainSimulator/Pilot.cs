using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlainSimulator
{
    class Pilot : AHuman
    {
        public Dictionary<string, int> points { get; set; }
       
        public Pilot(string name)
        {
            Name = name;
            this.points = new Dictionary<string, int>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Person
    {
        public string Name { get; set; } 
        public string Mail { get; set; }
        public string Dept { get; set; }
        public string Rank { get; set; }
        public string Phone { get; set; }
        public string Room { get; set; }

        public override string ToString()
        {
            return Name + " " + Dept + " " + Room;
        }
    }
}

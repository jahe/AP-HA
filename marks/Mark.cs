using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AP_HA
{
    class Mark
    {
        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

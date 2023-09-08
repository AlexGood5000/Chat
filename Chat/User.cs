using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat
{
    internal class User
    {
        internal string Name { get; set; }
        internal byte Address { get; set; }

        internal User()
        {
            Name = null;
            Address = 0;
        }
    }
}

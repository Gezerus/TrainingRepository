using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore
{
    public class Laptop : Product
    {
        public Laptop(string name, Price cost) : base(name, cost)
        {
            Type = GetType().ToString();
        }

        private Laptop(string name, Price cost, string type) : base(name, cost)
        {
            Type = type;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore
{
    public abstract class Product
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public Price Cost { get; set; }

        protected Product(string name, Price cost)
        {            
            Name = name;
            Cost = new Price(cost);
        }
    }

 
}

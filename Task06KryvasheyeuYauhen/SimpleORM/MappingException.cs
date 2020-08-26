using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleORM
{
    public class MappingException : Exception
    {
        public MappingException(string message) : base(message)
        {

        }
    }
}

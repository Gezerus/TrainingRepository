using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleORM
{
    /// <summary>
    /// abstract class for all sql string creators
    /// </summary>
    public abstract class SqlStringCreator
    {
        public abstract string Create();
    }
}

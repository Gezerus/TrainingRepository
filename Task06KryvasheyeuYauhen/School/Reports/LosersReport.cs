using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Reports
{
    /// <summary>
    /// report about students subject to expulsion
    /// </summary>
    public class LosersReport
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int GroupId { get; set; }
    }
}

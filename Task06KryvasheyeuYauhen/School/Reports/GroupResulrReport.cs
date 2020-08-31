using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Reports
{
    /// <summary>
    /// report of sessions results
    /// </summary>
    public class GroupResultReport
    {
        public int SessionId { get; set; }
        public int GroupId{ get; set; }

        public int Min { get; set; }

        public int Max { get; set; }

        public double Average { get; set; }

    }
}

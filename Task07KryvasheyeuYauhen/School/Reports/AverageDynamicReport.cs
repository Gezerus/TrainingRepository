using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Reports
{
    /// <summary>
    /// report of the average score in subjects in each year
    /// </summary>
    public class AverageDynamicReport
    {
        public int Year { get; set; }

        public string SubjectName { get; set; }

        public double Average { get; set; }
    }
}

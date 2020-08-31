using School;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programm
{
    class Program
    {
        static void Main(string[] args)
        {
            ScriptRunner.CreateSchoolDataBase();

            var reportGenerator = new ReportGenerator();

            var sessionResultreport = reportGenerator.SessinResultGenerate(r => r.GroupId);

            XlsxReporWriter.SessionREsultReportWrite(sessionResultreport, string.Format(@"..\..\Reports\SessionResultReports\{0}-{1}.xlsx",
                DateTime.Now.DayOfYear, (int)DateTime.Now.TimeOfDay.TotalSeconds));

            var loserReport = reportGenerator.LosersReportGenerate(r => r.Name);

            XlsxReporWriter.LosersReportWrite(loserReport, string.Format(@"..\..\Reports\LosersReports\{0}-{1}.xlsx",
                DateTime.Now.DayOfYear, (int)DateTime.Now.TimeOfDay.TotalSeconds));
        }
    }
}

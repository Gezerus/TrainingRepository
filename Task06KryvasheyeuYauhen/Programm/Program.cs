using School;
using System;

namespace Programm
{
    class Program
    {
        static void Main(string[] args)
        {
            ScriptRunner.CreateSchoolDbIfNotExist();

            var reportGenerator = new ReportGenerator();

            var sessionResultreport = reportGenerator.SessinResultGenerate(r => r.GroupId);

            XlsxReportWriter.SessionREsultReportWrite(sessionResultreport, string.Format(@"..\..\Reports\SessionResultReports\{0}-{1}.xlsx",
                DateTime.Now.DayOfYear, (int)DateTime.Now.TimeOfDay.TotalSeconds));

            var loserReport = reportGenerator.LosersReportGenerate(r => r.Name);

            XlsxReportWriter.LosersReportWrite(loserReport, string.Format(@"..\..\Reports\LosersReports\{0}-{1}.xlsx",
                DateTime.Now.DayOfYear, (int)DateTime.Now.TimeOfDay.TotalSeconds));
        }
    }
}

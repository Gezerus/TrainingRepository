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
            ScriptRunner.CreateSchoolDbIfNotExist();

            var reportGenerator = new ReportGenerator();

            var specialtyAvgReport = reportGenerator.SpecialtyAverageReportGenerate(r => r.Specialty);

            XlsxReportWriter.SpecialtyAverageReportWrite(specialtyAvgReport, string.Format(@"..\..\Reports\SpecialtyAverageReport\{0}-{1}.xlsx",
                DateTime.Now.DayOfYear, (int)DateTime.Now.TimeOfDay.TotalSeconds));

            var teacherAvgReport = reportGenerator.TeacherAverageReportGenerate(r => r.Name);

            XlsxReportWriter.TeacherAverageReportWrite(teacherAvgReport, string.Format(@"..\..\Reports\TeacherAverageReport\{0}-{1}.xlsx",
                DateTime.Now.DayOfYear, (int)DateTime.Now.TimeOfDay.TotalSeconds));

            var avgDynamicReport = reportGenerator.AverageDynamicReportGenerate();

            XlsxReportWriter.AverageDynamicReportWrite(avgDynamicReport, string.Format(@"..\..\Reports\AverageDynamicReport\{0}-{1}.xlsx",
                DateTime.Now.DayOfYear, (int)DateTime.Now.TimeOfDay.TotalSeconds));
        }
    }
}

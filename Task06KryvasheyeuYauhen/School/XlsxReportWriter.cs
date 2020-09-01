using OfficeOpenXml;
using School.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace School
{
    /// <summary>
    /// writer reports from school database to xlsx
    /// </summary>
    public static class XlsxReportWriter
    {
        /// <summary>
        /// writes report of students subject to expulsion  to xlsx
        /// </summary>
        /// <param name="reports"></param>
        /// <param name="path"></param>
        public static void LosersReportWrite(IEnumerable<IEnumerable<LosersReport>> reports, string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (reports.Count() < 1)
                throw new ArgumentException("report is empty");

            using(ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet;
                foreach (var gr in reports)
                {
                    worksheet = excelPackage.Workbook.Worksheets.Add(string.Format("Group {0}", gr.First().GroupId));
                    worksheet.Cells[1, 1].Value = "Id";
                    worksheet.Cells[1, 2].Value = "Name";

                    int row = 2;
                    int column = 1;
                    foreach(var report in gr)
                    {
                        worksheet.Cells[row, column].Value = report.Id;
                        worksheet.Cells[row++, column + 1].Value = report.Name;
                    }
                }

                FileInfo fi = new FileInfo(path);
                excelPackage.SaveAs(fi);
            }
        }

        /// <summary>
        /// writes report of session results to xlsx
        /// </summary>
        /// <param name="reports"></param>
        /// <param name="path"></param>
        public static void SessionREsultReportWrite(IEnumerable<IEnumerable<GroupResultReport>> reports, string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (reports.Count() < 1)
                throw new ArgumentException("report is empty");

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = null;
                foreach (var report in reports)
                {
                    worksheet = excelPackage.Workbook.Worksheets.Add(string.Format("Session {0}", report.First().SessionId));
                    worksheet.Cells[1, 1].Value = "Groups";
                    worksheet.Cells[1, 2].Value = "Min";
                    worksheet.Cells[1, 3].Value = "Max";
                    worksheet.Cells[1, 4].Value = "Average";
                    int row = 2;
                    int column = 1;
                    foreach (GroupResultReport gReport in report)
                    {
                        worksheet.Cells[row, column].Value = gReport.GroupId;
                        worksheet.Cells[row, column + 1].Value = gReport.Min;
                        worksheet.Cells[row, column + 2].Value = gReport.Max;
                        worksheet.Cells[row++, column + 3].Value = gReport.Average;
                    }
                }
                FileInfo fi = new FileInfo(path);
                excelPackage.SaveAs(fi);

                worksheet.Dispose();
            }
        }
    }
}

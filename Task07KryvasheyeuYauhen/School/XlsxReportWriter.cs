using OfficeOpenXml;
using School.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace School
{
    public static class XlsxReportWriter
    {
        /// <summary>
        /// writes report of the average score in subjects in each year to xlsx file
        /// </summary>
        /// <param name="reports"></param>
        /// <param name="path"></param>
        public static void SpecialtyAverageReportWrite(IQueryable<IEnumerable<SpecialtyAverageReport>> reports, string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (reports.Count() < 1)
                throw new ArgumentException("report is empty");

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet;
                foreach (var session in reports)
                {
                    worksheet = excelPackage.Workbook.Worksheets.Add(string.Format("Session {0}", session.First().SessionId));
                    worksheet.Cells[1, 1].Value = "Specialty";
                    worksheet.Cells[1, 2].Value = "Average";

                    int row = 2;
                    int column = 1;
                    foreach (var report in session)
                    {
                        worksheet.Cells[row, column].Value = report.Specialty;
                        worksheet.Cells[row++, column + 1].Value = report.Average;
                    }
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                }

                FileInfo fi = new FileInfo(path);
                excelPackage.SaveAs(fi);
            }
        }

        /// <summary>
        /// writes report of average score in each teacher to xlsx file
        /// </summary>
        /// <param name="reports"></param>
        /// <param name="path"></param>
        public static void TeacherAverageReportWrite(IQueryable<IEnumerable<TeacherAverageReport>> reports, string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (reports.Count() < 1)
                throw new ArgumentException("report is empty");

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet;
                foreach (var session in reports)
                {
                    worksheet = excelPackage.Workbook.Worksheets.Add(string.Format("Session {0}", session.First().SessionId));
                    worksheet.Cells[1, 1].Value = "Id";
                    worksheet.Cells[1, 2].Value = "Teacher";
                    worksheet.Cells[1, 3].Value = "Average";

                    int row = 2;
                    int column = 1;
                    foreach (var report in session)
                    {
                        worksheet.Cells[row, column].Value = report.Id;
                        worksheet.Cells[row, column + 1].Value = report.Name;
                        worksheet.Cells[row++, column + 2].Value = report.Average;
                    }
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                }

                FileInfo fi = new FileInfo(path);
                excelPackage.SaveAs(fi);
            }
        }

        /// <summary>
        /// writes report of the average score in subjects in each year to xslx file
        /// </summary>
        /// <param name="reports"></param>
        /// <param name="path"></param>
        public static void AverageDynamicReportWrite(IQueryable<IEnumerable<AverageDynamicReport>> reports, string path)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            if (reports.Count() < 1)
                throw new ArgumentException("report is empty");

            var cache = new Dictionary<string, int>();

            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                var worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                int row = 2;
                int column = 2;
                worksheet.Cells[1, 1].Value = "Subject";
                foreach(var year in reports)
                {
                    worksheet.Cells[1, column].Value = year.First().Year;
                    foreach(var report in year)
                    {
                        if (cache.Keys.Contains(report.SubjectName))
                            worksheet.Cells[cache[report.SubjectName], column].Value = report.Average;
                        else
                        {
                            cache[report.SubjectName] = row;
                            worksheet.Cells[row, 1].Value = report.SubjectName;
                            worksheet.Cells[row++, column].Value = report.Average;
                        }
                    }
                    column++;
                }
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
                FileInfo fi = new FileInfo(path);
                excelPackage.SaveAs(fi);
            }
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;
using School;
using School.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTest
{
    [TestClass]
    public class XlsxReportWriterTest
    {
        /// <summary>
        /// Test method SpecialtyAverageReportWrite. The method should write report to alsx file
        /// </summary>
        [TestMethod]
        public void SpecialtyAverageReportWrite_ShouldWriteToXlsxCorrectly()
        {
            // Arrange
            var syntheticReport = new List<List<SpecialtyAverageReport>>();
            var session1 = new List<SpecialtyAverageReport>
            {
                new SpecialtyAverageReport { SessionId = 1, Specialty = "Test1", Average = 1.1},
                new SpecialtyAverageReport { SessionId = 1, Specialty = "Test2", Average = 2.2}
            };
            var session2 = new List<SpecialtyAverageReport>
            {
                new SpecialtyAverageReport { SessionId = 2, Specialty = "Test3", Average = 3.3},
                new SpecialtyAverageReport { SessionId = 2, Specialty = "Test4", Average = 4.4}
            };


            syntheticReport.Add(session1);
            syntheticReport.Add(session2);

            // Act
            XlsxReportWriter.SpecialtyAverageReportWrite(syntheticReport.AsQueryable(), @"..\..\TestReports\SpecAvg.xlsx");
            // read xlsx
            // Assert
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo fi = new FileInfo(@"..\..\TestReports\SpecAvg.xlsx");
            using (ExcelPackage excelPackage = new ExcelPackage(fi))
            {
                ExcelWorksheet worksheet;
                int i = 0;
                int j = 0;
                foreach (var session in syntheticReport)
                {
                    worksheet = excelPackage.Workbook.Worksheets[string.Format("Session {0}", session.First().SessionId)];
                    Assert.AreEqual(worksheet.Cells[1, 1].Value.ToString(), "Specialty");
                    Assert.AreEqual(worksheet.Cells[1, 2].Value.ToString(), "Average");
                    int row = 2;
                    int column = 1;
                    foreach (var report in session)
                    {
                        Assert.AreEqual(worksheet.Cells[row, column].Value.ToString(), syntheticReport[i][j].Specialty);
                        Assert.AreEqual(worksheet.Cells[row++, column + 1].GetValue<double>(), syntheticReport[i][j].Average);
                        j++;
                    }
                    i++;
                    j = 0;
                }
                excelPackage.Save();
                fi.Delete();
            }
        }

        /// <summary>
        /// Test method SpecialtyAverageReportWrite when report is empty. The method should throw exception.
        /// </summary>
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void SpecialtyAverageReportWrite_WhenReportIsEmpty_ShouldThrowException()
        {
            // Arrange
            var syntheticReport = new List<List<SpecialtyAverageReport>>();
            // Act
            XlsxReportWriter.SpecialtyAverageReportWrite(syntheticReport.AsQueryable(), @"..\..\TestReports\SpecAvg.xlsx");

        }

        /// <summary>
        /// Test method TeacherAverageReportWrite. The method should write report to alsx file
        /// </summary>
        [TestMethod]
        public void TeacherAverageReportWrite_ShouldWriteToXlsxCorrectly()
        {
            // Arrange
            var syntheticReport = new List<List<TeacherAverageReport>>();
            var session1 = new List<TeacherAverageReport>
            {
                new TeacherAverageReport { SessionId = 1, Name = "Test1", Id = 123, Average = 1.1},
                new TeacherAverageReport { SessionId = 1, Name = "Test2", Id = 323, Average = 2.2},
            };

            var session2 = new List<TeacherAverageReport>
            {
                new TeacherAverageReport { SessionId = 2, Name = "Test3", Id = 423, Average = 3.1},
                new TeacherAverageReport { SessionId = 2, Name = "Test4", Id = 623, Average = 4.2},
            };

            syntheticReport.Add(session1);
            syntheticReport.Add(session2);

            // Act
            XlsxReportWriter.TeacherAverageReportWrite(syntheticReport.AsQueryable(), @"..\..\TestReports\TeachAvg.xlsx");
            // read xlsx
            // Assert
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo fi = new FileInfo(@"..\..\TestReports\TeachAvg.xlsx");
            using (ExcelPackage excelPackage = new ExcelPackage(fi))
            {
                ExcelWorksheet worksheet;
                int i = 0;
                int j = 0;
                foreach (var session in syntheticReport)
                {
                    worksheet = excelPackage.Workbook.Worksheets[string.Format("Session {0}", session.First().SessionId)];
                    Assert.AreEqual(worksheet.Cells[1, 1].Value.ToString(), "Id");
                    Assert.AreEqual(worksheet.Cells[1, 2].Value.ToString(), "Teacher");
                    Assert.AreEqual(worksheet.Cells[1, 3].Value.ToString(), "Average");
                    int row = 2;
                    int column = 1;
                    foreach (var report in session)
                    {
                        Assert.AreEqual(worksheet.Cells[row, column].GetValue<int>(), syntheticReport[i][j].Id);
                        Assert.AreEqual(worksheet.Cells[row, column + 1].Value.ToString(), syntheticReport[i][j].Name);
                        Assert.AreEqual(worksheet.Cells[row++, column + 2].GetValue<double>(), syntheticReport[i][j].Average);
                        j++;
                    }
                    i++;
                    j = 0;
                }
                excelPackage.Save();
                fi.Delete();
            }
        }

        /// <summary>
        /// Test method TeacherAverageReportWrite when report is empty. The method should throw exception.
        /// </summary>
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void TeacherAverageReportWrite_WhenReportIsEmpty_ShouldThrowException()
        {
            // Arrange
            var syntheticReport = new List<List<TeacherAverageReport>>();
            // Act
            XlsxReportWriter.TeacherAverageReportWrite(syntheticReport.AsQueryable(), @"..\..\TestReports\TeachAvg.xlsx");

        }

        /// <summary>
        /// Test method AverageDynamicReportWrite. The method should write report to alsx file
        /// </summary>
        [TestMethod]
        public void AverageDynamicReportWrite_ShouldWriteToXlsxCorrectly()
        {
            // Arrange
            var syntheticReport = new List<List<AverageDynamicReport>>();
            var year1 = new List<AverageDynamicReport>
            {
                new AverageDynamicReport { Year = 1001, SubjectName = "Test1", Average = 1.1},
                new AverageDynamicReport { Year = 1001, SubjectName = "Test2", Average = 2.2},
            };
            var year2 = new List<AverageDynamicReport>
            {
                new AverageDynamicReport { Year = 1002, SubjectName = "Test1", Average = 3.1},
                new AverageDynamicReport { Year = 1002, SubjectName = "Test2", Average = 4.2},
            };

            syntheticReport.Add(year1);
            syntheticReport.Add(year2);

            // Act
            XlsxReportWriter.AverageDynamicReportWrite(syntheticReport.AsQueryable(), @"..\..\TestReports\Avgdyn.xlsx");
            // read xlsx
            // Assert
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo fi = new FileInfo(@"..\..\TestReports\Avgdyn.xlsx");
            using (ExcelPackage excelPackage = new ExcelPackage(fi))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets["Sheet 1"];
                Assert.AreEqual(worksheet.Cells[1, 1].Value.ToString(), "Subject");
                int i = 0;
                int j = 0;                
                int column = 2;
                foreach (var year in syntheticReport)
                {
                    int row = 2;
                    Assert.AreEqual(worksheet.Cells[1, column].GetValue<int>(), syntheticReport[i][j].Year);
                    foreach (var report in year)
                    {
                        Assert.AreEqual(worksheet.Cells[row, 1].Value.ToString(), syntheticReport[i][j].SubjectName);                        
                        Assert.AreEqual(worksheet.Cells[row++, column].GetValue<double>(), syntheticReport[i][j].Average);
                        j++;
                    }
                    i++;
                    j = 0;
                    column++;
                }
                excelPackage.Save();
                fi.Delete();
            }
        }

        /// <summary>
        /// Test method AverageDynamicReportWrite when report is empty. The method should throw exception.
        /// </summary>
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void AverageDynamicReportWrite_WhenReportIsEmpty_ShouldThrowException()
        {
            // Arrange
            var syntheticReport = new List<List<AverageDynamicReport>>();
            // Act
            XlsxReportWriter.AverageDynamicReportWrite(syntheticReport.AsQueryable(), @"..\..\TestReports\Avgdyn.xlsx");

        }
    }
}

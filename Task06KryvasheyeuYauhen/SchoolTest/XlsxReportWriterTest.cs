using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeOpenXml;
using School;
using School.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SchoolTest
{
    [TestClass]
    public class XlsxReportWriterTest
    {
        /// <summary>
        /// Test method LoserReportWrite. The method should write report to alsx file
        /// </summary>
        [TestMethod]
        public void LoserReportWrite_ShouldWriteToXlsxCorrectly()
        {
            // Arrange
            var syntheticReport = new List<List<LosersReport>>();
            var group1 = new List<LosersReport>
            {
                new LosersReport { GroupId = 1, Name = "David", Id = 234},
                new LosersReport { GroupId = 1, Name = "Evgen", Id = 2}
            };
            var group2 = new List<LosersReport>
            {
                new LosersReport { GroupId = 2, Name = "Oleg", Id = 34}                
            };
            syntheticReport.Add(group1);
            syntheticReport.Add(group2);

            // Act
            XlsxReportWriter.LosersReportWrite(syntheticReport, @"..\..\TestReports\Losers.xlsx");
            // read xlsx
            // Assert
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo fi = new FileInfo(@"..\..\TestReports\Losers.xlsx");
            using (ExcelPackage excelPackage = new ExcelPackage(fi))
            {
                ExcelWorksheet worksheet;
                int i= 0;
                int j = 0;
                foreach(var group in syntheticReport)
                {
                    worksheet = excelPackage.Workbook.Worksheets[string.Format("Group {0}", group.First().GroupId)];
                    Assert.AreEqual(worksheet.Cells[1, 1].Value.ToString(), "Id");
                    Assert.AreEqual(worksheet.Cells[1, 2].Value.ToString(), "Name");
                    int row = 2;
                    int column = 1;
                    foreach (var report in group)
                    {
                        Assert.AreEqual(worksheet.Cells[row, column].GetValue<int>(), syntheticReport[i][j].Id);
                        Assert.AreEqual(worksheet.Cells[row++, column + 1].Value.ToString(), syntheticReport[i][j].Name);
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
        /// Test method LoserReportWrite when report is empty. The method should throw exception.
        /// </summary>
        [ExpectedException(typeof(ArgumentException))]
        [TestMethod]
        public void LoserReportWrite_WhenReportIsEmpty_ShouldThrowException()
        {
            // Arrange
            var syntheticReport = new List<List<LosersReport>>();
            // Act
            XlsxReportWriter.LosersReportWrite(syntheticReport, @"..\..\TestReports\Losers.xlsx");

        }

        /// <summary>
        /// Test method SessionResultReportWrite. The method should write report to xlsx file
        /// </summary>
        [TestMethod]
        public void SessionResultReportWrite_ShouldWriteToXlsxCorrectly()
        {
            // Arrange
            var syntheticReport = new List<List<GroupResultReport>>();
            var session1 = new List<GroupResultReport>
            {
                new GroupResultReport { SessionId = 1, GroupId = 13, Max = 5, Min = 1, Average = 2.5},
                new GroupResultReport { SessionId = 1, GroupId = 25, Max = 10, Min = 2, Average = 5}
            };
            var session2 = new List<GroupResultReport>
            {
                new GroupResultReport { SessionId = 2, GroupId = 103, Max = 5, Min = 1, Average = 2.5},
                new GroupResultReport { SessionId = 2, GroupId = 205, Max = 10, Min = 2, Average = 5}
            };

            syntheticReport.Add(session1);
            syntheticReport.Add(session2);

            // Act
            XlsxReportWriter.SessionREsultReportWrite(syntheticReport, @"..\..\TestReports\Result.xlsx");
            // read xlsx
            // Assert
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            FileInfo fi = new FileInfo(@"..\..\TestReports\Result.xlsx");
            using (ExcelPackage excelPackage = new ExcelPackage(fi))
            {
                ExcelWorksheet worksheet;
                int i = 0;
                int j = 0;
                foreach (var session in syntheticReport)
                {
                    worksheet = excelPackage.Workbook.Worksheets[string.Format("Session {0}", session.First().SessionId)];
                    Assert.AreEqual(worksheet.Cells[1, 1].Value.ToString(), "Groups");
                    Assert.AreEqual(worksheet.Cells[1, 2].Value.ToString(), "Min");
                    Assert.AreEqual(worksheet.Cells[1, 3].Value.ToString(), "Max");
                    Assert.AreEqual(worksheet.Cells[1, 4].Value.ToString(), "Average");                    
                    int row = 2;
                    int column = 1;
                    foreach (var report in session)
                    {
                        Assert.AreEqual(worksheet.Cells[row, column].GetValue<int>(), syntheticReport[i][j].GroupId);
                        Assert.AreEqual(worksheet.Cells[row, column + 1].GetValue<int>(), syntheticReport[i][j].Min);
                        Assert.AreEqual(worksheet.Cells[row, column + 2].GetValue<int>(), syntheticReport[i][j].Max);
                        Assert.AreEqual(worksheet.Cells[row++, column + 3].GetValue<double>(), syntheticReport[i][j].Average);                        
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
        /// Test method SessionResultReportWrite when report is empty. The method should throw exception
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SessionResultReportWrite_WhenReportIsEmpty_ShouldWriteToXlsxCorrectly()
        {
            // Arrange
            var syntheticReport = new List<List<GroupResultReport>>();
            // Act
            XlsxReportWriter.SessionREsultReportWrite(syntheticReport, @"..\..\TestReports\Result.xlsx");

        }
    }
}

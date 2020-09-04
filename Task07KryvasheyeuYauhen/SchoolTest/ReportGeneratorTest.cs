using Microsoft.VisualStudio.TestTools.UnitTesting;
using School;
using School.Models;
using School.Reports;
using School.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTest
{
    [TestClass]
    public class ReportGeneratorTest
    {
        /// <summary>
        /// Test Method SpecialtyAverageReportGenerate. The method should return correct report from database
        /// </summary>
        [TestMethod]
        public void SpecialtyAverageReportGenerate_ShouldReturnCorrectReport()
        {
            // Arrange
            ScriptRunner.DeleteSchoolDbIfExist();
            ScriptRunner.CreateSchoolDbIfNotExist();

            var syntheticReport = new List<List<SpecialtyAverageReport>>();

            var session1 = new List<SpecialtyAverageReport>
            {
                new SpecialtyAverageReport{ SessionId = 1, Specialty = "Industrial energy", Average = 4.67},
                new SpecialtyAverageReport{ SessionId = 1, Specialty = "Marketing", Average = 8},
                new SpecialtyAverageReport{ SessionId = 1, Specialty = "Mechanical engineering technology", Average = 7.83},
            };
            var session2 = new List<SpecialtyAverageReport>
            {
                new SpecialtyAverageReport{ SessionId = 2, Specialty = "Industrial energy", Average = 7.44},
                new SpecialtyAverageReport{ SessionId = 2, Specialty = "Marketing", Average = 8.22},
                new SpecialtyAverageReport{ SessionId = 2, Specialty = "Mechanical engineering technology", Average = 6.58}
            };
            var session3 = new List<SpecialtyAverageReport>
            {
                new SpecialtyAverageReport{ SessionId = 3, Specialty = "Industrial energy", Average = 7.5},
                new SpecialtyAverageReport{ SessionId = 3, Specialty = "Marketing", Average = 7.83},
                new SpecialtyAverageReport{ SessionId = 3, Specialty = "Mechanical engineering technology", Average = 6.75}
            };
            syntheticReport.Add(session1);
            syntheticReport.Add(session2);
            syntheticReport.Add(session3);
            var generator = new ReportGenerator();
            // Act
            var result = generator.SpecialtyAverageReportGenerate();
            // Assert

            int i = 0;
            int j = 0;
            foreach (var session in result)
            {
                foreach (var report in session)
                {
                    Assert.AreEqual(report.SessionId, syntheticReport[i][j].SessionId);
                    Assert.AreEqual(report.Specialty, syntheticReport[i][j].Specialty);
                    Assert.AreEqual(Math.Round(report.Average, 2), syntheticReport[i][j].Average);
                    j++;
                }
                i++;
                j = 0;
            }
        }

        /// <summary>
        /// Test Method SpecialtyAverageReportGenerate when sorting is used. The method should return correct report from database
        /// </summary>
        [TestMethod]
        public void SpecialtyAverageReportGenerate_WhenSortingIsUsed_ShouldReturnCorrectReport()
        {
            // Arrange
            ScriptRunner.DeleteSchoolDbIfExist();
            ScriptRunner.CreateSchoolDbIfNotExist();

            var syntheticReport = new List<List<SpecialtyAverageReport>>();

            var session1 = new List<SpecialtyAverageReport>
            {
                new SpecialtyAverageReport{ SessionId = 1, Specialty = "Industrial energy", Average = 4.67},
                new SpecialtyAverageReport{ SessionId = 1, Specialty = "Mechanical engineering technology", Average = 7.83},
                new SpecialtyAverageReport{ SessionId = 1, Specialty = "Marketing", Average = 8},               
            };
            var session2 = new List<SpecialtyAverageReport>
            {
                new SpecialtyAverageReport{ SessionId = 2, Specialty = "Mechanical engineering technology", Average = 6.58},
                new SpecialtyAverageReport{ SessionId = 2, Specialty = "Industrial energy", Average = 7.44},
                new SpecialtyAverageReport{ SessionId = 2, Specialty = "Marketing", Average = 8.22},               
            };
            var session3 = new List<SpecialtyAverageReport>
            {
                new SpecialtyAverageReport{ SessionId = 3, Specialty = "Mechanical engineering technology", Average = 6.75},
                new SpecialtyAverageReport{ SessionId = 3, Specialty = "Industrial energy", Average = 7.5},
                new SpecialtyAverageReport{ SessionId = 3, Specialty = "Marketing", Average = 7.83},               
            };
            syntheticReport.Add(session1);
            syntheticReport.Add(session2);
            syntheticReport.Add(session3);
            var generator = new ReportGenerator();
            // Act
            var result = generator.SpecialtyAverageReportGenerate(r => r.Average);
            // Assert

            int i = 0;
            int j = 0;
            foreach (var session in result)
            {
                foreach (var report in session)
                {
                    Assert.AreEqual(report.SessionId, syntheticReport[i][j].SessionId);
                    Assert.AreEqual(report.Specialty, syntheticReport[i][j].Specialty);
                    Assert.AreEqual(Math.Round(report.Average, 2), syntheticReport[i][j].Average);
                    j++;
                }
                i++;
                j = 0;
            }
        }

        /// <summary>
        /// Test Method TeacherAverageReportGenerate . The method should return correct report from database
        /// </summary>
        [TestMethod]
        public void TeacherAverageReportGenerate_ShouldReturnCorrectReport()
        {
            // Arrange
            ScriptRunner.DeleteSchoolDbIfExist();
            ScriptRunner.CreateSchoolDbIfNotExist();

            var syntheticReport = new List<TeacherAverageReport>
            {
                new TeacherAverageReport { SessionId = 1, Id = 1, Name = "Christopher Nolan", Average = 7.17},
                new TeacherAverageReport { SessionId = 1, Id = 2, Name = "Steven Spielberg", Average = 8.17},
                new TeacherAverageReport { SessionId = 1, Id = 3, Name = "Quentin Tarantino", Average = 8},
                new TeacherAverageReport { SessionId = 1, Id = 4, Name = "Martin Scorsese", Average = 8},
                new TeacherAverageReport { SessionId = 1, Id = 5, Name = "David Fincher", Average = 8.33},
                new TeacherAverageReport { SessionId = 1, Id = 6, Name = "Stanley Kubrick", Average = 5},
                new TeacherAverageReport { SessionId = 1, Id = 7, Name = "Robert Zemeckis", Average = 6.17},
            };

            var generator = new ReportGenerator();
            // Act
            var result = generator.TeacherAverageReportGenerate().FirstOrDefault();
            // Assert
            Assert.IsTrue(result != null);
            int i = 0;
            foreach(var report in result)
            {
                Assert.AreEqual(report.SessionId, syntheticReport[i].SessionId);
                Assert.AreEqual(report.Id, syntheticReport[i].Id);
                Assert.AreEqual(report.Name, syntheticReport[i].Name);
                Assert.AreEqual(Math.Round(report.Average, 2), syntheticReport[i++].Average);
            }

        }

        /// <summary>
        /// Test Method TeacherAverageReportGenerate when sorting is used. The method should return correct report from database
        /// </summary>
        [TestMethod]
        public void TeacherAverageReportGenerate_WhenSortingIsUsed_ShouldReturnCorrectReport()
        {
            // Arrange
            ScriptRunner.DeleteSchoolDbIfExist();
            ScriptRunner.CreateSchoolDbIfNotExist();

            var syntheticReport = new List<TeacherAverageReport>
            {
                new TeacherAverageReport { SessionId = 1, Id = 1, Name = "Christopher Nolan", Average = 7.17},
                new TeacherAverageReport { SessionId = 1, Id = 5, Name = "David Fincher", Average = 8.33},
                new TeacherAverageReport { SessionId = 1, Id = 4, Name = "Martin Scorsese", Average = 8},
                new TeacherAverageReport { SessionId = 1, Id = 3, Name = "Quentin Tarantino", Average = 8},
                new TeacherAverageReport { SessionId = 1, Id = 7, Name = "Robert Zemeckis", Average = 6.17},                
                new TeacherAverageReport { SessionId = 1, Id = 6, Name = "Stanley Kubrick", Average = 5},
                new TeacherAverageReport { SessionId = 1, Id = 2, Name = "Steven Spielberg", Average = 8.17}
            };

            var generator = new ReportGenerator();
            // Act
            var result = generator.TeacherAverageReportGenerate(r => r.Name).FirstOrDefault();
            // Assert
            Assert.IsTrue(result != null);
            int i = 0;
            foreach (var report in result)
            {
                Assert.AreEqual(report.SessionId, syntheticReport[i].SessionId);
                Assert.AreEqual(report.Id, syntheticReport[i].Id);
                Assert.AreEqual(report.Name, syntheticReport[i].Name);
                Assert.AreEqual(Math.Round(report.Average, 2), syntheticReport[i++].Average);
            }
        }
        /// <summary>
        /// Test Method AverageDynamicReportGenerate. The method should return correct report from database
        /// </summary>
        [TestMethod]
        public void AverageDynamicReportGenerate_ShouldReturnCorrectReport()
        {
            // Arrange
            ScriptRunner.DeleteSchoolDbIfExist();
            ScriptRunner.CreateSchoolDbIfNotExist();

            var syntheticReport = new List<AverageDynamicReport>
            {
                new AverageDynamicReport { Year = 2015, SubjectName = "Maths", Average = 8},
                new AverageDynamicReport { Year = 2015, SubjectName = "Physics", Average = 7.67},
                new AverageDynamicReport { Year = 2015, SubjectName = "English", Average = 6.78},
                new AverageDynamicReport { Year = 2015, SubjectName = "History", Average = 6.56},
                new AverageDynamicReport { Year = 2015, SubjectName = "Sociology", Average = 9}
            };

            var generator = new ReportGenerator();
            // Act
            var result = generator.AverageDynamicReportGenerate().FirstOrDefault();
            // Assert
            Assert.IsTrue(result != null);
            int i = 0;
            foreach (var report in result)
            {
                Assert.AreEqual(report.Year, syntheticReport[i].Year);                
                Assert.AreEqual(report.SubjectName, syntheticReport[i].SubjectName);
                Assert.AreEqual(Math.Round(report.Average, 2), syntheticReport[i++].Average);
            }
        }

        /// <summary>
        /// Test Method AverageDynamicReportGenerate. The method should return correct report from database
        /// </summary>
        [TestMethod]
        public void AverageDynamicReportGenerate_WhenSortingIsUsed_ShouldReturnCorrectReport()
        {
            // Arrange
            ScriptRunner.DeleteSchoolDbIfExist();
            ScriptRunner.CreateSchoolDbIfNotExist();

            var syntheticReport = new List<AverageDynamicReport>
            {
                new AverageDynamicReport { Year = 2015, SubjectName = "History", Average = 6.56},
                new AverageDynamicReport { Year = 2015, SubjectName = "English", Average = 6.78},
                new AverageDynamicReport { Year = 2015, SubjectName = "Physics", Average = 7.67},
                new AverageDynamicReport { Year = 2015, SubjectName = "Maths", Average = 8},
                new AverageDynamicReport { Year = 2015, SubjectName = "Sociology", Average = 9}
            };

            var generator = new ReportGenerator();
            // Act
            var result = generator.AverageDynamicReportGenerate(r => r.Average).FirstOrDefault();
            // Assert
            Assert.IsTrue(result != null);
            int i = 0;
            foreach (var report in result)
            {
                Assert.AreEqual(report.Year, syntheticReport[i].Year);
                Assert.AreEqual(report.SubjectName, syntheticReport[i].SubjectName);
                Assert.AreEqual(Math.Round(report.Average, 2), syntheticReport[i++].Average);
            }
        }
    }
}

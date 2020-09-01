using Microsoft.VisualStudio.TestTools.UnitTesting;
using School;
using School.Reports;
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
        /// Test method LoserReportGenerate. The method shoul get correct report from database
        /// </summary>
        [TestMethod]
        public void LoserReportGenerate_ShouldGetCorrectReportFromDatabase()
        {
            // Arrange
            ScriptRunner.DeleteSchoolDbIfExist();
            ScriptRunner.CreateSchoolDbIfNotExist();

            var syntheticReport = new List<List<LosersReport>>();
            var group1 = new List<LosersReport>
            {
                new LosersReport { GroupId = 1, Id = 2, Name = "Charlize Theron"},
                new LosersReport { GroupId = 1, Id = 3, Name = "Kirsten Dunst"}
            };
            var group2 = new List<LosersReport>
            {
                new LosersReport { GroupId = 2, Id = 4, Name = "Angelina Jolie"}                
            };
            var group3 = new List<LosersReport>
            {
                new LosersReport { GroupId = 3, Id = 8, Name = "Michael Fassbender"},
                new LosersReport { GroupId = 3, Id = 9, Name = "Daniel Craig"}
            };
            var group4 = new List<LosersReport>
            {
                new LosersReport { GroupId = 4, Id = 11, Name = "Vera Farmiga"},
                new LosersReport { GroupId = 4, Id = 12, Name = "Antony Starr"}
            };
            syntheticReport.Add(group1);
            syntheticReport.Add(group2);
            syntheticReport.Add(group3);
            syntheticReport.Add(group4);

            var reportGenerator = new ReportGenerator();
            // Act
            var groups = reportGenerator.LosersReportGenerate();
            //Assert
            int i = 0;
            int j = 0;
            foreach(var group in groups)
            {
                foreach(var report in group)
                {
                    Assert.AreEqual(report.GroupId, syntheticReport[i][j].GroupId);
                    Assert.AreEqual(report.Id, syntheticReport[i][j].Id);
                    Assert.AreEqual(report.Name, syntheticReport[i][j].Name);
                    j++;
                }
                i++;
                j = 0;
            }

        }

        /// <summary>
        /// Test method LoserReportGenerate when sorting is used. The method shoul get correct report from database
        /// </summary>
        [TestMethod]
        public void LoserReportGenerate_ЦhenЫortingIsUsed_ShouldGetCorrectReportFromDatabase()
        {
            // Arrange
            ScriptRunner.DeleteSchoolDbIfExist();
            ScriptRunner.CreateSchoolDbIfNotExist();

            var syntheticReport = new List<List<LosersReport>>();
            var group1 = new List<LosersReport>
            {
                new LosersReport { GroupId = 1, Id = 2, Name = "Charlize Theron"},
                new LosersReport { GroupId = 1, Id = 3, Name = "Kirsten Dunst"}
            };
            var group2 = new List<LosersReport>
            {
                new LosersReport { GroupId = 2, Id = 4, Name = "Angelina Jolie"}
            };
            var group3 = new List<LosersReport>
            {
                new LosersReport { GroupId = 3, Id = 9, Name = "Daniel Craig"},
                new LosersReport { GroupId = 3, Id = 8, Name = "Michael Fassbender"}                
            };
            var group4 = new List<LosersReport>
            {
                new LosersReport { GroupId = 4, Id = 12, Name = "Antony Starr"},
                new LosersReport { GroupId = 4, Id = 11, Name = "Vera Farmiga"}                
            };
            syntheticReport.Add(group1);
            syntheticReport.Add(group2);
            syntheticReport.Add(group3);
            syntheticReport.Add(group4);

            var reportGenerator = new ReportGenerator();
            // Act
            var groups = reportGenerator.LosersReportGenerate(l => l.Name);
            //Assert
            int i = 0;
            int j = 0;
            foreach (var group in groups)
            {
                foreach (var report in group)
                {
                    Assert.AreEqual(report.GroupId, syntheticReport[i][j].GroupId);
                    Assert.AreEqual(report.Id, syntheticReport[i][j].Id);
                    Assert.AreEqual(report.Name, syntheticReport[i][j].Name);
                    j++;
                }
                i++;
                j = 0;
            }

        }
        /// <summary>
        /// Test method SessionResultGenerate. The method shoul get correct report from database
        /// </summary>
        [TestMethod]
        public void SessionResultGenerate__ShouldGetCorrectReportFromDatabase()
        {
            // Arrange
            ScriptRunner.DeleteSchoolDbIfExist();
            ScriptRunner.CreateSchoolDbIfNotExist();

            var syntheticReport = new List<List<GroupResultReport>>();
            var session1 = new List<GroupResultReport>
            {
                new GroupResultReport { SessionId = 1, GroupId = 1, Min = 4, Max = 10, Average = 7.55555555555556},
                new GroupResultReport { SessionId = 1, GroupId = 2, Min = 7, Max = 9, Average = 8.11111111111111},
                new GroupResultReport { SessionId = 1, GroupId = 3, Min = 2, Max = 6, Average = 4.66666666666667},
                new GroupResultReport { SessionId = 1, GroupId = 4, Min = 6, Max = 10, Average = 8}

            };
            var session2 = new List<GroupResultReport>
            {
                new GroupResultReport { SessionId = 2, GroupId = 1, Min = 3, Max = 9, Average = 7},
                new GroupResultReport { SessionId = 2, GroupId = 2, Min = 4, Max = 9, Average = 6.16666666666667},
                new GroupResultReport { SessionId = 2, GroupId = 3, Min = 4, Max = 9, Average = 7.44444444444444},
                new GroupResultReport { SessionId = 2, GroupId = 4, Min = 7, Max = 10, Average = 8.22222222222222}
            };
            var session3 = new List<GroupResultReport>
            {
                new GroupResultReport { SessionId = 3, GroupId = 1, Min = 5, Max = 9, Average = 7.33333333333333},
                new GroupResultReport { SessionId = 3, GroupId = 2, Min = 4, Max = 9, Average = 6.16666666666667},
                new GroupResultReport { SessionId = 3, GroupId = 3, Min = 6, Max = 9, Average = 7.5},
                new GroupResultReport { SessionId = 3, GroupId = 4, Min = 7, Max = 9, Average = 7.83333333333333}
            };

            syntheticReport.Add(session1);
            syntheticReport.Add(session2);
            syntheticReport.Add(session3);

            var reportGenerator = new ReportGenerator();
            // Act
            var groups = reportGenerator.SessinResultGenerate();
            // Assert
            int i = 0;
            int j = 0;
            foreach (var group in groups)
            {
                foreach (var report in group)
                {
                    Assert.AreEqual(report.SessionId, syntheticReport[i][j].SessionId);
                    Assert.AreEqual(report.GroupId, syntheticReport[i][j].GroupId);
                    Assert.AreEqual(report.Min, syntheticReport[i][j].Min);
                    Assert.AreEqual(report.Max, syntheticReport[i][j].Max);
                    Assert.AreEqual(Math.Round(report.Average, 14), syntheticReport[i][j].Average);                    
                    j++;
                }
                i++;
                j = 0;
            }

        }

        /// <summary>
        /// Test method SessionResultGenerate when sorting is used. The method shoul get correct report from database
        /// </summary>
        [TestMethod]
        public void SessionResultGenerate_WhenSortingIsUsed_ShouldGetCorrectReportFromDatabase()
        {
            // Arrange
            ScriptRunner.DeleteSchoolDbIfExist();
            ScriptRunner.CreateSchoolDbIfNotExist();

            var syntheticReport = new List<List<GroupResultReport>>();
            var session1 = new List<GroupResultReport>
            {
                new GroupResultReport { SessionId = 1, GroupId = 3, Min = 2, Max = 6, Average = 4.66666666666667},
                new GroupResultReport { SessionId = 1, GroupId = 1, Min = 4, Max = 10, Average = 7.55555555555556},
                new GroupResultReport { SessionId = 1, GroupId = 4, Min = 6, Max = 10, Average = 8},
                new GroupResultReport { SessionId = 1, GroupId = 2, Min = 7, Max = 9, Average = 8.11111111111111} 
            };
            var session2 = new List<GroupResultReport>
            {
                new GroupResultReport { SessionId = 2, GroupId = 1, Min = 3, Max = 9, Average = 7},
                new GroupResultReport { SessionId = 2, GroupId = 2, Min = 4, Max = 9, Average = 6.16666666666667},
                new GroupResultReport { SessionId = 2, GroupId = 3, Min = 4, Max = 9, Average = 7.44444444444444},
                new GroupResultReport { SessionId = 2, GroupId = 4, Min = 7, Max = 10, Average = 8.22222222222222}
            };
            var session3 = new List<GroupResultReport>
            {
                new GroupResultReport { SessionId = 3, GroupId = 2, Min = 4, Max = 9, Average = 6.16666666666667},
                new GroupResultReport { SessionId = 3, GroupId = 1, Min = 5, Max = 9, Average = 7.33333333333333},                
                new GroupResultReport { SessionId = 3, GroupId = 3, Min = 6, Max = 9, Average = 7.5},
                new GroupResultReport { SessionId = 3, GroupId = 4, Min = 7, Max = 9, Average = 7.83333333333333}
            };

            syntheticReport.Add(session1);
            syntheticReport.Add(session2);
            syntheticReport.Add(session3);

            var reportGenerator = new ReportGenerator();
            // Act
            var groups = reportGenerator.SessinResultGenerate(r => r.Min);
            // Assert
            int i = 0;
            int j = 0;
            foreach (var group in groups)
            {
                foreach (var report in group)
                {
                    Assert.AreEqual(report.SessionId, syntheticReport[i][j].SessionId);
                    Assert.AreEqual(report.GroupId, syntheticReport[i][j].GroupId);
                    Assert.AreEqual(report.Min, syntheticReport[i][j].Min);
                    Assert.AreEqual(report.Max, syntheticReport[i][j].Max);
                    Assert.AreEqual(Math.Round(report.Average, 14), syntheticReport[i][j].Average);
                    j++;
                }
                i++;
                j = 0;
            }

        }

    }
}

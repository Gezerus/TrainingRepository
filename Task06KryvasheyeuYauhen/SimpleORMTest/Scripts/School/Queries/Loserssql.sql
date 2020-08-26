SELECT Students.Id, Students.Name, Groups.Name
FROM StudentExam, Groups, Students, StudentCredit 
WHERE StudentExam.StudentId = Students.Id AND Students.GroupId = Groups.Id 
AND StudentCredit.StudentId = Students.Id AND StudentCredit.StudentId = StudentExam.StudentId
AND (StudentCredit.Result = 0 OR StudentExam.Grade < 4)
GROUP BY Students.Id, Students.Name, Groups.Name


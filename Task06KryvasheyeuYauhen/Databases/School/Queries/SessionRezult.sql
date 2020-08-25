SELECT Groups.Name, MIN(Grade) AS Min, AVG(CAST(Grade AS FLOAT)) AS Avg, MAX(Grade) AS Max
FROM StudentsExams, Groups, Sessions, Exams, Students
WHERE StudentsExams.ExamId = Exams.Id AND StudentsExams.StudentId = Students.Id AND Exams.SessionId = Sessions.Id
AND Students.GroupId = Groups.Id AND Sessions.Id = 1
GROUP BY Groups.Name

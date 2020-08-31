USE School

--SELECT Groups.Name, MIN(Grade) AS Min, AVG(CAST(Grade AS FLOAT)) AS Avg, MAX(Grade) AS Max
--FROM StudentsExams, Groups, Sessions, Exams, Students
--WHERE StudentsExams.ExamId = Exams.Id AND StudentsExams.StudentId = Students.Id AND Exams.SessionId = Sessions.Id
--AND Students.GroupId = Groups.Id AND Sessions.Id = 1
--GROUP BY Groups.Name


SELECT Groups.Name, MIN(Grade) AS Min, AVG(CAST(Grade AS FLOAT)) AS Avg, MAX(Grade) AS Max
FROM StudentsExams
JOIN Exams ON Exams.Id = StudentsExams.ExamId
JOIN Students ON Students.Id = StudentsExams.StudentId
JOIN Groups ON Groups.Id = Students.GroupId
JOIN Sessions ON Sessions.Id = Exams.SessionId 
WHERE Sessions.Id = 2
GROUP BY Groups.Name
USE School

SELECT Groups.Specialty, AVG(CAST(Grade AS FLOAT))
FROM Groups
JOIN Students ON Groups.Id = Students.GroupId
JOIN StudentsExams ON StudentsExams.StudentId = Students.Id
JOIN Exams ON Exams.Id = StudentsExams.ExamId
JOIN Sessions ON Sessions.Id = Exams.SessionId
WHERE Sessions.Id = 3
GROUP BY Groups.Specialty
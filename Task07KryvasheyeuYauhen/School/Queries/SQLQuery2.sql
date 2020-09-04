USE School

SELECT Teachers.Id, Teachers.Name, AVG(CAST(Grade AS FLOAT))
FROM Teachers
JOIN Exams ON Exams.TeacherId = Teachers.Id
JOIN StudentsExams ON StudentsExams.ExamId = Exams.Id
JOIN Sessions ON Sessions.Id = Exams.SessionId
WHERE Sessions.Id = 1
GROUP BY Teachers.Id, Teachers.Name
ORDER BY Teachers.Name
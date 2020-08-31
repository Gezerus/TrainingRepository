SELECT Students.Id, Students.Name
FROM StudentsExams, Groups, Students, StudentsCredits 
WHERE StudentsExams.StudentId = Students.Id AND Students.GroupId = Groups.Id 
AND StudentsCredits.StudentId = Students.Id AND StudentsCredits.StudentId = StudentsExams.StudentId
AND (StudentsCredits.Result = 0 OR StudentsExams.Grade < 4)
GROUP BY Students.Id, Students.Name


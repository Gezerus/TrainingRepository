CREATE DATABASE School;
GO

USE School;

CREATE TABLE Groups
(
Id INT NOT NULL PRIMARY KEY IDENTITY,
Name NVARCHAR(30) NOT NULL
)

GO

CREATE TABLE Students
(
Id INT NOT NULL PRIMARY KEY IDENTITY,
Name NVARCHAR(30) NOT NULL,
Birthday DATE NOT NULL,
Gender INT NOT NULL,
GroupId INT NOT NULL REFERENCES Groups(Id)
)

CREATE TABLE Sessions
(
Id INT NOT NULL PRIMARY KEY IDENTITY,
Name NVARCHAR(30) NOT NULL,
StartDate DATE NOT NULL
)

CREATE TABLE Subjects
(
Id INT NOT NULL PRIMARY KEY IDENTITY,
Name NVARCHAR(50) NOT NULL UNIQUE
)

GO

CREATE TABLE Exams
(
Id INT NOT NULL PRIMARY KEY IDENTITY,
SubjectId INT NOT NULL REFERENCES Subjects(Id),
SessionId INT NOT NULL REFERENCES Sessions(Id),
Date DATE NOT NULL
)

GO

CREATE TABLE StudentsExams
(
Id INT NOT NULL PRIMARY KEY IDENTITY,
ExamId INT NOT NULL REFERENCES Exams(Id),
StudentId INT NOT NULL REFERENCES Students(Id),
Grade INT NOT NULL
)

CREATE TABLE Credits
(
Id INT NOT NULL PRIMARY KEY IDENTITY,
SubjectId INT NOT NULL REFERENCES Subjects(Id),
SessionId INT NOT NULL REFERENCES Sessions(Id),
Date DATE NOT NULL
)

GO

CREATE TABLE StudentsCredits
(
Id INT NOT NULL PRIMARY KEY IDENTITY,
CreditId INT NOT NULL REFERENCES Credits(Id),
StudentId INT NOT NULL REFERENCES Students(Id),
Result BIT NOT NULL
)

INSERT Groups 
VALUES
('TM'), ('PT'), ('PE'), ('MT')

GO

INSERT INTO Students
VALUES
('Brad Pitt', '1963-02-18', 1, 1),
('Charlize Theron', '1975-08-07', 0, 1),
('Kirsten Dunst', '1982-04-30', 0, 1),
('Angelina Jolie', '1975-07-04', 0, 2),
('Christian Bale', '1974-01-30', 1, 2),
('Nicole Kidman', '1967-07-20', 0, 2),
('Ryan Gosling', '1980-11-12', 1, 3),
('Michael Fassbender', '1977-04-02', 1, 3),
('Daniel Craig', '1968-03-02', 1, 3),
('Hugh Jackman', '1968-10-12', 1, 4),
('Vera Farmiga', '1973-08-06', 0, 4),
('Antony Starr', '1975-10-25', 1, 4)

INSERT INTO Subjects
VALUES
('Maths'),
('Physics'),
('English'),
('History'),
('Sociology'),
('Philosophy'),
('Electrical engineering')

INSERT INTO Sessions
VALUES
('Summer session', '2015-06-01'),
('Winter session', '2016-01-03'),
('Summer session', '2016-06-01')

GO

INSERT INTO Credits
VALUES
--Session 1
--Group 1 
(7, 1, '2015-06-15'), (6, 1, '2015-06-20'),
--Group 2
(6, 1, '2015-06-15'), (5, 1, '2015-06-20'),
--Group 3
(5, 1, '2015-06-10'), (3, 1, '2015-06-15'),
--Group 4
(3, 1, '2015-06-10'), (2, 1, '2015-06-15'),
--Session 2
--Group 1 
(1, 2, '2016-01-13'), (2, 2, '2016-01-18'),
--Group 2
(2, 2, '2016-01-13'), (3, 2, '2016-01-18'),
--Group 3
(3, 2, '2016-01-18'), (4, 2, '2016-01-23'),
--Group 4
(5, 2, '2016-01-18'), (6, 2, '2016-01-23'),
--Session 3
--Group 1
(3, 3, '2016-06-10'), (4, 3, '2016-06-15'),
--Group 2
(4, 3, '2016-06-10'), (5, 3, '2016-06-15'),
--Group 3
(5, 3, '2016-06-10'), (6, 3, '2016-06-15'),
--Group 4
(2, 3, '2016-06-10'), (3, 3, '2016-06-15')

GO

INSERT INTO StudentsCredits
VALUES
--Session 1
--Group 1
 (1, 1, 1),    (1, 2, 1),   (1, 3, 1),
 (2, 1, 1),    (2, 2, 1),  (2, 3, 1),
--Group 2
 (3, 4, 0),    (3, 5, 1),   (3, 6, 1),
 (4, 4, 1),    (4, 5, 1),   (4, 6, 1),
--Group 3
 (5, 7, 1),    (5, 8, 1),   (5, 9, 1),
 (6, 7, 1),    (6, 8, 1),   (6, 9, 1),
--Group 4
 (7, 10, 1),   (7, 11, 1),  (7, 12, 1),
 (8, 10, 1),  (8, 11, 1), (8, 12, 1),
--Session 2
--Group 1
 (9, 1, 1),   (9, 2, 1),  (9, 3, 1),
 (10, 1, 1),   (10, 2, 1),  (10, 3, 1),
--Group 2
 (11, 4, 1),   (11, 5, 1),  (11, 6, 1),
 (12, 4, 1),   (12, 5, 1),  (12, 6, 1),
--Group 3
 (13, 7, 1),   (13, 8, 1),  (13, 9, 1),
 (14, 7, 1),   (14, 8, 1),  (14, 9, 1),
--Group 4
 (15, 10, 1),  (15, 11, 1), (15, 12, 1),
 (16, 10, 1),  (16, 11, 1), (16, 12, 1),
 --Session 3
--Group 1
 (17, 1, 1),   (17, 2, 1),  (17, 3, 1),
 (18, 1, 1),   (18, 2, 1),  (18, 3, 1),
--Group 2
 (19, 4, 1),   (19, 5, 1),  (19, 6, 1),
 (20, 4, 1),   (20, 5, 1),  (20, 6, 1),
--Group 3
 (21, 7, 1),   (21, 8, 1),  (21, 9, 1),
 (22, 7, 1),   (22, 8, 1),  (22, 9, 1),
--Group 4
 (23, 10, 1),  (23, 11, 1), (23, 12, 1),
 (24, 10, 1),  (24, 11, 1), (24, 12, 0) 

INSERT INTO Exams
VALUES
--Session 1
--Group 1 
(1, 1, '2015-06-01'), (2, 1, '2015-06-05'), (3, 1, '2015-06-10'),
--Group 2
(2, 1, '2015-06-01'), (3, 1, '2015-06-05'), (4, 1, '2015-06-10'),
--Group 3
(3, 1, '2015-06-01'), (4, 1, '2015-06-05'),
--Group 4
(4, 1, '2015-06-01'), (5, 1, '2015-06-05'),
--Session 2
--Group 1 
(7, 2, '2016-01-03'), (6, 2, '2016-01-08'),
--Group 2
(6, 2, '2016-01-03'), (5, 2, '2016-01-08'),
--Group 3
(5, 2, '2016-01-03'), (4, 2, '2016-01-08'), (3, 2, '2016-01-13'),
--Group 4
(4, 2, '2016-01-03'), (3, 2, '2016-01-08'), (2, 2, '2016-01-13'),
--Session 3
--Group 1
(1, 3, '2016-06-01'), (2, 3, '2016-06-01'),
--Group 2
(2, 3, '2016-06-01'), (3, 3, '2016-06-01'),
--Group 3
(3, 3, '2016-06-01'), (4, 3, '2016-06-01'),
--Group 4
(4, 3, '2016-06-01'), (5, 3, '2016-06-01')

GO

INSERT INTO StudentsExams
VALUES
--Session 1
--Group 1
 (1, 1, 9),    (1, 2, 7),   (1, 3, 8),
 (2, 1, 8),    (2, 2, 10),  (2, 3, 4),
 (3, 1, 9),    (3, 2, 9),   (3, 3, 4),
--Group 2
 (4, 4, 7),    (4, 5, 8),   (4, 6, 9),
 (5, 4, 8),    (5, 5, 9),   (5, 6, 7),
 (6, 4, 9),    (6, 5, 9),   (6, 6, 7),
--Group 3
 (7, 7, 4),    (7, 8, 5),   (7, 9, 6),
 (8, 7, 5),    (8, 8, 6),   (8, 9, 2),
--Group 4
 (9, 10, 6),   (9, 11, 7),  (9, 12, 8),
(10, 10, 8),  (10, 11, 9), (10, 12, 10),
--Session 2
--Group 1
 (11, 1, 8),   (11, 2, 7),  (11, 3, 3),
 (12, 1, 7),   (12, 2, 8),  (12, 3, 9),
--Group 2
 (13, 4, 4),   (13, 5, 5),  (13, 6, 8),
 (14, 4, 5),   (14, 5, 9),  (14, 6, 6),
--Group 3
 (15, 7, 9),   (15, 8, 7),  (15, 9, 8),
 (16, 7, 8),   (16, 8, 6),  (16, 9, 7),
 (17, 7, 9),   (17, 8, 9),  (17, 9, 4),
--Group 4
 (18, 10, 7),  (18, 11, 7), (18, 12, 9),
 (19, 10, 8),  (19, 11, 9), (19, 12, 7),
 (20, 10, 10), (20, 11, 9), (20, 12, 8),
 --Session 3
--Group 1
 (21, 1, 8),   (21, 2, 7),  (21, 3, 5),
 (22, 1, 7),   (22, 2, 8),  (22, 3, 9),
--Group 2
 (23, 4, 4),   (23, 5, 5),  (23, 6, 8),
 (24, 4, 5),   (24, 5, 9),  (24, 6, 6),
--Group 3
 (25, 7, 9),   (25, 8, 7),  (25, 9, 8),
 (26, 7, 8),   (26, 8, 6),  (26, 9, 7),
--Group 4
 (27, 10, 7),  (27, 11, 7), (27, 12, 9),
 (28, 10, 8),  (28, 11, 9), (28, 12, 7) 
USE [master]
GO
CREATE DATABASE [PeJavaExamMarks]
GO
USE [PeJavaExamMarks]
GO
CREATE TABLE Accounts(
	AccountId			INT					IDENTITY(1,1)			PRIMARY KEY,
	UserName			NVARCHAR(100)		NOT NULL,
	[Password]			NVARCHAR(100)		NOT NULL,
)

GO
CREATE TABLE ClassAccounts(
	AccountId			INT				NOT NULL,
	ClassId				INT				NOT NULL
)

GO
ALTER TABLE ClassAccounts
ADD CONSTRAINT PK_Class PRIMARY KEY (AccountId,ClassId);


GO
CREATE TABLE Classes(
	ClassId					INT					IDENTITY(1,1)				PRIMARY KEY,
	ClassName				NVARCHAR(20)		NOT NULL,
	ClassDescription		NVARCHAR(100)
)

GO 
CREATE TABLE Students(
	StudentID				INT					IDENTITY					PRIMARY KEY,
	RollNumber				NVARCHAR(50)		NOT NULL,
	StudentName				NVARCHAR(100)		NOT NULL,
)

GO 
CREATE TABLE ScoreStudents(
	ClassId					INT					NOT NULL,
	StudentId				INT					NOT NULL,
	ExamCode				VARCHAR(10)			NOT NULL,
	ScoreDetails			NTEXT						,
	TotalScore				FLOAT,
	DateMark				DATETIME,
)

GO
ALTER TABLE ScoreStudents
ADD CONSTRAINT PK_ScoreStudent PRIMARY KEY(ClassId,StudentId)

GO
ALTER TABLE ClassAccounts
ADD CONSTRAINT FK_ClassAccounts_Accounts
FOREIGN KEY(AccountId) REFERENCES Accounts(AccountId)

GO
ALTER TABLE ClassAccounts
ADD CONSTRAINT FK_ClassAccounts_Classes
FOREIGN KEY(ClassId) REFERENCES Classes(ClassId)


GO
ALTER TABLE ScoreStudents
ADD CONSTRAINT FK_ScoreStudents_Students
FOREIGN KEY(StudentId) REFERENCES Students(StudentId)

GO
ALTER TABLE ScoreStudents
ADD CONSTRAINT FK_ScoreStudents_Classes
FOREIGN KEY(ClassId) REFERENCES Classes(ClassId)
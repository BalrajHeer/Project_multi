
IF db_id('EmpProj3') IS NULL CREATE DATABASE EmpProj3 ;
GO

-- if object_id('object_name', 'U') is null -- for table

USE EmpProj3

CREATE TABLE Employees(
	EmpId varchar(5) NOT NULL,
	EmpName varchar(50) NOT NULL,
	Salary decimal(9,2), 
 CONSTRAINT PK_Employees PRIMARY KEY (EmpId) )

CREATE TABLE Projects(
	ProjId varchar(4) NOT NULL,
	ProjName varchar(50) NOT NULL,
	Duration int , 
 CONSTRAINT PK_Projects PRIMARY KEY (ProjId) )

CREATE TABLE Assignments(
	EmpId varchar(5) NOT NULL,
	ProjId varchar(4) NOT NULL,
	Eval int,
 CONSTRAINT PK_Assignments PRIMARY KEY (EmpId, ProjId), 
 CONSTRAINT FK_Assignments_Employees 
   FOREIGN KEY(EmpId) REFERENCES Employees (EmpId)
   ON DELETE CASCADE
   ON UPDATE CASCADE,
 CONSTRAINT FK_Assignments_Projects 
   FOREIGN KEY(ProjId) REFERENCES Projects (ProjId) 
   ON DELETE NO ACTION
   ON UPDATE CASCADE)
GO

/***********************************************************************/
INSERT Projects (ProjId,  ProjName, Duration) VALUES ('P100', 'Project 1', 12);
INSERT Projects (ProjId,  ProjName, Duration) VALUES ('P200', 'Project 2', 18);
INSERT Projects (ProjId,  ProjName, Duration) VALUES ('P300', 'Project 3', 24);

/***********************************************************************/
INSERT Employees (EmpId, EmpName, Salary) VALUES ('E0001', 'John', 70000.0);
INSERT Employees (EmpId, EmpName, Salary) VALUES ('E0002', 'Mary', 75000.0);
INSERT Employees (EmpId, EmpName, Salary) VALUES ('E0003', 'Paul', 80000.0);
INSERT Employees (EmpId, EmpName, Salary) VALUES ('E0004', 'Bob', 72000.0);
GO
/***********************************************************************/
INSERT Assignments (EmpId, ProjId) VALUES ('E0001', 'P100');
INSERT Assignments (EmpId, ProjId, Eval) VALUES ('E0002', 'P100', 90);
INSERT Assignments (EmpId, ProjId) VALUES ('E0002', 'P200');
INSERT Assignments (EmpId, ProjId, Eval) VALUES ('E0003', 'P200', 95);
INSERT Assignments (EmpId, ProjId) VALUES ('E0003', 'P300');
INSERT Assignments (EmpId, ProjId) VALUES ('E0004', 'P300');
GO
/************************************************************************/


CREATE DATABASE College1en;
GO


USE College1en;
GO


CREATE TABLE Programs (
    ProgId VARCHAR(5) NOT NULL,
    ProgName VARCHAR(50) NOT NULL,
    PRIMARY KEY (ProgId)
);
GO


CREATE TABLE Courses (
    CId VARCHAR(7) NOT NULL,
    CName VARCHAR(50) NOT NULL,
    ProgId VARCHAR(5) NOT NULL,
    CONSTRAINT PK_Courses PRIMARY KEY (CId)
);
GO


CREATE TABLE Students (
    StId VARCHAR(10) NOT NULL,
    StName VARCHAR(50) NOT NULL,
    ProgId VARCHAR(5) NOT NULL,
    CONSTRAINT PK_Students PRIMARY KEY (StId)
);
GO


CREATE TABLE Enrollments (
    StId VARCHAR(10) NOT NULL,
    CId VARCHAR(7) NOT NULL,
    FinalGrade INT,
    CONSTRAINT PK_Enrollments PRIMARY KEY (CId, StId), 
 CONSTRAINT FK_Enrollments_Courses
   FOREIGN KEY(CId) REFERENCES Courses (CId)
   ON DELETE CASCADE
   ON UPDATE CASCADE,
 CONSTRAINT FK_Enrollments_Students
   FOREIGN KEY(StId) REFERENCES Students (StId) 
   ON DELETE NO ACTION
   ON UPDATE CASCADE
);
GO

-- Insert data into Programs
INSERT INTO Programs (ProgId, ProgName) VALUES 
('P0001', 'Computer Science'),
('P0002', 'Business Administration'),
('P0003', 'Mechanical Engineering'),
('P0004', 'Psychology');
GO
-- Insert data into Courses
INSERT INTO Courses (CId, CName, ProgId) VALUES 
('C000001', 'Introduction to Programming', 'P0001'),
('C000002', 'Data Structures', 'P0001'),
('C000003', 'Database Management', 'P0001'),
('C000004', 'Principles of Marketing', 'P0002'),
('C000005', 'Financial Accounting', 'P0002'),
('C000006', 'Thermodynamics', 'P0003'),
('C000007', 'Fluid Mechanics', 'P0003'),
('C000008', 'Abnormal Psychology', 'P0004'),
('C000009', 'Cognitive Psychology', 'P0004');
GO
-- Insert data into Students
INSERT INTO Students (StId, StName, ProgId) VALUES 
('S000000001', 'Alice Johnson', 'P0001'),
('S000000002', 'Bob Smith', 'P0001'),
('S000000003', 'Charlie Brown', 'P0002'),
('S000000004', 'Diana Prince', 'P0003'),
('S000000005', 'Ethan Hunt', 'P0004');
GO
-- Insert data into Enrollments
INSERT INTO Enrollments (StId, CId, FinalGrade) VALUES 
('S000000001', 'C000001', 85),
('S000000001', 'C000002', 90),
('S000000002', 'C000001', 78),
('S000000003', 'C000004', 88),
('S000000003', 'C000005', 92),
('S000000004', 'C000006', 75),
('S000000004', 'C000007', 80),
('S000000005', 'C000008', 95),
('S000000005', 'C000009', 87);
GO

PRINT 'Database College1en and its tables have been created successfully.';
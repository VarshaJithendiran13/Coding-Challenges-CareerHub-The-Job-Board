Create database CareerHub
USE CareerHub
/* 1. Provide a SQL script that initializes the database for the Job Board scenario “CareerHub”. 
   2. Create tables for Companies, Jobs, Applicants and Applications. 
   3. Define appropriate primary keys, foreign keys, and constraints. 
   4. Ensure the script handles potential errors, such as if the database or tables already exist. */

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Companies]') AND type in (N'U'))
BEGIN
    CREATE TABLE Companies (
        CompanyID INT PRIMARY KEY NOT NULL,
        CompanyName VARCHAR(255) NOT NULL,
        Location VARCHAR(255)
    );
END;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Jobs]') AND type in (N'U'))
BEGIN
    CREATE TABLE Jobs (
        JobID INT PRIMARY KEY NOT NULL,
        CompanyID INT NOT NULL,
        JobTitle VARCHAR(255) NOT NULL,
        JobDescription TEXT,
        JobLocation VARCHAR(255),
        Salary DECIMAL(10, 2),
        JobType VARCHAR(50),
        PostedDate DATETIME,
        FOREIGN KEY (CompanyID) REFERENCES Companies(CompanyID) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Applicants]') AND type in (N'U'))
BEGIN
    CREATE TABLE Applicants (
        ApplicantID INT PRIMARY KEY NOT NULL,
        FirstName VARCHAR(100) NOT NULL,
        LastName VARCHAR(100) NOT NULL,
        Email VARCHAR(255) NOT NULL,
        Phone VARCHAR(20),
        Resume TEXT
    );
END;

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Applications]') AND type in (N'U'))
BEGIN
    CREATE TABLE Applications (
        ApplicationID INT PRIMARY KEY NOT NULL,
        JobID INT NOT NULL,
        ApplicantID INT NOT NULL,
        ApplicationDate DATETIME NOT NULL,
        CoverLetter TEXT,
        FOREIGN KEY (JobID) REFERENCES Jobs(JobID) ON DELETE CASCADE,
        FOREIGN KEY (ApplicantID) REFERENCES Applicants(ApplicantID) ON DELETE CASCADE
    );
END;

INSERT INTO Companies (CompanyID, CompanyName, Location)
VALUES
(1, 'Synopsys', 'Bangalore'),
(2, 'Comcast', 'Chennai'),
(3, 'Intellect', 'Bangalore'),
(4, 'GlobalSoft', 'Hyderabad'),
(5, 'Freshworks', 'Chennai'),
(6, 'DataVision', 'Hyderabad');
INSERT INTO Jobs (JobID, CompanyID, JobTitle, JobDescription, JobLocation, Salary, JobType, PostedDate)
VALUES
(201, 1, 'Software Engineer', 'Backend development', 'Bangalore', 80000, 'Full-time', '2024-01-15 09:00:00'),
(202, 2, 'Data Analyst', 'Data analysis and BI', 'Chennai', 65000, 'Full-time', '2024-02-10 09:00:00'),
(203, 3, 'Frontend Developer', 'Frontend development', 'Bangalore', 75000, 'Contract', '2024-01-20 09:00:00'),
(204, 4, 'Project Manager', 'Managing projects', 'Hyderabad', 0, 'Full-time', '2024-03-05 09:00:00'),
(205, 5, 'System Engineer', 'System maintenance', 'Chennai', 90000, 'Full-time', '2024-02-01 09:00:00'),
(206, 6, 'Business Analyst', 'Management & BI','Hyderabad', 70000, 'Contract', '2024-01-20 09:00:00');

INSERT INTO Applicants (ApplicantID, FirstName, LastName, Email, Phone, Resume)
VALUES
(1001, 'Vishal', 'Bhaskaran', 'vishal.bhaskaran@gmail.com', '9876543210', 'Vishal_Resume.pdf'),
(1002, 'Madhuri', 'Kumar', 'madhuri.kumar@gmail.com', '9876543211', 'Madhuri_Resume.pdf'),
(1003, 'Sanjay', 'Kumar', 'sanjay.kumar@gmail.com', '9876543212', 'Sanjay_Resume.pdf'),
(1004, 'Varsha', 'Jithendiran', 'varsha.jithendiran@gmail.com', '9876543213', 'Varsha_Resume.pdf'),
(1005, 'Rithesh', 'Varma', 'rithesh.varma@gmail.com', '9876543214', 'Rithesh_Resume.pdf'),
(1006, 'Shalini', 'Hari', 'shalini.hari@gmail.com', '9876543215', 'Shalini_Resume.pdf');

INSERT INTO Applications (ApplicationID, JobID, ApplicantID, ApplicationDate, CoverLetter)
VALUES
(1, 201, 1001, '2024-01-20 10:00:00', 'CoverLetter_1.doc'),
(2, 202, 1002, '2024-02-15 11:00:00', 'CoverLetter_2.doc'),
(3, 203, 1003, '2024-01-25 12:00:00', 'CoverLetter_3.doc'),
(4, 201, 1002, '2024-01-21 10:00:00', 'CoverLetter_4.doc'),
(5, 205, 1004, '2024-02-10 09:30:00', 'CoverLetter_5.doc');

-- 5. Count the number of applications received for each job listing and display the job title and application count
SELECT j.JobTitle, COUNT(a.ApplicationID) AS ApplicationCount
FROM Jobs j
LEFT JOIN Applications a ON j.JobID = a.JobID
GROUP BY j.JobTitle
ORDER BY j.JobTitle;

-- 6. Retrieve job listings within a specified salary range, displaying job title, company name, location, and salary

SELECT j.JobTitle, c.CompanyName, j.JobLocation, j.Salary
FROM Jobs j
JOIN Companies c ON j.CompanyID = c.CompanyID
WHERE j.Salary BETWEEN 60000 AND 80000
ORDER BY j.Salary DESC; 

/*DECLARE @MinSalary DECIMAL(10,2) = 60000; 
DECLARE @MaxSalary DECIMAL(10,2) = 80000; 

SELECT j.JobTitle, c.CompanyName, j.JobLocation, j.Salary
FROM Jobs j
JOIN Companies c ON j.CompanyID = c.CompanyID
WHERE j.Salary BETWEEN @MinSalary AND @MaxSalary
ORDER BY j.Salary DESC;*/

-- 7. Retrieve the job application history for a specific applicant
DECLARE @ApplicantID INT = 1003; 

SELECT j.JobTitle, c.CompanyName, a.ApplicationDate
FROM Applications a
JOIN Jobs j ON a.JobID = j.JobID
JOIN Companies c ON j.CompanyID = c.CompanyID
WHERE a.ApplicantID = @ApplicantID
ORDER BY a.ApplicationDate DESC;

-- 8. Calculate the average salary offered by all companies, excluding jobs with a salary of zero
SELECT AVG(j.Salary) AS AverageSalary
FROM Jobs j
WHERE j.Salary > 0;

-- 9. Identify the company that has posted the most job listings and display the company name along with the job count
INSERT INTO Jobs (JobID, CompanyID, JobTitle, JobDescription, JobLocation, Salary, JobType, PostedDate)
VALUES 
    (207, 2, 'Software Engineer', 'Develop and maintain software applications.', 'Chennai', 90000, 'Full-time', '2024-03-25 09:00:00'),
    (208, 3, 'Data Analyst', 'Analyze data and provide insights for business decisions.', 'Bangalore', 85000, 'Full-time', '2024-01-20 09:00:00');

SELECT c.CompanyName, COUNT(j.JobID) AS JobCount
FROM Companies c
JOIN Jobs j ON c.CompanyID = j.CompanyID
GROUP BY c.CompanyName
HAVING COUNT(j.JobID) = (
    SELECT MAX(JobCount)
    FROM (SELECT COUNT(JobID) AS JobCount
          FROM Jobs
          GROUP BY CompanyID) AS JobCounts
)
ORDER BY JobCount DESC;

-- 10. Find applicants who applied for positions in companies located in 'Chennai' and have at least 3 years of experience

ALTER TABLE Applicants
ADD YearsOfExperience INT;

UPDATE Applicants
SET YearsOfExperience = CASE 
    WHEN ApplicantID = 1001 THEN 4  
    WHEN ApplicantID = 1002 THEN 5  
    WHEN ApplicantID = 1003 THEN 5  
    WHEN ApplicantID = 1004 THEN 2  
    WHEN ApplicantID = 1005 THEN 4  
	WHEN ApplicantID = 1006 THEN 2  
    ELSE 0  
END;

SELECT a.FirstName, a.LastName, j.JobTitle, c.CompanyName
FROM Applications ap
JOIN Jobs j ON ap.JobID = j.JobID
JOIN Companies c ON j.CompanyID = c.CompanyID
JOIN Applicants a ON ap.ApplicantID = a.ApplicantID
WHERE c.Location = 'Chennai'
AND a.YearsOfExperience >= 3;

-- 11. Retrieve a list of distinct job titles with salaries between $60,000 and $80,000
SELECT DISTINCT JobTitle
FROM Jobs
WHERE Salary BETWEEN 60000 AND 80000;

-- 12. Find the jobs that have not received any applications
SELECT j.JobTitle, c.CompanyName
FROM Jobs j
JOIN Companies c ON j.CompanyID = c.CompanyID
LEFT JOIN Applications a ON j.JobID = a.JobID
WHERE a.ApplicationID IS NULL;

-- 13. Retrieve a list of job applicants along with the companies they have applied to and the positions they have applied for
SELECT a.FirstName, a.LastName, c.CompanyName, j.JobTitle
FROM Applicants a
JOIN Applications ap ON a.ApplicantID = ap.ApplicantID
JOIN Jobs j ON ap.JobID = j.JobID
JOIN Companies c ON j.CompanyID = c.CompanyID;

-- 14. Retrieve a list of companies along with the count of jobs they have posted, even if they have not received any applications
SELECT c.CompanyName, COUNT(j.JobID) AS JobCount
FROM Companies c
LEFT JOIN Jobs j ON c.CompanyID = j.CompanyID
GROUP BY c.CompanyName;

-- 15. List all applicants along with the companies and positions they have applied for, including those who have not applied
SELECT a.FirstName, a.LastName, c.CompanyName, j.JobTitle
FROM Applicants a
LEFT JOIN Applications ap ON a.ApplicantID = ap.ApplicantID
LEFT JOIN Jobs j ON ap.JobID = j.JobID
LEFT JOIN Companies c ON j.CompanyID = c.CompanyID;

-- 16. Find companies that have posted jobs with a salary higher than the average salary of all jobs
SELECT DISTINCT c.CompanyName
FROM Companies c
JOIN Jobs j ON c.CompanyID = j.CompanyID
WHERE j.Salary > (SELECT AVG(Salary) FROM Jobs);

-- 17. Display a list of applicants with their names and a concatenated string of their city and state
SELECT * from Applicants

ALTER TABLE Applicants
ADD City VARCHAR(100),  
    State VARCHAR(100); 

UPDATE Applicants
SET City = CASE ApplicantID
    WHEN 1002 THEN 'Chennai'
    WHEN 1001 THEN 'Bangalore'
    WHEN 1003 THEN 'Hyderabad'
    WHEN 1004 THEN 'Pune'
    WHEN 1005 THEN 'Chennai'
    WHEN 1006 THEN 'Delhi'
END,
State = CASE ApplicantID
    WHEN 1002 THEN 'Tamil Nadu'
    WHEN 1001 THEN 'Karnataka'
    WHEN 1003 THEN 'Telangana'
    WHEN 1004 THEN 'Maharashtra'
    WHEN 1005 THEN 'Tamil Nadu'
    WHEN 1006 THEN 'Delhi'
END
WHERE ApplicantID IN (1001, 1002, 1003, 1004, 1005, 1006);

SELECT FirstName, LastName, CONCAT(City, ', ', State) AS Location
FROM Applicants;

-- 18. Retrieve a list of jobs with titles containing either 'Developer' or 'Engineer'
SELECT JobTitle
FROM Jobs
WHERE JobTitle LIKE '%Developer%' OR JobTitle LIKE '%Engineer%';

-- 19. Retrieve a list of applicants and the jobs they have applied for, including those who have not applied and jobs without applicants
SELECT a.FirstName, a.LastName, j.JobTitle
FROM Applicants a
LEFT JOIN Applications ap ON a.ApplicantID = ap.ApplicantID
LEFT JOIN Jobs j ON ap.JobID = j.JobID;

-- 20. List all combinations of applicants and companies where the company is in a specific city and the applicant has more than 2 years of experience
-- For example: city='Chennai'
SELECT a.FirstName, a.LastName, c.CompanyName
FROM Applicants a
JOIN Applications ap ON a.ApplicantID = ap.ApplicantID
JOIN Jobs j ON ap.JobID = j.JobID
JOIN Companies c ON j.CompanyID = c.CompanyID
WHERE c.Location LIKE '%Chennai%' AND a.YearsOfExperience > 2;




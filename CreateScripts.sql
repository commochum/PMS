-- =======================================================
-- Create the Practice Management Medical System's Database
-- Created by Karen T. Atas
-- 06-05-2018
-- Stage 2 Project - C#
-- =======================================================
USE MASTER
GO

IF EXISTS ( SELECT *
			FROM master..sysdatabases
			WHERE name = N'PractiseManagementSystemProject_kta')
DROP DATABASE PractiseManagementSystemProject_kta;
GO
CREATE DATABASE PractiseManagementSystemProject_kta;
GO
SET DATEFORMAT dmy;
GO
CREATE TABLE PractiseManagementSystemProject_kta.dbo.Login(
login_id			INT IDENTITY(1000,1) NOT NULL,
username			NVARCHAR(20) NOT NULL,
password    		NVARCHAR(20) NOT NULL
CONSTRAINT Login_PK PRIMARY KEY (login_id)
)
GO

CREATE TABLE PractiseManagementSystemProject_kta.dbo.Patient(
patientId				INT IDENTITY(10000,1) NOT NULL,
firstName				NVARCHAR(25) NOT NULL,
lastName				NVARCHAR(30) NOT NULL,
dob						DATETIME,
age						INT,
gender					NVARCHAR(20),
address1				NVARCHAR(50),
suburb					NVARCHAR(30),
state					NVARCHAR(30),
postcode				NCHAR(10),
country					NVARCHAR(50),
medicareNo				NCHAR(20) NOT NULL,	
recordCreated   		DATETIME,
recordUpdated   		DATETIME,
contactType				NVARCHAR(10),
contactNo				NVARCHAR(16),
emailAddress			NVARCHAR(30),
emergencyContactName	NVARCHAR(30),
emergencyContactNo		NVARCHAR(16),
relationship			NVARCHAR(20),
CONSTRAINT  Patient_PK PRIMARY KEY (patientId)
)

GO
CREATE TABLE PractiseManagementSystemProject_kta.dbo.Employee(
employeeId				INT IDENTITY(1,1) NOT NULL,
firstName				NVARCHAR(25) NOT NULL,
lastName				NVARCHAR(30) NOT NULL,
dob						DATETIME,
age						INT,
gender					NVARCHAR(20),
address1				NVARCHAR(50),
suburb					NVARCHAR(30),
state					NVARCHAR(30),
postcode				NCHAR(10),
country					NVARCHAR(30),
medicareNo				NCHAR(20) NOT NULL,	
recordCreated   		DATETIME,
recordUpdated   		DATETIME,
contactType				NVARCHAR(10),
contactNo				NVARCHAR(16),
emailAddress			NVARCHAR(30),
emergencyContactName	NVARCHAR(30),
emergencyContactNo		NVARCHAR(16),
relationship			NVARCHAR(25),
companyName				NVARCHAR(40),
companyAddress			NVARCHAR(50),
position				NVARCHAR(10),
jobTitle				NVARCHAR(50),
employeeStatus			NVARCHAR(10),
department				NVARCHAR(30),
hireDate				DATETIME,			
employmentType			NVARCHAR(10),
incomeType				NVARCHAR(10),			
incomeAmount			NVARCHAR(25),
hoursWorked				INT,
startTime				TIME
CONSTRAINT  Employee_PK PRIMARY KEY (employeeId)
)
GO
CREATE TABLE PractiseManagementSystemProject_kta.dbo.Doctor(
doctorId			INT IDENTITY(1000,1) NOT NULL,
employeeId			INT,
doctorLicenseNo		NVARCHAR(30),
specialty			NVARCHAR(50),
buildingLocation	NVARCHAR(30),
roomNumber			NVARCHAR(10)			
CONSTRAINT Doctor_PK PRIMARY KEY (doctorId, employeeId),
CONSTRAINT Doctor_Employee_FK_Cascade FOREIGN KEY (employeeId) REFERENCES Employee ON DELETE CASCADE
)
GO
CREATE TABLE PractiseManagementSystemProject_kta.dbo.Appointment(
appointmentId			INT IDENTITY(1,1) NOT NULL,
patientId				INT,
patientName				NVARCHAR(65),
doctorId				INT,
employeeId				INT,
doctorName				NVARCHAR(65),
apptDate				DATETIME,
apptTime				TIME,
purpose					NVARCHAR(100),
createdDate				DATETIME,
updatedDate				DATETIME
CONSTRAINT Appointment_PK PRIMARY KEY (appointmentId),
CONSTRAINT Appointment_Patient_FK_Cascade FOREIGN KEY (patientId) REFERENCES Patient ON DELETE CASCADE,
CONSTRAINT Appointment_Doctor_FK_Cascade FOREIGN KEY (doctorId, employeeId) REFERENCES Doctor ON DELETE CASCADE
)
GO

CREATE TABLE PractiseManagementSystemProject_kta.dbo.MedicalRecords(
medicalRecordId		INT IDENTITY(1,1) NOT NULL,
patientId           INT,
doctorId            INT,
employeeId          INT,
assignedDoctor      NVARCHAR(65),
consultationDate	DATETIME,
bloodPressure       NVARCHAR(10),
bloodType           NCHAR(2),
weight				decimal(5,1),
height				decimal(5,1),
familyHistory       NVARCHAR(250),
pastHealthHistory   NVARCHAR(250),
isSmoking			NCHAR(3),
isPregnant          NCHAR(3),
hasDiabetes         NCHAR(3),
hasHighBP           NCHAR(3),
hasHighCholesterol  NCHAR(3),
hasHeartProblem     NCHAR(3),
hasAsthma           NCHAR(3),
hasEpilepsy         NCHAR(3),
hasPneumonia        NCHAR(3),
hasHepatitis        NCHAR(3),
hasUlcer            NCHAR(3),	
hasLeukemia         NCHAR(3),
hasTB               NCHAR(3),
hasHIV              NCHAR(3),
chiefComplaint      NVARCHAR(250),
presentIllness      NVARCHAR(250),
medicationTaken     NVARCHAR(250),
diagnosis           NVARCHAR(250),
currentRegimen      NVARCHAR(250),
prescription        NVARCHAR(250),
recordedDate        DATETIME,
updatedDate         DATETIME
CONSTRAINT MedicalRecords_PK PRIMARY KEY (medicalRecordId),
CONSTRAINT MedicalRecords_Patient_FK_Cascade FOREIGN KEY (patientId) REFERENCES Patient ON DELETE CASCADE,
CONSTRAINT MedicalRecords_Doctor_FK_Cascade FOREIGN KEY (doctorId, employeeId) REFERENCES Doctor ON DELETE SET NULL
)
GO

USE PractiseManagementSystemProject_kta
GO

INSERT INTO Login
           (username
           ,password)
     VALUES
           ('admin'
           ,'admin')
GO
INSERT INTO Login
           (username
           ,password)
     VALUES
           ('staff'
           ,'staff')
GO



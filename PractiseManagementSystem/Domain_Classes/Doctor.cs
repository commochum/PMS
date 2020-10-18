using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiseManagementSystem
{
    class Doctor : Employee
    {
        
        string doctorId;
        string empId;
        string specialisation;
        string doctorLicenseNo;
        string buildingLocation;
        string roomNumber;
        
        string message = null;

        public Doctor() : base()
        {

        }

        internal String EmpId
        {
            get
            {
                return empId;
            }
            set
            {
                this.empId = value;
            }
        }
        public String DoctorId
        {
            get
            {
                return doctorId;
            }
            set
            {
                this.doctorId = value;
            }
        }
        
        public String Specialisation
        {
            get
            {
                return specialisation;
            }
            set
            {
                this.specialisation = value;
            }
        }
        public String DoctorLicenseNo
        {
            get
            {
                return doctorLicenseNo;
            }
            set
            {
                this.doctorLicenseNo = value;
            }
        }
        public string BuildingLocation
        {
            get
            {
                return buildingLocation;
            }
            set
            {
                this.buildingLocation = value;
            }
        }
        public string RoomNumber
        {
            get
            {
                return roomNumber;
            }
            set
            {
                this.roomNumber = value;
            }
        }

        internal string updateDoctorRecord(string employeeId, string doctorId)
        {
            string queryString = "UPDATE DOCTOR " +
                "SET doctorLicenseNo = '" + DoctorLicenseNo.Trim() +
                "',specialty = '" + Specialisation.Trim() +
                "',buildingLocation = '" + BuildingLocation.Trim() +
                "',roomNumber = '" + RoomNumber.Trim() +
                "' WHERE EXISTS(select employeeId from Employee where employeeId = Doctor.employeeId)" +
                " AND employeeId = " + Convert.ToInt32(employeeId);

            DataTable dt = new DataTable();
            message = connFactory.updateDataIntoDB(queryString);

            return message;
        }

        internal string createDoctorRecord()
        {
            string queryString = "INSERT INTO DOCTOR (employeeId, doctorLicenseNo, specialty, buildingLocation, roomNumber) VALUES " +
                          "((SELECT IDENT_CURRENT('EMPLOYEE'))," +
                          "'" + DoctorLicenseNo.Trim() +
                          "', '" + Specialisation.Trim() +
                           "', '" + BuildingLocation +
                           "', '" + RoomNumber +
                           "')";

            DataTable dt = new DataTable();
            message = connFactory.createDataIntoDB(queryString);

            return message;
        }

        internal string executeUpdateCreateTransaction(string employeeId)
        {
            string queryString1 = "SET DATEFORMAT dmy; " +
                "UPDATE EMPLOYEE " +
                "SET firstName = '" + FirstName.Trim() +
                "',lastName = '" + LastName.Trim() +
                "',dob = '" + DOB +
                "',age = " + Age +
                ",gender  = '" + Gender.Trim() +
                "',address1 = '" + AddressLine1.Trim() +
                "',suburb = '" + Suburb.Trim() +
                "',state = '" + State.Trim() +
                "',postcode = '" + PostCode.Trim() +
                "',country = '" + Country.Trim() +
                "',medicareNo = '" + MedicareNo.Trim() +
                "',recordUpdated = CONVERT(datetime, '" + RecordUpdated + "', 103)" +
                ",contactType = '" + contactType +
                "',contactNo  = '" + contactNo.Trim() +
                "',emailAddress = '" + emailAddress.Trim() +
                "',emergencyContactName = '" + EmergencyContactName.Trim() +
                "',emergencyContactNo = '" + EmergencyContactNo.Trim() +
                "',relationship = '" + Relationship.Trim() +
                "',companyName = '" + CompanyName.Trim() +
                "',companyAddress = '" + CompanyAddress.Trim() +
                "',position = '" + Position.Trim() +
                "',jobTitle = '" + JobTitle.Trim() +
                "',employeeStatus = '" + EmployeeStatus.Trim() +
                "',department = '" + Department.Trim() +
                "',hireDate = '" + HireDate +
                "',employmentType = '" + EmploymentType.Trim() +
                "',incomeType = '" + IncomeType.Trim() +
                "',incomeAmount = '" + Income.Trim() +
                "',hoursWorked = " + NoOfHoursWorked +
                ",startTime = CONVERT(TIME, '" + StartTime + "')" +
                " WHERE employeeId = " + Convert.ToInt32(employeeId);

            string queryString2 = "INSERT INTO DOCTOR (employeeId, doctorLicenseNo, specialty, buildingLocation, roomNumber) VALUES " +
                             "(" + employeeId +
                             ",'" + DoctorLicenseNo.Trim() +
                             "', '" + Specialisation.Trim() +
                              "', '" + BuildingLocation +
                              "', '" + RoomNumber +
                              "')";

            DataTable dt = new DataTable();
            message = "Doctor Employee record updates " + connFactory.executeTransactionIntoDB(queryString1, queryString2);

            return message;
        }

        internal string executeUpdateTransaction(string employeeId, string doctorId)
        {
            string queryString1 = "SET DATEFORMAT dmy; " +
                "UPDATE EMPLOYEE " +
                "SET firstName = '" + FirstName.Trim() +
                "',lastName = '" + LastName.Trim() +
                "',dob = '" + DOB +
                "',age = " + Age +
                ",gender  = '" + Gender.Trim() +
                "',address1 = '" + AddressLine1.Trim() +
                "',suburb = '" + Suburb.Trim() +
                "',state = '" + State.Trim() +
                "',postcode = '" + PostCode.Trim() +
                "',country = '" + Country.Trim() +
                "',medicareNo = '" + MedicareNo.Trim() +
                "',recordUpdated = CONVERT(datetime, '" + RecordUpdated + "', 103)" +
                ",contactType = '" + contactType +
                "',contactNo  = '" + contactNo.Trim() +
                "',emailAddress = '" + emailAddress.Trim() +
                "',emergencyContactName = '" + EmergencyContactName.Trim() +
                "',emergencyContactNo = '" + EmergencyContactNo.Trim() +
                "',relationship = '" + Relationship.Trim() +
                "',companyName = '" + CompanyName.Trim() +
                "',companyAddress = '" + CompanyAddress.Trim() +
                "',position = '" + Position.Trim() +
                "',jobTitle = '" + JobTitle.Trim() +
                "',employeeStatus = '" + EmployeeStatus.Trim() +
                "',department = '" + Department.Trim() +
                "',hireDate = '" + HireDate +
                "',employmentType = '" + EmploymentType.Trim() +
                "',incomeType = '" + IncomeType.Trim() +
                "',incomeAmount = '" + Income.Trim() +
                "',hoursWorked = " + NoOfHoursWorked +
                ",startTime = CONVERT(TIME, '" + StartTime + "')" +
                " WHERE employeeId = " + Convert.ToInt32(employeeId);

            string queryString2 = "UPDATE DOCTOR SET doctorLicenseNo = '" + DoctorLicenseNo.Trim() +
                "',specialty = '" + Specialisation.Trim() +
                "',buildingLocation = '" + BuildingLocation.Trim() +
                "',roomNumber = '" + RoomNumber.Trim() +
                "' from Doctor d, Employee e where e.employeeId = d.employeeId " +
                " AND d.doctorId = " + Convert.ToInt32(doctorId);

            DataTable dt = new DataTable();
            message = "Doctor Employee record updates" + connFactory.executeTransactionIntoDB(queryString1, queryString2);

            return message;
        }

       
        internal string executeCreateEmployeeTransaction()
        {
            string queryString1 = "SET DATEFORMAT dmy; INSERT INTO EMPLOYEE (firstName, lastName, dob, age, gender, " +
                 "address1, suburb, state, postcode, country, medicareNo, recordCreated, recordUpdated" +
                 ",contactType, contactNo, emailAddress, emergencyContactName, emergencyContactNo, relationship, companyName," +
                 "companyAddress, position, jobTitle, employeeStatus, department, hireDate, employmentType, incomeType, incomeAmount, hoursWorked, startTime) VALUES " +
                 "('" + FirstName.Trim() +
                    "', '" + LastName.Trim() +
                    "', '" + DOB +
                    "', " + Age +
                    ", '" + Gender.Trim() +
                    "', '" + AddressLine1.Trim() +
                    "', '" + Suburb.Trim() +
                    "', '" + State.Trim() +
                    "', '" + PostCode.Trim() +
                    "', '" + Country.Trim() +
                    "', '" + MedicareNo.Trim() +
                    "', CONVERT(datetime, '" + RecordCreated + "', 103)" +
                    ", CONVERT(datetime, '" + RecordUpdated + "', 103)" +
                    ", '" + contactType.Trim() +
                    "', '" + contactNo.Trim() +
                    "', '" + emailAddress.Trim() +
                    "', '" + EmergencyContactName.Trim() +
                    "', '" + EmergencyContactNo.Trim() +
                    "', '" + Relationship.Trim() +
                    "', '" + CompanyName.Trim() +
                    "', '" + CompanyAddress.Trim() +
                    "', '" + Position.Trim() +
                    "', '" + JobTitle.Trim() +
                    "', '" + EmployeeStatus.Trim() +
                   "', '" + Department.Trim() +
                   "', CONVERT(datetime, '" + HireDate + "', 103)" +
                    ", '" + EmploymentType.Trim() +
                    "', '" + IncomeType.Trim() +
                    "', '" + Income.Trim() +
                    "', " + NoOfHoursWorked +
                    ", CONVERT(TIME, '" + StartTime + "')" +
                    ")";

            string queryString2 = "INSERT INTO DOCTOR (employeeId, doctorLicenseNo, specialty, buildingLocation, roomNumber) VALUES " +
                             "((SELECT IDENT_CURRENT('EMPLOYEE'))," +
                             "'" + DoctorLicenseNo.Trim() +
                             "', '" + Specialisation.Trim() +
                              "', '" + BuildingLocation +
                              "', '" + RoomNumber +
                              "')";

            DataTable dt = new DataTable();
            message = "Doctor Employee record creation" + connFactory.executeTransactionIntoDB(queryString1, queryString2);

            return message;
        }

        public DataTable getDoctorDetails(string employeeId)
        {
            string queryString = "SELECT * FROM DOCTOR WHERE employeeId = " + Convert.ToInt32(employeeId);

            DataTable dt = connFactory.populateDataFromDB(queryString);

            return dt;
        }

    }
}

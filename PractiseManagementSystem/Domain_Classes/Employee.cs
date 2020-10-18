using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiseManagementSystem
{
    class Employee : Person
    {
        string employeeId;
        string companyName;
        string companyAddress;
        string position;
        string jobTitle;
        string employeeStatus;
        string department;
        DateTime hireDate;
        string employmentType;
        string incomeType;
        string income;
        int noOfHoursWorked;
        string startTime;

        protected ConnectionFactory connFactory = new ConnectionFactory();
        string message = null;

        public Employee() : base()
        {

        }

        public String EmployeeId
        {
            get
            {
                return employeeId;
            }
            set
            {
                this.employeeId = value;
            }
        }
        public String CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                this.companyName = value;
            }
        }
        public String CompanyAddress
        {
            get
            {
                return companyAddress;
            }
            set
            {
                this.companyAddress = value;
            }
        }
        public String Position
        {
            get
            {
                return position;
            }
            set
            {
                this.position = value;
            }
        }
        public String JobTitle
        {
            get
            {
                return jobTitle;
            }
            set
            {
                this.jobTitle = value;
            }
        }
        public String EmployeeStatus
        {
            get
            {
                return employeeStatus;
            }
            set
            {
                this.employeeStatus = value;
            }
        }
        public String Department
        {
            get
            {
                return department;
            }
            set
            {
                this.department = value;
            }
        }
        public DateTime HireDate
        {
            get
            {
                return hireDate;
            }
            set
            {
                this.hireDate = value;
            }
        }
        public String EmploymentType
        {
            get
            {
                return employmentType;
            }
            set
            {
                this.employmentType = value;
            }
        }
        public String IncomeType
        {
            get
            {
                return incomeType;
            }
            set
            {
                this.incomeType = value;
            }
        }
        public String Income
        {
            get
            {
                return income;
            }
            set
            {
                this.income = value;
            }
        }
        public int NoOfHoursWorked
        {
            get
            {
                return noOfHoursWorked;
            }
            set
            {
                this.noOfHoursWorked = value;
            }
        }
        public string StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                this.startTime = value;
            }
        }

        internal string executeUpdateDeleteTransaction(string employeeId)
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

            string queryString2 = "DELETE FROM Doctor where employeeId = " + Convert.ToInt32(employeeId);

            DataTable dt = new DataTable();
            message = "Employee record updates " + connFactory.executeTransactionIntoDB(queryString1, queryString2);

            return message;
        }


        internal string updateEmployeeRecord(string employeeId)
        {
            string queryString = "SET DATEFORMAT dmy; " +
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

            
            DataTable dt = new DataTable();
            message = "Employee " + connFactory.updateDataIntoDB(queryString);

            return message;
        }

        internal string createEmployeeRecord()
        {
            string queryString = "SET DATEFORMAT dmy; INSERT INTO EMPLOYEE (firstName, lastName, dob, age, gender, " +
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
            DataTable dt = new DataTable();
            message = "Employee " + connFactory.createDataIntoDB(queryString);

            return message;
        }
        

        public DataTable getEmployees()
        {
            string queryString = "SELECT * FROM EMPLOYEE";

            DataTable dt = connFactory.populateDataFromDB(queryString);

            return dt;
        }

        public DataTable getEmployeeDetails(string employeeId)
        {
            string queryString = "SELECT * FROM EMPLOYEE WHERE employeeId = " + Convert.ToInt32(employeeId);

            DataTable dt = connFactory.populateDataFromDB(queryString);

            return dt;
        }

        public string deleteEmployeeRecord(string employeeId)
        {
            string queryString = "DELETE FROM EMPLOYEE WHERE employeeId = " + Convert.ToInt32(employeeId);

            message = connFactory.deleteDataFromDB(queryString, "Employee");

            return message;
        }
    }
}

using PractiseManagementSystem.Domain_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PractiseManagementSystem
{
    class Patient : Person
    {
        string patientId;

        ConnectionFactory connFactory = new ConnectionFactory();
        string message = null;

        public Patient() : base()
        {
           
        }

        public string PatientId
        {
            get
            {
                return patientId;
            }
            set
            {
                this.patientId = value;
            }
        }
       
  
        internal string createPatientRecord()
        {
           string queryString = "SET DATEFORMAT dmy; INSERT INTO PATIENT (firstName, lastName, dob, age, gender, " +
                "address1, suburb, state, postcode, country, medicareNo, recordCreated, recordUpdated" +
                ",contactType, contactNo, emailAddress, emergencyContactName, emergencyContactNo, relationship) VALUES " +
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
                   "', CONVERT(datetime, '"+ RecordCreated + "', 103)" +
                   ", CONVERT(datetime, '" + RecordUpdated + "', 103)" +
                   ", '" + contactType.Trim() +
                   "', '" + contactNo.Trim() +
                   "', '" + emailAddress.Trim() +
                   "', '" + EmergencyContactName.Trim() +
                   "', '" + EmergencyContactNo.Trim() +
                   "', '" + Relationship.Trim() +
                   "')";

            DataTable dt = new DataTable();
            message = connFactory.createDataIntoDB(queryString);

            return message;
        }

        internal string updatePatientRecord(string patientId)
        {
            string queryString = "SET DATEFORMAT dmy; UPDATE PATIENT " +
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
                "', recordUpdated = CONVERT(datetime, '" + RecordUpdated + "', 103)" +
                ",contactType = '" + contactType +
                "',contactNo  = '" + contactNo.Trim() +
                "',emailAddress = '" + emailAddress.Trim() +
                "',emergencyContactName = '" + EmergencyContactName.Trim() +
                "',emergencyContactNo = '" + EmergencyContactNo.Trim() +
                "',relationship = '" + Relationship.Trim() +
                "'WHERE patientId = " + Convert.ToInt32(patientId);

            DataTable dt = new DataTable();
            message = connFactory.updateDataIntoDB(queryString);

            return message;
        }

        public DataTable getPatients()
        {
            string queryString = "SELECT * FROM PATIENT";

            DataTable dt = connFactory.populateDataFromDB(queryString);

            return dt;
        }

        public DataTable getPatientIntoAppointmentList()
        {
            string queryString = "select patientId, firstName, lastName, medicareNo, contactNo, emailAddress from patient";

            DataTable dt = connFactory.populateDataFromDB(queryString);

            return dt;
        }

        public DataTable getPatientDetails(string patientId)
        {
            string queryString = "SELECT * FROM PATIENT WHERE patientId = " +Convert.ToInt32(patientId);

            DataTable dt = connFactory.populateDataFromDB(queryString);

            return dt;
        }

        public DataTable getPatientDetailsForRecords(string patientId)
        {
            string queryString = "select distinct(p.patientId), a.patientName, p.age, p.gender, p.medicareNo, p.contactNo, p.dob, p.emergencyContactName, p.emergencyContactNo, p.relationship from patient p " +
                "join Appointment a on a.patientId = p.patientId where p.patientId = " + Convert.ToInt32(patientId);

            DataTable dt = connFactory.populateDataFromDB(queryString);

            return dt;
        }

        public Dictionary<string, string> populateDoctorsFromPatientAppointments(string patientId, ComboBox cbxList)
        {
            string query = " select distinct(a.doctorId), a.doctorName from patient p join Appointment a on a.patientId = p.patientId where p.patientId = " + Convert.ToInt32(patientId);
            Dictionary<string, string> patientAssignedDoctor = new Dictionary<string, string>();

            patientAssignedDoctor = connFactory.SelectFromDB(query);

            Console.WriteLine(patientAssignedDoctor);
            if (patientAssignedDoctor.ToList().Count > 0)
            {
                cbxList.ItemsSource = patientAssignedDoctor.Values.ToList();
            }

            return patientAssignedDoctor;
        }

        public string deletePatientRecord(string patientId)
        {
            string queryString = "DELETE FROM PATIENT WHERE patientId = " +Convert.ToInt32(patientId);

            message = connFactory.deleteDataFromDB(queryString, "Patient");

            return message;
        }
    }
}

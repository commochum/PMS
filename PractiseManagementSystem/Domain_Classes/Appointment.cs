using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace PractiseManagementSystem
{
    class Appointment
    {
        string apptAppointmentId;
        string apptPatientId;
        string apptPatientName;
        string apptDoctorId;
        string apptEmployeeId;
        string apptDoctorName;
        DateTime appointmentDate;
        string appointmentTime;
        string appointmentStatus;
        string purpose;
        DateTime createdDate;
        DateTime updatedDate;
        internal Doctor doctor = new Doctor();
        internal Patient patient = new Patient();

        ConnectionFactory connFactory = new ConnectionFactory();
        DataTable dt = new DataTable();
        string message = null;

        public string AppointmentId
        {
            get
            {
                return apptAppointmentId;
            }
            set
            {
                this.apptAppointmentId = value;
            }
        }

        public string ApptPatientId
        {
            get
            {
                return apptPatientId;
            }
            set
            {
                this.apptPatientId = value;
            }
        }
        public string ApptDoctorId
        {
            get
            {
                return apptDoctorId;
            }
            set
            {
                this.apptDoctorId = value;
            }
        }
        public string ApptEmployeeId
        {
            get
            {
                return apptEmployeeId;
            }
            set
            {
                this.apptEmployeeId = value;
            }
        }
        public string ApptPatientName
        {
            get
            {
                return apptPatientName;
            }
            set
            {
                this.apptPatientName = value;
            }
        }
        public string ApptPatientDoctor
        {
            get
            {
                return apptDoctorName;
            }
            set
            {
                this.apptDoctorName = value;
            }
        }
        public DateTime AppointmentDate
        {
            get
            {
                return appointmentDate;
            }
            set
            {
                this.appointmentDate = value;
            }
        }
        public string AppointmentTime
        {
            get
            {
                return appointmentTime;
            }
            set
            {
                this.appointmentTime = value;
            }
        }

        public string AppointmentStatus
        {
            get
            {
                return appointmentStatus;
            }
            set
            {
                this.appointmentStatus = value;
            }
        }
        public string Purpose
        {
            get
            {
                return purpose;
            }
            set
            {
                this.purpose = value;
            }
        }
        public DateTime CreatedDate
        {
            get
            {
                return createdDate;
            }
            set
            {
                this.createdDate = value;
            }
        }
        public DateTime UpdatedDate
        {
            get
            {
                return updatedDate;
            }
            set
            {
                this.updatedDate = value;
            }
        }

        internal string createAppointmentRecord(string ApptDoctorId)
        {
            string queryString = "SET DATEFORMAT dmy; INSERT INTO Appointment(patientId, patientName, doctorId, employeeId, doctorName, apptDate, apptTime, purpose, createdDate, updatedDate) VALUES " +
                "(" + Convert.ToInt32(ApptPatientId) +
                    ", '" + ApptPatientName.Trim() +
                    "', " + Convert.ToInt32(ApptDoctorId) +
                    ", (select employeeId from Doctor where doctorId = " + Convert.ToInt32(ApptDoctorId) + ") " +
                    ", '" + ApptPatientDoctor.Trim() +
                    "', CONVERT(datetime, '" + AppointmentDate + "', 103)" +
                    ", CONVERT(TIME, '" + AppointmentTime + "')" +
                    ", '" + Purpose.Trim() +
                   "', CONVERT(datetime, '" + createdDate + "', 103)" +
                   ", CONVERT(datetime, '" + updatedDate + "', 103)" +
                   ")";
            dt = new DataTable();
            message = connFactory.createDataIntoDB(queryString);

            return message;
        }

        internal string updateAppointmentRecord(string apptId, string ApptDoctorId)
        {
            string queryString = "SET DATEFORMAT dmy; UPDATE Appointment " +
                "SET doctorId = " + ApptDoctorId + ", " +
                "employeeId = (select employeeId from Doctor where doctorId = " + Convert.ToInt32(ApptDoctorId) + "), " +
                "doctorName = '" + ApptPatientDoctor.Trim() + "', " +
                "apptDate = '" + AppointmentDate + "', " +
                "apptTime = CONVERT(TIME, '" + AppointmentTime + "'), " +
                "purpose = '" + Purpose.Trim() + "', " +
                "updatedDate = '" + UpdatedDate + "' " +
                "WHERE appointmentId = " + Convert.ToInt32(apptId);

            DataTable dt = new DataTable();
            message = connFactory.updateDataIntoDB(queryString);

            return message;
        }

        internal DataTable getAppointmentRecords(string patientId)
        {
            string queryString = "select distinct(p.patientId), a.patientName from Appointment a  " +
                "join Patient p on p.patientId = a.patientId " +
                "and p.patientId = "+Convert.ToInt32(patientId);

            dt = connFactory.populateDataFromDB(queryString);

            return dt;
        }

        internal DataTable retrievePatientAppointmentDetails(string patientId)
        {
            string queryString = "select a.appointmentId, a.apptDate, a.apptTime, a.doctorName, d.specialty, e.department, d.buildingLocation, d.roomNumber, a.purpose " +
                "from Appointment a " +
                "join Employee e on e.employeeId = a.employeeId And e.jobTitle = 'Doctor' " +
                "join Doctor d on d.doctorId = a.doctorId AND d.employeeId = a.employeeId and e.employeeId = d.employeeId " +
                "and a.patientId = " + Convert.ToInt32(patientId);

            dt = connFactory.populateDataFromDB(queryString);

            return dt;
        }

        public Doctor PopulateDoctorNameList(ComboBox cbxList)
        {
            string query = "select e.employeeId, d.doctorId, e.firstName, e.lastName from EMPLOYEE e, Doctor d " +
                "where d.employeeId = e.employeeId and e.jobTitle = 'Doctor'";
            List<string> doctorIDandNameList = new List<string>();
            Doctor doc = new Doctor();
            dt = connFactory.populateDataFromDB(query);
            if (dt.Rows.Count > 0)
            {
                string doctorIdAndName = null;

                foreach (DataRow row in dt.Rows)
                {
                    doc.EmpId = row[0].ToString();
                    doc.DoctorId = row[1].ToString();
                    doc.FirstName = row[2].ToString();
                    doc.LastName = row[3].ToString();

                    doctorIdAndName = "[" + row[1].ToString() + "] - " + row[2].ToString() + " " + row[3].ToString();
                    doctorIDandNameList.Add(doctorIdAndName);
                }

            cbxList.ItemsSource = doctorIDandNameList.ToList();
            }
            return doc;
        }

        
        internal string deleteAppointmentRecord(string appointmentId)
        {
            string queryString = "DELETE FROM APPOINTMENT WHERE appointmentId = " + Convert.ToInt32(appointmentId);

            message = connFactory.deleteDataFromDB(queryString, "Appointment");

            return message;

        }

        public Doctor PopulateSelectedDoctorValues(string doctorId)
        {
            string query = "select e.employeeId, d.doctorId, e.lastName, e.hoursWorked, e.startTime, e.employeeStatus from EMPLOYEE e " +
                "JOIN Doctor d ON d.employeeId = e.employeeId where d.doctorId = "+Convert.ToInt32(doctorId);


            Doctor d = new Doctor();
            dt = connFactory.populateDataFromDB(query);

            if (dt.Rows.Count > 0)
            {
                d.EmpId = dt.Rows[0].Field<int>("employeeId").ToString();
                d.DoctorId = dt.Rows[0].Field<int>("doctorId").ToString();
                //d.FirstName = dt.Rows[0].Field<string>("firstName");
                d.LastName = dt.Rows[0].Field<string>("lastName");
                d.NoOfHoursWorked = Convert.ToInt32(dt.Rows[0].Field<int>("hoursWorked"));
                d.StartTime = dt.Rows[0].Field<TimeSpan>("startTime").ToString();
                d.EmployeeStatus = dt.Rows[0].Field<string>("employeeStatus").ToString();

            }
            return d;
        }
        public List<string> SelectedAppointmentTime(string apptDate, string doctorId)
        {
            DateTime dateString = Convert.ToDateTime(apptDate);
            string query = "SET DATEFORMAT dmy; select apptTime from appointment where apptDate = CONVERT(datetime, '" + dateString + "', 103)" +
                " and doctorId = " +Convert.ToInt32(doctorId);
            List<string> apptSelectedTimes = new List<string>();

            dt = connFactory.populateDataFromDB(query);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    apptSelectedTimes.Add(Convert.ToDateTime(row[0].ToString()).ToShortTimeString());
                }
                
            }
            return apptSelectedTimes;
        }


    }
}

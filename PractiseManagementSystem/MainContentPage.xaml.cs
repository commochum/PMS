using PractiseManagementSystem.Domain_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PractiseManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainContentPage : Window
    {
        ConnectionFactory connFactory = new ConnectionFactory();

        Patient patient = new Patient();
        Employee employee = new Employee();
        Doctor doctor = new Doctor();
        Appointment appointment = new Appointment();
        MedicalRecords medicalRecords = new MedicalRecords();
        internal Boolean isAddEmployeeSelected = false;

        public MainContentPage()
        {
            InitializeComponent();
            tbPatientMainMessage.Text = "";
            grdPatientList.Visibility = Visibility.Visible;
            FillDataForPatients();
            FillDataForPatientAppointment();
            FillDataForEmployees();
        }

        private void Patient_Selected(object sender, RoutedEventArgs e)
        {
            tbEmployeeMainMessage.Text = "";
            tbAppointmentMainMessage.Text = "";
            FillDataForPatients();

        }

        private void Employee_Selected(object sender, RoutedEventArgs e)
        {
            tbPatientMainMessage.Text = "";
            tbAppointmentMainMessage.Text = "";
            FillDataForEmployees();
        }
        private void Appointment_Selected(object sender, RoutedEventArgs e)
        {
            tbEmployeeMainMessage.Text = "";
            tbPatientMainMessage.Text = "";
            FillDataForPatientAppointment();
            //if (patient.PatientId != null)
            //{
            //    FillDataForSearchPatientAppointment();
            //}

        }

        private void btnViewPatient_Click(object sender, RoutedEventArgs e)
        {
            ViewAppointmentDetailsForm viewAppointmentForm = new ViewAppointmentDetailsForm(this);
            Doctor d = new Doctor();
            Patient p = new Patient();
            Appointment a = new Appointment();

            var row = (DataRowView)grdPatientAppointmentList.SelectedItem;
            if (row != null)
            {
                patient.PatientId = row["patientId"].ToString();
                patient.MedicareNo = row["medicareNo"].ToString();
                ((IContact)patient).ContactNo = row["contactNo"].ToString();
                ((IContact)patient).EmailAddress = row["emailAddress"].ToString();

                foreach (DataRow dr in appointment.getAppointmentRecords(patient.PatientId).Rows)
                {
                    p.PatientId = dr["patientId"].ToString();
                    a.ApptPatientName = dr["patientName"].ToString();
                    p.MedicareNo = patient.MedicareNo;
                    ((IContact)p).ContactNo = ((IContact)patient).ContactNo;
                    ((IContact)p).EmailAddress = ((IContact)patient).EmailAddress;
                }
                viewAppointmentForm.viewAppointmentDetails(a, p);
                viewAppointmentForm.Show();
                this.Hide();
            }
            else
            {
                tbAppointmentMainMessage.Text = "Please highlight a patient to view the patient's appointment records!";
                tbAppointmentMainMessage.Foreground = Brushes.Red;
            }

        }

        private void btnAddPatient_Click(object sender, RoutedEventArgs e)
        {
            PatientProcessingForm patientForm = new PatientProcessingForm(this);
            patientForm.Show();
            patientForm.viewPatientForm();
            this.Hide();
        }

        private void btnAddMedicalRecords_Click(object sender, RoutedEventArgs e)
        {
            patientsMedicalRecords();
        }

        private void btnDeletePatient_Click(object sender, RoutedEventArgs e)
        {

            var row = (DataRowView)grdPatientList.SelectedItem;

            if (row != null)
            {
                patient.PatientId = row["patientId"].ToString();
                DialogResult result = System.Windows.Forms.MessageBox.Show("Do you want to delete patient id # " + patient.PatientId + "?", "Delete Patient?", MessageBoxButtons.YesNo);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    tbPatientMainMessage.Text = patient.deletePatientRecord(patient.PatientId);
                    tbPatientMainMessage.Foreground = Brushes.Green;
                    row.Delete();
                    FillDataForPatients();
                    
                }
                else
                {
                    tbPatientMainMessage.Text = "";
                }
                grdPatientList.Visibility = Visibility.Visible;
            }
            else
            {
                tbPatientMainMessage.Text = "Please highlight a patient to delete in the list below!";
                tbPatientMainMessage.Foreground = Brushes.Red;
            }
        }

        public void FillDataForPatientAppointment()
        {
            DataTable dt = new DataTable();
            Patient p = new Patient();

          //  dt = patient.getPatientAppointmentList();
            dt= p.getPatientIntoAppointmentList();

            DataTable finalDataTable = new DataTable();

            if (dt.Rows.Count > 0)
            {
                finalDataTable.Columns.Add("patientId", typeof(System.Int32));
                finalDataTable.Columns.Add("firstName", typeof(System.String));
                finalDataTable.Columns.Add("lastName", typeof(System.String));
                finalDataTable.Columns.Add("medicareNo", typeof(System.String));
                finalDataTable.Columns.Add("contactNo", typeof(System.String));
                finalDataTable.Columns.Add("emailAddress", typeof(System.String));

                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = finalDataTable.NewRow();

                    newRow[0] = row[0];
                    newRow[1] = row[1];
                    newRow[2] = row[2];

                    newRow[3] = row[3];
                    newRow[4] = row[4];
                    newRow[5] = row[5];

                    finalDataTable.Rows.Add(newRow);
                }

                if (finalDataTable.Rows.Count > 0)
                {
                    grdPatientAppointmentList.ItemsSource = finalDataTable.DefaultView;
                }
                else
                {
                    tbAppointmentMainMessage.Text = "No record/s found.";
                    tbAppointmentMainMessage.Foreground = Brushes.Red;
                    grdPatientAppointmentList.Visibility = Visibility.Hidden;
                }
                //KTA
                //if(txtSearchAppointment.Text == null)
                //{
                //    tbAppointmentMainMessage.Text = "";
                //}
            }
            else
            {
                tbAppointmentMainMessage.Text = "Currently there are no patient's registered.";
                //txtSearchAppointment.IsReadOnly = Constants.ISTRUE;
                tbAppointmentMainMessage.Foreground = Brushes.Red;
                grdPatientAppointmentList.Visibility = Visibility.Hidden;


            }

        }

        //KTA
        //public void FillDataForSearchPatientAppointment()
        //{

        //    DataTable dt = new DataTable();
        //    if (!string.IsNullOrEmpty(txtSearchAppointment.Text.Trim()))
        //    {
        //        int searchid = 0;
        //        Boolean isNumeric = Int32.TryParse(txtSearchAppointment.Text.Trim(), out searchid);
        //        if (isNumeric == false)
        //        {
        //            showErrorFields(txtSearchAppointment, Constants.ISTRUE);
        //            tbAppointmentMainMessage.Text = "Invalid Search Patient Id! Please type a number.";
        //            tbAppointmentMainMessage.Foreground = Brushes.Red;
        //            return;
        //        }
        //        else
        //        {
        //            showErrorFields(txtSearchAppointment, false);
        //            tbAppointmentMainMessage.Text = "";
        //        }

        //        dt = patient.getPatientAppointmentList(txtSearchAppointment.Text);

        //        DataTable finalDataTable = new DataTable();

        //        if (dt.Rows.Count > 0)
        //        {
        //            finalDataTable.Columns.Add("patientId", typeof(System.Int32));
        //            finalDataTable.Columns.Add("firstName", typeof(System.String));
        //            finalDataTable.Columns.Add("lastName", typeof(System.String));
        //            finalDataTable.Columns.Add("medicareNo", typeof(System.String));
        //            finalDataTable.Columns.Add("contactNo", typeof(System.String));
        //            finalDataTable.Columns.Add("emailAddress", typeof(System.String));

        //            foreach (DataRow row in dt.Rows)
        //            {
        //                DataRow newRow = finalDataTable.NewRow();
        //                newRow[0] = row[0];
        //                newRow[1] = row[1];
        //                newRow[2] = row[2];

        //                newRow[3] = row[3];
        //                newRow[4] = row[4];
        //                newRow[5] = row[5];

        //                finalDataTable.Rows.Add(newRow);
        //            }

        //            if (finalDataTable.Rows.Count > 0)
        //            {
        //                grdPatientAppointmentList.ItemsSource = finalDataTable.DefaultView;
        //            }

        //        }
        //        else
        //        {
        //            tbAppointmentMainMessage.Text = "Patient " + txtSearchAppointment.Text + " is not found in the list.";
        //            tbAppointmentMainMessage.Foreground = Brushes.Red;
        //           // txtSearchAppointment.IsReadOnly = Constants.ISTRUE;
        //            FillDataForPatientAppointment();
        //        }
        //    }
        //}



        public void FillDataForPatients()
        {

            DataTable dt = new DataTable();
            dt = patient.getPatients();

            DataTable finalDataTable = new DataTable();

            if (dt.Rows.Count > 0)
            {
                finalDataTable.Columns.Add("patientId", typeof(System.Int32));
                finalDataTable.Columns.Add("firstName", typeof(System.String));
                finalDataTable.Columns.Add("lastName", typeof(System.String));
                finalDataTable.Columns.Add("dob", typeof(System.DateTime));
                finalDataTable.Columns.Add("age", typeof(System.Int32));
                finalDataTable.Columns.Add("gender", typeof(System.String));
                finalDataTable.Columns.Add("address1", typeof(System.String));
                finalDataTable.Columns.Add("suburb", typeof(System.String));
                finalDataTable.Columns.Add("state", typeof(System.String));
                finalDataTable.Columns.Add("postcode", typeof(System.String));
                finalDataTable.Columns.Add("country", typeof(System.String));
                finalDataTable.Columns.Add("medicareNo", typeof(System.String));
                finalDataTable.Columns.Add("recordCreated", typeof(System.DateTime));
                finalDataTable.Columns.Add("recordUpdated", typeof(System.DateTime));
                finalDataTable.Columns.Add("contactType", typeof(System.String));
                finalDataTable.Columns.Add("contactNo", typeof(System.String));
                finalDataTable.Columns.Add("emailAddress", typeof(System.String));
                finalDataTable.Columns.Add("emergencyContactName", typeof(System.String));
                finalDataTable.Columns.Add("emergencyContactNo", typeof(System.String));
                finalDataTable.Columns.Add("relationship", typeof(System.String));

                foreach (DataRow row in dt.Rows)
                {
                    DataRow newRow = finalDataTable.NewRow();
                    
                    newRow[0] = row[0];
                    if (!string.IsNullOrEmpty(row[1].ToString()))
                    {
                        newRow[1] = row[1];
                    }
                    else
                    {
                        newRow[1] = null;
                    }
                    if (!string.IsNullOrEmpty(row[2].ToString()))
                    {
                        newRow[2] = row[2];
                    }
                    else
                    {
                        newRow[2] = null;
                    }

                    if (!string.IsNullOrEmpty(row[3].ToString()))
                    {
                        DateTime birthDate;
                        string dateString = row[3].ToString();
                        bool isDate = DateTime.TryParse(dateString, out birthDate);

                        if (isDate == Constants.ISTRUE)
                        {
                            newRow[3] = birthDate.ToShortDateString();
                        }
                        else
                        {
                            newRow[3] = null;
                        }
                    }

                    if (!string.IsNullOrEmpty(row[4].ToString()))
                    {
                        newRow[4] = row[4];
                    }

                    if (Convert.ToString(row[5]).Equals("Female"))
                    {
                        newRow[5] = "Female";
                    }
                    else if (Convert.ToString(row[5]).Equals("Male"))
                    {
                        newRow[5] = "Male";
                    }
                    else
                    {
                        newRow[5] = "Decline To State";
                    }

                    newRow[6] = row[6];
                    newRow[7] = row[7];
                    newRow[8] = row[8];
                    newRow[9] = row[9];
                    newRow[10] = row[10];
                    newRow[11] = row[11];
                    newRow[12] = row[12];
                    newRow[13] = row[13];
                    newRow[14] = row[14];

                    if (Convert.ToString(row[14]).Equals("Home"))
                    {
                        newRow[14] = "Home";
                    }
                    else if (Convert.ToString(row[14]).Equals("Office"))
                    {
                        newRow[14] = "Office";
                    }
                    else
                    {
                        newRow[14] = "mobile";
                    }

                    newRow[15] = row[15];
                    newRow[16] = row[16];
                    newRow[17] = row[17];
                    newRow[18] = row[18];
                    newRow[19] = row[19];

                    finalDataTable.Rows.Add(newRow);
                }

                if (finalDataTable.Rows.Count > 0)
                {
                    grdPatientList.ItemsSource = finalDataTable.DefaultView;
                }
                else
                {
                    tbPatientMainMessage.Text = "No record/s found.";
                    tbPatientMainMessage.Foreground = Brushes.Red;
                    grdPatientList.Visibility = Visibility.Hidden;
                }

            }
            else
            {
                tbPatientMainMessage.Text = "Currently, there are no record of patients found.";
                tbPatientMainMessage.Foreground = Brushes.Red;
                grdPatientList.Visibility = Visibility.Hidden;
            }
        }


        private void gridPatientList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = (DataRowView)grdPatientList.SelectedItem;
            
            Patient p = new Patient();
            patient.PatientId = row["patientId"].ToString();

            foreach (DataRow dr in patient.getPatientDetails(patient.PatientId).Rows)
            {
                p.PatientId = dr["patientId"].ToString();
                p.FirstName = dr["firstName"].ToString();
                p.LastName = dr["lastName"].ToString();
                p.DOB = Convert.ToDateTime(dr["dob"]);
                p.Age = Convert.ToInt32(dr["age"]);
                p.Gender = dr["gender"].ToString();
                p.AddressLine1 = dr["address1"].ToString();
                p.Suburb = dr["suburb"].ToString();
                p.State = dr["state"].ToString();
                p.PostCode = dr["postcode"].ToString();
                p.Country = dr["country"].ToString();
                p.MedicareNo = dr["medicareNo"].ToString();
                ((IContact)p).ContactType = dr["contactType"].ToString();
                ((IContact)p).ContactNo = dr["contactNo"].ToString();
                ((IContact)p).EmailAddress = dr["emailAddress"].ToString();
                p.EmergencyContactName = dr["emergencyContactName"].ToString();
                p.EmergencyContactNo = dr["emergencyContactNo"].ToString();
                p.Relationship = dr["relationship"].ToString();
            }
            PatientProcessingForm patientForm = new PatientProcessingForm(this);
            patientForm.Show();
            patientForm.viewPatientDetails(p);
            this.Hide();
        }

        private void gridEmployeeList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EmployeeProcessingForm employeeForm = new EmployeeProcessingForm(this);
            var row = (DataRowView)grdEmployeeList.SelectedItem;

            Employee emp = new Employee();
            Doctor doc = new Doctor();
            employee.EmployeeId = row["employeeId"].ToString();
            employee.JobTitle = row["jobTitle"].ToString();

            isAddEmployeeSelected = Constants.ISFALSE;
            foreach (DataRow dr in employee.getEmployeeDetails(employee.EmployeeId).Rows)
            {
                emp.JobTitle = employee.JobTitle;
                employeeDetails(emp, dr);

            }
            if (employee.JobTitle.Equals("Doctor"))
            {
                foreach (DataRow drow in doctor.getDoctorDetails(employee.EmployeeId).Rows)
                {
                    doc.DoctorId = drow["doctorId"].ToString();
                    doc.DoctorLicenseNo = drow["doctorLicenseNo"].ToString();
                    doc.Specialisation = drow["specialty"].ToString();
                    doc.BuildingLocation = drow["buildingLocation"].ToString();
                    doc.RoomNumber = drow["roomNumber"].ToString();
                }
                employeeForm.viewEmployeeDetails(emp, doc);
            }
            else
            {
                employeeForm.viewEmployeeDetails(emp, null);
            }
            employeeForm.Show();
            this.Hide();
        }

        private void employeeDetails(Employee emp, DataRow dr)
        {
            emp.EmployeeId = dr["employeeId"].ToString();
            emp.FirstName = dr["firstName"].ToString();
            emp.LastName = dr["lastName"].ToString();
            emp.DOB = Convert.ToDateTime(dr["dob"]);
            emp.Age = Convert.ToInt32(dr["age"]);
            emp.Gender = dr["gender"].ToString();
            emp.AddressLine1 = dr["address1"].ToString();
            emp.Suburb = dr["suburb"].ToString();
            emp.State = dr["state"].ToString();
            emp.PostCode = dr["postcode"].ToString();
            emp.Country = dr["country"].ToString();
            emp.MedicareNo = dr["medicareNo"].ToString();
            ((IContact)emp).ContactType = dr["contactType"].ToString();
            ((IContact)emp).ContactNo = dr["contactNo"].ToString();
            ((IContact)emp).EmailAddress = dr["emailAddress"].ToString();
            emp.EmergencyContactName = dr["emergencyContactName"].ToString();
            emp.EmergencyContactNo = dr["emergencyContactNo"].ToString();
            emp.Relationship = dr["relationship"].ToString();
            emp.CompanyName = dr["companyName"].ToString();
            emp.CompanyAddress = dr["companyAddress"].ToString();
            emp.Position = dr["position"].ToString();
            // emp.JobTitle = dr["jobTitle"].ToString();
            emp.EmployeeStatus = dr["employeeStatus"].ToString();
            emp.Department = dr["department"].ToString();
            emp.HireDate = Convert.ToDateTime(dr["hireDate"]);
            emp.EmploymentType = dr["employmentType"].ToString();
            emp.IncomeType = dr["incomeType"].ToString();
            emp.Income = dr["incomeAmount"].ToString();
            emp.NoOfHoursWorked = Convert.ToInt32(dr["hoursWorked"]);
            emp.StartTime = dr["startTime"].ToString();
        }

        private void btnAddAppointment_Click(object sender, RoutedEventArgs e)
        {
            AppointmentProcessingForm appointmentForm = new AppointmentProcessingForm(this);
            
            var row = (DataRowView)grdPatientAppointmentList.SelectedItem;

            if (row != null)
            {
                appointment.patient.PatientId = row["patientId"].ToString();
                appointment.patient.FirstName = row["firstName"].ToString();
                appointment.patient.LastName = row["lastName"].ToString();
                appointment.patient.MedicareNo = row["medicareNo"].ToString();
                ((IContact)appointment.patient).ContactNo = row["contactNo"].ToString();
                ((IContact)appointment.patient).EmailAddress = row["emailAddress"].ToString();

                appointmentForm.Show();
                appointmentForm.viewAppointmentForm(appointment);
                this.Hide();
            }
            else
            {
                tbAppointmentMainMessage.Text = "Please highlight a patient to create an appointment in the list below!";
                tbAppointmentMainMessage.Foreground = Brushes.Red;
            }


        }

        private void btnViewAppointment_Click(object sender, RoutedEventArgs e)
        {
            ViewAppointmentDetailsForm viewAppointmentForm = new ViewAppointmentDetailsForm(this);
            Doctor d = new Doctor();
            Patient p = new Patient();
            Appointment a = new Appointment();

            var row = (DataRowView)grdPatientAppointmentList.SelectedItem;
            if (row != null)
            {
                patient.PatientId = row["patientId"].ToString();
                patient.MedicareNo = row["medicareNo"].ToString();
                ((IContact)patient).ContactNo = row["contactNo"].ToString();
                ((IContact)patient).EmailAddress = row["emailAddress"].ToString();

                foreach (DataRow dr in appointment.getAppointmentRecords(patient.PatientId).Rows)
                {
                    p.PatientId = dr["patientId"].ToString();
                    a.ApptPatientName = dr["patientName"].ToString();
                    p.MedicareNo = patient.MedicareNo;
                    ((IContact)p).ContactNo = ((IContact)patient).ContactNo;
                    ((IContact)p).EmailAddress = ((IContact)patient).EmailAddress;
                }
                if (p.PatientId != null)
                {
                    viewAppointmentForm.viewAppointmentDetails(a, p);
                    viewAppointmentForm.Show();
                    this.Hide();
                }
                else
                {
                    tbAppointmentMainMessage.Text = "Patient ID "+ patient.PatientId + " has no appointment record/s! Please create one to view it.";
                    tbAppointmentMainMessage.Foreground = Brushes.Red;
                }
            }
            else
            {
                tbAppointmentMainMessage.Text = "Please highlight a patient to view the patient's appointment records!";
                tbAppointmentMainMessage.Foreground = Brushes.Red;
            }
        }

        private void btnViewMedicalRecords_Click(object sender, RoutedEventArgs e)
        {
            var row = (DataRowView)grdPatientList.SelectedItem;

            if (row != null)
            {
                Patient p = new Patient();
                Appointment a = new Appointment();

                patient.PatientId = row["patientId"].ToString();

                foreach (DataRow dr in patient.getPatientDetailsForRecords(patient.PatientId).Rows)
                {
                    p.PatientId = dr["patientId"].ToString();
                    a.ApptPatientName = dr["patientName"].ToString();
                    p.Age = Convert.ToInt32(dr["age"].ToString());
                    p.Gender = dr["gender"].ToString();
                    p.MedicareNo = dr["medicareNo"].ToString();
                    p.DOB = Convert.ToDateTime(dr["dob"].ToString());
                    ((IContact)p).ContactNo = dr["contactNo"].ToString();
                    p.EmergencyContactName = dr["emergencyContactName"].ToString();
                    p.EmergencyContactNo = dr["emergencyContactNo"].ToString();
                    p.Relationship = dr["relationship"].ToString();
                }

                if (string.IsNullOrEmpty(p.PatientId))
                {
                    tbPatientMainMessage.Text = "No medical records for Patient ID " + patient.PatientId + ".";
                    tbPatientMainMessage.Foreground = Brushes.Red;
                    return;
                }

                ViewPatientMedicalRecords viewMedicalForm = new ViewPatientMedicalRecords(this);
                viewMedicalForm.Show();
                viewMedicalForm.viewPatientsMedicalRecords(p, a);
                this.Hide();
            }
            else
            {
                tbPatientMainMessage.Text = "Please highlight a patient from the list to view the Medical Records!";
                tbPatientMainMessage.Foreground = Brushes.Red;
            }
        }

        private void patientsMedicalRecords()
        {
            var row = (DataRowView)grdPatientList.SelectedItem;

            if (row != null)
            {
                Patient p = new Patient();
                Appointment a = new Appointment();

                patient.PatientId = row["patientId"].ToString();

                foreach (DataRow dr in patient.getPatientDetailsForRecords(patient.PatientId).Rows)
                {
                    p.PatientId = dr["patientId"].ToString();
                    a.ApptPatientName = dr["patientName"].ToString();
                    p.Age = Convert.ToInt32(dr["age"].ToString());
                    p.Gender = dr["gender"].ToString();
                    p.MedicareNo = dr["medicareNo"].ToString();
                    p.DOB = Convert.ToDateTime(dr["dob"].ToString());
                    ((IContact)p).ContactNo = dr["contactNo"].ToString();
                    p.EmergencyContactName = dr["emergencyContactName"].ToString();
                    p.EmergencyContactNo = dr["emergencyContactNo"].ToString();
                    p.Relationship = dr["relationship"].ToString();
                }

                if (string.IsNullOrEmpty(p.PatientId))
                {
                    tbPatientMainMessage.Text = "Patient ID " + patient.PatientId + " has not made any consultations yet. Please arrange an appointment first!";
                    tbPatientMainMessage.Foreground = Brushes.Red;
                    return;
                }

                MedicalRecordsProcessingForm medicalForm = new MedicalRecordsProcessingForm(this);
                medicalForm.Show();
                medicalForm.viewMedicalForm(p, a);
                this.Hide();
            }
            else
            {
                tbPatientMainMessage.Text = "Please highlight a patient from the list to add the patient's Medical Records!";
                tbPatientMainMessage.Foreground = Brushes.Red;
            }
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeProcessingForm employeeForm = new EmployeeProcessingForm(this);
            employeeForm.Show();
            isAddEmployeeSelected = Constants.ISTRUE;
            employeeForm.viewEmployeeForm();
            this.Hide();
        }
        private void btnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            var row = (DataRowView)grdEmployeeList.SelectedItem;

            if (row != null)
            {
                employee.EmployeeId = row["employeeId"].ToString();

                DialogResult result = System.Windows.Forms.MessageBox.Show("Do you want to delete employee id # " + employee.EmployeeId + "?", "Delete Employee?", MessageBoxButtons.YesNo);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    tbEmployeeMainMessage.Text = employee.deleteEmployeeRecord(employee.EmployeeId);
                    tbEmployeeMainMessage.Foreground = Brushes.Green;
                    row.Delete();
                    FillDataForEmployees();
                }
                else
                {
                    tbEmployeeMainMessage.Text = "";
                }
                grdEmployeeList.Visibility = Visibility.Visible;
            }
            else
            {
                tbEmployeeMainMessage.Text = "Please highlight an employee to delete in the list below!";
                tbEmployeeMainMessage.Foreground = Brushes.Red;
            }

        }

        private void addPatientIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgAddPatient.IsMouseOver == Constants.ISTRUE)
            {
                imgAddPatient.Opacity = 1.0;
            }
        }

        private void addPatientIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgAddPatient.IsMouseOver == Constants.ISFALSE)
            {
                imgAddPatient.Opacity = 0.6;
            }
        }

        internal void FillDataForEmployees()
        {
            DataTable dt = new DataTable();
            dt = employee.getEmployees();

            DataTable finalDataTable = new DataTable();

            if (dt.Rows.Count > 0)
            {
                finalDataTable.Columns.Add("employeeId", typeof(System.Int32));
                finalDataTable.Columns.Add("firstName", typeof(System.String));
                finalDataTable.Columns.Add("lastName", typeof(System.String));
                finalDataTable.Columns.Add("dob", typeof(System.DateTime));
                finalDataTable.Columns.Add("age", typeof(System.Int32));
                finalDataTable.Columns.Add("gender", typeof(System.String));
                finalDataTable.Columns.Add("address1", typeof(System.String));
                finalDataTable.Columns.Add("suburb", typeof(System.String));
                finalDataTable.Columns.Add("state", typeof(System.String));
                finalDataTable.Columns.Add("postcode", typeof(System.String));
                finalDataTable.Columns.Add("country", typeof(System.String));
                finalDataTable.Columns.Add("medicareNo", typeof(System.String));
                finalDataTable.Columns.Add("recordCreated", typeof(System.DateTime));
                finalDataTable.Columns.Add("recordUpdated", typeof(System.DateTime));
                finalDataTable.Columns.Add("contactType", typeof(System.String));
                finalDataTable.Columns.Add("contactNo", typeof(System.String));
                finalDataTable.Columns.Add("emailAddress", typeof(System.String));
                finalDataTable.Columns.Add("emergencyContactName", typeof(System.String));
                finalDataTable.Columns.Add("emergencyContactNo", typeof(System.String));
                finalDataTable.Columns.Add("relationship", typeof(System.String));
                finalDataTable.Columns.Add("companyName", typeof(System.String));
                finalDataTable.Columns.Add("companyAddress", typeof(System.String));
                finalDataTable.Columns.Add("position", typeof(System.String));
                finalDataTable.Columns.Add("jobTitle", typeof(System.String));
                finalDataTable.Columns.Add("employeeStatus", typeof(System.String));
                finalDataTable.Columns.Add("department", typeof(System.String));
                finalDataTable.Columns.Add("hireDate", typeof(System.DateTime));
                finalDataTable.Columns.Add("employmentType", typeof(System.String));
                finalDataTable.Columns.Add("incomeType", typeof(System.String));
                finalDataTable.Columns.Add("incomeAmount", typeof(System.String));
                finalDataTable.Columns.Add("hoursWorked", typeof(System.Int32));
                finalDataTable.Columns.Add("startTime", typeof(System.TimeSpan));

                foreach (DataRow row in dt.Rows)
                {

                    DataRow newRow = finalDataTable.NewRow();


                    newRow[0] = row[0];
                    if (!string.IsNullOrEmpty(row[1].ToString()))
                    {
                        newRow[1] = row[1];
                    }
                    else
                    {
                        newRow[1] = null;
                    }
                    if (!string.IsNullOrEmpty(row[2].ToString()))
                    {
                        newRow[2] = row[2];
                    }
                    else
                    {
                        newRow[2] = null;
                    }

                    if (!string.IsNullOrEmpty(row[3].ToString()))
                    {
                        DateTime birthDate;
                        string dateString = row[3].ToString();
                        bool isDate = DateTime.TryParse(dateString, out birthDate);

                        if (isDate == Constants.ISTRUE)
                        {
                            newRow[3] = birthDate.ToShortDateString();
                        }
                        else
                        {
                            newRow[3] = null;
                        }
                    }

                    if (!string.IsNullOrEmpty(row[4].ToString()))
                    {
                        newRow[4] = row[4];
                    }

                    if (Convert.ToString(row[5]).Equals("Female"))
                    {
                        newRow[5] = "Female";
                    }
                    else if (Convert.ToString(row[5]).Equals("Male"))
                    {
                        newRow[5] = "Male";
                    }
                    else
                    {
                        newRow[5] = "Decline To State";
                    }

                    newRow[6] = row[6];
                    newRow[7] = row[7];
                    newRow[8] = row[8];
                    newRow[9] = row[9];
                    newRow[10] = row[10];
                    newRow[11] = row[11];
                    newRow[12] = row[12];
                    newRow[13] = row[13];
                    newRow[14] = row[14];

                    if (Convert.ToString(row[14]).Equals("Home"))
                    {
                        newRow[14] = "Home";
                    }
                    else if (Convert.ToString(row[14]).Equals("Office"))
                    {
                        newRow[14] = "Office";
                    }
                    else
                    {
                        newRow[14] = "mobile";
                    }
                    newRow[15] = row[15];
                    newRow[16] = row[16];
                    newRow[17] = row[17];
                    newRow[18] = row[18];
                    newRow[19] = row[19];
                    newRow[20] = row[20];
                    newRow[21] = row[21];
                    newRow[22] = row[22];
                    newRow[23] = row[23];
                    newRow[24] = row[24];
                    newRow[25] = row[25];
                    if (!string.IsNullOrEmpty(row[26].ToString()))
                    {
                        DateTime hireDate;
                        string dateString = row[26].ToString();
                        bool isDate = DateTime.TryParse(dateString, out hireDate);

                        if (isDate == Constants.ISTRUE)
                        {
                            newRow[26] = hireDate.ToShortDateString();
                        }
                        else
                        {
                            newRow[26] = null;
                        }
                    }

                    newRow[27] = row[27];
                    newRow[28] = row[28];
                    newRow[29] = row[29];
                    newRow[30] = row[30];

                    if (!string.IsNullOrEmpty(row[31].ToString()))
                    {
                        TimeSpan startWorkingTime;
                        string timeString = row[31].ToString();
                        bool isTime = TimeSpan.TryParse(timeString, out startWorkingTime);

                        if (isTime == Constants.ISTRUE)
                        {
                            newRow[26] = startWorkingTime.ToString();
                        }
                        else
                        {
                            newRow[26] = null;
                        }
                    }
                    finalDataTable.Rows.Add(newRow);
                }

                if (finalDataTable.Rows.Count > 0)
                {
                    grdEmployeeList.ItemsSource = finalDataTable.DefaultView;
                }
                else
                {
                    tbEmployeeMainMessage.Text = "No record/s found.";
                    tbEmployeeMainMessage.Foreground = Brushes.Red;
                    grdEmployeeList.Visibility = Visibility.Hidden;
                }
            }
            else
            {

                tbEmployeeMainMessage.Text = "Currently, there are no record of employees found.";
                tbEmployeeMainMessage.Foreground = Brushes.Red;
                grdEmployeeList.Visibility = Visibility.Hidden;

            }
        }
        
        internal void showErrorFields(System.Windows.Controls.TextBox field, Boolean isError)
        {
            var brushConverter = new BrushConverter();
            if (isError)
            {
                field.Background = (Brush)brushConverter.ConvertFrom("#ffccdd");
            }
            else
            {
                field.Background = Brushes.White;
            }
        }

        internal void showErrorFields(DatePicker field, Boolean isError)
        {
            var brushConverter = new BrushConverter();
            if (isError)
            {
                field.Background = (Brush)brushConverter.ConvertFrom("#ffccdd");
            }
            else
            {
                field.Background = Brushes.White;
            }
        }

        internal void showErrorFields(System.Windows.Controls.ComboBox field, Boolean isError)
        {
            var brushConverter = new BrushConverter();
            if (isError)
            {
                field.Background = (Brush)brushConverter.ConvertFrom("#ffccdd");
                field.BorderBrush = (Brush)brushConverter.ConvertFrom("#ffccdd");
            }
            else
            {
                field.Background = Brushes.White;
                field.BorderBrush = Brushes.Gray;
            }
        }

        //KTA
        //private void btnSearchPatientAppt_Click(object sender, RoutedEventArgs e)
        //{
        //    FillDataForSearchPatientAppointment();
        //}

        private void deletePatientIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgDeletePatient.IsMouseOver == Constants.ISTRUE)
            {
                imgDeletePatient.Opacity = 1.0;
            }
        }

        private void deletePatientIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgDeletePatient.IsMouseOver == Constants.ISFALSE)
            {
                imgDeletePatient.Opacity = 0.6;
            }
        }
        
        private void addEmployeeIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgAddEmployee.IsMouseOver == Constants.ISTRUE)
            {
                imgAddEmployee.Opacity = 1.0;
            }
        }

        private void addEmployeeIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgAddEmployee.IsMouseOver == Constants.ISFALSE)
            {
                imgAddEmployee.Opacity = 0.6;
            }
        }

        private void deleteEmployeeIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgDeleteEmployee.IsMouseOver == Constants.ISTRUE)
            {
                imgDeleteEmployee.Opacity = 1.0;
            }
        }

        private void deleteEmployeeIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgDeleteEmployee.IsMouseOver == Constants.ISFALSE)
            {
                imgDeleteEmployee.Opacity = 0.6;
            }
        }
        private void addAppointmentIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgAddAppointment.IsMouseOver == Constants.ISTRUE)
            {
                imgAddAppointment.Opacity = 1.0;
            }
        }

        private void addAppointmentIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgAddAppointment.IsMouseOver == Constants.ISFALSE)
            {
                imgAddAppointment.Opacity = 0.6;
            }
        }
       
        private void viewAppointmentIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgViewAppointment.IsMouseOver == Constants.ISTRUE)
            {
                imgViewAppointment.Opacity = 1.0;
            }
        }

        private void viewAppointmentIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgViewAppointment.IsMouseOver == Constants.ISFALSE)
            {
                imgViewAppointment.Opacity = 0.6;
            }
        }

        private void viewMedicalRecordsIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgViewMedicalRecords.IsMouseOver == Constants.ISTRUE)
            {
                imgViewMedicalRecords.Opacity = 1.0;
                imgYourMedicalRecordsLogo.Opacity = 1.0;
            }
        }

        private void viewMedicalRecordsIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgViewMedicalRecords.IsMouseOver == Constants.ISFALSE)
            {
                imgViewMedicalRecords.Opacity = 0.5;
                imgYourMedicalRecordsLogo.Opacity = 0.5;
            }
        }
        private void addMedicalRecordsIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgAddPatientMedicalRecords.IsMouseOver == Constants.ISTRUE)
            {
                imgAddPatientMedicalRecords.Opacity = 1.0;
            }
        }

        private void addMedicalRecordsIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgAddPatientMedicalRecords.IsMouseOver == Constants.ISFALSE)
            {
                imgAddPatientMedicalRecords.Opacity = 0.5;
            }
        }

        public static Boolean IsValidEmailAddress(string emailAdd)
        {
            var regex = new Regex(Constants.VALIDEMAIL_REGEX_PATTERN, RegexOptions.IgnoreCase);
            return regex.IsMatch(emailAdd);
        }

        private void exitApplication_onMouseDown(object sender, RoutedEventArgs e)
        {
            //Environment.Exit(0);
            System.Windows.Application.Current.Shutdown();
        }
        

    }

    internal static class Constants
    {
        public const string GENDER_FEMALE = "Female";
        public const string GENDER_MALE = "Male";
        public const string GENDER_DECLINE_TO_STATE = "Decline to State";

        public const string CONTACTTYPE_HOME = "Home";
        public const string CONTACTTYPE_OFFICE = "Office";
        public const string CONTACTTYPE_MOBILE = "Mobile";

        public const string JOBTITLE_NURSE = "Nurse";
        public const string JOBTITLE_DOCTOR = "Doctor";
        public const string JOBTITLE_RECEPTIONIST = "Receptionist";
        public const string JOBTITLE_ADMIN = "Admin";

        public const string DEPARTMENT_GENERAL = "General Department";
        public const string DEPARTMENT_SURGERY = "Surgery Department";
        public const string DEPARTMENT_NURSING = "Basic Nursing Department";
        public const string DEPARTMENT_PSYCHOLOGY = "Psychologist Department";

        public const string POSITION_EXECUTIVE = "Executive";
        public const string POSITION_STAFF = "Staff";
        public const string POSITION_ADMIN = "Admin";

        public const string EMPLOYEESTATUS_ACTIVE = "Active";
        public const string EMPLOYEESTATUS_ONLEAVE = "Inactive";
        public const string EMPLOYEESTATUS_INACTIVE = "OnLeave";

        public const string EMPLOYEETYPE_FULL = "Full-Time";
        public const string EMPLOYEETYPE_PART = "Part-Time";

        public const string MEDICALRECORDS_RBPREGNANTYES = "Yes";
        public const string MEDICALRECORDS_RBPREGNANTNO = "No";
        public const string MEDICALRECORDS_RBPREGNANTNA = "N/A";

        public const string MEDICALRECORDS_ISCHECKEDYES = "Yes";
        public const string MEDICALRECORDS_ISCHECKEDNO = "No";

        public const Boolean ISTRUE = true;
        public const Boolean ISFALSE = false;

        public const string VALIDEMAIL_REGEX_PATTERN = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

    }
}

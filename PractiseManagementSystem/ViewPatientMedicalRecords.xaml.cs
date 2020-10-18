using PractiseManagementSystem.Domain_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PractiseManagementSystem
{
    /// <summary>
    /// Interaction logic for ViewPatientMedicalRecords.xaml
    /// </summary>
    public partial class ViewPatientMedicalRecords : Window
    {
        MainContentPage mainPage = null;
        Patient patient = new Patient();
        Appointment appointment = new Appointment();

        MedicalRecords medicalRecord = new MedicalRecords();

        public ViewPatientMedicalRecords()
        {
            InitializeComponent();
        }
        public ViewPatientMedicalRecords(MainContentPage objMainPage)
        {
            mainPage = objMainPage;
            InitializeComponent();
        }


        internal void viewPatientsMedicalRecords(Patient p, Appointment a)
        {
                txtMRPatientId.Text = p.PatientId;
                txtMRPatientName.Text = a.ApptPatientName;
                txtMRPatientAge.Text = p.Age.ToString();
                txtMRPatientGender.Text = p.Gender;
                txtMRPatientMedicare.Text = p.MedicareNo;
                txtMRPatientContact.Text = ((IContact)p).ContactNo;
                txtMRPatientDOB.Text = p.DOB.ToShortDateString();
                txtMRPatientERName.Text = p.EmergencyContactName;
                txtMRPatientERContact.Text = p.EmergencyContactNo;
                txtMRERRelationship.Text = p.Relationship;
                FillDataForPatientMedicalRecords(txtMRPatientId.Text);

        }

        public void FillDataForPatientMedicalRecords(string patientId)
        {
            DataTable dt = new DataTable();
            
                int filterdoctorId = 0;

                Boolean isNumeric = Int32.TryParse(txtFilterByDoctor.Text.Trim(), out filterdoctorId);

                if (!string.IsNullOrEmpty(txtFilterByDoctor.Text.Trim()))
                {
                    if (isNumeric == Constants.ISTRUE)
                    {
                        dt = medicalRecord.retrievePatientMedicalRecordsFilterByDoctor(patientId, filterdoctorId);
                    }
                    else
                    {
                    lblViewMRMessage.Content = "Invalid Doctor Id entered. Please try again.";
                    mainPage.showErrorFields(txtFilterByDoctor, Constants.ISTRUE);
                    }
                }
                else
                {
                    dt = medicalRecord.retrievePatientMedicalRecords(patientId);
                    lblViewMRMessage.Content = "";
                    mainPage.showErrorFields(txtFilterByDoctor, Constants.ISFALSE);
            }

            DataTable finalDataTable = new DataTable();

            if (dt.Rows.Count > 0)
            {
                finalDataTable.Columns.Add("consultationDate", typeof(System.DateTime));
                finalDataTable.Columns.Add("assignedDoctor", typeof(System.String));
                finalDataTable.Columns.Add("doctorId", typeof(System.Int32));
                finalDataTable.Columns.Add("chiefComplaint", typeof(System.String));
                finalDataTable.Columns.Add("diagnosis", typeof(System.String));
                finalDataTable.Columns.Add("prescription", typeof(System.String));
                finalDataTable.Columns.Add("medicalRecordId", typeof(System.Int32));

                foreach (DataRow dgRow in dt.Rows)
                {
                    DataRow newRow = finalDataTable.NewRow();

                    if (!string.IsNullOrEmpty(dgRow[0].ToString()))
                    {
                        DateTime consultationDate;
                        string dateString = dgRow[0].ToString();
                        bool isDate = DateTime.TryParse(dateString, out consultationDate);

                        if (isDate == Constants.ISTRUE)
                        {
                            newRow[0] = consultationDate.ToShortDateString();
                        }
                        else
                        {
                            newRow[0] = "";
                        }
                    }

                    newRow[1] = dgRow[1];
                    newRow[2] = dgRow[2];
                    newRow[3] = dgRow[3];
                    newRow[4] = dgRow[4];
                    newRow[5] = dgRow[5];
                    newRow[6] = dgRow[6];

                    finalDataTable.Rows.Add(newRow);
                }

                if (finalDataTable.Rows.Count > 0)
                {
                    grdMedicalRecordsList.ItemsSource = finalDataTable.DefaultView;
                }
                else
                {
                    lblViewMRMessage.Content = "No medical record/s found which may be cause of no doctor id when filtered.";
                    lblViewMRMessage.Foreground = Brushes.Red;
                    grdMedicalRecordsList.Visibility = Visibility.Hidden;
                }

            }
            else
            {
                lblViewMRMessage.Content = "Currently, there are no record for patient found. May be cause when doctor Id entered is invalid when filtering.";
                lblViewMRMessage.Foreground = Brushes.Red;
                grdMedicalRecordsList.Visibility = Visibility.Hidden;
            }
        }

        
        private void btnFilterRecordsByDoctorId_Click(object sender, MouseButtonEventArgs e)
        {
            FillDataForPatientMedicalRecords(txtMRPatientId.Text);
        }


        private void gridPatientsMedicalRecordsList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            updateSelectedPatientMedicalRecords();
        }

        private void updateSelectedPatientMedicalRecords()
        {
            MedicalRecordsProcessingForm medForm = new MedicalRecordsProcessingForm(this);
            MedicalRecords md = new MedicalRecords();
            Patient p = new Patient();
            Appointment a = new Appointment();
            var row = (DataRowView)grdMedicalRecordsList.SelectedItem;

            if (row != null)
            {
                medicalRecord.MedicalRecordsId = row["medicalRecordId"].ToString();
                p.PatientId = txtMRPatientId.Text;
                a.ApptPatientName = txtMRPatientName.Text;
                p.Age = Convert.ToInt32(txtMRPatientAge.Text);
                p.Gender = txtMRPatientGender.Text;
                p.MedicareNo = txtMRPatientMedicare.Text;
                ((IContact)p).ContactNo = txtMRPatientContact.Text;
                p.DOB = Convert.ToDateTime(txtMRPatientDOB.Text);
                p.EmergencyContactName = txtMRPatientERName.Text;
                p.EmergencyContactNo = txtMRPatientERContact.Text;
                p.Relationship = txtMRERRelationship.Text;

                foreach (DataRow dr in medicalRecord.getPatientMedicalRecords(medicalRecord.MedicalRecordsId).Rows)
                {
                    medicalRecord.MedicalRecordsId = dr["medicalRecordId"].ToString();
                    medicalRecord.MedDoctorId = dr["doctorId"].ToString();
                    //medicalRecord.MedEmployeeId = dr["[employeeId]"].ToString();
                    medicalRecord.MedAssignedDoctor = dr["assignedDoctor"].ToString();
                    medicalRecord.ConsultationDate = Convert.ToDateTime(dr["consultationDate"].ToString());

                    medicalRecord.BloodPressure = dr["bloodPressure"].ToString();
                    medicalRecord.BloodType = dr["bloodType"].ToString();
                    medicalRecord.Weight = Convert.ToDouble(dr["weight"].ToString());
                    medicalRecord.Height = Convert.ToDouble(dr["height"].ToString());
                    medicalRecord.FamilyHistory = dr["familyHistory"].ToString();
                    medicalRecord.PastHealthHistory = dr["pastHealthHistory"].ToString();
                    medicalRecord.IsSmoking = dr["isSmoking"].ToString();

                    medicalRecord.isPregnant = dr["isPregnant"].ToString();
                    medicalRecord.HasDiabetes = dr["hasDiabetes"].ToString();
                    medicalRecord.HasHighBP = dr["hasHighBP"].ToString();
                    medicalRecord.HasHighCholesterol = dr["hasHighCholesterol"].ToString();
                    medicalRecord.HasHeartProblem = dr["hasHeartProblem"].ToString();
                    medicalRecord.HasAsthma = dr["hasAsthma"].ToString();
                    medicalRecord.HasEpilepsy = dr["hasEpilepsy"].ToString();
                    medicalRecord.HasPneumonia = dr["hasPneumonia"].ToString();
                    medicalRecord.HasLeukemia = dr["hasLeukemia"].ToString();
                    medicalRecord.HasTB = dr["hasTB"].ToString();
                    medicalRecord.HasUlcer = dr["hasUlcer"].ToString();
                    medicalRecord.HasHepatitis = dr["hasHepatitis"].ToString();
                    medicalRecord.HasHIV = dr["hasHIV"].ToString();
                    medicalRecord.ChiefComplaint = dr["chiefComplaint"].ToString();
                    medicalRecord.PresentIllness = dr["presentIllness"].ToString();
                    medicalRecord.MedicationTaken = dr["medicationTaken"].ToString();
                    medicalRecord.Prescription = dr["prescription"].ToString();
                    medicalRecord.Diagnosis = dr["diagnosis"].ToString();
                    medicalRecord.CurrentRegimen = dr["currentRegimen"].ToString();
                }


                medForm.Show();
                medForm.viewMedicalRecordsDetailsForm(p, a, medicalRecord);
                this.Hide();
            }
            else
            {
                lblViewMRMessage.Content = "Please highlight a patient to view the patient's appointment records!";
                lblViewMRMessage.Foreground = Brushes.Red;
            }
        }

        private void filterByDoctor_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.IsUp)
            {
                if (string.IsNullOrEmpty(txtFilterByDoctor.Text))
                {
                    FillDataForPatientMedicalRecords(txtMRPatientId.Text);
                    grdMedicalRecordsList.Visibility = Visibility.Visible;
                }
            }
        }

        private void btnDeleteMedicalRecords_Click(object sender, RoutedEventArgs e)
        {
            var row = (DataRowView)grdMedicalRecordsList.SelectedItem;

            if (row != null)
            {
                medicalRecord.MedicalRecordsId = row["medicalRecordId"].ToString();

                DialogResult result = System.Windows.Forms.MessageBox.Show("Do you want to delete patient's medical record id # " + medicalRecord.MedicalRecordsId + "?", "Delete Patient Medical Record?", MessageBoxButtons.YesNo);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    lblViewMRMessage.Content = medicalRecord.deleteMedicalRecord(medicalRecord.MedicalRecordsId);
                    lblViewMRMessage.Foreground = Brushes.Green;
                    row.Delete();
                    FillDataForPatientMedicalRecords(txtMRPatientId.Text);
                }
                else
                {
                    lblViewMRMessage.Content = "";
                }
                grdMedicalRecordsList.Visibility = Visibility.Visible;
            }
            else
            {
                lblViewMRMessage.Content = "Please highlight a medical record to delete in the list below!";
                lblViewMRMessage.Foreground = Brushes.Red;
            }
        }
            

        private void deleteMedicalRecordsIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgDeleteMedicalRecords.IsMouseOver == Constants.ISTRUE)
            {
                imgDeleteMedicalRecords.Opacity = 1.0;
            }
        }

        private void deleteMedicalRecordsIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgDeleteMedicalRecords.IsMouseOver == Constants.ISFALSE)
            {
                imgDeleteMedicalRecords.Opacity = 0.6;
            }
        }

        private void homeViewMRIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgHomeViewMRBtn.IsMouseOver == Constants.ISTRUE)
            {
                imgHomeViewMRBtn.Opacity = 1.0;
            }
        }

        private void homeViewMRIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgHomeViewMRBtn.IsMouseOver == Constants.ISFALSE)
            {
                imgHomeViewMRBtn.Opacity = 0.5;
            }
        }


        private void homeViewMR_onMouseDown(object sender, RoutedEventArgs e)
        {
            this.Hide();
            mainPage.tbAppointmentMainMessage.Text = "";
            mainPage.Show();
        }

        private void exitApplication_onMouseDown(object sender, RoutedEventArgs e)
        {
            //Environment.Exit(0);
            System.Windows.Application.Current.Shutdown();
        }

        private void grdMedicalRecordsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        
    }
}

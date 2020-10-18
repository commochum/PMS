using PractiseManagementSystem.Domain_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PractiseManagementSystem
{
    /// <summary>
    /// Interaction logic for MedicalRecordsProcessingForm.xaml
    /// </summary>
    public partial class MedicalRecordsProcessingForm : Window
    {
        MainContentPage mainPage = null;
        ViewPatientMedicalRecords viewMedicalRecords = null;

        Doctor doctor = new Doctor();
        MedicalRecords medicalRecords = new MedicalRecords();

        Patient patient = new Patient();

        string message = null;
        string errorMessage = "Please enter ";

        public MedicalRecordsProcessingForm()
        {
            InitializeComponent();
        }

        public MedicalRecordsProcessingForm(MainContentPage objMainPage)
        {
            mainPage = objMainPage;
            InitializeComponent();
            rbPregnantNO.IsChecked = Constants.ISTRUE;
            cbxMRDoctorList.SelectedIndex = 0;
        }
        public MedicalRecordsProcessingForm(ViewPatientMedicalRecords objViewMedicalRecords)
        {
            viewMedicalRecords = objViewMedicalRecords;
            InitializeComponent();
            rbPregnantNO.IsChecked = Constants.ISTRUE;
            cbxMRDoctorList.Items.Clear();
            cbxMRDoctorList.SelectedIndex = 0;
        }


        internal void viewMedicalForm(Patient p, Appointment a)
        {
            resetMedicalRecordsFields();
            medicalRecords = new MedicalRecords();
            txtMedicalRecordsId.Visibility = Visibility.Hidden;
            txtMedicalRecordsId.Text = null;
            lblMedicalRecordsId.Visibility = Visibility.Hidden;
            imgAddMedicalRecords.Visibility = Visibility.Visible;
            imgResetMedicalRecords.Visibility = Visibility.Visible;
            imgUpdateMedicalRecords.Visibility = Visibility.Hidden;

            imgBackMedRecordsBtn.Visibility = Visibility.Hidden;
            imgHomeMedRecordsBtn.Visibility = Visibility.Visible;
            lblMedRecordsHeaderName.Content = "Patient Medical Records Form";

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

           // MedicalRecords patientAssignedDoctors = new MedicalRecords();
            Dictionary<string, string> patientAssignedDoctors = new Dictionary<string, string>();

            if (patient.populateDoctorsFromPatientAppointments(p.PatientId, cbxMRDoctorList) != null)
            {
                selectAssignedDoctor();
            }
        }
        private void resetMedicalRecords_onMouseDown(object sender, RoutedEventArgs e)
        {
            resetMedicalRecordsFields();
        }

        private void resetMedicalRecordsFields()
        {
            txtMRConsultDate.SelectedDate = null;
            txtBP.Text = "";
            txtWeight.Text = "";
            txtHeight.Text = "";
            txtFamilyHistory.Text = "";
            txtChiefComplaint.Text = "";
            txtPresentMedHistory.Text = "";
            txtDiagnosis.Text = "";
            txtTreatment.Text = "";
            txtPresciption.Text = "";
            cbxMRDoctorList.SelectedIndex = 0;
            txtMRConsultDate.Background = Brushes.White;
            txtBP.Background = Brushes.White;
            txtWeight.Background = Brushes.White;
            txtHeight.Background = Brushes.White;
            txtFamilyHistory.Background = Brushes.White;
            txtChiefComplaint.Background = Brushes.White;
            txtPresentMedHistory.Background = Brushes.White;
            txtDiagnosis.Background = Brushes.White;
            txtTreatment.Background = Brushes.White;
            txtPresciption.Background = Brushes.White;
            cbxMRDoctorList.Background = Brushes.White;
            txtBloodType.Text = "";
            txtMedTaken.Text = "";
            txtPastMedHistory.Text = "";
            rbPregnantNO.IsChecked = Constants.ISTRUE;
            rbPregnantYES.IsChecked = Constants.ISFALSE;
            rbPregnantNA.IsChecked = Constants.ISFALSE;

            chkIsSmoking.IsChecked = Constants.ISFALSE;
            chkHasDiabetes.IsChecked = Constants.ISFALSE;
            chkHasHighBlood.IsChecked = Constants.ISFALSE;
            chkHasHighChol.IsChecked = Constants.ISFALSE;
            chkHasHeartProb.IsChecked = Constants.ISFALSE;
            chkHasAsthma.IsChecked = Constants.ISFALSE;
            chkHasUlcer.IsChecked = Constants.ISFALSE;
            chkHasPneumonia.IsChecked = Constants.ISFALSE;
            chkHasLeukemia.IsChecked = Constants.ISFALSE;
            chkHasTB.IsChecked = Constants.ISFALSE;
            chkHasEpilepsy.IsChecked = Constants.ISFALSE;
            chkHasHIV.IsChecked = Constants.ISFALSE;
            chkHasHepatitis.IsChecked = Constants.ISFALSE;
            lblMedRecordsMessage.Content = "";


        }


        private void addMedicalRecords_onMouseDown(object sender, RoutedEventArgs e)
        {
            validatePatientMedicalRecordsDetails();

            if (errorMessage != "Please enter " && !string.IsNullOrEmpty(errorMessage))
            {
                lblMedRecordsMessage.Content += errorMessage;
                return;
            }

            message += "Patient's Medical Records " + medicalRecords.createPatientMedicalRecords();
            lblMedRecordsMessage.Foreground = Brushes.Green;

            //DataGrid dgPatientList = (DataGrid)mainPage.grdPatientList;
            //dgPatientList.Visibility = Visibility.Visible;
            //mainPage.FillDataForPatients();

            this.Hide();
            mainPage.tbPatientMainMessage.Text = message;
            mainPage.tbPatientMainMessage.Foreground = Brushes.Green;
            mainPage.Show();

        }

        private void validatePatientMedicalRecordsDetails()
        {
            
            if (errorMessage != "Please enter " && !string.IsNullOrEmpty(errorMessage))
            {
                lblMedRecordsMessage.Content = "";
                errorMessage = "Please enter ";
            }

            if (!string.IsNullOrEmpty(txtMRPatientId.Text.Trim()))
            {
                medicalRecords.MedPatientId = txtMRPatientId.Text;
                showErrorFields(txtMRPatientId, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", a Patient", "a Patient");
                showErrorFields(txtMRPatientId, Constants.ISTRUE);
            }

            if (txtMRConsultDate.SelectedDate != null)
            {
                medicalRecords.ConsultationDate = Convert.ToDateTime(txtMRConsultDate.Text);
                showErrorFields(txtMRConsultDate, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Consultation Date", "Consultation Date");
                showErrorFields(txtMRConsultDate, Constants.ISTRUE);
            }

            if (!string.IsNullOrEmpty(txtBP.Text.Trim()))
            {
                medicalRecords.BloodPressure = txtBP.Text;
                showErrorFields(txtBP, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Blood Pressure", "Blood Pressure");
                showErrorFields(txtBP, Constants.ISTRUE);
            }

            double weight = 0;

            Boolean isNumeric = Double.TryParse(txtWeight.Text, out weight);

            if (!string.IsNullOrEmpty(txtWeight.Text.Trim()))
            {
                if (isNumeric == Constants.ISTRUE)
                {
                    medicalRecords.Weight = weight;
                    showErrorFields(txtWeight, Constants.ISFALSE);
                }
                else
                {
                    showErrorMessages(", a valid Weight", "a valid Weight");
                    showErrorFields(txtWeight, Constants.ISTRUE);
                }
            }
            else
            {
                showErrorMessages(", Weight", "Weight");
                showErrorFields(txtWeight, Constants.ISTRUE);
            }

            double height = 0;

            Boolean isNumericHeight = Double.TryParse(txtHeight.Text, out height);

            if (!string.IsNullOrEmpty(txtHeight.Text.Trim()))
            {
                if (isNumericHeight == Constants.ISTRUE)
                {
                    medicalRecords.Height = height;
                    showErrorFields(txtHeight, Constants.ISFALSE);
                }
                else
                {
                    showErrorMessages(", a valid Height", "a valid Height");
                    showErrorFields(txtHeight, Constants.ISTRUE);
                }
            }
            else
            {
                showErrorMessages(", Height", "Height");
                showErrorFields(txtHeight, Constants.ISTRUE);
            }

            if (!string.IsNullOrEmpty(txtFamilyHistory.Text.Trim()))
            {
                medicalRecords.FamilyHistory = txtFamilyHistory.Text;
                showErrorFields(txtFamilyHistory, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Family History", "Family History");
                showErrorFields(txtFamilyHistory, Constants.ISTRUE);
            }

            if (!string.IsNullOrEmpty(txtChiefComplaint.Text.Trim()))
            {
                medicalRecords.ChiefComplaint = txtChiefComplaint.Text;
                showErrorFields(txtChiefComplaint, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Chief Complaint", "Chief Complaint");
                showErrorFields(txtChiefComplaint, Constants.ISTRUE);
            }
            if (!string.IsNullOrEmpty(txtPresentMedHistory.Text.Trim()))
            {
                medicalRecords.PresentIllness = txtPresentMedHistory.Text;
                showErrorFields(txtPresentMedHistory, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Present Illness", "Present Illness");
                showErrorFields(txtPresentMedHistory, Constants.ISTRUE);
            }

            if (!string.IsNullOrEmpty(txtTreatment.Text.Trim()))
            {
                medicalRecords.CurrentRegimen = txtTreatment.Text;
                showErrorFields(txtTreatment, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Current Regimen", "Current Regimen");
                showErrorFields(txtTreatment, Constants.ISTRUE);
            }
            if (!string.IsNullOrEmpty(txtPresciption.Text.Trim()))
            {
                medicalRecords.Prescription = txtPresciption.Text;
                showErrorFields(txtPresciption, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Prescription", "Prescription");
                showErrorFields(txtPresciption, Constants.ISTRUE);
            }

            if (!string.IsNullOrEmpty(cbxMRDoctorList.Text.Trim()))
            {
                medicalRecords.MedAssignedDoctor = cbxMRDoctorList.Text;
                showErrorFields(cbxMRDoctorList, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", a Doctor", "a Doctor");
                showErrorFields(cbxMRDoctorList, Constants.ISTRUE);
            }
            if (!string.IsNullOrEmpty(txtMRDoctorId.Text.Trim()))
            {
                medicalRecords.MedDoctorId = txtMRDoctorId.Text;
                showErrorFields(txtMRDoctorId, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", a Doctor", "a Doctor");
                showErrorFields(txtMRDoctorId, Constants.ISTRUE);
            }

            if (!string.IsNullOrEmpty(txtDiagnosis.Text.Trim()))
            {
                medicalRecords.Diagnosis = txtDiagnosis.Text;
                showErrorFields(txtDiagnosis, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Diagnosis", "Diagnosis");
                showErrorFields(txtDiagnosis, Constants.ISTRUE);
            }


            medicalRecords.IsSmoking = check_CheckBoxSelection(chkIsSmoking);
            medicalRecords.HasAsthma = check_CheckBoxSelection(chkHasAsthma);
            medicalRecords.HasDiabetes = check_CheckBoxSelection(chkHasDiabetes);
            medicalRecords.HasHighBP = check_CheckBoxSelection( chkHasHighBlood);
            medicalRecords.HasHighCholesterol = check_CheckBoxSelection(chkHasHighChol);
            medicalRecords.HasHepatitis = check_CheckBoxSelection(chkHasHepatitis);

            medicalRecords.HasEpilepsy = check_CheckBoxSelection(chkHasEpilepsy);
            medicalRecords.HasHeartProblem = check_CheckBoxSelection(chkHasHeartProb);
            medicalRecords.HasTB = check_CheckBoxSelection(chkHasTB);
            medicalRecords.HasUlcer = check_CheckBoxSelection(chkHasUlcer);
            medicalRecords.HasPneumonia = check_CheckBoxSelection(chkHasPneumonia);
            medicalRecords.HasLeukemia = check_CheckBoxSelection(chkHasLeukemia);
            medicalRecords.HasHIV = check_CheckBoxSelection(chkHasHIV);

            medicalRecords.BloodType = txtBloodType.Text;
            medicalRecords.PastHealthHistory = txtPastMedHistory.Text;
            medicalRecords.MedicationTaken = txtMedTaken.Text;
            medicalRecords.RecordedDate = DateTime.Now;
            medicalRecords.UpdatedDate = DateTime.Now;

            if (rbPregnantYES.IsChecked == Constants.ISTRUE)

            { medicalRecords.isPregnant = rbPregnantYES.Content.ToString(); }

            else if (rbPregnantNO.IsChecked == Constants.ISTRUE)

            { medicalRecords.isPregnant = rbPregnantNO.Content.ToString(); }

            else

            { medicalRecords.isPregnant = rbPregnantNA.Content.ToString(); }

        }

        internal void viewMedicalRecordsDetailsForm(Patient p, Appointment a, MedicalRecords mr)
        {
            imgAddMedicalRecords.Visibility = Visibility.Hidden;
            imgResetMedicalRecords.Visibility = Visibility.Visible;
            imgUpdateMedicalRecords.Visibility = Visibility.Visible;
            imgBackMedRecordsBtn.Visibility = Visibility.Visible;
            imgHomeMedRecordsBtn.Visibility = Visibility.Hidden;

            lblMedRecordsHeaderName.Content = "Patient Medical Records Update Form";
            txtMedicalRecordsId.Text = mr.MedicalRecordsId;
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
            txtDiagnosis.Text = mr.Diagnosis;
            
            Dictionary<string, string> patientAssignedDoctors = new Dictionary<string, string>();

            if (patient.populateDoctorsFromPatientAppointments(p.PatientId, cbxMRDoctorList) != null)
            {
                selectAssignedDoctor();
            }

            cbxMRDoctorList.Text = mr.MedAssignedDoctor;
            txtMRDoctorId.Text = mr.MedDoctorId;
            txtBP.Text = mr.BloodPressure;
            txtBloodType.Text = mr.BloodType;
            txtWeight.Text = mr.Weight.ToString();
            txtHeight.Text = mr.Height.ToString();
            txtFamilyHistory.Text = mr.FamilyHistory;
            txtPastMedHistory.Text = mr.PastHealthHistory;
            txtChiefComplaint.Text = mr.ChiefComplaint;
            txtPresentMedHistory.Text = mr.PresentIllness;
            txtMedTaken.Text = mr.MedicationTaken;
            txtPresciption.Text = mr.Prescription;
            txtPresciption.Text = mr.Diagnosis;
            txtTreatment.Text = mr.CurrentRegimen;
            txtMRConsultDate.Text = mr.ConsultationDate.ToShortDateString();

            chkIsSmoking.IsChecked = check_CheckBoxSelected(mr.IsSmoking);
            chkHasDiabetes.IsChecked = check_CheckBoxSelected(mr.HasDiabetes);
            chkHasDiabetes.IsChecked = check_CheckBoxSelected(mr.HasDiabetes);
            chkHasHighBlood.IsChecked = check_CheckBoxSelected(mr.HasHighBP);
            chkHasHighChol.IsChecked = check_CheckBoxSelected(mr.HasHighCholesterol);
            chkHasHeartProb.IsChecked = check_CheckBoxSelected(mr.HasHeartProblem);
            chkHasAsthma.IsChecked = check_CheckBoxSelected(mr.HasAsthma);
            chkHasEpilepsy.IsChecked = check_CheckBoxSelected(mr.HasEpilepsy);
            chkHasPneumonia.IsChecked = check_CheckBoxSelected(mr.HasPneumonia);
            chkHasLeukemia.IsChecked = check_CheckBoxSelected(mr.HasLeukemia);
            chkHasTB.IsChecked = check_CheckBoxSelected(mr.HasTB);
            chkHasUlcer.IsChecked = check_CheckBoxSelected(mr.HasUlcer);
            chkHasHepatitis.IsChecked = check_CheckBoxSelected(mr.HasHepatitis);
            chkHasHIV.IsChecked = check_CheckBoxSelected(mr.HasHIV);

            if (mr.isPregnant == Constants.MEDICALRECORDS_RBPREGNANTYES)
            {
                rbPregnantYES.IsChecked = Constants.ISTRUE;
            }
            else if (mr.isPregnant == Constants.MEDICALRECORDS_RBPREGNANTNO)
            {
                rbPregnantNO.IsChecked = Constants.ISTRUE;
            }
            else
            {
                rbPregnantNA.IsChecked = Constants.ISTRUE;
            }

        }

        private Boolean check_CheckBoxSelected(string recordValue)
        {
            Boolean flag = Constants.ISFALSE;
            if (recordValue == Constants.MEDICALRECORDS_ISCHECKEDYES)
            {
                flag = Constants.ISTRUE;
            }
            else
            {
                flag = Constants.ISFALSE;
            }

            return flag;
        }

        private string check_CheckBoxSelection(CheckBox cb)
        {
            string record = Constants.MEDICALRECORDS_ISCHECKEDNO;
            if (cb.IsChecked == Constants.ISTRUE)
            {
                cb.IsChecked = Constants.ISTRUE;
                record = Constants.MEDICALRECORDS_ISCHECKEDYES;
            }
            else
            {
                cb.IsChecked = Constants.ISFALSE;
                record = Constants.MEDICALRECORDS_ISCHECKEDNO;
            }

            return record;

        }

        private void showErrorMessages(string showMsgIfTrue, string showMsgIfFalse)
        {
            errorMessage += (errorMessage != "Please enter " && errorMessage != null) ? showMsgIfTrue : showMsgIfFalse;
            lblMedRecordsMessage.Foreground = Brushes.Red;
        }

        private void updateMedicalRecords_onMouseDown(object sender, RoutedEventArgs e)
        {
            validatePatientMedicalRecordsDetails();

            if (errorMessage != "Please enter " && !string.IsNullOrEmpty(errorMessage))
            {
                lblMedRecordsMessage.Content += errorMessage;
                return;
            }
            string selectedDoctorId = null;

            if (cbxMRDoctorList.SelectedValue != null)
            {
                selectedDoctorId = cbxMRDoctorList.SelectedValue.ToString();
            }
            else
            {
                lblMedRecordsMessage.Content = "Please check if Doctor is selected/available";
                lblMedRecordsMessage.Foreground = Brushes.Red;
                return;
            }

            message += "Medical Record Reference ID # " + txtMedicalRecordsId.Text + medicalRecords.updatePatientMedicalRecords(txtMedicalRecordsId.Text);
            viewMedicalRecords.Show();
            viewMedicalRecords.FillDataForPatientMedicalRecords(txtMRPatientId.Text);
            viewMedicalRecords.lblViewMRMessage.Content = message;
            viewMedicalRecords.lblViewMRMessage.Foreground = Brushes.Green;
            viewMedicalRecords.Show();
            this.Hide();
        }

        private void AssignedDoctor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            selectAssignedDoctor();
        }

        private void selectAssignedDoctor()
        {

            Dictionary<string, string> patientAssignedDoctors = patient.populateDoctorsFromPatientAppointments(txtMRPatientId.Text, cbxMRDoctorList);

            foreach (var pair in patientAssignedDoctors)
            {
                string key = pair.Key;
                if (cbxMRDoctorList.Text.Trim() == pair.Value)
                {
                    txtMRDoctorId.Text = pair.Key;
                }
            }
        }


        private void ConsultationDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                txtMRConsultDate.Text = "No date";
            }
            else
            {
                // ... No need to display the time.
                txtMRConsultDate.Text = date.Value.ToShortDateString();
            }
        }

        private void showErrorFields(System.Windows.Controls.TextBox field, Boolean isError)
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

        private void showErrorFields(System.Windows.Controls.TextBlock field, Boolean isError)
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

        private void showErrorFields(DatePicker field, Boolean isError)
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

        private void showErrorFields(System.Windows.Controls.ComboBox field, Boolean isError)
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


        private void addMedicalIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgAddMedicalRecords.IsMouseOver == Constants.ISTRUE)
            {
                imgAddMedicalRecords.Opacity = 1.0;
            }
        }

        private void addMedicalIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgAddMedicalRecords.IsMouseOver == Constants.ISFALSE)
            {
                imgAddMedicalRecords.Opacity = 0.6;
            }
        }

        private void homeMedRecordsIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgHomeMedRecordsBtn.IsMouseOver == Constants.ISTRUE)
            {
                imgHomeMedRecordsBtn.Opacity = 1.0;
            }
        }

        private void homeMedRecordsIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgHomeMedRecordsBtn.IsMouseOver == Constants.ISFALSE)
            {
                imgHomeMedRecordsBtn.Opacity = 0.5;
            }
        }

        private void homeMedRecords_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgHomeMedRecordsBtn.IsMouseOver == Constants.ISTRUE)
            {
                imgHomeMedRecordsBtn.Opacity = 1.0;
            }
        }

        private void homeMedRecords_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgHomeMedRecordsBtn.IsMouseOver == Constants.ISFALSE)
            {
                imgHomeMedRecordsBtn.Opacity = 0.5;
            }
        }

        private void homeMedRecords_onMouseDown(object sender, RoutedEventArgs e)
        {
            this.Hide();
            mainPage.tbPatientMainMessage.Text = "";
            mainPage.Show();
        }

        private void backMedRecordsIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgBackMedRecordsBtn.IsMouseOver == Constants.ISTRUE)
            {
                imgBackMedRecordsBtn.Opacity = 1.0;
            }
        }

        private void backMedRecordsIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgBackMedRecordsBtn.IsMouseOver == Constants.ISFALSE)
            {
                imgBackMedRecordsBtn.Opacity = 0.6;
            }
        }

        private void backMedRecords_onMouseDown(object sender, RoutedEventArgs e)
        {
            this.Hide();
            viewMedicalRecords.lblViewMRMessage.Content = "";
            viewMedicalRecords.Show();
        }

        private void exitApplication_onMouseDown(object sender, RoutedEventArgs e)
        {
            //Environment.Exit(0);
            System.Windows.Application.Current.Shutdown();
        }

    }
}

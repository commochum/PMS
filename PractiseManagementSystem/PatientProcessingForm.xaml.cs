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
    /// Interaction logic for PatientProcessingForm.xaml
    /// </summary>
    public partial class PatientProcessingForm : Window
    {
        Patient patient = new Patient();
        MainContentPage mainPage = null;

        string message = null;
        string errorMessage = "Please enter ";

        public PatientProcessingForm()
        {
            InitializeComponent();
        }

        public PatientProcessingForm(MainContentPage objMainPage)
        {
                mainPage = objMainPage;
                InitializeComponent();
        }

        internal void viewPatientDetails(Patient patient)
        {
            mainPage.tbPatientMainMessage.Text = "";
            lblPatientHeaderName.Content = "Patient Information";

            txtPatientId.Text = patient.PatientId;
            txtPatientFName.Text = patient.FirstName;
            txtPatientLName.Text = patient.LastName;
            txtPatientDOB.SelectedDate = patient.DOB;
            txtPatientAge.Text = patient.Age.ToString();
            cbxPatientGender.Text = patient.Gender;
            txtPatientAddress.Text = patient.AddressLine1;
            txtPatientPostcode.Text = patient.PostCode;
            txtPatientSuburb.Text = patient.Suburb;
            txtPatientState.Text = patient.State;
            txtPatientCountry.Text = patient.Country;
            txtPatientMedicareNo.Text = patient.MedicareNo;
            cbxPatientContactType.Text = ((IContact)patient).ContactType;
            txtPatientContactNo.Text = ((IContact)patient).ContactNo;
            txtPatientEmailAdd.Text = ((IContact)patient).EmailAddress;
            txtPatientERContactName.Text = patient.EmergencyContactName;
            txtPatientERContactNo.Text = patient.EmergencyContactNo;
            txtPatientERRelationship.Text = patient.Relationship;

            imgAddPatientBtn.Visibility = Visibility.Hidden;
            imgResetPatientBtn.Visibility = Visibility.Visible;
            imgUpdatePatientBtn.Visibility = Visibility.Visible;

        }

        private void updatePatient_onMouseDown(object sender, RoutedEventArgs e)
        {
            updatePatientDetails(txtPatientId.Text);
        }

        private void updatePatientDetails(string patientId)
        {
            lblPatientHeaderName.Content = "Patient Information";
            
            validatePatientDetails();

            if (errorMessage != "Please enter " && !string.IsNullOrEmpty(errorMessage))
            {
                lblPatientMessage.Content += errorMessage;
                return;
            }

            string message = "Patient with ID # " + patientId + patient.updatePatientRecord(patientId);

            DataGrid dgPatientList = (DataGrid)mainPage.grdPatientList;
            dgPatientList.Visibility = Visibility.Visible;
            mainPage.FillDataForPatients();

            mainPage.tbPatientMainMessage.Text = message;
            mainPage.tbPatientMainMessage.Foreground = Brushes.Green;

            this.Hide();
            mainPage.Show(); 

        }


        internal void viewPatientForm()
        {
            patient = new Patient();
            resetPatientFields();
            txtPatientId.Visibility = Visibility.Hidden;
            txtPatientId.Text = null;
            lblPatientId.Visibility = Visibility.Hidden;
            imgAddPatientBtn.Visibility = Visibility.Visible;
            imgResetPatientBtn.Visibility = Visibility.Visible;
            imgUpdatePatientBtn.Visibility = Visibility.Hidden;
            //mainPage.tbPatientMainMessage.Text = "";
            lblPatientHeaderName.Content = "Patient Form";
        }

        private void addPatient_onMouseDown(object sender, RoutedEventArgs e)
        {
            addPatient();
        }


        private void addPatient()
        {
            lblPatientHeaderName.Content = "Patient Form";

            validatePatientDetails();

            if (errorMessage != "Please enter " && !string.IsNullOrEmpty(errorMessage))
            {
                lblPatientMessage.Content += errorMessage;
                return;
            }
           
            message += "Patient" + patient.createPatientRecord();
            lblPatientMessage.Foreground = Brushes.Green;
      
            DataGrid dgPatientList = (DataGrid)mainPage.grdPatientList;
            dgPatientList.Visibility = Visibility.Visible;
            mainPage.FillDataForPatients();

            this.Hide();
            mainPage.tbPatientMainMessage.Text = message;
            mainPage.tbPatientMainMessage.Foreground = Brushes.Green;
            mainPage.Show();

        }

        private void validatePatientDetails()
        {
            if (errorMessage != "Please enter " && !string.IsNullOrEmpty(errorMessage))
            {
                lblPatientMessage.Content = "";
                errorMessage = "Please enter ";
            }

            string selectedPatientContactType = (cbxPatientContactType.SelectedValue != null ? cbxPatientContactType.SelectedValue.ToString() : Constants.CONTACTTYPE_HOME);

            if (cbxPatientContactType.SelectedIndex == 0)
            {
                selectedPatientContactType = Constants.CONTACTTYPE_HOME;
                mainPage.showErrorFields(cbxPatientContactType, Constants.ISFALSE);
            }
            else if (cbxPatientContactType.SelectedIndex == 1)
            {
                selectedPatientContactType = Constants.CONTACTTYPE_OFFICE;
                mainPage.showErrorFields(cbxPatientContactType, Constants.ISFALSE);
            }
            else if (cbxPatientContactType.SelectedIndex == 2)
            {
                selectedPatientContactType = Constants.CONTACTTYPE_MOBILE;
                mainPage.showErrorFields(cbxPatientContactType, Constants.ISFALSE);
            }
            else
            {
                mainPage.showErrorFields(cbxPatientContactType, Constants.ISTRUE);
                showErrorMessages(", Contact Type", "Contact Type");
            }

            string selectedPatientGender = (cbxPatientGender.SelectedValue != null ? cbxPatientGender.SelectedValue.ToString() : "Female");

            if (cbxPatientGender.SelectedIndex == 0)
            {
                selectedPatientGender = "Female";
                mainPage.showErrorFields(cbxPatientGender, Constants.ISFALSE);
            }
            else if (cbxPatientGender.SelectedIndex == 1)
            {
                selectedPatientGender = "Male";
                mainPage.showErrorFields(cbxPatientGender, Constants.ISFALSE);
            }
            else if (cbxPatientGender.SelectedIndex == 2)
            {
                selectedPatientGender = "Decline to State";
                mainPage.showErrorFields(cbxPatientGender, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Gender", "Gender");
                mainPage.showErrorFields(cbxPatientGender, Constants.ISTRUE);
            }

            if (!string.IsNullOrEmpty(txtPatientFName.Text.Trim()))
            {
                patient.FirstName = txtPatientFName.Text;
                mainPage.showErrorFields(txtPatientFName, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", FirstName", "FirstName");
                mainPage.showErrorFields(txtPatientFName, Constants.ISTRUE);
            }
            if (!string.IsNullOrEmpty(txtPatientLName.Text.Trim()))
            {
                patient.LastName = txtPatientLName.Text;
                mainPage.showErrorFields(txtPatientLName, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", LastName", "LastName");
                mainPage.showErrorFields(txtPatientLName, Constants.ISTRUE);
            }

            int age = 0;

            Boolean isNumeric = Int32.TryParse(txtPatientAge.Text, out age);

            if (!string.IsNullOrEmpty(txtPatientAge.Text.Trim()))
            {
                if (isNumeric == Constants.ISTRUE)
                {
                    patient.Age = age;
                    mainPage.showErrorFields(txtPatientAge, Constants.ISFALSE);
                }
                else
                {
                    showErrorMessages(", a valid Age", "a valid Age");
                    mainPage.showErrorFields(txtPatientAge, Constants.ISTRUE);
                }
            }
            else
            {
                showErrorMessages(", Age", "Age");
                mainPage.showErrorFields(txtPatientAge, Constants.ISTRUE);
            }

            if (txtPatientDOB.SelectedDate != null)
            {
                patient.DOB = Convert.ToDateTime(txtPatientDOB.Text);
                mainPage.showErrorFields(txtPatientDOB, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Date of Birth", "Date of Birth");
                mainPage.showErrorFields(txtPatientDOB, Constants.ISTRUE);
            }

            if (!string.IsNullOrEmpty(txtPatientMedicareNo.Text.Trim()))
            {
                patient.MedicareNo = txtPatientMedicareNo.Text;
                mainPage.showErrorFields(txtPatientMedicareNo, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Medicare No", "Medicare No");
                mainPage.showErrorFields(txtPatientMedicareNo, Constants.ISTRUE);
            }

            patient.Gender = cbxPatientGender.Text;
            patient.AddressLine1 = txtPatientAddress.Text;
            patient.Suburb = txtPatientSuburb.Text;
            patient.State = txtPatientState.Text;
            patient.PostCode = txtPatientPostcode.Text;
            patient.Country = txtPatientCountry.Text;
            patient.RecordCreated = DateTime.Now;
            patient.RecordUpdated = DateTime.Now;
            ((IContact)patient).ContactType = selectedPatientContactType;

            if (!string.IsNullOrEmpty(txtPatientContactNo.Text.Trim()))
            {
                ((IContact)patient).ContactNo = txtPatientContactNo.Text;
                mainPage.showErrorFields(txtPatientContactNo, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Contact No", "Contact No");
                mainPage.showErrorFields(txtPatientContactNo, Constants.ISTRUE);
            }

            if (!string.IsNullOrEmpty(txtPatientEmailAdd.Text.Trim()) && MainContentPage.IsValidEmailAddress(txtPatientEmailAdd.Text))
            {
                ((IContact)patient).EmailAddress = txtPatientEmailAdd.Text;
                mainPage.showErrorFields(txtPatientEmailAdd, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", a Valid Email Address", "Valid Email Address");
                mainPage.showErrorFields(txtPatientEmailAdd, Constants.ISTRUE);
            }

            patient.EmergencyContactName = txtPatientERContactName.Text;
            patient.EmergencyContactNo = txtPatientERContactNo.Text;
            patient.Relationship = txtPatientERRelationship.Text;

        }

        private void showErrorMessages(string showMsgIfTrue, string showMsgIfFalse)
        {
            errorMessage += (errorMessage != "Please enter " && errorMessage != null) ? showMsgIfTrue : showMsgIfFalse;
            lblPatientMessage.Foreground = Brushes.Red;
        }

        private void resetPatient_onMouseDown(object sender, RoutedEventArgs e)
        {
            resetPatientFields();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                txtPatientDOB.Text = "No date";
            }
            else
            {
                // ... No need to display the time.
                txtPatientDOB.Text = date.Value.ToShortDateString();
            }
        }

        private void resetPatientFields()
        {
            txtPatientAddress.Text = "";
            txtPatientAge.Text = "";
            txtPatientContactNo.Text = "";
            cbxPatientContactType.Text = Constants.CONTACTTYPE_HOME;
            txtPatientCountry.Text = "";
            txtPatientDOB.SelectedDate = null;
            txtPatientFName.Text = "";
            cbxPatientGender.Text = "Female";
            txtPatientLName.Text = "";
            txtPatientMedicareNo.Text = "";
            txtPatientPostcode.Text = "";
            txtPatientState.Text = "";
            txtPatientSuburb.Text = "";
            txtPatientContactNo.Text = "";
            txtPatientEmailAdd.Text = "";
            lblPatientMessage.Content = "";
            txtPatientFName.Background = Brushes.White;
            txtPatientLName.Background = Brushes.White;
            txtPatientAge.Background = Brushes.White;
            txtPatientDOB.Background = Brushes.White;
            txtPatientContactNo.Background = Brushes.White;
            txtPatientMedicareNo.Background = Brushes.White;
            txtPatientEmailAdd.Background = Brushes.White;
            txtPatientERContactName.Text = "";
            txtPatientERContactNo.Text = "";
            txtPatientERRelationship.Text = "";
        }

        private void homePatientIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgHomePatientBtn.IsMouseOver == Constants.ISTRUE)
            {
                imgHomePatientBtn.Opacity = 1.0;
            }
        }

        private void homePatientIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgHomePatientBtn.IsMouseOver == Constants.ISFALSE)
            {
                imgHomePatientBtn.Opacity = 0.5;
            }
        }

        private void homePatient_onMouseDown(object sender, RoutedEventArgs e)
        {
            this.Hide();
            mainPage.tbPatientMainMessage.Text = "";
            mainPage.Show();
        }

        private void exitApplication_onMouseDown(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

    }


}

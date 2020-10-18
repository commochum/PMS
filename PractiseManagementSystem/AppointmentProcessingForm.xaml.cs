using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;

namespace PractiseManagementSystem
{
    /// <summary>
    /// Interaction logic for AppointmentProcessingForm.xaml
    /// </summary>
    public partial class AppointmentProcessingForm : Window
    {
        ConnectionFactory connFactory = new ConnectionFactory();
        Patient patient = new Patient();
        Doctor doctor = new Doctor();
        Doctor doctorNameList = new Doctor();
        Appointment appt = new Appointment();
        MainContentPage mainPage = null;
        ViewAppointmentDetailsForm viewApptPage = null;

        ListBoxItem availableTimeItem = new ListBoxItem();

        string message = null;
        string errorMessage = "Please enter ";

        public AppointmentProcessingForm()
        {
            InitializeComponent();
        }

        public AppointmentProcessingForm(MainContentPage objMainPage)
        {
            mainPage = objMainPage;
            InitializeComponent();
            cbxApptDoctor.SelectedIndex = 0;
            txtSelectedTime.Text = "";
        }

        public AppointmentProcessingForm(ViewAppointmentDetailsForm objViewApptPage)
        {
            viewApptPage = objViewApptPage;
            InitializeComponent();
            appt.PopulateDoctorNameList(cbxApptDoctor);
        }
        
        internal void viewAppointmentDetailsForm(Appointment appointment)
        {
            lblAppointmentHeaderName.Content = "Appointment Update Form";
            txtApptId.Visibility = Visibility.Visible;
            txtApptId.Text = appointment.AppointmentId;
            txtApptDate.Text = appointment.AppointmentDate.ToShortDateString();
            txtSelectedTime.Text = Convert.ToDateTime(appointment.AppointmentTime).ToShortTimeString();
            txtApptPurpose.Text = appointment.Purpose;
            tbApptPatientId.Text = appointment.ApptPatientId;
            tbApptPatientName.Text = appointment.ApptPatientName;
            cbxApptDoctor.Text = appointment.ApptPatientDoctor;

            if (!availableTimeItem.IsSelected)
            {
                availableTimeItem.Content = txtSelectedTime.Text;
                availableTimeItem.Foreground = Brushes.DarkCyan;

            }

            imgBackApptBtn.Visibility = Visibility.Visible;
            imgHomeApptBtn.Visibility = Visibility.Hidden;
            imgAddAppt.Visibility = Visibility.Hidden;
            imgResetAppt.Visibility = Visibility.Visible;
            imgUpdateAppt.Visibility = Visibility.Visible;
        }

        private void updateAppt_onMouseDown(object sender, RoutedEventArgs e)
        {
            updateAppointment();
        }

        private void updateAppointment()
        {
            validateAppointmentDetails();

            if (errorMessage != "Please enter " && !string.IsNullOrEmpty(errorMessage))
            {
                lblAppointmentMessage.Content += errorMessage;
                return;
            }
            string selectedDoctorId = null;

            if (cbxApptDoctor.SelectedValue != null)
            {
                selectedDoctorId = cbxApptDoctor.SelectedValue.ToString().Substring(1, 4);
            }
            else
            {
                lblAppointmentMessage.Content = "Please check if Doctor is selected/available";
                lblAppointmentMessage.Foreground = Brushes.Red;
                return;
            }

            message += "Appointment with ID # " + txtApptId.Text + appt.updateAppointmentRecord(txtApptId.Text, selectedDoctorId);
            viewApptPage.Show();
            viewApptPage.FillDataForApptPatient(tbApptPatientId.Text);
            viewApptPage.lblViewApptMessage.Content = message;
            viewApptPage.lblViewApptMessage.Foreground = Brushes.Green;
            viewApptPage.Show();
            this.Hide();
        }

        internal void viewAppointmentForm(Appointment a)
        {

            appt = new Appointment();
            resetAppointmentFields();
            cbxApptDoctor.Items.Clear();
            lblAppointmentHeaderName.Content = "Appointment Form";

            tbApptPatientId.Text = a.patient.PatientId;
            tbApptPatientName.Text = a.patient.FirstName + " " + a.patient.LastName;

            if (appt.PopulateDoctorNameList(cbxApptDoctor) != null)
                {
                    doctorNameList = appt.PopulateDoctorNameList(cbxApptDoctor);
                    showDoctorDetails();
                    txtApptId.Visibility = Visibility.Hidden;
                    txtApptId.Text = null;
                    lblApptId.Visibility = Visibility.Hidden;
                }
                else
                {
                    resetAppointmentDoctorFields();
                    lblAppointmentMessage.Content = "Please add a doctor to continue with appointment.";
                    lblAppointmentMessage.Foreground = Brushes.Red;
                    return;
                }

                imgBackApptBtn.Visibility = Visibility.Hidden;
                imgHomeApptBtn.Margin = new System.Windows.Thickness(0,20,0,0);
                imgAddAppt.Visibility = Visibility.Visible;
                imgResetAppt.Visibility = Visibility.Visible;
                imgUpdateAppt.Visibility = Visibility.Hidden;
            
        }

        private void showDoctorDetails()
        {
            if(!string.IsNullOrEmpty(cbxApptDoctor.Text)) {

                lblAppointmentMessage.Content = "";
                lblAvailableTimes.Visibility = Visibility.Visible;
                lblSelectedTime.Visibility = Visibility.Visible;
                txtSelectedTime.Visibility = Visibility.Visible;
                string selectedDoctorId = cbxApptDoctor.SelectedValue.ToString().Substring(1, doctorNameList.DoctorId.Length);

                doctor = appt.PopulateSelectedDoctorValues(selectedDoctorId);

                if (selectedDoctorId.Equals(doctor.DoctorId))
                {
                    showAppointmentFormDetails();
                }
            }
            else
            {
                resetAppointmentDoctorFields();
                lblAppointmentMessage.Content = "Please add a doctor to continue with appointment.";
                lblAppointmentMessage.Foreground = Brushes.Red;
                lblAppointmentMessage.FontWeight = FontWeights.Bold;
                lblAvailableTimes.Visibility = Visibility.Hidden;
                lblSelectedTime.Visibility = Visibility.Hidden;
                txtSelectedTime.Visibility = Visibility.Hidden;
                return;
            }


        }

        private void showAppointmentFormDetails()
        {
            aListBox.Items.Clear();
           
            DateTime startTime = Convert.ToDateTime(doctor.StartTime);
            DateTime endTime = (Convert.ToDateTime(doctor.StartTime).AddHours(doctor.NoOfHoursWorked));

            if (doctor.EmployeeStatus.Equals("Active"))
            {
                tbDoctorTimeMsg.Text = "Dr. " + doctor.LastName + " is available between  " + Environment.NewLine + startTime.ToShortTimeString() + " to " + endTime.ToShortTimeString();
                tbDoctorTimeMsg.Foreground = Brushes.Green;
                tbDoctorTimeMsg.FontWeight = FontWeights.Bold;
                lblAvailableTimes.Visibility = Visibility.Visible;
                lblSelectedTime.Visibility = Visibility.Visible;
                txtSelectedTime.Visibility = Visibility.Visible;
            }
            else
            {
                tbDoctorTimeMsg.Text = "Dr. " + doctor.LastName + " is not available. " + Environment.NewLine + "You may select another doctor.";
                tbDoctorTimeMsg.Foreground = Brushes.Red;
                tbDoctorTimeMsg.FontWeight = FontWeights.Bold;
                lblAvailableTimes.Visibility = Visibility.Hidden;
                lblSelectedTime.Visibility = Visibility.Hidden;
                txtSelectedTime.Visibility = Visibility.Hidden;
                return;
            }

            //1 hour = 60 minutes divided by time interval of 20 min appointment per patients
            int noOfTimes = (doctor.NoOfHoursWorked * 60) / 20;

            string newTime = null;

            int i = 0;
            while (i <= noOfTimes-1)
            {
                if (i == 0)
                {
                    newTime = startTime.ToShortTimeString();
                }
                else if (i == 1)
                {

                    newTime = startTime.AddMinutes(20).ToShortTimeString();
                }
                else
                {

                    newTime = (DateTime.Parse(newTime)).AddMinutes(20).ToShortTimeString();
                }
                //availableTimes += newTime.Trim() + ((i < 3) ? new string(' ',20) : ((i % 4 == 0) ? Environment.NewLine : new string(' ', 20)));
               // availableTimeItem.Content = ((i < 4) ? newTime.PadRight(20) : ((i % 5 == 0) ? newTime + Environment.NewLine : newTime.PadRight(20)));
                availableTimeItem.Content = newTime.PadRight(20);
                aListBox.Items.Insert(i, availableTimeItem.Content);
                i++;
                }
            if (availableTimeItem.IsSelected)
            {
                txtSelectedTime.Text = availableTimeItem.Content.ToString();
            }
        }

        private void comboBoxExistingDoctors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            doctorNameList = appt.PopulateDoctorNameList(cbxApptDoctor);

            if (doctorNameList != null)
            {
                showDoctorDetails();
            }
            else
            {
                resetAppointmentDoctorFields();
                lblAppointmentMessage.Content = "Please add a doctor to continue with appointment!";
                return;
            }
        }
        

        private void resetAppt_onMouseDown(object sender, RoutedEventArgs e)
        {
            resetAppointmentFields();
        }
        private void addAppt_onMouseDown(object sender, RoutedEventArgs e)
        {
            addAppointment();
        }

        private void addAppointment()
        {
            validateAppointmentDetails();

            if (errorMessage != "Please enter " && !string.IsNullOrEmpty(errorMessage))
            {
                lblAppointmentMessage.Content += errorMessage;
                return;
            }
            
            string selectedDoctorId = null;

            if (cbxApptDoctor.SelectedValue != null)
            {
                selectedDoctorId = cbxApptDoctor.SelectedValue.ToString().Substring(1, 4);
            }
            else
            {
                lblAppointmentMessage.Content = "Please check if Doctor is selected/available";
                lblAppointmentMessage.Foreground = Brushes.Red;
                return;
            }

            message += "Appointment " + appt.createAppointmentRecord(selectedDoctorId);
            mainPage.tbAppointmentMainMessage.Text = message;
            mainPage.tbAppointmentMainMessage.Foreground = Brushes.Green;
            mainPage.Show();
            this.Hide();
        }

        private void validateAppointmentDetails()
        {
            if (errorMessage != "Please enter " && !string.IsNullOrEmpty(errorMessage))
            {
                lblAppointmentMessage.Content = "";
                errorMessage = "Please enter ";
            }
            if (!string.IsNullOrEmpty(tbApptPatientId.Text))
            {
                appt.ApptPatientId = tbApptPatientId.Text.ToString();
                showErrorFields(tbApptPatientId, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Valid/Registered Patient", "Valid/Registered Patient");
                showErrorFields(tbApptPatientId, Constants.ISTRUE);
            }
            if (!string.IsNullOrEmpty(tbApptPatientName.Text))
            {
                appt.ApptPatientName = tbApptPatientName.Text;
                showErrorFields(tbApptPatientName, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", or check Patient Name", "or check Patient Name");
                showErrorFields(tbApptPatientName, Constants.ISTRUE);
            }

            if (!string.IsNullOrEmpty(cbxApptDoctor.Text))
            {
                appt.ApptPatientDoctor = cbxApptDoctor.Text;
                showErrorFields(cbxApptDoctor, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", or check Doctor Details", "or check Doctor Details");
                showErrorFields(cbxApptDoctor, Constants.ISTRUE);
            }

            if (txtApptDate.SelectedDate != null)
            {
                int result = DateTime.Compare(Convert.ToDateTime(txtApptDate.SelectedDate), Convert.ToDateTime(DateTime.Now));

                if (result >= 0)
                {
                    appt.AppointmentDate = Convert.ToDateTime(txtApptDate.Text);
                    showErrorFields(txtApptDate, Constants.ISFALSE);
                } else
                {
                    showErrorMessages(", an Appointment Date later than today", "an Appointment Date later than today");
                    showErrorFields(cbxApptDoctor, Constants.ISTRUE);
                }
            }
            else
            {
                showErrorMessages(", Appointment Date", "Appointment Date");
                showErrorFields(txtApptDate, Constants.ISTRUE);
            }

            if (!string.IsNullOrEmpty(txtSelectedTime.Text))
            {
                string selecteddoctorid = cbxApptDoctor.SelectedValue.ToString().Substring(1, 4);

                List<string> selectedApptTimeList = appt.SelectedAppointmentTime(txtApptDate.Text, selecteddoctorid);
                Console.WriteLine("TIME   " + txtSelectedTime.Text);
                if (selectedApptTimeList.Contains(txtSelectedTime.Text.Trim().ToString()))
                {
                    showErrorMessages(", another TIME as this is unavailable", "another TIME as this is unavailable or taken already.");
                    showErrorFields(txtApptDate, Constants.ISTRUE);
                }
                else
                {
                    appt.AppointmentTime = Convert.ToDateTime(txtSelectedTime.Text).ToShortTimeString();
                    showErrorFields(txtSelectedTime, Constants.ISFALSE);
                }
            }
            else
            {
                showErrorMessages(", Appointment Time", "Appointment Time");
                showErrorFields(txtSelectedTime, Constants.ISTRUE);
            }

            appt.Purpose = txtApptPurpose.Text;
            appt.CreatedDate = DateTime.Now;
            appt.UpdatedDate = DateTime.Now;

        }


        private void showErrorMessages(string showMsgIfTrue, string showMsgIfFalse)
        {
            errorMessage += (errorMessage != "Please enter " && errorMessage != null) ? showMsgIfTrue : showMsgIfFalse;
            lblAppointmentMessage.Foreground = Brushes.Red;
        }

        private void resetAppointmentFields()
        {
            txtApptDate.SelectedDate = null;
            txtSelectedTime.Text = "";
            txtApptDate.Background = Brushes.White;
            txtSelectedTime.Background = Brushes.LightCyan;
            cbxApptDoctor.SelectedIndex = 0;
            txtApptPurpose.Text = "";
            lblAppointmentMessage.Content = "";
        }

        private void resetAppointmentDoctorFields()
        {
            aListBox.Items.Clear();
            cbxApptDoctor.SelectedIndex = 0;
            txtSelectedTime.Text = "";
            tbDoctorTimeMsg.Text = "";
            imgAddAppt.Visibility = Visibility.Visible;
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
                txtApptDate.Text = "No date";
            }
            else
            {
                // ... No need to display the time.
                txtApptDate.Text = date.Value.ToShortDateString();
            }
        }

        private void homeAppointmentIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgHomeApptBtn.IsMouseOver == Constants.ISTRUE)
            {
                imgHomeApptBtn.Opacity = 1.0;
            }
        }

        private void homeAppointmentIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgHomeApptBtn.IsMouseOver == Constants.ISFALSE)
            {
                imgHomeApptBtn.Opacity = 0.5;
            }
        }

        private void homeAppointment_onMouseDown(object sender, RoutedEventArgs e)
        {
            this.Hide();
            mainPage.tbAppointmentMainMessage.Text = "";
            mainPage.Show();
        }

        private void backAppointmentIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgBackApptBtn.IsMouseOver == Constants.ISTRUE)
            {
                imgBackApptBtn.Opacity = 1.0;
            }
        }

        private void backAppointmentIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgBackApptBtn.IsMouseOver == Constants.ISFALSE)
            {
                imgBackApptBtn.Opacity = 0.6;
            }
        }

        private void backAppointment_onMouseDown(object sender, RoutedEventArgs e)
        {
            this.Hide();
            viewApptPage.lblViewApptMessage.Content = "";
            viewApptPage.Show();
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

        internal void showErrorFields(System.Windows.Controls.TextBlock field, Boolean isError)
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


        private void exitApplication_onMouseDown(object sender, RoutedEventArgs e)
        {
            //Environment.Exit(0);
            System.Windows.Application.Current.Shutdown();
        }


    }
}

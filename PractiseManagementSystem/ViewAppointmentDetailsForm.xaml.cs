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
    /// Interaction logic for ViewAppointmentDetailsForm.xaml
    /// </summary>
    public partial class ViewAppointmentDetailsForm : Window
    {
        MainContentPage mainPage = null;
        Appointment appointment = new Appointment();

        Patient patient = new Patient();

        public ViewAppointmentDetailsForm()
        {
            InitializeComponent();
        }

        public ViewAppointmentDetailsForm(MainContentPage objMainPage)
        {
            mainPage = objMainPage;
            InitializeComponent();
        }

        private void gridAppointmentList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            updateSelectedPatientAppointment();
        }

        private void updateSelectedPatientAppointment()
        {
            AppointmentProcessingForm apptForm = new AppointmentProcessingForm(this);
            var row = (DataRowView)grdAppointmentList.SelectedItem;

            appointment.AppointmentId = row["appointmentId"].ToString();
            appointment.ApptPatientId = tbViewApptPatientId.Text.ToString();
            appointment.ApptPatientName = tbViewApptPatientName.Text.ToString();
            appointment.AppointmentDate = Convert.ToDateTime(row["apptDate"]);
            appointment.AppointmentTime = row["apptTime"].ToString();
            appointment.ApptPatientDoctor = row["doctorName"].ToString();
            appointment.doctor.Specialisation = row["specialty"].ToString();
            appointment.Purpose = row["purpose"].ToString();

            apptForm.Show();
            apptForm.viewAppointmentDetailsForm(appointment);
            this.Hide();
        }

        internal void viewAppointmentDetails(Appointment a, Patient p)
        {
            tbViewApptPatientId.Text = p.PatientId;
            tbViewApptPatientName.Text = a.ApptPatientName;
            tbViewApptMedicareNo.Text = p.MedicareNo;
            tbViewApptContactNo.Text = ((IContact)p).ContactNo;
            tbViewApptEmailAdd.Text = ((IContact)p).EmailAddress;
            FillDataForApptPatient(p.PatientId);

        }

        public void FillDataForApptPatient(string patientId)
        {
            DataTable dt = new DataTable();
            dt = appointment.retrievePatientAppointmentDetails(patientId);

            DataTable finalDataTable = new DataTable();

            if (dt.Rows.Count > 0)
            {
                finalDataTable.Columns.Add("appointmentId", typeof(System.Int32));
                finalDataTable.Columns.Add("apptDate", typeof(System.DateTime));
                finalDataTable.Columns.Add("apptTime", typeof(System.TimeSpan));
                finalDataTable.Columns.Add("doctorName", typeof(System.String));
                finalDataTable.Columns.Add("specialty", typeof(System.String));
                finalDataTable.Columns.Add("department", typeof(System.String));
                finalDataTable.Columns.Add("buildingLocation", typeof(System.String));
                finalDataTable.Columns.Add("roomNumber", typeof(System.String));
                finalDataTable.Columns.Add("purpose", typeof(System.String));

                foreach (DataRow dgRow in dt.Rows)
                {
                    DataRow newRow = finalDataTable.NewRow();

                    if (!string.IsNullOrEmpty(dgRow[0].ToString()))
                    {
                        newRow[0] = dgRow[0];
                    }
                    if (!string.IsNullOrEmpty(dgRow[1].ToString()))
                    {
                        DateTime appointmentDate;
                        string dateString = dgRow[1].ToString();
                        bool isDate = DateTime.TryParse(dateString, out appointmentDate);

                        if (isDate == Constants.ISTRUE)
                        {
                            newRow[1] = appointmentDate.ToShortDateString();
                        }
                        else
                        {
                            newRow[1] = "";
                        }
                    }
                    if (!string.IsNullOrEmpty(dgRow[2].ToString()))
                    {
                        TimeSpan appointmentTime;
                        string timeString = dgRow[2].ToString();
                        bool isTime = TimeSpan.TryParse(timeString, out appointmentTime);

                        if (isTime == Constants.ISTRUE)
                        {
                            newRow[2] = appointmentTime.ToString(); ;
                        }
                        else
                        {
                            newRow[2] = null;
                        }
                    }

                    newRow[3] = dgRow[3];
                    newRow[4] = dgRow[4];
                    newRow[5] = dgRow[5];
                    newRow[6] = dgRow[6];
                    newRow[7] = dgRow[7];
                    newRow[8] = dgRow[8];

                    finalDataTable.Rows.Add(newRow);
                }

                if (finalDataTable.Rows.Count > 0)
                {
                    grdAppointmentList.ItemsSource = finalDataTable.DefaultView;
                }
                else
                {
                    lblViewApptMessage.Content = "No appointment record/s found.";
                    lblViewApptMessage.Foreground = Brushes.Red;
                    grdAppointmentList.Visibility = Visibility.Hidden;
                }

            }
            else
            {
                lblViewApptMessage.Content = "Currently, there are no record of appointment found.";
                lblViewApptMessage.Foreground = Brushes.Red;
                grdAppointmentList.Visibility = Visibility.Hidden;
            }
        }
        
        private void btnDeleteAppointment_Click(object sender, RoutedEventArgs e)
        {
            var row = (DataRowView)grdAppointmentList.SelectedItem;

            if (row != null)
            {
                appointment.AppointmentId = row["appointmentId"].ToString();

                DialogResult result = System.Windows.Forms.MessageBox.Show("Do you want to delete appointment id # " + appointment.AppointmentId + "?", "Delete Appointment?", MessageBoxButtons.YesNo);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    lblViewApptMessage.Content = appointment.deleteAppointmentRecord(appointment.AppointmentId);
                    lblViewApptMessage.Foreground = Brushes.Green;
                    row.Delete();
                    FillDataForApptPatient(tbViewApptPatientId.Text);
                }
                else
                {
                    lblViewApptMessage.Content = "";
                }
                grdAppointmentList.Visibility = Visibility.Visible;
            }
            else
            {
                lblViewApptMessage.Content = "Please highlight an appointment to delete in the list below!";
                lblViewApptMessage.Foreground = Brushes.Red;
            }
        }

        private void deleteAppointmentIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgDeleteAppointment.IsMouseOver == Constants.ISTRUE)
            {
                imgDeleteAppointment.Opacity = 1.0;
            }
        }

        private void deleteAppointmentIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgDeleteAppointment.IsMouseOver == Constants.ISFALSE)
            {
                imgDeleteAppointment.Opacity = 0.6;
            }
        }

        private void homeViewApptIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgHomeViewApptBtn.IsMouseOver == Constants.ISTRUE)
            {
                imgHomeViewApptBtn.Opacity = 1.0;
            }
        }

        private void homeViewApptIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgHomeViewApptBtn.IsMouseOver == Constants.ISFALSE)
            {
                imgHomeViewApptBtn.Opacity = 0.5;
            }
        }

       
        private void homeViewAppt_onMouseDown(object sender, RoutedEventArgs e)
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
    }
}

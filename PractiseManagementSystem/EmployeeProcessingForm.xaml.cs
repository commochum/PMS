using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for EmployeeProcessingForm.xaml
    /// </summary>
    public partial class EmployeeProcessingForm : Window
    {
        Employee employee = new Employee();
        Doctor doctor = new Doctor();
        MainContentPage mainPage = null;

        string message = null;
        string errorMessage = "Please enter ";

        Boolean isJobTitleChangedToDoctor = Constants.ISFALSE;
        Boolean isJobTitleChangedFromDoctor = Constants.ISFALSE;

        public EmployeeProcessingForm()
        {
            InitializeComponent();
            employee.NoOfHoursWorked = 0;
        }

        public EmployeeProcessingForm(MainContentPage objMainPage)
        {
            mainPage = objMainPage;
            InitializeComponent();
            cbxJobTitle.SelectedIndex = 0;
            cbxEmployeeGender.SelectedIndex = 0;
            cbxEmployeeContactType.SelectedIndex = 0;
            cbxDepartment.SelectedIndex = 0;
            cbxIncomeType.SelectedIndex = 0;
            cbxPosition.SelectedIndex = 0;
            cbxEmployeeStatus.SelectedIndex = 0;
            cbxEmploymentType.SelectedIndex = 0;
        }

        internal void viewEmployeeDetails(Employee employee, Doctor doctor)
        {
            lblEmployeeHeaderName.Content = "Employee Information";
           
            txtEmployeeId.Text = employee.EmployeeId;
            txtEmployeeFName.Text = employee.FirstName;
            txtEmployeeLName.Text = employee.LastName;
            txtEmployeeDOB.SelectedDate = employee.DOB;
            txtEmployeeAge.Text = employee.Age.ToString();
            cbxEmployeeGender.Text = employee.Gender;
            txtEmployeeAddress.Text = employee.AddressLine1;
            txtEmployeePostcode.Text = employee.PostCode;
            txtEmployeeSuburb.Text = employee.Suburb;
            txtEmployeeState.Text = employee.State;
            txtEmployeeCountry.Text = employee.Country;
            txtEmployeeMedicareNo.Text = employee.MedicareNo;
            cbxEmployeeContactType.Text = ((IContact)employee).ContactType;
            txtEmployeeContactNo.Text = ((IContact)employee).ContactNo;
            txtEmployeeEmailAdd.Text = ((IContact)employee).EmailAddress;
            txtEmployeeERContactName.Text = employee.EmergencyContactName;
            txtEmployeeERContactNo.Text = employee.EmergencyContactNo;
            txtEmployeeERRelationship.Text = employee.Relationship;
            txtCompanyName.Text = employee.CompanyName;
            txtCompanyAddress.Text = employee.CompanyAddress;
            cbxPosition.Text = employee.Position;
            cbxJobTitle.Text = employee.JobTitle;
            cbxEmployeeStatus.Text = employee.EmployeeStatus;
            txtDateHired.SelectedDate = employee.HireDate;
            cbxEmploymentType.Text = employee.EmploymentType;
            cbxIncomeType.Text = employee.IncomeType;
            txtIncomeAmount.Text = employee.Income;
            cbxDepartment.Text = employee.Department;
            txtHoursWorked.Text = employee.NoOfHoursWorked.ToString();
            txtStartTime.Text = employee.StartTime;
            imgResetEmployee.Visibility = Visibility.Hidden;

            if (employee.JobTitle.Equals(Constants.JOBTITLE_DOCTOR))
            {
                changeEmployeeBackground("pack://application:,,,/Images/bgSelectedDoctor.jpg");
                txtDoctorId.Text = doctor.DoctorId;
                lblDoctorId.Visibility = Visibility.Visible;
                txtDoctorId.Visibility = Visibility.Visible;
                txtDoctorLicenseNo.Text = doctor.DoctorLicenseNo;
                txtSpecialisation.Text = doctor.Specialisation;
                txtBuildingLocation.Text = doctor.BuildingLocation;
                txtRoomNo.Text = doctor.RoomNumber;
                updateEmployeeSelected();
                doctorInfoVisibility("Visible");

                if (string.IsNullOrEmpty(txtDoctorId.Text.Trim()) && cbxJobTitle.Text.Trim().Equals(Constants.JOBTITLE_DOCTOR))
                {
                    //congratulations for being a doctor
                    isJobTitleChangedToDoctor = Constants.ISTRUE;
                    //performed update emp and insert doc
                }
                else
                {
                    isJobTitleChangedToDoctor = Constants.ISFALSE;
                }
            }
            if (employee.JobTitle.Equals(Constants.JOBTITLE_NURSE))
            {
                changeEmployeeBackground("pack://application:,,,/Images/bgSelectedNurse.jpg");
                updateEmployeeSelected();
                doctorInfoVisibility("Collapsed");
                jobTitleChanged();
            }
            if (employee.JobTitle.Equals(Constants.JOBTITLE_RECEPTIONIST))
            {
                changeEmployeeBackground("pack://application:,,,/Images/bgSelectedReceptionist.jpg");
                updateEmployeeSelected();
                doctorInfoVisibility("Collapsed");
                jobTitleChanged();
            }
            if (employee.JobTitle.Equals(Constants.JOBTITLE_ADMIN))
            {
                changeEmployeeBackground("pack://application:,,,/Images/bgSelectedAdmin.jpg");
                updateEmployeeSelected();
                doctorInfoVisibility("Collapsed");
                jobTitleChanged();
            }

            imgResetEmployee.Visibility = Visibility.Visible;

        }

        private void updateEmployee_onMouseDown(object sender, RoutedEventArgs e)
        {
            updateEmployeeDetails(txtEmployeeId.Text);
        }

        private void updateEmployeeDetails(string employeeId)
        {
            mainPage.isAddEmployeeSelected = Constants.ISFALSE;

            if (cbxJobTitle.Text.Trim().Equals(Constants.JOBTITLE_DOCTOR))
            {
                validateEmployeeDoctorDetails();
            }
            else
            {
               // if (string.IsNullOrEmpty(txtDoctorId.Text.Trim()))
               if(isJobTitleChangedFromDoctor || string.IsNullOrEmpty(txtDoctorId.Text.Trim()))
                {
                   // if(cbxJobTitle.SelectedValue.ToString() != Constants.JOBTITLE_DOCTOR)
                    validateEmployeeDetails();
                }
                else
                {
                    validateEmployeeDoctorDetails();
                }
            }

            if (errorMessage != "Please enter " && !string.IsNullOrEmpty(errorMessage))
            {
                lblEmployeeMessage.Content += errorMessage;
                return;
            }

            if (!isJobTitleChangedToDoctor == Constants.ISFALSE)
            {
                if (cbxJobTitle.Text.Trim().Equals(Constants.JOBTITLE_DOCTOR))
                {
                    if (isJobTitleChangedToDoctor == Constants.ISTRUE)
                    {
                        //do update emp and delete doctor
                        message += doctor.executeUpdateCreateTransaction(txtEmployeeId.Text);

                    }
                    else if (isJobTitleChangedFromDoctor == Constants.ISTRUE)
                    {
                        message += doctor.executeUpdateDeleteTransaction(txtEmployeeId.Text);
                    }
                    else
                    {
                        message += doctor.executeUpdateTransaction(txtEmployeeId.Text, txtDoctorId.Text);
                    }
                }
                else
                {
                    message += employee.updateEmployeeRecord(txtEmployeeId.Text);
                }
            }
            else
            {
                if (cbxJobTitle.Text.Equals(Constants.JOBTITLE_DOCTOR))
                {
                    if (isJobTitleChangedToDoctor == Constants.ISTRUE)
                    {
                        //do update emp and create doctor
                        message += doctor.executeUpdateCreateTransaction(txtEmployeeId.Text);

                    }
                    else
                    {
                        message += doctor.executeUpdateTransaction(txtEmployeeId.Text, txtDoctorId.Text);
                    }
                } else
                {
                    if (string.IsNullOrEmpty(txtDoctorId.Text.Trim()))
                    {
                        message += employee.updateEmployeeRecord(txtEmployeeId.Text);
                    }
                    else
                    {
                        message += employee.executeUpdateDeleteTransaction(txtEmployeeId.Text.Trim());
                    }
                }
            }
            
            DataGrid dgEmployeeList = (DataGrid)mainPage.grdEmployeeList;
            dgEmployeeList.Visibility = Visibility.Visible;
            mainPage.FillDataForEmployees();

            mainPage.tbEmployeeMainMessage.Text = message;
            mainPage.tbEmployeeMainMessage.Foreground = Brushes.Green;
            this.Hide();
            mainPage.Show();
        }

        private void changeEmployeeBackground(string packUriContentSource)
        {
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(packUriContentSource));
            image.Stretch = Stretch.Fill;
            image.Opacity = 0.8;
            bgEmployeePage.ImageSource = image.Source;

        }

        private void showErrorMessages(string showMsgIfTrue, string showMsgIfFalse)
        {
            errorMessage += (errorMessage != "Please enter " && errorMessage != null) ? showMsgIfTrue : showMsgIfFalse;
            lblEmployeeMessage.Foreground = Brushes.Red;
        }

        internal void viewEmployeeForm()
        {
           
            employee = new Employee();
            resetEmployeeFields();
            txtEmployeeId.Visibility = Visibility.Hidden;
            txtEmployeeId.Text = null;
            lblEmployeeId.Visibility = Visibility.Hidden;
           
            imgResetEmployee.Visibility = Visibility.Visible;
            
            lblEmployeeHeaderName.Content = "Employee Form";
            
            if (cbxJobTitle.Text.Equals(Constants.JOBTITLE_DOCTOR))
            {
                changeEmployeeBackground("pack://application:,,,/Images/bgSelectedDoctor.jpg");
                txtDoctorId.Visibility = Visibility.Hidden;
                txtDoctorId.Text = null;
                lblDoctorId.Visibility = Visibility.Hidden;
                addEmployeeSelected();
                doctorInfoVisibility("Visible");
            }
            if (cbxJobTitle.Text.Equals(Constants.JOBTITLE_NURSE))
            {
                changeEmployeeBackground("pack://application:,,,/Images/bgSelectedNurse.jpg");
                addEmployeeSelected();
                doctorInfoVisibility("Collapsed");
            }
            if (cbxJobTitle.Text.Equals(Constants.JOBTITLE_RECEPTIONIST))
            {
                changeEmployeeBackground("pack://application:,,,/Images/bgSelectedReceptionist.jpg");
                addEmployeeSelected();
                doctorInfoVisibility("Collapsed");
            }
            if (cbxJobTitle.Text.Equals(Constants.JOBTITLE_ADMIN))
            {
                changeEmployeeBackground("pack://application:,,,/Images/bgSelectedAdmin.jpg");
                addEmployeeSelected();
                doctorInfoVisibility("Collapsed");
            }
            
        }
        private void updateEmployeeSelected()
        {
            imgAddEmployee.Visibility = Visibility.Hidden;
            imgUpdateEmployee.Visibility = Visibility.Visible;
        }
        private void addEmployeeSelected()
        {
            imgAddEmployee.Visibility = Visibility.Visible;
            imgUpdateEmployee.Visibility = Visibility.Hidden;
        }

        private void addEmployee_onMouseDown(object sender, RoutedEventArgs e)
        {
            addEmployee();
        }

        private void addEmployee()
        {
            if(cbxJobTitle.Text.Trim().Equals(Constants.JOBTITLE_DOCTOR))
            {
                validateEmployeeDoctorDetails();
            }
            else
            {
                validateEmployeeDetails();
            }
            
            if (errorMessage != "Please enter " && !string.IsNullOrEmpty(errorMessage))
            {
                lblEmployeeMessage.Content += errorMessage;
                return;
            }

            if (cbxJobTitle.Text.Trim().Equals(Constants.JOBTITLE_DOCTOR))
            {
                message += doctor.executeCreateEmployeeTransaction();
            }
            else
            {
                message += employee.createEmployeeRecord();
            }

                DataGrid dgEmployeeList = (DataGrid)mainPage.grdEmployeeList;
                dgEmployeeList.Visibility = Visibility.Visible;
                mainPage.FillDataForEmployees();

                mainPage.tbEmployeeMainMessage.Text = message;
                mainPage.tbEmployeeMainMessage.Foreground = Brushes.Green;
                this.Hide();
                mainPage.Show();

        }

        private void validateEmployeeDetails()
        {
            if (errorMessage != "Please enter " && !string.IsNullOrEmpty(errorMessage))
            {
                lblEmployeeMessage.Content = "";
                errorMessage = "Please enter ";
            }

            string selectedEmployeeContactType = (cbxEmployeeContactType.SelectedValue != null ? cbxEmployeeContactType.SelectedValue.ToString() : Constants.CONTACTTYPE_HOME);
            if (cbxEmployeeContactType.SelectedIndex == 0)
            {
                selectedEmployeeContactType = Constants.CONTACTTYPE_HOME;
                mainPage.showErrorFields(cbxEmployeeContactType, Constants.ISFALSE);
            }
            else if (cbxEmployeeContactType.SelectedIndex == 1)
            {
                selectedEmployeeContactType = Constants.CONTACTTYPE_OFFICE;
                mainPage.showErrorFields(cbxEmployeeContactType, Constants.ISFALSE);
            }
            else if (cbxEmployeeContactType.SelectedIndex == 2)
            {
                selectedEmployeeContactType = Constants.CONTACTTYPE_MOBILE;
                mainPage.showErrorFields(cbxEmployeeContactType, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Contact Type", "Contact Type");
                mainPage.showErrorFields(cbxEmployeeContactType, Constants.ISTRUE);
            }

            ((IContact)employee).ContactType = selectedEmployeeContactType;

            string selectedEmployeeGender = (cbxEmployeeGender.SelectedValue != null ? cbxEmployeeGender.SelectedValue.ToString() : Constants.GENDER_FEMALE);

            if (cbxEmployeeGender.SelectedIndex == 0)
            {
                selectedEmployeeGender = Constants.GENDER_FEMALE;
                mainPage.showErrorFields(cbxEmployeeGender, Constants.ISFALSE);
            }
            else if (cbxEmployeeGender.SelectedIndex == 1)
            {
                selectedEmployeeGender = Constants.GENDER_MALE;
                mainPage.showErrorFields(cbxEmployeeGender, Constants.ISFALSE);
            }
            else if (cbxEmployeeGender.SelectedIndex == 2)
            {
                selectedEmployeeGender = Constants.GENDER_DECLINE_TO_STATE;
                mainPage.showErrorFields(cbxEmployeeGender, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Gender", "Gender");
                mainPage.showErrorFields(cbxEmployeeGender, Constants.ISTRUE);
            }
            employee.Gender = selectedEmployeeGender;

            if (!string.IsNullOrEmpty(txtEmployeeFName.Text))
            {
                employee.FirstName = txtEmployeeFName.Text;
                mainPage.showErrorFields(txtEmployeeFName, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Firstname", "FirstName");
                mainPage.showErrorFields(txtEmployeeFName, Constants.ISTRUE);
            }
            if (!string.IsNullOrEmpty(txtEmployeeLName.Text))
            {
                employee.LastName = txtEmployeeLName.Text;
                mainPage.showErrorFields(txtEmployeeLName, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Lastname", "Lastname");
                mainPage.showErrorFields(txtEmployeeLName, Constants.ISTRUE);
            }

            int age = 0;

            Boolean isNumeric = Int32.TryParse(txtEmployeeAge.Text.Trim(), out age);

            if (!string.IsNullOrEmpty(txtEmployeeAge.Text.Trim()))
            {
                if (isNumeric == Constants.ISTRUE)
                {
                    if (age >= 18)
                    {
                        employee.Age = age;
                        mainPage.showErrorFields(txtEmployeeAge, Constants.ISFALSE);
                    }
                    else
                    {
                        showErrorMessages(", Age 18 and above only", "Age 18 and above only");
                        mainPage.showErrorFields(txtEmployeeAge, Constants.ISTRUE);
                    }
                }
                else
                {
                    showErrorMessages(", valid Age", "valid Age");
                    mainPage.showErrorFields(txtEmployeeAge, Constants.ISTRUE);
                }
            }
            else
            {
                showErrorMessages(", Age", "Age");
                mainPage.showErrorFields(txtEmployeeAge, Constants.ISTRUE);
            }

            if (txtEmployeeDOB.SelectedDate != null)
            {
                employee.DOB = Convert.ToDateTime(txtEmployeeDOB.Text);
                mainPage.showErrorFields(txtEmployeeDOB, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Date of Birth", "Date of Birth");
                mainPage.showErrorFields(txtEmployeeDOB, Constants.ISTRUE);
            }

            employee.AddressLine1 = txtEmployeeAddress.Text.Trim();
            employee.Suburb = txtEmployeeSuburb.Text.Trim();
            employee.State = txtEmployeeState.Text.Trim();
            employee.PostCode = txtEmployeePostcode.Text.Trim();
            employee.Country = txtEmployeeCountry.Text.Trim();
            employee.MedicareNo = txtEmployeeMedicareNo.Text.Trim();
            ((IContact)employee).ContactNo = txtEmployeeContactNo.Text.Trim();
            employee.RecordCreated = DateTime.Now;
            employee.RecordUpdated = DateTime.Now;

            if (!string.IsNullOrEmpty(txtEmployeeEmailAdd.Text.Trim()) && MainContentPage.IsValidEmailAddress(txtEmployeeEmailAdd.Text))
            {
                ((IContact)employee).EmailAddress = txtEmployeeEmailAdd.Text.Trim();
                mainPage.showErrorFields(txtEmployeeEmailAdd, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", a Valid Email Address", "Valid Email Address");
                mainPage.showErrorFields(txtEmployeeEmailAdd, Constants.ISTRUE);
            }
            employee.EmergencyContactName = txtEmployeeERContactName.Text.Trim();
            employee.EmergencyContactNo = txtEmployeeERContactNo.Text.Trim();
            employee.Relationship = txtEmployeeERRelationship.Text.Trim();
            employee.CompanyName = txtCompanyName.Text.Trim();
            employee.CompanyAddress = txtCompanyAddress.Text.Trim();

            string selectedPosition = (cbxPosition.SelectedValue != null ? cbxPosition.SelectedValue.ToString() : Constants.POSITION_EXECUTIVE);

            if (cbxPosition.SelectedIndex == 0)
            {
                selectedPosition = Constants.POSITION_EXECUTIVE;
                mainPage.showErrorFields(cbxPosition, Constants.ISFALSE);
            }
            else if (cbxPosition.SelectedIndex == 1)
            {
                selectedPosition = Constants.POSITION_STAFF;
                mainPage.showErrorFields(cbxPosition, Constants.ISFALSE);
            }
            else if (cbxPosition.SelectedIndex == 2)
            {
                selectedPosition = Constants.POSITION_ADMIN;
                mainPage.showErrorFields(cbxPosition, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Position", "Position");
                mainPage.showErrorFields(cbxPosition, Constants.ISTRUE);
            }

            employee.Position = selectedPosition;
            employee.Income = txtIncomeAmount.Text.Trim();

            if (txtDateHired.SelectedDate != null)
            {
                employee.HireDate = Convert.ToDateTime(txtDateHired.Text);
                mainPage.showErrorFields(txtDateHired, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Hire Date", "Hire Date");
                mainPage.showErrorFields(txtDateHired, Constants.ISTRUE);
            }

            string selectedDepartment = (cbxDepartment.SelectedValue != null ? cbxDepartment.SelectedValue.ToString() : Constants.DEPARTMENT_GENERAL);

            if (cbxDepartment.SelectedIndex == 0)
            {
                selectedDepartment = Constants.DEPARTMENT_GENERAL;
                mainPage.showErrorFields(cbxDepartment, Constants.ISFALSE);
            }
            else if (cbxDepartment.SelectedIndex == 1)
            {
                selectedDepartment = Constants.DEPARTMENT_SURGERY;
                mainPage.showErrorFields(cbxDepartment, Constants.ISFALSE);
            }
            else if (cbxDepartment.SelectedIndex == 2)
            {
                selectedDepartment = Constants.DEPARTMENT_NURSING;
                mainPage.showErrorFields(cbxDepartment, Constants.ISFALSE);
            }
            else if (cbxDepartment.SelectedIndex == 3)
            {
                selectedDepartment = Constants.DEPARTMENT_PSYCHOLOGY;
                mainPage.showErrorFields(cbxDepartment, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Department", "Department");
                mainPage.showErrorFields(cbxDepartment, Constants.ISTRUE);
            }

            employee.Department = selectedDepartment;

            string selectedEmpType = (cbxEmploymentType.SelectedValue != null ? cbxEmploymentType.SelectedValue.ToString() : Constants.EMPLOYEETYPE_FULL);

            if (cbxEmploymentType.SelectedIndex == 0)
            {
                selectedEmpType = Constants.EMPLOYEETYPE_FULL;
                mainPage.showErrorFields(cbxEmploymentType, Constants.ISFALSE);
            }
            else if (cbxEmploymentType.SelectedIndex == 1)
            {
                selectedEmpType = Constants.EMPLOYEETYPE_PART;
                mainPage.showErrorFields(cbxEmploymentType, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Employment Type", "Employment Type");
                mainPage.showErrorFields(cbxEmploymentType, Constants.ISTRUE);
            }
            employee.EmploymentType = selectedEmpType;

            string selectedEmpStatus = (cbxEmployeeStatus.SelectedValue != null ? cbxEmployeeStatus.SelectedValue.ToString() : Constants.EMPLOYEESTATUS_ACTIVE);

            if (cbxEmployeeStatus.SelectedIndex == 0)
            {
                selectedEmpStatus = Constants.EMPLOYEESTATUS_ACTIVE;
                mainPage.showErrorFields(cbxEmployeeStatus, Constants.ISFALSE);
            }
            else if (cbxEmployeeStatus.SelectedIndex == 1)
            {
                selectedEmpStatus = Constants.EMPLOYEESTATUS_ONLEAVE;
                mainPage.showErrorFields(cbxEmployeeStatus, Constants.ISFALSE);
            }
            else if (cbxEmployeeStatus.SelectedIndex == 2)
            {
                selectedEmpStatus = "Inactive";
                mainPage.showErrorFields(cbxEmployeeStatus, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Employment Status", "Employment Status");
                mainPage.showErrorFields(cbxEmployeeStatus, Constants.ISTRUE);
            }
            employee.EmployeeStatus = selectedEmpStatus;

            string selectedIncomeType = (cbxIncomeType.SelectedValue != null ? cbxIncomeType.SelectedValue.ToString() : "Annually");

            if (cbxIncomeType.SelectedIndex == 0)
            {
                selectedIncomeType = "Annually";
                mainPage.showErrorFields(cbxIncomeType, Constants.ISFALSE);
            }
            else if (cbxIncomeType.SelectedIndex == 1)
            {
                selectedIncomeType = "Daily";
                mainPage.showErrorFields(cbxIncomeType, Constants.ISFALSE);
            }
            else if (cbxIncomeType.SelectedIndex == 2)
            {
                selectedIncomeType = "Hourly";
                mainPage.showErrorFields(cbxIncomeType, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Income Type", "Income Type");
                mainPage.showErrorFields(cbxIncomeType, Constants.ISTRUE);
            }
            employee.IncomeType = selectedIncomeType;

            string selectedJobTitle = (cbxJobTitle.SelectedValue != null ? cbxJobTitle.SelectedValue.ToString() : Constants.JOBTITLE_RECEPTIONIST);

            if (cbxJobTitle.SelectedIndex == 0)
            {
                selectedJobTitle = Constants.JOBTITLE_RECEPTIONIST;
                mainPage.showErrorFields(cbxJobTitle, Constants.ISFALSE);
            }
            else if (cbxJobTitle.SelectedIndex == 1)
            {
                selectedJobTitle = Constants.JOBTITLE_NURSE;
                mainPage.showErrorFields(cbxJobTitle, Constants.ISFALSE);
            }
            else if (cbxJobTitle.SelectedIndex == 2)
            {
                selectedJobTitle = Constants.JOBTITLE_DOCTOR;
                mainPage.showErrorFields(cbxJobTitle, Constants.ISFALSE);
            }
            else if (cbxJobTitle.SelectedIndex == 3)
            {
                selectedJobTitle = Constants.JOBTITLE_ADMIN;
                mainPage.showErrorFields(cbxJobTitle, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Job Title", "Job Title");
                mainPage.showErrorFields(cbxJobTitle, Constants.ISTRUE);
            }
            


            employee.JobTitle = selectedJobTitle;

            int noOfHoursWorked = 0;

            Boolean isHoursNumeric = Int32.TryParse(txtHoursWorked.Text.Trim(), out noOfHoursWorked);

            if (!string.IsNullOrEmpty(txtHoursWorked.Text.Trim()) && noOfHoursWorked != 0)
            {
                if (isHoursNumeric == Constants.ISTRUE)
                {
                    employee.NoOfHoursWorked = noOfHoursWorked;
                    mainPage.showErrorFields(txtHoursWorked, Constants.ISFALSE);
                }
                else
                {
                    showErrorMessages(", valid No. of Working Hours", "a valid No. of Working Hours");
                    mainPage.showErrorFields(txtHoursWorked, Constants.ISTRUE);
                }
            }
            else
            {
                showErrorMessages(", No. of Working Hours", "No. of Working Hours");
                mainPage.showErrorFields(txtHoursWorked, Constants.ISTRUE);
            }

            if (!string.IsNullOrEmpty(txtStartTime.Text.Trim()))
            {
                try
                {
                    employee.StartTime = Convert.ToDateTime(txtStartTime.Text).ToShortTimeString();
                    mainPage.showErrorFields(txtStartTime, Constants.ISFALSE);

                }
                catch (Exception)
                {
                    showErrorMessages(", valid Start Time", "a valid Start Time");
                    mainPage.showErrorFields(txtStartTime, Constants.ISTRUE);
                }
            }
            else
            {
                showErrorMessages(", Start Time", "Start Time");
                mainPage.showErrorFields(txtStartTime, Constants.ISTRUE);
            }

            //Start of Entering Doctor Values
            if (cbxJobTitle.Text.Equals(Constants.JOBTITLE_DOCTOR))
            {
                if (!string.IsNullOrEmpty(txtDoctorLicenseNo.Text.Trim()))
                {
                    doctor.DoctorLicenseNo = txtDoctorLicenseNo.Text.Trim();
                    mainPage.showErrorFields(txtDoctorLicenseNo, Constants.ISFALSE);
                }
                else
                {
                    showErrorMessages(", Doctor License No", "Doctor License No");
                    mainPage.showErrorFields(txtDoctorLicenseNo, Constants.ISTRUE);
                }

                doctor.Specialisation = txtSpecialisation.Text;
                doctor.BuildingLocation = txtBuildingLocation.Text;
                doctor.RoomNumber = txtRoomNo.Text;

                //message += Environment.NewLine + "Doctor " + doctor.createDoctorRecord();
            }
        }

        private void validateEmployeeDoctorDetails()
        {
            if (errorMessage != "Please enter " && !string.IsNullOrEmpty(errorMessage))
            {
                lblEmployeeMessage.Content = "";
                errorMessage = "Please enter ";
            }

            string selectedEmployeeContactType = (cbxEmployeeContactType.SelectedValue != null ? cbxEmployeeContactType.SelectedValue.ToString() : Constants.CONTACTTYPE_HOME);
            if (cbxEmployeeContactType.SelectedIndex == 0)
            {
                selectedEmployeeContactType = Constants.CONTACTTYPE_HOME;
                mainPage.showErrorFields(cbxEmployeeContactType, Constants.ISFALSE);
            }
            else if (cbxEmployeeContactType.SelectedIndex == 1)
            {
                selectedEmployeeContactType = Constants.CONTACTTYPE_OFFICE;
                mainPage.showErrorFields(cbxEmployeeContactType, Constants.ISFALSE);
            }
            else if (cbxEmployeeContactType.SelectedIndex == 2)
            {
                selectedEmployeeContactType = Constants.CONTACTTYPE_MOBILE;
                mainPage.showErrorFields(cbxEmployeeContactType, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Contact Type", "Contact Type");
                mainPage.showErrorFields(cbxEmployeeContactType, Constants.ISTRUE);
            }

            ((IContact)doctor).ContactType = selectedEmployeeContactType;

            string selectedEmployeeGender = (cbxEmployeeGender.SelectedValue != null ? cbxEmployeeGender.SelectedValue.ToString() : Constants.GENDER_FEMALE);

            if (cbxEmployeeGender.SelectedIndex == 0)
            {
                selectedEmployeeGender = Constants.GENDER_FEMALE;
                mainPage.showErrorFields(cbxEmployeeGender, Constants.ISFALSE);
            }
            else if (cbxEmployeeGender.SelectedIndex == 1)
            {
                selectedEmployeeGender = Constants.GENDER_MALE;
                mainPage.showErrorFields(cbxEmployeeGender, Constants.ISFALSE);
            }
            else if (cbxEmployeeGender.SelectedIndex == 2)
            {
                selectedEmployeeGender = Constants.GENDER_DECLINE_TO_STATE;
                mainPage.showErrorFields(cbxEmployeeGender, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Gender", "Gender");
                mainPage.showErrorFields(cbxEmployeeGender, Constants.ISTRUE);
            }
            doctor.Gender = selectedEmployeeGender;

            if (!string.IsNullOrEmpty(txtEmployeeFName.Text))
            {
                doctor.FirstName = txtEmployeeFName.Text;
                mainPage.showErrorFields(txtEmployeeFName, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Firstname", "FirstName");
                mainPage.showErrorFields(txtEmployeeFName, Constants.ISTRUE);
            }
            if (!string.IsNullOrEmpty(txtEmployeeLName.Text))
            {
                doctor.LastName = txtEmployeeLName.Text;
                mainPage.showErrorFields(txtEmployeeLName, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Lastname", "Lastname");
                mainPage.showErrorFields(txtEmployeeLName, Constants.ISTRUE);
            }

            int age = 0;

            Boolean isNumeric = Int32.TryParse(txtEmployeeAge.Text, out age);

            if (!string.IsNullOrEmpty(txtEmployeeAge.Text.Trim()))
            {
                if (isNumeric == Constants.ISTRUE)
                {
                    if (age >= 18)
                    {
                        doctor.Age = age;
                        mainPage.showErrorFields(txtEmployeeAge, Constants.ISFALSE);
                    }
                    else
                    {
                        showErrorMessages(", Age 18 and above only", "Age 18 and above only");
                        mainPage.showErrorFields(txtEmployeeAge, Constants.ISTRUE);
                    }
                }
                else
                {
                    showErrorMessages(", a valid Age", "a valid Age");
                    mainPage.showErrorFields(txtEmployeeAge, Constants.ISTRUE);
                }
            }
            else
            {
                showErrorMessages(", Age", "Age");
                mainPage.showErrorFields(txtEmployeeAge, Constants.ISTRUE);
            }

            if (txtEmployeeDOB.SelectedDate != null)
            {
                doctor.DOB = Convert.ToDateTime(txtEmployeeDOB.Text);
                mainPage.showErrorFields(txtEmployeeDOB, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Date of Birth", "Date of Birth");
                mainPage.showErrorFields(txtEmployeeDOB, Constants.ISTRUE);
            }

            doctor.AddressLine1 = txtEmployeeAddress.Text.Trim();
            doctor.Suburb = txtEmployeeSuburb.Text.Trim();
            doctor.State = txtEmployeeState.Text.Trim();
            doctor.PostCode = txtEmployeePostcode.Text.Trim();
            doctor.Country = txtEmployeeCountry.Text.Trim();
            doctor.MedicareNo = txtEmployeeMedicareNo.Text.Trim();
            ((IContact)doctor).ContactNo = txtEmployeeContactNo.Text.Trim();
            doctor.RecordCreated = DateTime.Now;
            doctor.RecordUpdated = DateTime.Now;

            if (!string.IsNullOrEmpty(txtEmployeeEmailAdd.Text.Trim()) && MainContentPage.IsValidEmailAddress(txtEmployeeEmailAdd.Text))
            {
                ((IContact)doctor).EmailAddress = txtEmployeeEmailAdd.Text.Trim();
                mainPage.showErrorFields(txtEmployeeEmailAdd, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", a Valid Email Address", "Valid Email Address");
                mainPage.showErrorFields(txtEmployeeEmailAdd, Constants.ISTRUE);
            }
           
            doctor.EmergencyContactName = txtEmployeeERContactName.Text.Trim();
            doctor.EmergencyContactNo = txtEmployeeERContactNo.Text.Trim();
            doctor.Relationship = txtEmployeeERRelationship.Text.Trim();
            doctor.CompanyName = txtCompanyName.Text.Trim();
            doctor.CompanyAddress = txtCompanyAddress.Text.Trim();

            string selectedPosition = (cbxPosition.SelectedValue != null ? cbxPosition.SelectedValue.ToString() : Constants.POSITION_EXECUTIVE);

            if (cbxPosition.SelectedIndex == 0)
            {
                selectedPosition = Constants.POSITION_EXECUTIVE;
                mainPage.showErrorFields(cbxPosition, Constants.ISFALSE);
            }
            else if (cbxPosition.SelectedIndex == 1)
            {
                selectedPosition = Constants.POSITION_STAFF;
                mainPage.showErrorFields(cbxPosition, Constants.ISFALSE);
            }
            else if (cbxPosition.SelectedIndex == 2)
            {
                selectedPosition = Constants.POSITION_ADMIN;
                mainPage.showErrorFields(cbxPosition, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Position", "Position");
                mainPage.showErrorFields(cbxPosition, Constants.ISTRUE);
            }

            doctor.Position = selectedPosition;
            doctor.Income = txtIncomeAmount.Text.Trim();

            if (txtDateHired.SelectedDate != null)
            {
                doctor.HireDate = Convert.ToDateTime(txtDateHired.Text);
                mainPage.showErrorFields(txtDateHired, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Hire Date", "Hire Date");
                mainPage.showErrorFields(txtDateHired, Constants.ISTRUE);
            }

            string selectedDepartment = (cbxDepartment.SelectedValue != null ? cbxDepartment.SelectedValue.ToString() : Constants.DEPARTMENT_GENERAL);

            if (cbxDepartment.SelectedIndex == 0)
            {
                selectedDepartment = Constants.DEPARTMENT_GENERAL;
                mainPage.showErrorFields(cbxDepartment, Constants.ISFALSE);
            }
            else if (cbxDepartment.SelectedIndex == 1)
            {
                selectedDepartment = Constants.DEPARTMENT_SURGERY;
                mainPage.showErrorFields(cbxDepartment, Constants.ISFALSE);
            }
            else if (cbxDepartment.SelectedIndex == 2)
            {
                selectedDepartment = Constants.DEPARTMENT_NURSING;
                mainPage.showErrorFields(cbxDepartment, Constants.ISFALSE);
            }
            else if (cbxDepartment.SelectedIndex == 3)
            {
                selectedDepartment = Constants.DEPARTMENT_PSYCHOLOGY;
                mainPage.showErrorFields(cbxDepartment, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Department", "Department");
                mainPage.showErrorFields(cbxDepartment, Constants.ISTRUE);
            }

            doctor.Department = selectedDepartment;

            string selectedEmpType = (cbxEmploymentType.SelectedValue != null ? cbxEmploymentType.SelectedValue.ToString() : Constants.EMPLOYEETYPE_FULL);

            if (cbxEmploymentType.SelectedIndex == 0)
            {
                selectedEmpType = Constants.EMPLOYEETYPE_FULL;
                mainPage.showErrorFields(cbxEmploymentType, Constants.ISFALSE);
            }
            else if (cbxEmploymentType.SelectedIndex == 1)
            {
                selectedEmpType = Constants.EMPLOYEETYPE_PART;
                mainPage.showErrorFields(cbxEmploymentType, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Employment Type", "Employment Type");
                mainPage.showErrorFields(cbxEmploymentType, Constants.ISTRUE);
            }
            doctor.EmploymentType = selectedEmpType;

            string selectedEmpStatus = (cbxEmployeeStatus.SelectedValue != null ? cbxEmployeeStatus.SelectedValue.ToString() : Constants.EMPLOYEESTATUS_ACTIVE);

            if (cbxEmployeeStatus.SelectedIndex == 0)
            {
                selectedEmpStatus = Constants.EMPLOYEESTATUS_ACTIVE;
                mainPage.showErrorFields(cbxEmployeeStatus, Constants.ISFALSE);
            }
            else if (cbxEmployeeStatus.SelectedIndex == 1)
            {
                selectedEmpStatus = Constants.EMPLOYEESTATUS_ONLEAVE;
                mainPage.showErrorFields(cbxEmployeeStatus, Constants.ISFALSE);
            }
            else if (cbxEmployeeStatus.SelectedIndex == 2)
            {
                selectedEmpStatus = Constants.EMPLOYEESTATUS_INACTIVE;
                mainPage.showErrorFields(cbxEmployeeStatus, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Employment Status", "Employment Status");
                mainPage.showErrorFields(cbxEmployeeStatus, Constants.ISTRUE);
            }
            doctor.EmployeeStatus = selectedEmpStatus;

            string selectedIncomeType = (cbxIncomeType.SelectedValue != null ? cbxIncomeType.SelectedValue.ToString() : "Annually");

            if (cbxIncomeType.SelectedIndex == 0)
            {
                selectedIncomeType = "Annually";
                mainPage.showErrorFields(cbxIncomeType, Constants.ISFALSE);
            }
            else if (cbxIncomeType.SelectedIndex == 1)
            {
                selectedIncomeType = "Daily";
                mainPage.showErrorFields(cbxIncomeType, Constants.ISFALSE);
            }
            else if (cbxIncomeType.SelectedIndex == 2)
            {
                selectedIncomeType = "Hourly";
                mainPage.showErrorFields(cbxIncomeType, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Income Type", "Income Type");
                mainPage.showErrorFields(cbxIncomeType, Constants.ISTRUE);
            }
            doctor.IncomeType = selectedIncomeType;

            string selectedJobTitle = (cbxJobTitle.SelectedValue != null ? cbxJobTitle.SelectedValue.ToString() : Constants.JOBTITLE_RECEPTIONIST);

            if (cbxJobTitle.SelectedIndex == 0)
            {
                selectedJobTitle = Constants.JOBTITLE_RECEPTIONIST;
                mainPage.showErrorFields(cbxJobTitle, Constants.ISFALSE);
            }
            else if (cbxJobTitle.SelectedIndex == 1)
            {
                selectedJobTitle = Constants.JOBTITLE_NURSE;
                mainPage.showErrorFields(cbxJobTitle, Constants.ISFALSE);
            }
            else if (cbxJobTitle.SelectedIndex == 2)
            {
                selectedJobTitle = Constants.JOBTITLE_DOCTOR;
                mainPage.showErrorFields(cbxJobTitle, Constants.ISFALSE);
            }
            else if (cbxJobTitle.SelectedIndex == 3)
            {
                selectedJobTitle = Constants.JOBTITLE_ADMIN;
                mainPage.showErrorFields(cbxJobTitle, Constants.ISFALSE);
            }
            else
            {
                showErrorMessages(", Job Title", "Job Title");
                mainPage.showErrorFields(cbxJobTitle, Constants.ISTRUE);
            }

            if (!cbxJobTitle.Text.Equals(Constants.JOBTITLE_DOCTOR) && !string.IsNullOrEmpty(txtDoctorId.Text.Trim()))
            {
                showErrorMessages(", correct JobTitle for Doctor", "correct JobTitle for Doctor");
                mainPage.showErrorFields(cbxJobTitle, Constants.ISTRUE);
            }
            else
            {
                selectedJobTitle = Constants.JOBTITLE_DOCTOR;
                mainPage.showErrorFields(cbxJobTitle, Constants.ISFALSE);

            }

            doctor.JobTitle = selectedJobTitle;

            int noOfHoursWorked = 0;

            Boolean isHoursNumeric = Int32.TryParse(txtHoursWorked.Text.Trim(), out noOfHoursWorked);

            if (!string.IsNullOrEmpty(txtHoursWorked.Text.Trim()) && noOfHoursWorked != 0)
            {
                if (isHoursNumeric == Constants.ISTRUE)
                {
                    doctor.NoOfHoursWorked = noOfHoursWorked;
                    mainPage.showErrorFields(txtHoursWorked, Constants.ISFALSE);
                }
                else
                {
                    showErrorMessages(", valid No. of Working Hours", "a valid No. of Working Hours");
                    mainPage.showErrorFields(txtHoursWorked, Constants.ISTRUE);
                }
            }
            else
            {
                showErrorMessages(", No. of Working Hours", "No. of Working Hours");
                mainPage.showErrorFields(txtHoursWorked, Constants.ISTRUE);
            }

            if (!string.IsNullOrEmpty(txtStartTime.Text.Trim()))
            {
                try
                {
                    doctor.StartTime = Convert.ToDateTime(txtStartTime.Text).ToShortTimeString();
                    mainPage.showErrorFields(txtStartTime, Constants.ISFALSE);

                }
                catch (Exception)
                {
                    showErrorMessages(", valid Start Time", "a valid Start Time");
                    mainPage.showErrorFields(txtStartTime, Constants.ISTRUE);
                }
            }
            else
            {
                showErrorMessages(", Start Time", "Start Time");
                mainPage.showErrorFields(txtStartTime, Constants.ISTRUE);
            }

            //if (mainPage.isAddEmployeeSelected == Constants.ISTRUE)
            //{
                //Start of Entering Doctor Values
                validateDoctorDetails();
            //}
        }

        private void validateDoctorDetails()
        {
            if (cbxJobTitle.Text.Equals(Constants.JOBTITLE_DOCTOR))
            {
                if (!string.IsNullOrEmpty(txtDoctorLicenseNo.Text.Trim()))
                {
                    doctor.DoctorLicenseNo = txtDoctorLicenseNo.Text.Trim();
                    mainPage.showErrorFields(txtDoctorLicenseNo, Constants.ISFALSE);
                }
                else
                {
                    showErrorMessages(", Doctor License No", "Doctor License No");
                    mainPage.showErrorFields(txtDoctorLicenseNo, Constants.ISTRUE);
                }

                doctor.Specialisation = txtSpecialisation.Text;
                doctor.BuildingLocation = txtBuildingLocation.Text;
                doctor.RoomNumber = txtRoomNo.Text;

            }
        }

        private void doctorInfoVisibility(string visibility)
        {
            if(visibility.Equals("Visible"))
            {
                doctorInfo.Visibility = Visibility.Visible;
                doctorInfoBorder.Visibility = Visibility.Visible;
            } else if (visibility.Equals("Collapsed"))
            {
                doctorInfo.Visibility = Visibility.Collapsed;
                doctorInfoBorder.Visibility = Visibility.Collapsed;
            } else
            {
                doctorInfo.Visibility = Visibility.Hidden;
                doctorInfoBorder.Visibility = Visibility.Hidden;
            }
        }

        private void Doctor_Selected(object sender, RoutedEventArgs e)
        {
            this.Height = 680;
            changeEmployeeBackground("pack://application:,,,/Images/bgSelectedDoctor.jpg");
            lblEmployeeMessage.Content = "";

            if (mainPage.isAddEmployeeSelected == Constants.ISTRUE)
            {
                txtDoctorId.Visibility = Visibility.Hidden;
                txtDoctorId.Text = null;
                lblDoctorId.Visibility = Visibility.Hidden;
                addEmployeeSelected();
                doctorInfoVisibility("Visible");
            }
            else
            {
                if (string.IsNullOrEmpty(txtDoctorId.Text))
                {
                    txtDoctorId.Visibility = Visibility.Hidden;
                    lblDoctorId.Visibility = Visibility.Hidden;
                    doctorInfo.HorizontalAlignment = HorizontalAlignment.Center;
                }
                else
                {
                    txtDoctorId.Visibility = Visibility.Visible;
                    lblDoctorId.Visibility = Visibility.Visible;
                }
                updateEmployeeSelected();
                doctorInfoVisibility("Visible");

                if (string.IsNullOrEmpty(txtDoctorId.Text.Trim()) && !cbxJobTitle.Text.Trim().Equals(Constants.JOBTITLE_DOCTOR))
                {
                    //lblmessage congratuations for being a doctor
                    isJobTitleChangedToDoctor = Constants.ISTRUE;
                    //performed update emp and insert doc
                }
                else
                {
                    isJobTitleChangedToDoctor = Constants.ISFALSE;
                }

            }
        }

        private void jobTitleChanged()
        {
            if (!string.IsNullOrEmpty(txtDoctorId.Text.Trim()) && !cbxJobTitle.SelectedValue.Equals(Constants.JOBTITLE_DOCTOR))
            {
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("All your doctor details will be deleted when you click Update. Do you want to change your job title? ", "Change job title From Doctor?", System.Windows.Forms.MessageBoxButtons.YesNo);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    isJobTitleChangedFromDoctor = Constants.ISTRUE;
                    lblEmployeeMessage.Content = "You selected YES means you confirm to change your job title FROM Doctor." +Environment.NewLine + " Please note that all your doctor details will be deleted upon clicking update.";
                    lblEmployeeMessage.Foreground = Brushes.Blue;
                }
                else
                {
                    isJobTitleChangedFromDoctor = Constants.ISFALSE;
                    cbxJobTitle.SelectedIndex = 2;
                    if (cbxJobTitle.SelectedIndex == 2)
                    {
                        lblEmployeeMessage.Content = "";
                        cbxJobTitle.Text = cbxJobTitle.SelectedValue.ToString();
                        doctorInfoVisibility("Visible");
                    }

                }
            }
        }
        
        private void Receptionist_Selected(object sender, RoutedEventArgs e)
        {
            if (txtDoctorId.Text.Trim() != null)
            {
                this.Height = 680;
            }
            else
            {
                this.Height = 620;
            }
            changeEmployeeBackground("pack://application:,,,/Images/bgSelectedReceptionist.jpg");
            if (mainPage.isAddEmployeeSelected == Constants.ISTRUE)
            {
                addEmployeeSelected();
                doctorInfoVisibility("Collapsed");
            }
            else
            {
                updateEmployeeSelected();
                doctorInfoVisibility("Collapsed");
                jobTitleChanged();

            }
        }
        private void Nurse_Selected(object sender, RoutedEventArgs e)
        {
            if (txtDoctorId.Text.Trim() != null)
            {
                this.Height = 680;
            }
            else
            {
                this.Height = 620;
            }
            changeEmployeeBackground("pack://application:,,,/Images/bgSelectedNurse.jpg");
            if (mainPage.isAddEmployeeSelected == Constants.ISTRUE)
            {
                addEmployeeSelected();
                doctorInfoVisibility("Collapsed");
            }
            else
            {
                updateEmployeeSelected();
                doctorInfoVisibility("Collapsed");
                jobTitleChanged();

            }
        }
        private void Admin_Selected(object sender, RoutedEventArgs e)
        {
            if (txtDoctorId.Text.Trim() != null)
            {
                this.Height = 680;
            }
            else
            {
                this.Height = 620;
            }
            changeEmployeeBackground("pack://application:,,,/Images/bgSelectedAdmin.jpg");
            if (mainPage.isAddEmployeeSelected == Constants.ISTRUE)
            {
                addEmployeeSelected();
                doctorInfoVisibility("Collapsed");
            }
            else
            {
                updateEmployeeSelected();
                doctorInfoVisibility("Collapsed");
                jobTitleChanged();

            }
        }

        private void resetEmployee_onMouseDown(object sender, RoutedEventArgs e)
        {
            resetEmployeeFields();
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
                txtEmployeeDOB.Text = "No date";
            }
            else
            {
                // ... No need to display the time.
                txtEmployeeDOB.Text = date.Value.ToShortDateString();
            }
        }

        private void DateHiredPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get DatePicker reference.
            var picker = sender as DatePicker;

            // ... Get nullable DateTime from SelectedDate.
            DateTime? date = picker.SelectedDate;
            if (date == null)
            {
                // ... A null object.
                txtDateHired.Text = "No date";
            }
            else
            {
                // ... No need to display the time.
                txtDateHired.Text = date.Value.ToShortDateString();
            }
        }

        private void resetEmployeeFields()
        {
            
            txtEmployeeContactNo.Text = "";
            txtEmployeeContactNo.Background = Brushes.White;
            cbxEmployeeContactType.Text = Constants.CONTACTTYPE_HOME;
            txtEmployeeCountry.Text = "";
            txtEmployeeDOB.SelectedDate = null;
            txtEmployeeFName.Text = "";
            txtEmployeeLName.Text = "";
            txtEmployeeAge.Text = "";
            cbxEmployeeGender.Text = Constants.GENDER_FEMALE;
            txtEmployeeAddress.Text = "";
            txtEmployeeMedicareNo.Text = "";
            txtEmployeePostcode.Text = "";
            txtEmployeeState.Text = "";
            txtEmployeeSuburb.Text = "";
            txtEmployeeEmailAdd.Text = "";
            lblEmployeeMessage.Content = "";
            txtEmployeeERContactName.Text = "";
            txtEmployeeERRelationship.Text = "";
            txtEmployeeERContactNo.Text = "";
            txtCompanyName.Text = "";
            txtCompanyAddress.Text = "";
            cbxPosition.Text = Constants.POSITION_EXECUTIVE;
            txtDateHired.SelectedDate = null;
            cbxDepartment.Text = Constants.DEPARTMENT_GENERAL;
            cbxEmployeeStatus.Text = Constants.EMPLOYEESTATUS_ACTIVE;
            cbxEmploymentType.Text = Constants.EMPLOYEETYPE_FULL;
            cbxIncomeType.Text = "Annually";
            txtIncomeAmount.Text = "";
            cbxJobTitle.Text = Constants.JOBTITLE_RECEPTIONIST;
            txtHoursWorked.Text = "";
            txtStartTime.Text = "";
            txtDoctorLicenseNo.Text = "";
            txtRoomNo.Text = "";
            txtBuildingLocation.Text = "";
            txtSpecialisation.Text = "";
            txtEmployeeFName.Background = Brushes.White;
            txtEmployeeLName.Background = Brushes.White;
            txtEmployeeAge.Background = Brushes.White;
            txtEmployeeDOB.Background = Brushes.White;
            txtDateHired.Background = Brushes.White;
            txtHoursWorked.Background = Brushes.White;
            txtStartTime.Background = Brushes.White;
            txtDoctorLicenseNo.Background = Brushes.White;

        }

        private void homeEmployeeIcon_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if (imgHomeEmployeeBtn.IsMouseOver == Constants.ISTRUE)
            {
                imgHomeEmployeeBtn.Opacity = 1.0;
            }
        }

        private void homeEmployeeIcon_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (imgHomeEmployeeBtn.IsMouseOver == Constants.ISFALSE)
            {
                imgHomeEmployeeBtn.Opacity = 0.5;
            }
        }

        private void homeEmployee_onMouseDown(object sender, RoutedEventArgs e)
        {
            this.Hide();
            mainPage.tbEmployeeMainMessage.Text = "";
            mainPage.Show();
        }

        private void exitApplication_onMouseDown(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

    }
}

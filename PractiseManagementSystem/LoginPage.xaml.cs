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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PractiseManagementSystem
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        ConnectionFactory connFactory = new ConnectionFactory();
        DataTable datatable = new DataTable();
        Login login = new Login();

        public LoginPage()
        {
            InitializeComponent();
            clearLoginData();
        }

        private void loginImage_onMouseEnter(object sender, RoutedEventArgs e)
        {
            if(loginImage.IsMouseOver == true)
            {
                loginImage.Opacity = 1.0;
            }
        }

        private void loginImage_onMouseLeave(object sender, RoutedEventArgs e)
        {
            if (loginImage.IsMouseOver == false)
            {
                loginImage.Opacity = 0.4;
            }
        }

        private void login_onMouseDown(object sender, RoutedEventArgs e)
        {
            //Login login = new Login(txtUsername.Text, txtPassword.Text);
            login.Username = txtUsername.Text;
            login.Password = txtPassword.Text;
            //Console.WriteLine("USERNAME -- " + login.Username);
            //Console.WriteLine("PASSWORD -- " + login.Password);
            try
            {
                if (!string.IsNullOrEmpty(txtUsername.Text) && !string.IsNullOrEmpty(txtPassword.Text))
                {
                  Boolean isLoginSuccess =  login.isUserLogin("Login");

                    if (isLoginSuccess == true)
                    {
                        this.Hide();
                        MainContentPage mainContent = new MainContentPage();
                        mainContent.Show();
                    }
                    else
                    {
                        lblLoginMessage.Content = "ERROR: Username or Password is invalid.";
                        lblLoginMessage.Foreground = Brushes.Red;
                    }

                }
                else
                {
                    lblLoginMessage.Content = "ERROR: Username or Password is empty.";
                    lblLoginMessage.Foreground = Brushes.Red;
                }
        }
            catch(Exception ex)
            {
                lblLoginMessage.Content = ex.Message;
                lblLoginMessage.Foreground = Brushes.Red;
            }
            
        }

        private void txtUsername_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.IsUp)
            {
                txtPassword.Text = string.Empty;
                lblLoginMessage.Content = string.Empty;
            }
        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.IsUp)
            {
                lblLoginMessage.Content = string.Empty;
            }
        }

        private void clearLoginData()
        {
            lblLoginMessage.Content = string.Empty;
            txtUsername.Text = string.Empty;
            txtPassword.Text = string.Empty;
        }
    }
}

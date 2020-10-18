using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PractiseManagementSystem
{
    class Login
    {
        ConnectionFactory connectionFactory = new ConnectionFactory();
        int loginId;
        string username;
        string password;

        public Login()
        {

        }

        public Login(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public int LoginId
        {
            get
            {
                return loginId;
            }
            set
            {
                this.loginId = value;
            }
        }
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                this.username = value;
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                this.password = value;
            }
        }
        public Boolean isUserLogin(string tablename)
        {
            string queryString = "select * from " + tablename + " where username='" + this.username + "' and password='" + this.password + "'";

            Boolean userLoginFlag = connectionFactory.isReadDataFromDB(queryString);

            return userLoginFlag;
        }

    }
}

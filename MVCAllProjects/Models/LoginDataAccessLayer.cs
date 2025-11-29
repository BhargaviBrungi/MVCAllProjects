using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Web;
using System.Windows.Controls;

namespace MVCAllProjects.Models
{
    public class LoginDataAccessLayer
    {
        public SqlConnection Connection()
        {
            string Connectionstring = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(Connectionstring);
            return con;
        }
        public int CheckUserDetails(string userName,string password)
        {
            var con = Connection();
            //SqlConnection con = new SqlConnection("data source=LAPTOP-P63QLHUI\\SQLSERVER;database=Employee;integrated security=yes");
            con.Open();
            string query = "Select count(*) from UserRegister where username=@username and Password=@password";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", userName);
            cmd.Parameters.AddWithValue("@Password", password);
            int response = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            return response;
        }
    }
}
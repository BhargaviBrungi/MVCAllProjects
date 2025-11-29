using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace MVCAllProjects.Models
{
    public class UserRegistration
    {
        [Required(ErrorMessage = "Id is mandatory")]
        public int Id { get; set; }
        [Required(ErrorMessage = "Firstname is mandatory field")]
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Lastname is mandatory field")]
        public string Lastname { get; set; }
        [Required(ErrorMessage = "Username is mandatory field")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is mandatory")]
        public string Password { get; set; }
        [Required(ErrorMessage = "ConfirmPassword is Required")]
        [Compare("Password",ErrorMessage ="Password do not match")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression("\\(?\\d{3}\\)?-? *\\d{3}-? *-?\\d{4}", ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }
        [Required(ErrorMessage ="Age is Mandatory")]
        [Range(18, 90, ErrorMessage = "Age should be between 18 to 90")]
        public string Age { get; set; }
        [Required(ErrorMessage ="Select Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage ="Select Country")]
        public string Country { get; set; }
        public List<string> Countries
        {
            get
            {
                var countrylist = new List<string>();
                countrylist.Add("India");
                countrylist.Add("London");
                countrylist.Add("Canada");
                countrylist.Add("Australia");
                countrylist.Add("Swizzerland");
                return countrylist;
            }
        }
        public SqlConnection GetConnetion()
        {
            string connection = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            return con;
        }
        public List<UserRegistration> GetAllUsers()
        {
            var userlist = new List<UserRegistration>();
            var con = GetConnetion();
            SqlCommand cmd = new SqlCommand("SP_GetAllUsers",con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                userlist.Add(new UserRegistration()
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Firstname = Convert.ToString(dr["Firstname"]),
                    Lastname = Convert.ToString(dr["Lastname"]),
                    Username = Convert.ToString(dr["Username"]),
                    Password = Convert.ToString(dr["Password"]),
                    ConfirmPassword = Convert.ToString(dr["ConfirmPassword"]),
                    Email = Convert.ToString(dr["Email"]),
                    Phone = Convert.ToString(dr["Phone"]),
                    Age = Convert.ToString(dr["Age"]),
                    Gender = Convert.ToString(dr["Gender"]),
                    Country = Convert.ToString(dr["Country"])
                });
            }
            return userlist;
        }
        public void AddUser(UserRegistration obj)
        {
           // var con = GetConnetion();
            SqlConnection con = new SqlConnection("data source=LAPTOP-P63QLHUI\\SQLSERVER;database=Employee;integrated security=yes");
            con.Open();
            string query ="Insert into UserRegister values(@Id,@Firstname,@Lastname,@Username,@Password,@ConfirmPassword,@Email,@Phone,@Age,@Gender,@Country)";
            SqlCommand cmd = new SqlCommand(query, con);
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@Firstname", obj.Firstname);
            cmd.Parameters.AddWithValue("@Lastname", obj.Lastname);
            cmd.Parameters.AddWithValue("@Username", obj.Username);
            cmd.Parameters.AddWithValue("@Password", obj.Password);
            cmd.Parameters.AddWithValue("@ConfirmPassword", obj.ConfirmPassword);
            cmd.Parameters.AddWithValue("@Email", obj.Email);
            cmd.Parameters.AddWithValue("@Phone", obj.Phone);
            cmd.Parameters.AddWithValue("@Age", obj.Age);
            cmd.Parameters.AddWithValue("@Gender", obj.Gender);
            cmd.Parameters.AddWithValue("@Country", obj.Country);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void UpdateUser(UserRegistration obj)
        {
            var con = GetConnetion();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_UpdateUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.Parameters.AddWithValue("@Firstname", obj.Firstname);
            cmd.Parameters.AddWithValue("@Lastname", obj.Lastname);
            cmd.Parameters.AddWithValue("@Username", obj.Username);
            cmd.Parameters.AddWithValue("@Password", obj.Password);
            cmd.Parameters.AddWithValue("@ConfirmPassword", obj.ConfirmPassword);
            cmd.Parameters.AddWithValue("@Email", obj.Email);
            cmd.Parameters.AddWithValue("@Phone", obj.Phone);
            cmd.Parameters.AddWithValue("@Age", obj.Age);
            cmd.Parameters.AddWithValue("@Gender", obj.Gender);
            cmd.Parameters.AddWithValue("@Country", obj.Country);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void DeleteUser(UserRegistration obj)
        {
            var con = GetConnetion();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_DeleteUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", obj.Id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
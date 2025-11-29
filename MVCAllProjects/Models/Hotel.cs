using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCAllProjects.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is Mandatory")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please Select Room Type")]
        public string RoomType { get; set; }
        [Required(ErrorMessage = "Select Amenities")]
        public string Amenities { get; set; }
        public string TotalCost { get; set; }
        public List<string> SelectAmenities
        {
            get
            {
                return new List<string> { "AC", "Non AC" };
            }
        }
        public SqlConnection GetConnection()
        {
            var connection = ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            return con;
        }
        public List<Hotel> GetAllRegistrations()
        {
            var list = new List<Hotel>();
            var con = GetConnection();
            SqlCommand cmd = new SqlCommand("SP_GetAllRegistrations", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                list.Add(new Hotel
                {
                    Id = Convert.ToInt32(dr["Id"]),
                    Username = Convert.ToString(dr["Username"]),
                    RoomType = Convert.ToString(dr["RoomType"]),
                    Amenities = Convert.ToString(dr["Amenities"]),
                    TotalCost = Convert.ToString(dr["TotalCost"])
                });
            }
            return list;
        }
        public void AddRegistration(Hotel hotel)
        {
            //var con = GetConnection();
            SqlConnection con = new SqlConnection("data source=LAPTOP-P63QLHUI\\SQLSERVER;database=Employee;integrated security=yes");
            con.Open();
            //string query = "insert into HotelRegistration values(@Username,@RoomType,@Amenities,@TotalCost)";
            SqlCommand cmd = new SqlCommand("SP_AddRegistration", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Username", hotel.Username);
            cmd.Parameters.AddWithValue("@RoomType", hotel.RoomType);
            cmd.Parameters.AddWithValue("@Amenities", hotel.Amenities);
            cmd.Parameters.AddWithValue("@TotalCost", hotel.TotalCost);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void UpdateRegistration(Hotel hotel)
        {
            var con = GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_UpdateRegistration", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", hotel.Id);
            cmd.Parameters.AddWithValue("@Username", hotel.Username);
            cmd.Parameters.AddWithValue("@RoomType", hotel.RoomType);
            cmd.Parameters.AddWithValue("@Amenities", hotel.Amenities);
            cmd.Parameters.AddWithValue("@TotalCost", hotel.TotalCost);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void DeleteRegistration(Hotel hotel)
        {
            var con = GetConnection();
            con.Open();
            SqlCommand cmd = new SqlCommand("SP_DeleteRegistration", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", hotel.Id);
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}
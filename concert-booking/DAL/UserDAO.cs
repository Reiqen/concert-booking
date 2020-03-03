using concert_booking.Common.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace concert_booking.DAL
{
    public class UserDAO : IUserDAO
    {
        private string _sqlConnection = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private SqlDataReader _dataReader;
        private List<User> users;
        public void ChangePassword(int userID, string password)
        {
            using (SqlConnection con = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand("ChangePassword", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CreateUser(int roleID, string username, string password)
        {
            using (SqlConnection con = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand("CreateUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = roleID;
                    cmd.Parameters.Add("@Username", SqlDbType.VarChar).Value = username;
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(int userID)
        {
            using (SqlConnection con = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<User> GetUsers()
        {
            users = new List<User>();
            using (SqlConnection con = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand("GetUsers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    _dataReader = cmd.ExecuteReader();
                    while (_dataReader.Read())
                    {
                        User user = new User();
                        user.UserID = int.Parse(_dataReader.GetValue(0).ToString());
                        user.RoleID = int.Parse(_dataReader.GetValue(1).ToString());
                        user.RoleTitle = _dataReader.GetValue(2).ToString();
                        user.Username = _dataReader.GetValue(3).ToString();
                        user.Password = _dataReader.GetValue(4).ToString();
                        users.Add(user);
                    }
                    return users;
                }
            }
        }
    }
}
using concert_booking.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace concert_booking.DAL
{
    public class RoleDAO : IRoleDAO
    {
        private string _sqlConnection = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private SqlDataReader _dataReader;
        private List<Role> roles;
        public IEnumerable<Role> GetRoles()
        {
            roles = new List<Role>();
            using (SqlConnection con = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand("GetRoles", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    _dataReader = cmd.ExecuteReader();
                    while (_dataReader.Read())
                    {
                        Role role = new Role();
                        role.RoleID = int.Parse(_dataReader.GetValue(0).ToString());
                        role.Title = _dataReader.GetValue(1).ToString();
                        roles.Add(role);
                    }
                    return roles;
                }
            }
        }
    }
}
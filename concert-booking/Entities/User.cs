using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace concert_booking.Entities
{
    public class User
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
        public string RoleTitle { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
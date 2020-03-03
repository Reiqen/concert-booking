using concert_booking.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace concert_booking.BLL
{
    public interface IUserBL
    {
        IEnumerable<User> GetUsers();
        void CreateUser(int roleID, string username, string password);
        void DeleteUser(int userID);
        void ChangePassword(int userID, string password);
        bool PasswordFitsUsername(string username, string password);
    }
}

using static concert_booking.Checkers.ObjectValidation;
using concert_booking.DAL;
using concert_booking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace concert_booking.BLL
{
    public class UserBL : IUserBL
    {
        private readonly IUserDAO _userDAO;
        private readonly IRoleDAO _roleDAO;

        public UserBL(IUserDAO userDAO)
        {
            _userDAO = userDAO;
        }
        public void ChangePassword(int userID, string password)
        {
            var users = _userDAO.GetUsers();
            var ids = new List<int>();
            foreach (var user in users)
            {
                ids.Add(user.UserID);
            }
            if (!IsValidID(userID, ids))
            {
                throw new ArgumentException("No such ID found.");
            }
            else if (password == null || password.Length > 50 || !IsVarcharString(password))
            {
                throw new ArgumentException("The maximum length must be 50 characters or below. It also must be in ASCII, and must not be empty.");
            }
            else _userDAO.ChangePassword(userID, password);
        }

        public void CreateUser(int roleID, string username, string password)
        {
            var roles = _roleDAO.GetRoles();
            var ids = new List<int>();
            foreach (var role in roles)
            {
                ids.Add(role.RoleID);
            }
            if (!IsValidID(roleID, ids))
            {
                throw new ArgumentException("No such ID found.");
            }
            else if (username == null || username.Length > 50 || !IsVarcharString(username))
            {
                throw new ArgumentException("The maximum length must be 50 characters or below. It also must be in ASCII, and must not be empty.");
            }
            else if (password == null || password.Length > 50 || !IsVarcharString(password))
            {
                throw new ArgumentException("The maximum length must be 50 characters or below. It also must be in ASCII, and must not be empty.");
            }
            else _userDAO.CreateUser(roleID, username, password);
        }

        public void DeleteUser(int userID)
        {
            var users = _userDAO.GetUsers();
            var ids = new List<int>();
            foreach (var user in users)
            {
                ids.Add(user.UserID);
            }
            if (!IsValidID(userID, ids))
            {
                throw new ArgumentException("No such ID found.");
            }
            else _userDAO.DeleteUser(userID);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userDAO.GetUsers();
        }
    }
}
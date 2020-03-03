using static concert_booking.Common.Checkers.ObjectValidation;
using concert_booking.DAL;
using concert_booking.Common.Entities;
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

        public UserBL(IUserDAO userDAO, IRoleDAO roleDAO)
        {
            _userDAO = userDAO;
            _roleDAO = roleDAO;
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
                throw new ArgumentException("No such user found.");
            }
            else if (password == null || password == "")
            {
                throw new ArgumentException("Password must not be empty.");
            }
            else if (password.Length > 50)
            {
                throw new ArgumentException("The maximum length of password must be 50 characters or below.");
            }
            else if (!IsVarcharString(password))
            {
                throw new ArgumentException("Password must be in ASCII.");
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
                throw new ArgumentException("No such user found.");
            }
            else if (username == null || username == "")
            {
                throw new ArgumentException("Username must not be empty.");
            }
            else if (UserAlreadyExists(_userDAO.GetUsers(), username))
            {
                throw new ArgumentException("User with such a name already exists.");
            }
            else if (username.Length > 50)
            {
                throw new ArgumentException("The maximum length of username must be 50 characters or below.");
            }
            else if (!IsVarcharString(username))
            {
                throw new ArgumentException("Username must be in ASCII.");
            }
            else if (password == null || password == "")
            {
                throw new ArgumentException("Password must not be empty.");
            }
            else if (password.Length > 50)
            {
                throw new ArgumentException("The maximum length of password must be 50 characters or below.");
            }
            else if (!IsVarcharString(password))
            {
                throw new ArgumentException("Password must be in ASCII.");
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
                throw new ArgumentException("No such user found.");
            }
            else _userDAO.DeleteUser(userID);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userDAO.GetUsers();
        }

        public bool PasswordFitsUsername(string username, string password)
        {
            bool checker = false;
            var users = _userDAO.GetUsers();
            var usernames = new List<string>();
            foreach (var user in users)
            {
                usernames.Add(user.Username);
                if (username == user.Username && password != user.Password)
                {
                    throw new ArgumentException("Wrong password.");
                }
                else if (username == user.Username && password == user.Password)
                {
                    checker = true;
                }
            }
            if (!IsValidUsername(username, usernames))
            {
                throw new ArgumentException("No such user found.");
            }
            return checker;
        }
    }
}
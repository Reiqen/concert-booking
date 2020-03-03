using concert_booking.Common.Dependencies;
using concert_booking.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace concert_booking.PL.Models
{
    public class Roles : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            var userBl = DependencyResolver.UserBL;
            IEnumerable<User> users = userBl.GetUsers();
            var checker = false;
            foreach (var user in users)
            {
                if (username == user.Username)
                {
                    if (roleName == user.RoleTitle)
                    {
                        checker = true;
                    }
                }
            }

            return checker;
        }

        public override string[] GetRolesForUser(string username)
        {
            var roles = new string[1];
            if (username == "Guest")
            {
                roles[0] = "Guest";
            }
            else
            {
                var userBl = DependencyResolver.UserBL;
                IEnumerable<User> users = userBl.GetUsers();
                foreach (var user in users)
                {
                    if (username == user.Username)
                    {
                        roles[0] = user.RoleTitle;
                    }
                }
            }
            return roles;
        }

        #region NOT_IMPLEMENTED
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }



        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
using concert_booking.DAL;
using concert_booking.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace concert_booking.BLL
{
    public class RoleBL : IRoleBL
    {
        private readonly IRoleDAO _roleDAO;

        public RoleBL(IRoleDAO roleDAO)
        {
            _roleDAO = roleDAO;
        }
        public IEnumerable<Role> GetRoles()
        {
            return _roleDAO.GetRoles();
        }
    }
}
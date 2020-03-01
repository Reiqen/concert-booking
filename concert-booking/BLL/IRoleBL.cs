using concert_booking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace concert_booking.BLL
{
    public interface IRoleBL
    {
        IEnumerable<Role> GetRoles();
    }
}

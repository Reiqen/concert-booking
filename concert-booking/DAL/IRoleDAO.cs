using concert_booking.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace concert_booking.DAL
{
    public interface IRoleDAO
    {
        IEnumerable<Role> GetRoles();
    }
}

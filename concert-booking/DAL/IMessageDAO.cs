using concert_booking.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace concert_booking.DAL
{
    public interface IMessageDAO
    {
        IEnumerable<Message> GetMessages();
        void CreateMessage(int fromID, int toID, DateTime date, string subject, string text, byte[] pictureBytes);
        void DeleteMessage(int messageID);
    }
}

using concert_booking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace concert_booking.BLL
{
    public interface IMessageBL
    {
        IEnumerable<Message> GetMessages();
        void CreateMessage(int fromID, int toID, string dateString, string subject, string text, byte[] pictureBytes);
        void DeleteMessage(int messageID);
    }
}

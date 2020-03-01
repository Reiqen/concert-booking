using static concert_booking.Checkers.ObjectValidation;
using concert_booking.DAL;
using concert_booking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace concert_booking.BLL
{
    public class MessageBL : IMessageBL
    {
        private readonly IMessageDAO _messageDAO;
        private readonly IUserDAO _userDAO;

        public MessageBL(IMessageDAO messageDAO)
        {
            _messageDAO = messageDAO;
        }
        public void CreateMessage(int fromID, int toID, string dateString, string subject, string text, byte[] pictureBytes)
        {
            var users = _userDAO.GetUsers();
            var ids = new List<int>();
            foreach (var user in users)
            {
                ids.Add(user.UserID);
            }
            if (!IsValidID(fromID, ids))
            {
                throw new ArgumentException("No such ID found (from).");
            }
            else if (!IsValidID(toID, ids))
            {
                throw new ArgumentException("No such ID found (to).");
            }
            else if (dateString == null || !IsValidDate(dateString))
            {
                throw new ArgumentException("Wrong date definition. It also cannot be empty.");
            }
            else if (subject != null && subject.Length > 50)
            {
                throw new ArgumentException("The maximum length must be 50 characters or below.");
            }
            else if (text == null)
            {
                throw new ArgumentException("It cannot be empty.");
            }
            else if (pictureBytes != null && !IsValidImage(pictureBytes))
            {
                throw new ArgumentException("Incorrect file which cannot be converted into an image.");
            }
            else
            {
                DateTime date = DateTime.ParseExact(dateString, "dd.MM.yyyy hh:mm", CultureInfo.InvariantCulture, DateTimeStyles.None);
                _messageDAO.CreateMessage(fromID, toID, date, subject, text, pictureBytes);
            }
        }

        public void DeleteMessage(int messageID)
        {
            var messages = _messageDAO.GetMessages();
            var ids = new List<int>();
            foreach (var message in messages)
            {
                ids.Add(message.MessageID);
            }
            if (!IsValidID(messageID, ids))
            {
                throw new ArgumentException("No such ID found.");
            }
            else _messageDAO.DeleteMessage(messageID);
        }

        public IEnumerable<Message> GetMessages()
        {
            return _messageDAO.GetMessages();
        }
    }
}
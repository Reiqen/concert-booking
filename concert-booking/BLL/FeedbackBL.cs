using static concert_booking.Common.Checkers.ObjectValidation;
using concert_booking.DAL;
using concert_booking.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace concert_booking.BLL
{
    public class FeedbackBL : IFeedbackBL
    {
        private readonly IFeedbackDAO _feedbackDAO;
        private readonly IConcertDAO _concertDAO;
        private readonly IUserDAO _userDAO;

        public FeedbackBL(IFeedbackDAO feedbackDAO, IConcertDAO concertDAO, IUserDAO userDAO)
        {
            _feedbackDAO = feedbackDAO;
            _concertDAO = concertDAO;
            _userDAO = userDAO;
        }
        public void CreateFeedback(int concertID, int userID, string dateString, string text)
        {
            var concerts = _concertDAO.GetConcerts();
            var users = _userDAO.GetUsers();
            var idsConcerts = new List<int>();
            var idsUsers = new List<int>();
            foreach (var concert in concerts)
            {
                idsConcerts.Add(concert.ConcertID);
            }
            foreach (var user in users)
            {
                idsUsers.Add(user.UserID);
            }
            if (!IsValidID(concertID, idsConcerts))
            {
                throw new ArgumentException("No such ID found (concert).");
            }
            else if (!IsValidID(userID, idsUsers))
            {
                throw new ArgumentException("No such ID found (user).");
            }
            else if (dateString == null || !IsValidDate(dateString))
            {
                throw new ArgumentException("Wrong date definition. It also cannot be empty.");
            }
            else if (text == null)
            {
                throw new ArgumentException("It cannot be empty.");
            }
            else
            {
                DateTime date = DateTime.ParseExact(dateString, "dd.MM.yyyy hh:mm", CultureInfo.InvariantCulture, DateTimeStyles.None);
                _feedbackDAO.CreateFeedback(concertID, userID, date, text);
            }
        }

        public IEnumerable<Feedback> GetFeedbacks()
        {
            return _feedbackDAO.GetFeedbacks();
        }
    }
}
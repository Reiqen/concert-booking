using concert_booking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace concert_booking.DAL
{
    public interface IFeedbackDAO
    {
        IEnumerable<Feedback> GetFeedbacks();
        void CreateFeedback(int concertID, int userID, DateTime date, string text);
    }
}

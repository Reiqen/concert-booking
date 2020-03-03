using concert_booking.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace concert_booking.BLL
{
    public interface IFeedbackBL
    {
        IEnumerable<Feedback> GetFeedbacks();
        void CreateFeedback(int concertID, int userID, string dateString, string text);
    }
}

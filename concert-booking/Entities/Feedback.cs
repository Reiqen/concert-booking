using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace concert_booking.Entities
{
    public class Feedback
    {
        public int FeedbackID { get; set; }
        public int ConcertID { get; set; }
        public string ConcertTitle { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace concert_booking.Entities
{
    public class Message
    {
        public int MessageID { get; set; }
        public int FromID { get; set; }
        public string FromUser { get; set; }
        public int ToID { get; set; }
        public string ToUser { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }
        public byte[] PictureBytes { get; set; }
    }
}
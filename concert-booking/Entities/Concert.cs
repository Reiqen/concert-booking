using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace concert_booking.Entities
{
    public class Concert
    {
        public int ConcertID { get; set; }
        public byte[] PictureBytes { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
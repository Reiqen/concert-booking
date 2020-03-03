using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace concert_booking.Common.Entities
{
    public class Image
    {
        public int ImageID { get; set; }
        public int ConcertID { get; set; }
        public string ConcertTitle { get; set; }
        public byte[] ImageBytes { get; set; }
    }
}
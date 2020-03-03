using concert_booking.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace concert_booking.BLL
{
    public interface IImageBL
    {
        IEnumerable<Image> GetImages();
        void CreateImage(int concertID, byte[] imageBytes);
        void DeleteImage(int imageID);
    }
}

using static concert_booking.Common.Checkers.ObjectValidation;
using concert_booking.DAL;
using concert_booking.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace concert_booking.BLL
{
    public class ImageBL : IImageBL
    {
        private readonly IImageDAO _imageDAO;
        private readonly IConcertDAO _concertDAO;

        public ImageBL(IImageDAO imageDAO, IConcertDAO concertDAO)
        {
            _imageDAO = imageDAO;
            _concertDAO = concertDAO;
        }
        public void CreateImage(int concertID, byte[] imageBytes)
        {
            var concerts = _concertDAO.GetConcerts();
            var ids = new List<int>();
            foreach (var concert in concerts)
            {
                ids.Add(concert.ConcertID);
            }
            if (!IsValidID(concertID, ids))
            {
                throw new ArgumentException("No such ID found.");
            }
            else if (imageBytes == null || !IsValidImage(imageBytes))
            {
                throw new ArgumentException("Incorrect file which cannot be converted into an image. It also cannot be empty.");
            }
            else _imageDAO.CreateImage(concertID, imageBytes);
        }

        public void DeleteImage(int imageID)
        {
            var images = _imageDAO.GetImages();
            var ids = new List<int>();
            foreach (var image in images)
            {
                ids.Add(image.ImageID);
            }
            if (!IsValidID(imageID, ids))
            {
                throw new ArgumentException("No such ID found.");
            }
            else _imageDAO.DeleteImage(imageID);
        }

        public IEnumerable<Image> GetImages()
        {
            return _imageDAO.GetImages();
        }
    }
}
using static concert_booking.Checkers.ObjectValidation;
using concert_booking.DAL;
using concert_booking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace concert_booking.BLL
{
    public class ConcertBL : IConcertBL
    {
        private readonly IConcertDAO _concertDAO;

        public ConcertBL(IConcertDAO concertDAO)
        {
            _concertDAO = concertDAO;
        }
        public void CreateConcert(byte[] pictureBytes, string title, string description)
        {
            if (pictureBytes != null && !IsValidImage(pictureBytes))
            {
                throw new ArgumentException("Incorrect file which cannot be converted into an image.");
            }
            else if (title == null || title.Length > 50)
            {
                throw new ArgumentException("The maximum length must be 50 characters or below. It also cannot be empty.");
            }
            else _concertDAO.CreateConcert(pictureBytes, title, description);
        }

        public void DeleteConcert(int concertID)
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
            else _concertDAO.DeleteConcert(concertID);
        }

        public IEnumerable<Concert> GetConcerts()
        {
            return _concertDAO.GetConcerts();
        }

        public void UpdateConcert(int concertID, byte[] pictureBytes, string title, string description)
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
            else if (pictureBytes != null && !IsValidImage(pictureBytes))
            {
                throw new ArgumentException("Incorrect file which cannot be converted into an image.");
            }
            else if (title == null || title.Length > 50)
            {
                throw new ArgumentException("The maximum length must be 50 characters or below. It also cannot be empty.");
            }
            else _concertDAO.UpdateConcert(concertID, pictureBytes, title, description);
        }
    }
}
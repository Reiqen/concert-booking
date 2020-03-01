﻿using concert_booking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace concert_booking.DAL
{
    public interface IConcertDAO
    {
        IEnumerable<Concert> GetConcerts();
        void CreateConcert(byte[] pictureBytes, string title, string description);
        void DeleteConcert(int concertID);
        void UpdateConcert(int concertID, byte[] pictureBytes, string title, string description);
    }
}
using concert_booking.Common.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace concert_booking.DAL
{
    public class ConcertDAO : IConcertDAO
    {
        private string _sqlConnection = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private SqlDataReader _dataReader;
        private List<Concert> concerts;
        public void CreateConcert(byte[] pictureBytes, string title, string description)
        {
            using (SqlConnection con = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand("CreateConcert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Picture", SqlDbType.Image).Value = pictureBytes;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = title;
                    cmd.Parameters.Add("@Description", SqlDbType.NText).Value = description;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteConcert(int concertID)
        {
            using (SqlConnection con = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteConcert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ConcertID", SqlDbType.Int).Value = concertID;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Concert> GetConcerts()
        {
            concerts = new List<Concert>();
            using (SqlConnection con = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand("GetConcerts", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    _dataReader = cmd.ExecuteReader();
                    while (_dataReader.Read())
                    {
                        Concert concert = new Concert();

                        concert.ConcertID = int.Parse(_dataReader.GetValue(0).ToString());
                        byte[] byteArray;
                        if (_dataReader.GetValue(1) != DBNull.Value)
                        {
                            byteArray = new byte[((byte[])_dataReader.GetValue(1)).Length];
                            byteArray = (byte[])_dataReader.GetValue(1);
                        }
                        else
                        {
                            byteArray = null;
                        }
                        concert.PictureBytes = byteArray;
                        concert.Title = _dataReader.GetValue(2).ToString();
                        concert.Description = _dataReader.GetValue(3).ToString();

                        concerts.Add(concert);
                    }
                    return concerts;
                }
            }
        }

        public void UpdateConcert(int concertID, byte[] pictureBytes, string title, string description)
        {
            using (SqlConnection con = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateConcert", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ConcertID", SqlDbType.Int).Value = concertID;
                    cmd.Parameters.Add("@Picture", SqlDbType.Image).Value = pictureBytes;
                    cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = title;
                    cmd.Parameters.Add("@Description", SqlDbType.NText).Value = description;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
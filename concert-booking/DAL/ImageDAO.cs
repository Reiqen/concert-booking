using concert_booking.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace concert_booking.DAL
{
    public class ImageDAO : IImageDAO
    {
        private string _sqlConnection = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private SqlDataReader _dataReader;
        private List<Image> images;
        public void CreateImage(int concertID, byte[] imageBytes)
        {
            using (SqlConnection con = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand("CreateImage", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ConcertID", SqlDbType.Int).Value = concertID;
                    cmd.Parameters.Add("@Image", SqlDbType.Image).Value = imageBytes;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteImage(int imageID)
        {
            using (SqlConnection con = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteImage", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ImageID", SqlDbType.Int).Value = imageID;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Image> GetImages()
        {
            images = new List<Image>();
            using (SqlConnection con = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand("GetImages", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    _dataReader = cmd.ExecuteReader();
                    while (_dataReader.Read())
                    {
                        Image image = new Image();

                        image.ImageID = int.Parse(_dataReader.GetValue(0).ToString());
                        image.ConcertID = int.Parse(_dataReader.GetValue(1).ToString());
                        image.ConcertTitle = _dataReader.GetValue(2).ToString();
                        byte[] byteArray;
                        if (_dataReader.GetValue(3) != DBNull.Value)
                        {
                            byteArray = new byte[((byte[])_dataReader.GetValue(3)).Length];
                            byteArray = (byte[])_dataReader.GetValue(3);
                        }
                        else
                        {
                            byteArray = null;
                        }
                        image.ImageBytes = byteArray;

                        images.Add(image);
                    }
                    return images;
                }
            }
        }
    }
}
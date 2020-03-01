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
    public class MessageDAO : IMessageDAO
    {
        private string _sqlConnection = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private SqlDataReader _dataReader;
        private List<Message> messages;
        public void CreateMessage(int fromID, int toID, DateTime date, string subject, string text, byte[] pictureBytes)
        {
            using (SqlConnection con = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand("CreateMessage", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@FromID", SqlDbType.Int).Value = fromID;
                    cmd.Parameters.Add("@ToID", SqlDbType.Int).Value = toID;
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = date;
                    cmd.Parameters.Add("@Subject", SqlDbType.NVarChar).Value = subject;
                    cmd.Parameters.Add("@Text", SqlDbType.NText).Value = text;
                    cmd.Parameters.Add("@Picture", SqlDbType.Image).Value = pictureBytes;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteMessage(int messageID)
        {
            using (SqlConnection con = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteMessage", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@MessageID", SqlDbType.Int).Value = messageID;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Message> GetMessages()
        {
            messages = new List<Message>();
            using (SqlConnection con = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand("GetMessages", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    _dataReader = cmd.ExecuteReader();
                    while (_dataReader.Read())
                    {
                        Message message = new Message();

                        message.MessageID = int.Parse(_dataReader.GetValue(0).ToString());
                        message.FromID = int.Parse(_dataReader.GetValue(1).ToString());
                        message.FromUser = _dataReader.GetValue(2).ToString();
                        message.ToID = int.Parse(_dataReader.GetValue(3).ToString());
                        message.ToUser = _dataReader.GetValue(4).ToString();
                        message.Date = DateTime.Parse(_dataReader.GetValue(5).ToString());
                        message.Subject = _dataReader.GetValue(6).ToString();
                        message.Text = _dataReader.GetValue(7).ToString();
                        byte[] byteArray;
                        if (_dataReader.GetValue(8) != DBNull.Value)
                        {
                            byteArray = new byte[((byte[])_dataReader.GetValue(8)).Length];
                            byteArray = (byte[])_dataReader.GetValue(8);
                        }
                        else
                        {
                            byteArray = null;
                        }
                        message.PictureBytes = byteArray;

                        messages.Add(message);
                    }
                    return messages;
                }
            }
        }
    }
}
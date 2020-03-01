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
    public class FeedbackDAO : IFeedbackDAO
    {
        private string _sqlConnection = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        private SqlDataReader _dataReader;
        private List<Feedback> feedbacks;
        public void CreateFeedback(int concertID, int userID, DateTime date, string text)
        {
            using (SqlConnection con = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand("CreateFeedback", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@ConcertID", SqlDbType.Int).Value = concertID;
                    cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime).Value = date;
                    cmd.Parameters.Add("@Text", SqlDbType.NText).Value = text;

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Feedback> GetFeedbacks()
        {
            feedbacks = new List<Feedback>();
            using (SqlConnection con = new SqlConnection(_sqlConnection))
            {
                using (SqlCommand cmd = new SqlCommand("GetFeedbacks", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    cmd.ExecuteNonQuery();

                    _dataReader = cmd.ExecuteReader();
                    while (_dataReader.Read())
                    {
                        Feedback feedback = new Feedback();

                        feedback.FeedbackID = int.Parse(_dataReader.GetValue(0).ToString());
                        feedback.ConcertID = int.Parse(_dataReader.GetValue(1).ToString());
                        feedback.ConcertTitle = _dataReader.GetValue(2).ToString();
                        feedback.UserID = int.Parse(_dataReader.GetValue(3).ToString());
                        feedback.Username = _dataReader.GetValue(4).ToString();
                        feedback.Date = DateTime.Parse(_dataReader.GetValue(5).ToString());
                        feedback.Text = _dataReader.GetValue(6).ToString();

                        feedbacks.Add(feedback);
                    }
                    return feedbacks;
                }
            }
        }
    }
}
using BookstoreModel;
using BookstoreRepository.IBookstoreRepository;
using Microsoft.Extensions.Configuration;
using NlogImplementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Text;

namespace BookstoreRepository.BookstoreRepository
{
    public class CustomerFeedbackRepository: ICustomerFeedbackRepository
    {
        NlogOperation nlog = new NlogOperation();
        private SqlConnection objSqlConnection;
        public readonly IConfiguration configuration;
        public CustomerFeedbackRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private void Connection()
        {
            string connectionstr = this.configuration[("ConnectionStrings:UserDbConnection")];
            objSqlConnection = new SqlConnection(connectionstr);
        }

        public Feedback AddFeedback(Feedback objFeedBack)
        {
            try
            {
                Connection();
                SqlCommand objSqlCommand = new SqlCommand("SP_AddFeedback", objSqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.Parameters.AddWithValue("@UserId", objFeedBack.UserId);
                objSqlCommand.Parameters.AddWithValue("@BookId", objFeedBack.BookId);
                objSqlCommand.Parameters.AddWithValue("@CustomerDescription", objFeedBack.CustomerDescription);
                objSqlCommand.Parameters.AddWithValue("@Rating", objFeedBack.Rating);
                objSqlConnection.Open();
                SqlDataReader reader = objSqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    objFeedBack.User = new User
                    {
                        UserId = (int)reader["userId"],
                        FullName = (string)reader["FullName"],
                        Email = (string)reader["email"],
                        Password = (string)reader["Password"],
                        MobileNumber = (string)reader["MobileNumber"]
                    };
                    objFeedBack.Book = new Book()
                    {
                        BookId = Convert.ToInt32(reader["BookId"]),
                        BookName = Convert.ToString(reader["bookName"]),
                        BookDescription = Convert.ToString(reader["BookDescription"]),
                        BookAuthor = Convert.ToString(reader["BookAuthor"]),
                        BookImage = Convert.ToString(reader["BookImage"]),
                        BookCount = Convert.ToInt32(reader["BookCount"]),
                        BookPrize = Convert.ToInt32(reader["BookPrize"]),
                        Rating = Convert.ToDecimal(reader["Rating"]),
                        IsAvailable = Convert.ToBoolean(reader["IsAvailable"])
                    };
                    nlog.LogDebug("Feedback added successfull for " + objFeedBack.CustomerDescription);
                }
                else
                {
                    nlog.LogDebug("Feedback added Unsuccessfull for " + objFeedBack.CustomerDescription);
                }
                objSqlConnection.Close();
                return objFeedBack;
            }
            catch (Exception ex)
            {
                nlog.LogError("book added Unsuccessfull due to " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                objSqlConnection.Close();
            }
        }
        public List<Feedback> GetFeedback(int bookId)
        {
            try
            {
                Connection();
                SqlCommand objSqlCommand = new SqlCommand("SP_GetFeedback", objSqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.Parameters.AddWithValue("@BookId", bookId);
                SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);
                DataTable objDataTable = new DataTable();
                objSqlConnection.Open();
                objSqlDataAdapter.Fill(objDataTable);
                objSqlConnection.Close();
                List<Feedback> feedbacks = new List<Feedback>();
                foreach (DataRow reader in objDataTable.Rows)
                {
                    Feedback objFeedBack = new Feedback();
                    objFeedBack.FeedbackId = (int)reader["feedbackId"];
                    objFeedBack.UserId = (int)reader["userId"];
                    objFeedBack.BookId = Convert.ToInt32(reader["BookId"]);
                    objFeedBack.CustomerDescription = Convert.ToString(reader["customerDescription"]);
                    objFeedBack.Rating = Convert.ToInt32(reader["Rating"]);
                    objFeedBack.User = new User
                    {
                        UserId = (int)reader["userId"],
                        FullName = (string)reader["FullName"],
                        Email = (string)reader["email"],
                        Password = (string)reader["Password"],
                        MobileNumber = (string)reader["MobileNumber"],
                        IsAdmin=(Boolean)reader["isAdmin"]
                    };
                    objFeedBack.Book = new Book()
                    {
                        BookId = Convert.ToInt32(reader["BookId"]),
                        BookName = Convert.ToString(reader["bookName"]),
                        BookDescription = Convert.ToString(reader["BookDescription"]),
                        BookAuthor = Convert.ToString(reader["BookAuthor"]),
                        BookImage = Convert.ToString(reader["BookImage"]),
                        BookCount = Convert.ToInt32(reader["BookCount"]),
                        BookPrize = Convert.ToInt32(reader["BookPrize"]),
                        Rating = Convert.ToDecimal(reader["Rating"]),
                        IsAvailable = Convert.ToBoolean(reader["IsAvailable"])
                    };
                    feedbacks.Add(objFeedBack);
                    nlog.LogDebug("Feedback added successfull for " + objFeedBack.CustomerDescription);
                }
                objSqlConnection.Close();
                return feedbacks;
            }
            catch (Exception ex)
            {
                nlog.LogError("book added Unsuccessfull due to " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                objSqlConnection.Close();
            }
        }
    }
}

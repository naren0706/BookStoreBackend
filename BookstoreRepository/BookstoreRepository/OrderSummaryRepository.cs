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
    public class OrderSummaryRepository : IOrderSummaryRepository
    {
        NlogOperation nlog = new NlogOperation();
        private SqlConnection objSqlConnection;
        public readonly IConfiguration configuration;
        private void Connection()
        {
            string connectionstr = this.configuration[("ConnectionStrings:UserDbConnection")];
            objSqlConnection = new SqlConnection(connectionstr);
        }
        public OrderSummaryRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public List<OrderSummary> GetAllSummmary(int userId)
        {
            try
            {
                List<OrderSummary> objList = new List<OrderSummary>();
                OrderSummary objUser = new OrderSummary();
                Connection();
                SqlCommand com = new SqlCommand("SP_GetOrderSummary", objSqlConnection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@userId", userId);
                SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(com);
                DataTable objDataTable = new DataTable();
                objSqlConnection.Open();
                objSqlDataAdapter.Fill(objDataTable);
                objSqlConnection.Close();
                foreach (DataRow dr in objDataTable.Rows)
                {
                    objUser = new OrderSummary
                    {
                        SummaryId = (int)dr["SummaryId"],
                        CartId = (int)dr["CartId"],
                        Cart = new Cart()
                        {
                            CartId = (int)dr["CartId"],
                            BookCount = (int)dr["BookCount"],
                            UserId = (int)dr["UserId"],
                            User = new User() 
                            {
                                UserId = (int)dr["userId"],
                                FullName = (string)dr["FullName"],
                                Email = (string)dr["email"],
                                Password = (string)dr["Password"],
                                MobileNumber = (string)dr["MobileNumber"],
                                IsAdmin = (bool)dr["IsAdmin"]
                                
                            },
                            BookId = (int)dr["BookId"],
                            Book = new Book()
                            {
                                BookId = Convert.ToInt32(dr["BookId"]),
                                BookName = Convert.ToString(dr["bookName"]),
                                BookDescription = Convert.ToString(dr["BookDescription"]),
                                BookAuthor = Convert.ToString(dr["BookAuthor"]),
                                BookImage = Convert.ToString(dr["BookImage"]),
                                BookCount = Convert.ToInt32(dr["BookCount"]),
                                BookPrize = Convert.ToInt32(dr["BookPrize"]),
                                Rating = Convert.ToInt32(dr["Rating"]),
                                IsAvailable = Convert.ToBoolean(dr["IsAvailable"])
                            }
                        }
                    };
                    objList.Add(objUser);
                }
                objSqlConnection.Close();
                nlog.LogDebug("summary has been reterived " + objUser.SummaryId);
                return objList;

            }
            catch (Exception ex)
            {
                nlog.LogError("summary has not been reterived due to " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally { objSqlConnection.Close(); }

        }
    }
}

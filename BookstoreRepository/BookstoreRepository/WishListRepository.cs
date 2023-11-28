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
    public class WishListRepository : IWishListRepository
    {
        NlogOperation nlog = new NlogOperation();
        private SqlConnection objSqlConnection;
        public readonly IConfiguration configuration;
        private void Connection()
        {
            string connectionstr = this.configuration[("ConnectionStrings:UserDbConnection")];
            objSqlConnection = new SqlConnection(connectionstr);
        }
        public WishListRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public WishList AddToWishLoist(int userId,int bookId)
        {
            WishList objwishList = new WishList();
            try
            {
                bool result;
                Connection();
                SqlCommand objSqlCommand = new SqlCommand("SP_AddToWIshList", objSqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.Parameters.AddWithValue("@userId", userId);
                objSqlCommand.Parameters.AddWithValue("@bookId", bookId);
                objSqlConnection.Open();
                var SqlValue= objSqlCommand.ExecuteScalar();
                SqlDataReader reader = objSqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    result = true;
                    objwishList.BookId = Convert.ToInt32(reader["BookId"]);
                    objwishList.Book = new Book()
                    {
                        BookId = Convert.ToInt32(reader["BookId"]),
                        BookName = Convert.ToString(reader["bookName"]),
                        BookDescription = Convert.ToString(reader["BookDescription"]),
                        BookAuthor = Convert.ToString(reader["BookAuthor"]),
                        BookImage = Convert.ToString(reader["BookImage"]),
                        BookCount = Convert.ToInt32(reader["BookCount"]),
                        BookPrize = Convert.ToInt32(reader["BookPrize"]),
                        Rating = Convert.ToInt32(reader["Rating"]),
                        IsAvailable = Convert.ToBoolean(reader["IsAvailable"])
                    };
                    objwishList.UserId = Convert.ToInt32(reader["userId"]);
                    objwishList.User = new User
                    {
                        UserId = (int)reader["userId"],
                        FullName = (string)reader["FullName"],
                        Email = (string)reader["email"],
                        Password = (string)reader["Password"],
                        MobileNumber = (string)reader["MobileNumber"]
                    };
                    objwishList.WishListId = Convert.ToInt32(reader["WishListId"]);
                }
                else
                {
                    result = false;
                    nlog.LogError("book is not added to wishlist : \"");
                }
                objSqlConnection.Close();
                if (!result)
                {
                    return null;
                }
                return objwishList;
            }
            catch (Exception ex)
            {
                nlog.LogError("book is not added to wishlist due to " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveBookFromWishList(int userId, string bookId)
        {
            try
            {
                Connection();
                SqlCommand objSqlCommand = new SqlCommand("SP_RemoveFromWishList", objSqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.Parameters.AddWithValue("@userId", userId);
                objSqlCommand.Parameters.AddWithValue("@bookId", bookId);
                objSqlConnection.Open();
                var SqlValue= objSqlCommand.ExecuteScalar();
                nlog.LogError("book is removed from wishlist : ");
                return true;
            }
            catch(Exception ex)
            {
                nlog.LogError("book is not removed to wishlist due to " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public List<WishList> GetAllWishListBooks(int userId)
        {
            try
            {
                Connection();
                List<WishList> ListwishList = new List<WishList>();
                SqlCommand objSqlCommand = new SqlCommand("SP_GetWishList", objSqlConnection);
                objSqlCommand.Parameters.AddWithValue("@userID", userId);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);
                DataTable objDataTable = new DataTable();
                objSqlConnection.Open();
                objSqlDataAdapter.Fill(objDataTable);
                objSqlConnection.Close();
                WishList objwishList;
                foreach (DataRow reader in objDataTable.Rows)
                {
                    objwishList = new WishList();
                    objwishList.BookId = Convert.ToInt32(reader["BookId"]);
                    objwishList.Book = new Book()
                    {
                        BookId = Convert.ToInt32(reader["BookId"]),
                        BookName = Convert.ToString(reader["bookName"]),
                        BookDescription = Convert.ToString(reader["BookDescription"]),
                        BookAuthor = Convert.ToString(reader["BookAuthor"]),
                        BookImage = Convert.ToString(reader["BookImage"]),
                        BookCount = Convert.ToInt32(reader["BookCount"]),
                        BookPrize = Convert.ToInt32(reader["BookPrize"]),
                        Rating = Convert.ToInt32(reader["Rating"]),
                        IsAvailable = Convert.ToBoolean(reader["IsAvailable"])
                    };
                    objwishList.UserId = Convert.ToInt32(reader["userId"]);
                    objwishList.User = new User
                    {
                        UserId = (int)reader["userId"],
                        FullName = (string)reader["FullName"],
                        Email = (string)reader["email"],
                        Password = (string)reader["Password"],
                        MobileNumber = (string)reader["MobileNumber"]
                    };
                    objwishList.WishListId = Convert.ToInt32(reader["WishListId"]);

                    ListwishList.Add(objwishList);
                }
                return ListwishList;
            }
            catch (Exception ex)
            {
                nlog.LogError("book added Unsuccessfull due to " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}

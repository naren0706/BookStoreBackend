using BookstoreModel;
using BookstoreRepository.IBookstoreRepository;
using Microsoft.Extensions.Configuration;
using NlogImplementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace BookstoreRepository.BookstoreRepository
{
    public class WishListRepository : IWishListRepository
    {
        NlogOperation nlog = new NlogOperation();
        private SqlConnection con;
        public readonly IConfiguration configuration;
        private void Connection()
        {
            string connectionstr = this.configuration[("ConnectionStrings:UserDbConnection")];
            con = new SqlConnection(connectionstr);
        }
        public WishListRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public WishList AddToWishLoist(WishList objwishList)
        {
            try
            {
                bool result;
                Connection();
                SqlCommand com = new SqlCommand("SP_AddToWIshList", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@userId", objwishList.UserId);
                com.Parameters.AddWithValue("@bookId", objwishList.BookId);
                con.Open();
                var i = com.ExecuteScalar();
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    result = true;
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
                    objwishList.User = new User
                    {
                        UserId = (int)reader["userId"],
                        FullName = (string)reader["FullName"],
                        Email = (string)reader["email"],
                        Password = (string)reader["Password"],
                        MobileNumber = (string)reader["MobileNumber"]
                    };

                }
                else
                {
                    result = false;
                    nlog.LogError("book is not added to wishlist : \"");
                }
                con.Close();
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
                SqlCommand com = new SqlCommand("SP_RemoveFromWishList", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@userId", userId);
                com.Parameters.AddWithValue("@bookId", bookId);
                con.Open();
                var i = com.ExecuteScalar();
                nlog.LogError("book is not added to wishlist : \"");
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
            throw new NotImplementedException();
        }
    }
}

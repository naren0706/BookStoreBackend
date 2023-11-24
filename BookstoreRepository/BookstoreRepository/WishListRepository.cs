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
        public WishList AddToWishLoist(int userId,int bookId)
        {
            WishList objwishList = new WishList();
            try
            {
                bool result;
                Connection();
                SqlCommand com = new SqlCommand("SP_AddToWIshList", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@userId", userId);
                com.Parameters.AddWithValue("@bookId", bookId);
                con.Open();
                var i = com.ExecuteScalar();
                SqlDataReader reader = com.ExecuteReader();
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
                SqlCommand com = new SqlCommand("SP_GetWishList", con);
                com.Parameters.AddWithValue("@userID", userId);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                WishList objwishList;
                foreach (DataRow reader in dt.Rows)
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

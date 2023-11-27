﻿using BookstoreModel;
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
    public class CartRepository: ICartRepository
    {
        NlogOperation nlog = new NlogOperation();
        private SqlConnection objSqlConnection;
        public readonly IConfiguration configuration;
        public CartRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private void Connection()
        {
            string connectionstr = this.configuration[("ConnectionStrings:UserDbConnection")];
            objSqlConnection = new SqlConnection(connectionstr);
        }

        public bool AddToCart(int userId, string bookId)
        {
            try
            {
                Connection();
                SqlCommand ObjSqlCommand = new SqlCommand("SP_AddToCart", objSqlConnection);
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;
                ObjSqlCommand.Parameters.AddWithValue("@userId", userId);
                ObjSqlCommand.Parameters.AddWithValue("@bookId", bookId);
                objSqlConnection.Open();
                var i = ObjSqlCommand.ExecuteNonQuery();
                objSqlConnection.Close();
                if (i != 0)
                {
                    nlog.LogInfo("Cart added successfull");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                nlog.LogError("Unabe to add to cart due to " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public List<Cart> Getallccart(int userId)
        {
            try
            {
                Connection();
                List<Cart> ListCart = new List<Cart>();
                SqlCommand ObjSqlCommand = new SqlCommand("SP_GetAllCart", objSqlConnection);
                ObjSqlCommand.Parameters.AddWithValue("@userID", userId);
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(ObjSqlCommand);
                DataTable dt = new DataTable();
                objSqlConnection.Open();
                da.Fill(dt);
                objSqlConnection.Close();
                Cart objCart;
                foreach (DataRow reader in dt.Rows)
                {
                    objCart = new Cart();
                    objCart.BookId = Convert.ToInt32(reader["BookId"]);
                    objCart.Book = new Book()
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
                    objCart.UserId = Convert.ToInt32(reader["userId"]);
                    objCart.User = new User
                    {
                        UserId = (int)reader["userId"],
                        FullName = (string)reader["FullName"],
                        Email = (string)reader["email"],
                        Password = (string)reader["Password"],
                        MobileNumber = (string)reader["MobileNumber"]
                    };
                    objCart.CartId = Convert.ToInt32(reader["CartId"]);
                    objCart.BookCount = Convert.ToInt32(reader["CartCount"]);
                    ListCart.Add(objCart);
                }
                return ListCart;
            }
            catch (Exception ex)
            {
                nlog.LogError("book added Unsuccessfull due to " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public bool Removefromcart(int userId, string bookId)
        {
            try
            {
                Connection();
                SqlCommand ObjSqlCommand = new SqlCommand("SP_RemoveFromCart", objSqlConnection);
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;
                ObjSqlCommand.Parameters.AddWithValue("@userId", userId);
                ObjSqlCommand.Parameters.AddWithValue("@bookId", bookId);
                objSqlConnection.Open();
                var i = ObjSqlCommand.ExecuteScalar();
                objSqlConnection.Close();
                nlog.LogInfo("User Registers successfull");
                return true;
            }
            catch (Exception ex)
            {
                nlog.LogError("User Registers successfull due to " + ex.Message);
                return false;
                throw new Exception(ex.Message);
            }
        }

        public bool UpdateCart(int userId, string bookId,string updateValue)
        {
            try
            {
                Connection();
                SqlCommand ObjSqlCommand = new SqlCommand("SP_UpdateCart", objSqlConnection);
                ObjSqlCommand.CommandType = CommandType.StoredProcedure;
                ObjSqlCommand.Parameters.AddWithValue("@userId", userId);
                ObjSqlCommand.Parameters.AddWithValue("@bookId", bookId);
                ObjSqlCommand.Parameters.AddWithValue("@Cartcount", updateValue);
                objSqlConnection.Open();
                var i = ObjSqlCommand.ExecuteScalar();
                objSqlConnection.Close();
                nlog.LogInfo("User Registers successfull");
                return true;
            }
            catch (Exception ex)
            {
                nlog.LogError("User Registers successfull due to " + ex.Message);
                return false;
                throw new Exception(ex.Message);
            }
        }

        
    }
}

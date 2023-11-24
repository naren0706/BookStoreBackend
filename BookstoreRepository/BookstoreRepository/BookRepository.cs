using BookstoreModel;
using BookstoreRepository.IBookstoreRepository;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FundooModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NlogImplementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Text;

namespace BookstoreRepository.BookstoreRepository
{
    public class BookRepository : IBookRepository
    {
        NlogOperation nlog = new NlogOperation();
        private SqlConnection con;
        public readonly IConfiguration configuration;
        private void Connection()
        {
            string connectionstr = this.configuration[("ConnectionStrings:UserDbConnection")];
            con = new SqlConnection(connectionstr);
        }
        public BookRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Book AdddBook(IFormFile file, Book objBook)
        {
            try
            {
                if (file == null)
                {
                    return null;
                }
                var stream = file.OpenReadStream();
                var name = file.FileName;
                Account account = new Account("dzel62mup", "691995444473279", "PFGqItzTvOzXSLeOT8n3VyhENno");
                Cloudinary cloudinary = new Cloudinary(account);
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(name, stream)
                };
                ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);
                cloudinary.Api.UrlImgUp.BuildUrl(String.Format("{0}.{1}", uploadResult.PublicId, uploadResult.Format));                
                var cloudnaryfile = uploadResult.Uri.ToString();
                objBook.BookImage = cloudnaryfile;
                var result = this.AdddBook(objBook);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private Book AdddBook(Book objBook)
        {
            try
            {
                bool result;
                Connection();
                SqlCommand com = new SqlCommand("SP_AddBooks", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@bookName", objBook.BookName);
                com.Parameters.AddWithValue("@bookAuthor", objBook.BookAuthor);
                com.Parameters.AddWithValue("@bookDescription", objBook.BookDescription);
                com.Parameters.AddWithValue("@bookImage", objBook.BookImage);
                com.Parameters.AddWithValue("@bookCount", objBook.BookCount);
                com.Parameters.AddWithValue("@bookPrize", objBook.BookPrize);
                con.Open();
                var i = com.ExecuteScalar();
                SqlDataReader reader = com.ExecuteReader();
                if (reader.Read())
                {
                    objBook.BookId= Convert.ToInt32(reader["BookId"]);
                    nlog.LogInfo("book added successfull for " + objBook.BookName);
                    result = true;
                }
                else
                {
                    result = false;
                    nlog.LogError("book added Unsuccessfull");
                }
                con.Close();
                if(!result)
                {
                    return null;
                }
                return objBook;
            }
            catch (Exception ex)
            {
                nlog.LogError("book added Unsuccessfull due to " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public List<Book> GetAllBook()
        {
            try
            {
                Connection();
                List<Book> ObjListBook = new List<Book>();
                SqlCommand com = new SqlCommand("SP_GetAllBooks", con);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                da.Fill(dt);
                con.Close();
                //bookname,BookDescription,BookAuthor,BookImage,BookCount,BookPrize,Rating
                foreach (DataRow dr in dt.Rows)
                {
                    ObjListBook.Add(
                    new Book
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
                    });
                }
                return ObjListBook;
            }
            catch (Exception ex)
            {
                nlog.LogError("book added Unsuccessfull due to " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public bool DeleteBook(string bookId)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("SP_DeleteBook", con);
                com.Parameters.AddWithValue("@bookId", bookId);
                com.CommandType = CommandType.StoredProcedure;
                con.Open();
                var i = com.ExecuteScalar();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                nlog.LogError("book added Unsuccessfull due to " + ex.Message);
                throw new Exception(ex.Message);
                return false;
            }
        }

        public bool UpdateBook(Book objBook)
        {
            try
            {
                Connection();
                SqlCommand com = new SqlCommand("SP_UpdateBook", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@bookId", objBook.BookId);
                com.Parameters.AddWithValue("@bookName", objBook.BookName);
                com.Parameters.AddWithValue("@bookAuthor", objBook.BookAuthor);
                com.Parameters.AddWithValue("@bookDescription", objBook.BookDescription);
                com.Parameters.AddWithValue("@bookImage", objBook.BookImage);
                com.Parameters.AddWithValue("@bookCount", objBook.BookCount);
                com.Parameters.AddWithValue("@bookPrize", objBook.BookPrize);
                con.Open();
                var i = com.ExecuteScalar();
                return true; 
            }
            catch (Exception ex)
            {
                nlog.LogError("book added Unsuccessfull due to " + ex.Message);
                throw new Exception(ex.Message);
                return false;
            }
        }
    }
}

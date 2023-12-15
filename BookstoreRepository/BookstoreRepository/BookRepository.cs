using BookstoreModel;
using BookstoreRepository.IBookstoreRepository;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FundooModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using NlogImplementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Text;


//Book(1),wishlish,cart,summary

namespace BookstoreRepository.BookstoreRepository
{
    public class BookRepository : IBookRepository
    {
        NlogOperation nlog = new NlogOperation();
        private SqlConnection objSqlConnection;
        public readonly IConfiguration configuration;
        private readonly IDistributedCache distributedCache;

        private void Connection()
        {
            string connectionstr = this.configuration[("ConnectionStrings:UserDbConnection")];
            objSqlConnection = new SqlConnection(connectionstr);
        }
        public BookRepository(IConfiguration configuration, IDistributedCache distributedCache)
        {
            this.configuration = configuration;
            this.distributedCache = distributedCache;
        }
        public Book AdddBook(Book objBook)
        {
            try
            {
                Connection();
                string noimageurl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTIYXRat2l2itCOItlYpY5cR-ebHyygIuGyAxDYPLhCBLE-IpTenmRu5quH-OiSPPtSKno&usqp=CAU";
                SqlCommand objSqlCommand = new SqlCommand("SP_AddBooks", objSqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.Parameters.AddWithValue("@bookName", objBook.BookName);
                objSqlCommand.Parameters.AddWithValue("@bookAuthor", objBook.BookAuthor);
                objSqlCommand.Parameters.AddWithValue("@bookDescription", objBook.BookDescription);
                objSqlCommand.Parameters.AddWithValue("@bookImage", noimageurl);
                objSqlCommand.Parameters.AddWithValue("@bookCount", objBook.BookCount);
                objSqlCommand.Parameters.AddWithValue("@bookPrize", objBook.BookPrize);
                objSqlConnection.Open();
                SqlDataReader reader = objSqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    objBook.BookId= Convert.ToInt32(reader["BookId"]);
                    nlog.LogDebug("book added successfull for " + objBook.BookName);
                }
                else
                {
                    nlog.LogDebug("book added Unsuccessfull for " + objBook.BookName);
                }
                objSqlConnection.Close();
                return objBook;
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

        public List<Book> GetAllBook()
        {
            try
            {
                Connection();
                List<Book> ObjListBook = new List<Book>();
                SqlCommand com = new SqlCommand("SP_GetAllBooks", objSqlConnection);
                com.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(com);
                DataTable objDataTable = new DataTable();
                objSqlConnection.Open();
                objSqlDataAdapter.Fill(objDataTable);
                objSqlConnection.Close();
                foreach (DataRow dr in objDataTable.Rows)
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
                        Rating = Convert.ToDecimal(dr["Rating"]),
                        IsAvailable = Convert.ToBoolean(dr["IsAvailable"])
                    });
                }
                nlog.LogDebug("book has been reterived successfully");
                return ObjListBook;

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

        public bool DeleteBook(string bookId)
        {
            try
            {
                Connection();
                SqlCommand objSqlCommand = new SqlCommand("SP_DeleteBook", objSqlConnection);
                objSqlCommand.Parameters.AddWithValue("@bookId", bookId);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlConnection.Open();
                var SqlValue = objSqlCommand.ExecuteNonQuery();
                objSqlConnection.Close();
                if(SqlValue!=0)
                {
                    nlog.LogError("book deleted successfull ");
                    return true;
                }
                nlog.LogDebug("book deleted Unsuccessfull ");
                return false;
            }
            catch (Exception ex)
            {
                nlog.LogError("book added Unsuccessfull due to " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally{
                objSqlConnection.Close();
            }
        }

        public Book UpdateBook(Book objBook)
        {
            try
            {
                Connection();
                SqlCommand objSqlCommand = new SqlCommand("SP_UpdateBook", objSqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.Parameters.AddWithValue("@bookId", objBook.BookId);
                objSqlCommand.Parameters.AddWithValue("@bookName", objBook.BookName);
                objSqlCommand.Parameters.AddWithValue("@bookAuthor", objBook.BookAuthor);
                objSqlCommand.Parameters.AddWithValue("@bookDescription", objBook.BookDescription);
                objSqlCommand.Parameters.AddWithValue("@bookImage", objBook.BookImage);
                objSqlCommand.Parameters.AddWithValue("@bookCount", objBook.BookCount);
                objSqlCommand.Parameters.AddWithValue("@bookPrize", objBook.BookPrize);
                objSqlConnection.Open();
                var SqlValue = objSqlCommand.ExecuteScalar();
                objSqlConnection.Close();
                nlog.LogDebug("book updated successfull");
                return objBook;
            }
            catch (Exception ex)
            {
                nlog.LogError("book updated Unsuccessfull due to " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally { objSqlConnection.Close(); }
        }

        [Obsolete]
        public string UploadImage(IFormFile file, string bookId)
        {
            try
            {
                if (file == null)
                {
                    return string.Empty;
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
                var cloudnaryfilelink = uploadResult.Uri.ToString();
                UpdateLinkToSql(cloudnaryfilelink, bookId);
                return cloudnaryfilelink;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateLinkToSql(string cloudnaryfilelink, string bookId)
        {
            try
            {
                Connection();
                SqlCommand objSqlCommand = new SqlCommand("SP_UploadImage", objSqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.Parameters.AddWithValue("@bookId", bookId);
                objSqlCommand.Parameters.AddWithValue("@fileLink", cloudnaryfilelink);
                objSqlConnection.Open();
                nlog.LogDebug("image added successfull due to ");
                var SqlValue = objSqlCommand.ExecuteScalar();
                objSqlConnection.Close();
            }
            catch (Exception ex)
            {
                nlog.LogError("image added Unsuccessfull due to " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                objSqlConnection.Close();
            }
        }
    }
}

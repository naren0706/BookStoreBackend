﻿using BookstoreModel;
using BookstoreRepository.IBookstoreRepository;
using Microsoft.Extensions.Configuration;
using NlogImplementation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using NLog.Fluent;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using FundooModel.User;

namespace BookstoreRepository.BookstoreRepository
{
    public class UserRepository:IUserRepository
    {
        NlogOperation nlog = new NlogOperation();
        private SqlConnection con;
        public readonly IConfiguration configuration;
        public string key = "narenwtuyg212835725382172qiueqw8et8w";
        private void Connection()
        {
            string connectionstr = "data source = (localdb)\\MSSQLLocalDB; initial catalog = Bookstore; integrated security = true";
            con = new SqlConnection(connectionstr);
        }
        public UserRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public User RegisterUser(User ObjUser)
        {
            try
            {
                User objUser = new User();
                Connection();
                SqlCommand com = new SqlCommand("SP_RegisterUser", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@fullName", ObjUser.FullName);
                com.Parameters.AddWithValue("@email", ObjUser.Email);
                com.Parameters.AddWithValue("@password", EncryptPassword(ObjUser.Password));
                com.Parameters.AddWithValue("@mobileNumber", ObjUser.MobileNumber);
                con.Open();
                var i = com.ExecuteScalar();
                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    objUser = new User
                    {
                        UserId = (int)reader["userId"],
                        FullName= (string)reader["FullName"],
                        Email = (string)reader["email"],
                        Password = (string)reader["Password"],
                        MobileNumber = (string)reader["MobileNumber"]
                    };
                }
                con.Close();
                nlog.LogInfo("User Registers successfull");
                return objUser;
            }
            catch (Exception ex)
            {
                nlog.LogError("User Registers successfull due to "+ ex.Message);

                throw new Exception(ex.Message);
            }
        }
        public string UserLogin(string email,string password)
        {
            try
            {
                string token = "";
                User objUser = new User();
                Connection();
                SqlCommand com = new SqlCommand("SP_LoginUser", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@email", email);
                com.Parameters.AddWithValue("@password", EncryptPassword(password));
                con.Open();
                var i = com.ExecuteScalar();
                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    objUser = new User
                    {
                        UserId = (int)reader["userId"],
                        FullName = (string)reader["FullName"],
                        Email = (string)reader["email"],
                        Password = (string)reader["Password"],
                        MobileNumber = (string)reader["MobileNumber"]
                    };
                    token = GenerateSecurity(objUser.Email, objUser.UserId);
                }
                con.Close();
                nlog.LogInfo("User login successfull for "+objUser.Email);
                return token;

            }
            catch (Exception ex)
            {
                nlog.LogError("User login Unsuccessfull due to " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public bool ForgetPassword(string Email)
        {
            try
            {
                string token = "";
                User objUser = new User();
                Connection();
                SqlCommand com = new SqlCommand("SP_ForgotPassword", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@email", Email);
                con.Open();
                var i = com.ExecuteScalar();
                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    objUser = new User
                    {
                        UserId = (int)reader["userId"],
                        Email = (string)reader["email"]                        
                    };
                    token = GenerateSecurity(objUser.Email, objUser.UserId);
                    MSMQ msmq = new MSMQ();
                    msmq.sendData2Queue(token, Email);
                    return true;
                }
                con.Close();
                nlog.LogInfo("ForgetPassword successfull for " + objUser.Email);
                return false;
            }
            catch (Exception ex)
            {
                nlog.LogError("User login Unsuccessfull due to " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public bool ResetPassword(string email, string password)
        {
            bool result = false;
            try
            {
                string token = "";
                User objUser = new User();
                Connection();
                SqlCommand com = new SqlCommand("SP_ResetPassword", con);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@email", email);
                com.Parameters.AddWithValue("@password", EncryptPassword(password));
                con.Open();
                var i = com.ExecuteScalar();
                con.Close();
                result= true;
                nlog.LogInfo("ResetPassword successfull for " + objUser.Email);
            }
            catch (Exception ex)
            {
                result = false;
                nlog.LogError("User login ResetPassword due to " + ex.Message);
                throw new Exception(ex.Message);

            }
            return result;

        }
        public string GenerateSecurity(string email, int userId)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key1 = Encoding.ASCII.GetBytes(this.configuration[("JWT:Key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("Id",userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key1),
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);
        }
        private string EncryptPassword(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }
        public string DecryptPassword(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new string(decoded_char);
            return decryptpwd;
        }
    }
}
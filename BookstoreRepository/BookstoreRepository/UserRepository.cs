using BookstoreModel;
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
        private SqlConnection objSqlConnection;
        public readonly IConfiguration configuration;
        private void Connection()
        {
            string connectionstr = this.configuration[("ConnectionStrings:UserDbConnection")];
            objSqlConnection = new SqlConnection(connectionstr);
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
                SqlCommand objSqlCommand = new SqlCommand("SP_RegisterUser", objSqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.Parameters.AddWithValue("@fullName", ObjUser.FullName);
                objSqlCommand.Parameters.AddWithValue("@email", ObjUser.Email);
                objSqlCommand.Parameters.AddWithValue("@password", EncryptPassword(ObjUser.Password));
                objSqlCommand.Parameters.AddWithValue("@mobileNumber", ObjUser.MobileNumber);
                objSqlConnection.Open();
                SqlDataReader reader = objSqlCommand.ExecuteReader();

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
                    nlog.LogDebug(objUser.FullName + " User Registers successfull");
                }
                else
                {
                    nlog.LogDebug("Email already exist");
                    throw new Exception("Email already exist");
                }
                objSqlConnection.Close();
                return objUser;
            }
            catch (Exception ex)
            {
                nlog.LogError("User Registers successfull due to "+ ex.Message);

                throw new Exception(ex.Message);
            }
            finally { objSqlConnection.Close(); }

        }
        public string UserLogin(string email,string password)
        {
            try
            {
                string token = String.Empty;
                User objUser = new User();
                Connection();
                SqlCommand com = new SqlCommand("SP_LoginUser", objSqlConnection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@email", email);
                com.Parameters.AddWithValue("@password", EncryptPassword(password));
                objSqlConnection.Open();
                var SqlValue= com.ExecuteScalar();
                SqlDataReader reader = com.ExecuteReader();

                if (reader.Read())
                {
                    objUser = new User
                    {
                        UserId = (int)reader["userId"],
                        FullName = (string)reader["FullName"],
                        Email = (string)reader["email"],
                        Password = (string)reader["Password"],
                        MobileNumber = (string)reader["MobileNumber"],
                        IsAdmin = (bool)reader["IsAdmin"]
                    };
                    token = GenerateSecurity(objUser.Email, objUser.UserId,objUser.IsAdmin);
                }
                objSqlConnection.Close();
                nlog.LogDebug("User login successfull for "+objUser.Email);
                return token;
            }
            catch (Exception ex)
            {
                nlog.LogError("User login Unsuccessfull due to " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally { objSqlConnection.Close(); }

        }
        public bool ForgetPassword(string Email)
        {
            try
            {
                string token = "";
                User objUser = new User();
                Connection();
                SqlCommand objSqlCommand = new SqlCommand("SP_ForgotPassword", objSqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.Parameters.AddWithValue("@email", Email);
                objSqlConnection.Open();
                var SqlValue= objSqlCommand.ExecuteScalar();
                SqlDataReader reader = objSqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    objUser = new User
                    {
                        UserId = (int)reader["userId"],
                        Email = (string)reader["email"],
                        IsAdmin = (bool)reader["IsAdmin"]
                    };
                    token = GenerateSecurity(objUser.Email, objUser.UserId, objUser.IsAdmin);
                    MSMQ msmq = new MSMQ();
                    msmq.sendData2Queue(token, Email);
                    return true;
                }
                objSqlConnection.Close();
                nlog.LogDebug("ForgetPassword successfull for " + objUser.Email);
                return false;
            }
            catch (Exception ex)
            {
                nlog.LogError("User login Unsuccessfull due to " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally { objSqlConnection.Close(); }

        }
        public bool ResetPassword(string email, string password)
        {
            bool result = false;
            try
            {
                User objUser = new User();
                Connection();
                SqlCommand com = new SqlCommand("SP_ResetPassword", objSqlConnection);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.AddWithValue("@email", email);
                com.Parameters.AddWithValue("@password", EncryptPassword(password));
                objSqlConnection.Open();
                var SqlValue= com.ExecuteScalar();
                objSqlConnection.Close();
                result= true;
                nlog.LogDebug("ResetPassword successfull for " + objUser.Email);
            }
            catch (Exception ex)
            {
                result = false;
                nlog.LogError("User login ResetPassword due to " + ex.Message);
                throw new Exception(ex.Message);

            }
            finally
            {
                objSqlConnection.Close();
            }
            return result;

        }
        public string GenerateSecurity(string email, int userId,bool IsAdmin)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this.configuration[("JWT:Key")]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId",userId.ToString()),
                    new Claim(ClaimTypes.Role,IsAdmin?"Admin":"normal")
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
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

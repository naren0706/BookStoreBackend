using BookstoreModel;
using BookstoreRepository.IBookstoreRepository;
using Microsoft.Extensions.Configuration;
using NlogImplementation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Type = BookstoreModel.Type;

namespace BookstoreRepository.BookstoreRepository
{
    public class CustomerDetailsRepository: ICustomerDetailsRepository
    {
        NlogOperation nlog = new NlogOperation();
        private SqlConnection objSqlConnection;
        public readonly IConfiguration configuration;
        public CustomerDetailsRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        private void Connection()
        {
            string connectionstr = this.configuration[("ConnectionStrings:UserDbConnection")];
            objSqlConnection = new SqlConnection(connectionstr);
        }

        public CustomerDetails AddAddress(CustomerDetails objCustomerDetails,int userId)
        {
            try
            {
                Connection();
                SqlCommand objSqlCommand = new SqlCommand("SP_AddCustomerDetails", objSqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.Parameters.AddWithValue("@userId", userId);
                objSqlCommand.Parameters.AddWithValue("@FullName", objCustomerDetails.FullName);
                objSqlCommand.Parameters.AddWithValue("@MobileNum", objCustomerDetails.MobileNum);
                objSqlCommand.Parameters.AddWithValue("@Address", objCustomerDetails.Address);
                objSqlCommand.Parameters.AddWithValue("@CityOrTown", objCustomerDetails.CityOrTown);
                objSqlCommand.Parameters.AddWithValue("@State", objCustomerDetails.State);
                objSqlCommand.Parameters.AddWithValue("@TypeId", objCustomerDetails.TypeId);

                objSqlConnection.Open();
                var SqlValue= objSqlCommand.ExecuteScalar();
                SqlDataReader reader = objSqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    objCustomerDetails.CustomerID = Convert.ToInt32(reader["customerID"]);
                    objCustomerDetails.Type = new Type()
                    {
                        TypeId = Convert.ToInt32(reader["TypeId"]),
                        TypeValue = Convert.ToString(reader["TypeValue"])
                    };
                    objCustomerDetails.UserId = Convert.ToInt32(reader["userId"]);
                    objCustomerDetails.User = new User
                    {
                        UserId = (int)reader["userId"],
                        FullName = (string)reader["FullName"],
                        Email = (string)reader["email"],
                        Password = (string)reader["Password"],
                        MobileNumber = (string)reader["MobileNumber"]
                    };
                    nlog.LogInfo("address added successfull for " + objCustomerDetails.CustomerID);
                }
                else
                {
                    nlog.LogError("address added Unsuccessfull");
                }
                objSqlConnection.Close();
                
                return objCustomerDetails;
            }
            catch (Exception ex)
            {
                nlog.LogError("address added Unsuccessfull due to " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                objSqlConnection.Close();
            }
        }

        public bool DeleteAddress(int customerId,int userId)
        {
            try
            {
                Connection();
                SqlCommand objSqlCommand = new SqlCommand("SP_RemoveCustomerDetails", objSqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.Parameters.AddWithValue("@userId", userId);
                objSqlCommand.Parameters.AddWithValue("@customerId", customerId);

                objSqlConnection.Open();
                var SqlValue= objSqlCommand.ExecuteNonQuery();
                if (SqlValue != 0)
                {
                    nlog.LogError("address deleted successfull");
                    return true;
                }
                nlog.LogError("address deleted unsuccessfull");
                return false;
               
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

        public List<CustomerDetails> GetAllAddressByUserId(int userId)
        {
            try
            {
                Connection();
                List<CustomerDetails> ObjListCustomerDetails = new List<CustomerDetails>();
                SqlCommand objSqlCommand = new SqlCommand("SP_GetAllCustomerDetails", objSqlConnection);
                objSqlCommand.Parameters.AddWithValue("@userId", userId);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter objSqlDataAdapter = new SqlDataAdapter(objSqlCommand);
                DataTable objDataTable = new DataTable();
                objSqlConnection.Open();
                objSqlDataAdapter.Fill(objDataTable);
                objSqlConnection.Close();
                //bookname,BookDescription,BookAuthor,BookImage,BookCount,BookPrize,Rating
                foreach (DataRow objDatarow in objDataTable.Rows)
                {
                    ObjListCustomerDetails.Add(
                    new CustomerDetails()
                    {
                        CustomerID = Convert.ToInt32(objDatarow["CustomerID"]),
                        FullName = Convert.ToString(objDatarow["FullName"]),
                        MobileNum = Convert.ToString(objDatarow["MobileNum"]),
                        Address = Convert.ToString(objDatarow["Address"]),
                        CityOrTown = Convert.ToString(objDatarow["CityOrTown"]),
                        State = Convert.ToString(objDatarow["State"]),
                        TypeId = Convert.ToInt32(objDatarow["TypeId"]),
                        UserId = Convert.ToInt32(objDatarow["UserId"]),
                        Type = new Type()
                        {
                            TypeId = Convert.ToInt32(objDatarow["TypeId"]),
                            TypeValue = Convert.ToString(objDatarow["TypeValue"])
                        },
                        User = new User
                        {
                            UserId = (int)objDatarow["userId"],
                            FullName = (string)objDatarow["FullName"],
                            Email = (string)objDatarow["email"],
                            Password = (string)objDatarow["Password"],
                            MobileNumber = (string)objDatarow["MobileNumber"]
                        },

                    });
                    nlog.LogError("get all Address  is sccessfull for user id "+ userId);

                }
                return ObjListCustomerDetails;
            }
            catch (Exception ex)
            {
                nlog.LogError("Address added Unsuccessfull due to " + ex.Message);
                throw new Exception(ex.Message);
            }
            finally
            {
                objSqlConnection.Close();
            }
        }

        public CustomerDetails UpdateAddress(CustomerDetails objCustomerDetails)
        {
            try
            {
                Connection();
                SqlCommand objSqlCommand = new SqlCommand("SP_UpdateCustomerDetails", objSqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.Parameters.AddWithValue("@userId", objCustomerDetails.UserId);
                objSqlCommand.Parameters.AddWithValue("@customerID", objCustomerDetails.CustomerID);
                objSqlCommand.Parameters.AddWithValue("@FullName", objCustomerDetails.FullName);
                objSqlCommand.Parameters.AddWithValue("@MobileNum", objCustomerDetails.MobileNum);
                objSqlCommand.Parameters.AddWithValue("@Address", objCustomerDetails.Address);
                objSqlCommand.Parameters.AddWithValue("@CityOrTown", objCustomerDetails.CityOrTown);
                objSqlCommand.Parameters.AddWithValue("@State", objCustomerDetails.State);
                objSqlCommand.Parameters.AddWithValue("@TypeId", objCustomerDetails.TypeId);

                objSqlConnection.Open();
                var SqlValue= objSqlCommand.ExecuteScalar();
                SqlDataReader reader = objSqlCommand.ExecuteReader();
                if (reader.Read())
                {
                    objCustomerDetails.Type = new Type()
                    {
                        TypeId = Convert.ToInt32(reader["typeId"]),
                        TypeValue = Convert.ToString(reader["typeValue"])
                    };
                    objCustomerDetails.UserId = Convert.ToInt32(reader["userId"]);
                    objCustomerDetails.User = new User
                    {
                        UserId = (int)reader["userId"],
                        FullName = (string)reader["FullName"],
                        Email = (string)reader["email"],
                        Password = (string)reader["Password"],
                        MobileNumber = (string)reader["MobileNumber"]
                    };
                    nlog.LogInfo("book added successfull for " + objCustomerDetails.CustomerID);
                }
                else
                {
                    nlog.LogError("book added Unsuccessfull");
                }
                objSqlConnection.Close();

                return objCustomerDetails;
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

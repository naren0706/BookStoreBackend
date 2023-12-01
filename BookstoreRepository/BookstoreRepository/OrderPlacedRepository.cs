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
    public class OrderPlacedRepository: IOrderPlacedRepository
    {
        NlogOperation nlog = new NlogOperation();
        private SqlConnection objSqlConnection;
        public readonly IConfiguration configuration;
        private void Connection()
        {
            string connectionstr = this.configuration[("ConnectionStrings:UserDbConnection")];
            objSqlConnection = new SqlConnection(connectionstr);
        }
        public OrderPlacedRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool PlaceOrder(int userId, int customerId)
        {
            try
            {
                Connection();
                OrderPlaced objOrderPlaced = new OrderPlaced();
                SqlCommand objSqlCommand = new SqlCommand("SP_AddnewPlaceOrder", objSqlConnection);
                objSqlCommand.CommandType = CommandType.StoredProcedure;
                objSqlCommand.Parameters.AddWithValue("@userId", userId);
                objSqlCommand.Parameters.AddWithValue("@customerId", customerId);
                objSqlConnection.Open();
                var SqlValue = objSqlCommand.ExecuteNonQuery();
                objSqlConnection.Close();
                if(SqlValue !=0)
                {
                    nlog.LogDebug("Order placed Successfully");

                    return true;
                }
                nlog.LogDebug("Order placed UnSuccessfully");

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
    }
}

using BookstoreModel;
using BookstoreRepository.IBookstoreRepository;
using Microsoft.Extensions.Configuration;
using NlogImplementation;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BookstoreRepository.BookstoreRepository
{
    public class CustomerDetailsRepository: ICustomerDetailsRepository
    {
        NlogOperation nlog = new NlogOperation();
        private SqlConnection objSqlConnection;
        public readonly IConfiguration configuration;
        private void Connection()
        {
            string connectionstr = this.configuration[("ConnectionStrings:UserDbConnection")];
            objSqlConnection = new SqlConnection(connectionstr);
        }

        public CustomerDetails AddAddress(int userId)
        {
            throw new NotImplementedException();
        }

        public CustomerDetails DeleteAddress(int customerId)
        {
            throw new NotImplementedException();
        }

        public CustomerDetails GetAllAddressByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public CustomerDetails UpdateAddress(int userId, int customerId)
        {
            throw new NotImplementedException();
        }

        public CustomerDetailsRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
    }
}

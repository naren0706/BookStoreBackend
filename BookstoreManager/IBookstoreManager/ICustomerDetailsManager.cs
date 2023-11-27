using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.IBookstoreManager
{
    public interface ICustomerDetailsManager
    {
        public CustomerDetails AddAddress(int userId);
        public CustomerDetails DeleteAddress(int customerId);
        public CustomerDetails GetAllAddressByUserId(int userId);
        public CustomerDetails UpdateAddress(int userId, int customerId);
    }
}

using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.IBookstoreManager
{
    public interface ICustomerDetailsManager
    {
        public CustomerDetails AddAddress(CustomerDetails objCustomerDetails,int userId);
        public bool DeleteAddress(int customerId, int userId);
        public List<CustomerDetails> GetAllAddressByUserId(int userId);
        public CustomerDetails UpdateAddress( CustomerDetails objCustomerDetails);
    }
}

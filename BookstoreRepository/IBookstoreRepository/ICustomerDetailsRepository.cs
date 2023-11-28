using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreRepository.IBookstoreRepository
{
    public interface ICustomerDetailsRepository
    {
        CustomerDetails AddAddress(CustomerDetails objCustomerDetails, int userId);
        bool DeleteAddress(int customerId, int userId);
        List<CustomerDetails> GetAllAddressByUserId(int userId);
        CustomerDetails UpdateAddress(CustomerDetails objCustomerDetails);
    }
}

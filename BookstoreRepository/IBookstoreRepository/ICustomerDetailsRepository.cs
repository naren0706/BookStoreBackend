using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreRepository.IBookstoreRepository
{
    public interface ICustomerDetailsRepository
    {
        CustomerDetails AddAddress(int userId);
        CustomerDetails DeleteAddress(int customerId);
        CustomerDetails GetAllAddressByUserId(int userId);
        CustomerDetails UpdateAddress(int userId, int customerId);
    }
}

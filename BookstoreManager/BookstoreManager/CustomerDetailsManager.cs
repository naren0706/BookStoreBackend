using BookstoreManager.IBookstoreManager;
using BookstoreModel;
using BookstoreRepository.IBookstoreRepository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BookstoreManager.BookstoreManager
{
    public class CustomerDetailsManager: ICustomerDetailsManager
    {
        public ICustomerDetailsRepository customerDetailsRepository;
        public CustomerDetailsManager(ICustomerDetailsRepository customerDetailsRepository)
        {
            this.customerDetailsRepository = customerDetailsRepository;
        }

        public CustomerDetails AddAddress(int userId)
        {
            var result = this.customerDetailsRepository.AddAddress(userId);
            return result;
        }

        public CustomerDetails DeleteAddress(int customerId)
        {
            var result = this.customerDetailsRepository.DeleteAddress(customerId);
            return result;
        }

        public CustomerDetails GetAllAddressByUserId(int userId)
        {
            var result = this.customerDetailsRepository.GetAllAddressByUserId(userId);
            return result;
        }

        public CustomerDetails UpdateAddress(int userId, int customerId)
        {
            var result = this.customerDetailsRepository.UpdateAddress(userId,customerId);
            return result;
        }
    }
}

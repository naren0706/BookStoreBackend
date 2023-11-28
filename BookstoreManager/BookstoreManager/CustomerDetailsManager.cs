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

        public CustomerDetails AddAddress(CustomerDetails objCustomerDetails, int userId)
        {
            var result = this.customerDetailsRepository.AddAddress(objCustomerDetails,userId);
            return result;
        }


        public bool DeleteAddress(int customerId, int userId)
        {
            var result = this.customerDetailsRepository.DeleteAddress(customerId,userId);
            return result;
        }

        public List<CustomerDetails> GetAllAddressByUserId(int userId)
        {
            var result = this.customerDetailsRepository.GetAllAddressByUserId(userId);
            return result;
        }

        public CustomerDetails UpdateAddress(CustomerDetails objCustomerDetails)
        {
            var result = this.customerDetailsRepository.UpdateAddress(objCustomerDetails);
            return result;
        }
    }
}

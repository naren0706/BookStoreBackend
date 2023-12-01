using BookstoreManager.IBookstoreManager;
using BookstoreModel;
using BookstoreRepository.IBookstoreRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.BookstoreManager
{
    public class OrderPlacedManager : IOrderPlacedManager
    {
        public IOrderPlacedRepository orderPlacedRepository;
        public OrderPlacedManager(IOrderPlacedRepository orderPlacedRepository) 
        {
            this.orderPlacedRepository = orderPlacedRepository;
        }
        public bool PlaceOrder(int userId, int customerId)
        {
            var result = this.orderPlacedRepository.PlaceOrder(userId, customerId);
            return result;
        }

    }
}

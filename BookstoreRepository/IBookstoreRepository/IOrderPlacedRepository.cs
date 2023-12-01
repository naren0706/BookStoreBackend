using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreRepository.IBookstoreRepository
{
    public interface IOrderPlacedRepository
    {
        public bool PlaceOrder(int userId, int customerId);
    }
}

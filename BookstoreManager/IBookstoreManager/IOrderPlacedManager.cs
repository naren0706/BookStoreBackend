using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.IBookstoreManager
{
    public interface IOrderPlacedManager
    {
        public bool PlaceOrder(int userId, int customerId);
    }
}

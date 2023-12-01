using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.IBookstoreManager
{
    public interface IOrderSummaryManager
    {
        public List<OrderSummary> GetAllSummmary(int userId);
    }
}

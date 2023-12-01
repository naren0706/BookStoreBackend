using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreRepository.IBookstoreRepository
{
    public interface IOrderSummaryRepository
    {
        public List<OrderSummary> GetAllSummmary(int userid);
    }
}

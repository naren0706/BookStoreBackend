using BookstoreManager.IBookstoreManager;
using BookstoreModel;
using BookstoreRepository.IBookstoreRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.BookstoreManager
{
    public class OrderSummaryManager : IOrderSummaryManager
    {
        public IOrderSummaryRepository orderSummaryRepository;
        public OrderSummaryManager(IOrderSummaryRepository orderSummaryRepository) 
        {
            this.orderSummaryRepository = orderSummaryRepository;
        }
        public List<OrderSummary> GetAllSummmary(int userId)
        {
            var result = this.orderSummaryRepository.GetAllSummmary(userId);
            return result;
        }
    }
}

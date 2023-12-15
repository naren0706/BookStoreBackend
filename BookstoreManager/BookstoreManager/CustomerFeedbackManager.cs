using BookstoreManager.IBookstoreManager;
using BookstoreModel;
using BookstoreRepository.BookstoreRepository;
using BookstoreRepository.IBookstoreRepository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BookstoreManager.BookstoreManager
{
    public class CustomerFeedbackManager: ICustomerFeedbackManager
    {
        public ICustomerFeedbackRepository customerFeedbackRepository;
        public CustomerFeedbackManager(ICustomerFeedbackRepository customerFeedbackRepository)
        {
            this.customerFeedbackRepository = customerFeedbackRepository;
        }

        public Feedback AddFeedback(Feedback objFeedBack)
        {
            var result = this.customerFeedbackRepository.AddFeedback(objFeedBack);
            return result;
        }

        public List<Feedback> GetFeedback(int bookId)
        {
            var result = this.customerFeedbackRepository.GetFeedback(bookId);
            return result;
        }
    }
}

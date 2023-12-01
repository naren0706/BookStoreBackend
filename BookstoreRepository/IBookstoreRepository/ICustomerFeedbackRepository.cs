using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreRepository.IBookstoreRepository
{
    public interface ICustomerFeedbackRepository
    {
        Feedback AddFeedback(Feedback objFeedBack);
    }
}

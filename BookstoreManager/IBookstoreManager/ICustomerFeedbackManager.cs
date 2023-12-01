using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.IBookstoreManager
{
    public interface ICustomerFeedbackManager
    {
        public Feedback AddFeedback(Feedback objFeedBack);
    }
}

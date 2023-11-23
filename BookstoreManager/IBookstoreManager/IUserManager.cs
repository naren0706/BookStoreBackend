using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreManager.IBookstoreManager
{
    public interface IUserManager
    {
        public string UserLogin(string email,string password);
        public User RegisterUser(User register);
        public bool ForgetPassword(string email);
        public bool ResetPassword(string email, string password);
    }
}

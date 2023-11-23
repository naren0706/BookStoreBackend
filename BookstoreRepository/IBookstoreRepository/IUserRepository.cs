using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreRepository.IBookstoreRepository
{
    public interface IUserRepository
    {
        public User RegisterUser(User ObjUser);
        public string UserLogin(string email,string password);
        public bool ForgetPassword(string Email);
        public bool ResetPassword(string password, string password1);

    }
}

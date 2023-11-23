using BookstoreManager.IBookstoreManager;
using BookstoreModel;
using BookstoreRepository.IBookstoreRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreManager.BookstoreManager
{
    public class UserManager : IUserManager
    {
        public readonly IUserRepository userRepository;
        public UserManager(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public string UserLogin(string email,string password)
        {
            var result = this.userRepository.UserLogin(email,password);
            return result;
        }

        public User RegisterUser(User register)
        {
            var result = this.userRepository.RegisterUser(register);
            return result;
        }

        public bool ForgetPassword(string email)
        {
            var result = this.userRepository.ForgetPassword(email);
            return result;
        }
        public bool ResetPassword(string email, string password)
        {
            var result = this.userRepository.ResetPassword(email,password);
            return result;
        }

    }
}

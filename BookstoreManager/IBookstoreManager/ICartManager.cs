using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.IBookstoreManager
{
    public interface ICartManager
    {
        public bool AddToCart(int userId, string bookId);
        public List<Cart> GetAllCart(int userId);
        public bool Removefromcart(int userId, string bookId);
        public bool UpdateCart(int userId, string bookId, string updateValue);
    }
}

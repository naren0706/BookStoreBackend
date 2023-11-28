using BookstoreManager.IBookstoreManager;
using BookstoreModel;
using BookstoreRepository.IBookstoreRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.BookstoreManager
{
    public class CartManager : ICartManager
    {
        public ICartRepository cartRepository;
        public CartManager(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }
        public Cart AddToCart(int userId, string bookId)
        {
            var result = this.cartRepository.AddToCart(userId, bookId);
            return result;
        }

        public List<Cart> GetAllCart(int userId)
        {
            var result = this.cartRepository.Getallccart(userId);
            return result;
        }

        public bool Removefromcart(int userId, string bookId)
        {
            var result = this.cartRepository.Removefromcart(userId, bookId);
            return result;
        }

        public Cart UpdateCart(int userId, string bookId, string updateValue)
        {
            var result = this.cartRepository.UpdateCart(userId,bookId,updateValue);
            return result;
        }
    }
}

using BookstoreManager.IBookstoreManager;
using BookstoreModel;
using BookstoreRepository.IBookstoreRepository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BookstoreManager.BookstoreManager
{
    public class WishListManager : IWishListManager
    {
        IWishListRepository wishListRepository;
        public WishListManager(IWishListRepository WishListRepository) {
            this.wishListRepository = WishListRepository;
        }
        public WishList AddToWishList(int userId, int bookId)
        {
            var result = this.wishListRepository.AddToWishLoist(userId,bookId);
            return result;
        }

        public bool AddWishListBookToCart(int userId, string bookId)
        {
            var result = this.wishListRepository.AddWishListBookToCart(userId, bookId);
            return result;
        }

        public List<WishList> GetAllWishListBooks(int userId)
        {
            var result = this.wishListRepository.GetAllWishListBooks(userId);
            return result;
        }

        public bool RemoveBookFromWishList(int userId, string bookId)
        {
            var result = this.wishListRepository.RemoveBookFromWishList(userId, bookId);
            return result;
        }

        public bool WishlistToCart(int userId)
        {
            var result = this.wishListRepository.WishlistToCart(userId);
            return result;
        }
    }
}

using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.IBookstoreManager
{
    public interface IWishListManager
    {
        public WishList AddToWishList(int userId, int bookId);
        public bool AddWishListBookToCart(int userId, string bookId);
        public List<WishList> GetAllWishListBooks(int userId);
        public bool RemoveBookFromWishList(int userId, string bookId);
        public bool WishlistToCart(int userId);
    }
}

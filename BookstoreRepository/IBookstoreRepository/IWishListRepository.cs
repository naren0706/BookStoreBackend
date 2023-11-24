using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreRepository.IBookstoreRepository
{
    public interface IWishListRepository
    {
        public WishList AddToWishLoist(int userId, int bookId);
        public bool RemoveBookFromWishList(int userId, string bookId);
        public List<WishList> GetAllWishListBooks(int userId);
    }
}

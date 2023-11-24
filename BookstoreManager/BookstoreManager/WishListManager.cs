using BookstoreManager.IBookstoreManager;
using BookstoreModel;
using BookstoreRepository.IBookstoreRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.BookstoreManager
{
    public class WishListManager : IWishListManager
    {
        IWishListRepository wishListRepository;
        public WishListManager(IWishListRepository WishListRepository) {
            this.wishListRepository = WishListRepository;
        }
        public WishList AddToWishList(WishList ObjwishList)
        {
            var result = this.wishListRepository.AddToWishLoist(ObjwishList);
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
    }
}

﻿using BookstoreModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreRepository.IBookstoreRepository
{
    public interface ICartRepository
    {
        public bool AddToCart(int userId, string bookId);
        public List<Cart> Getallccart(int userId);
        public bool Removefromcart(int userId, string bookId);
        public bool UpdateCart(int userId, string bookId, string updateValue);
    }
}

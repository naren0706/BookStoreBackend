﻿using BookstoreModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreRepository.IBookstoreRepository
{
    public interface IBookRepository
    {
        public Book AdddBook(Book objBook);
        public bool DeleteBook(string bookId);
        public List<Book> GetAllBook();
        public Book UpdateBook(Book ObjBook);
        public string UploadImage(IFormFile file, string bookId);
    }
}

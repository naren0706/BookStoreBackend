using BookstoreModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.IBookstoreManager
{
    public interface IBookManager
    {
        public Book AddBook(IFormFile file, Book objBook);
        public bool DeleteBook(string bookId);
        public List<Book> GetAllBook();
        public bool UpdateBook(Book ObjBook);
    }
}

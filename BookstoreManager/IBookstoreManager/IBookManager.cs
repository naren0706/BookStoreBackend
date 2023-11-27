using BookstoreModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager.IBookstoreManager
{
    public interface IBookManager
    {
        public Book AddBook(Book objBook);
        public bool DeleteBook(string bookId);
        public List<Book> GetAllBook();
        public Book UpdateBook(Book ObjBook);
        public bool UploadImage(IFormFile file,string bookId);
    }
}

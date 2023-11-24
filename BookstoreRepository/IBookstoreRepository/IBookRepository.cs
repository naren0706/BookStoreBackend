using BookstoreModel;
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
        public bool UpdateBook(Book ObjBook);
        public bool UploadImage(IFormFile file, string bookId);
    }
}

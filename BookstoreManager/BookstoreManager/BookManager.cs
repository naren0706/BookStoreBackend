using BookstoreManager.IBookstoreManager;
using BookstoreModel;
using BookstoreRepository.IBookstoreRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreManager
{
    public class BookManager : IBookManager
    {
        public IBookRepository bookstoreRepository;
        public  BookManager(IBookRepository bookstoreRepository)
        {
            this.bookstoreRepository = bookstoreRepository;
        }
        public Book AddBook(Book objBook)
        {
            var result = this.bookstoreRepository.AdddBook(objBook);
            return result;
        }

        public bool DeleteBook(string bookId)
        {
            var result = this.bookstoreRepository.DeleteBook(bookId);
            return result;
        }

        public List<Book> GetAllBook()
        {
            var result = this.bookstoreRepository.GetAllBook();
            return result;
        }

        public Book UpdateBook(Book ObjBook)
        {
            var result = this.bookstoreRepository.UpdateBook(ObjBook);
            return result;
        }

        public bool UploadImage(IFormFile file, string bookId)
        {
            var result = this.bookstoreRepository.UploadImage(file,bookId);
            return result;
        }
    }
}

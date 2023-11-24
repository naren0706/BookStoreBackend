using BookstoreManager.IBookstoreManager;
using BookstoreModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        public readonly IBookManager bookManager;
        public BookController(IBookManager userManager)
        {
            this.bookManager = userManager;
        }

        [HttpPost]
        [Route("addBook")]
        public ActionResult AddBook(IFormFile file,Book dsad)
        {
            try
            {
                Book objBook = new Book();
                var result = this.bookManager.AddBook(file, objBook);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note resotored Successful", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllBook")]
        public ActionResult GetAllBook()
        {
            try
            {
                var result = this.bookManager.GetAllBook();
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Note resotored Successful", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("UpdateBook")]
        public ActionResult UpdateBook(Book updateBook)
        {
            try
            {
                var result = this.bookManager.UpdateBook(updateBook);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "Book updated Successful", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Book Not Updated" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("DeleteBook")]
        public ActionResult DeleteBook(string bookId)
        {
            try
            {
                var result = this.bookManager.DeleteBook(bookId);
                if (result)
                {
                    return this.Ok(new { Status = true, Message = "Book deleted Successful"});
                }
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}

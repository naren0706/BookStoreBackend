using BookstoreManager.IBookstoreManager;
using BookstoreModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NlogImplementation;
using System;

namespace BookstoreApplication.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        public readonly IBookManager bookManager;
        NlogOperation nlog = new NlogOperation();
        public BookController(IBookManager userManager)
        {
            this.bookManager = userManager;
        }

        [HttpPost]
        [Route("addBook")]
        [Authorize(Roles = "Admin")]
        public ActionResult AddBook(Book objBook)
        {
            try
            {
                var result = this.bookManager.AddBook(objBook);
                if (result != null)
                {
                    nlog.LogInfo("book added Successfully");
                    return this.Ok(new { Status = true, Message = "book added Successfully", data = result });
                }
                nlog.LogInfo("book added UnSuccessfully");
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                nlog.LogError("book added Successfully"+ex.Message);
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
                    nlog.LogInfo("book reterived");
                    return this.Ok(new { Status = true, Message = "book resotored Successful", data = result });
                }
                nlog.LogInfo("book not reterived");
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                nlog.LogInfo("book not reterived due to "+ex.Message);
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("UpdateBook")]
        [Authorize(Roles = "Admin")]

        public ActionResult UpdateBook(Book updateBook)
        {
            try
            {
                var result = this.bookManager.UpdateBook(updateBook);
                if (result!=null)
                {
                    nlog.LogInfo("book updated");
                    return this.Ok(new { Status = true, Message = "Book updated Successful", data = result });
                }
                nlog.LogInfo("book not updated");
                return this.BadRequest(new { Status = false, Message = "Book Not Updated" });
            }
            catch (Exception ex)
            {
                nlog.LogInfo("book not updated"+ex.Message);
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("DeleteBook")]
        [Authorize(Roles = "Admin")]

        public ActionResult DeleteBook(string bookId)
        {
            try
            {
                var result = this.bookManager.DeleteBook(bookId);
                if (result)
                {
                    nlog.LogInfo("book Deleted");
                    return this.Ok(new { Status = true, Message = "Book deleted Successful"});
                }
                nlog.LogInfo("book not Deleted");
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                nlog.LogError("book not Deleted due to"+ex.Message);
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPut]
        [Route("UploadImage")]
        [Authorize(Roles = "Admin")]

        public ActionResult UploadImage(IFormFile file,string bookId)
        {
            try
            {
                var result = this.bookManager.UploadImage(file,bookId);
                if (result != string.Empty)
                {
                    nlog.LogInfo("image updated");
                    return this.Ok(new { Status = true, Message = "Note resotored Successful", data = result });
                }
                nlog.LogInfo("image not updated");
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                nlog.LogError("image not updated");
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}

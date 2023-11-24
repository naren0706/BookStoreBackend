using BookstoreManager.IBookstoreManager;
using BookstoreModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookstoreApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : Controller
    {
        public readonly IWishListManager wishListManager;
        public WishListController(IWishListManager wishListManager)
        {
            this.wishListManager = wishListManager;
        }

        [HttpPost]
        [Route("AddToWishList")]
        public ActionResult AddBook(WishList ObjWishList)
        {
            try
            {
                var result = this.wishListManager.AddToWishList(ObjWishList);
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
        [Route("GetAllWishListBooks")]
        public ActionResult GetAllWishListBooks()
        {
            try
            {
                var userId = 1;
                var result = this.wishListManager.GetAllWishListBooks(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "got all wishlist", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("RemoveBookFromWishList")]
        public ActionResult RemoveBookFromWishList(string bookId)
        {
            try
            {
                var userId = 1;
                var result = this.wishListManager.RemoveBookFromWishList(userId,bookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "WishList removed successfully", data = result });
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

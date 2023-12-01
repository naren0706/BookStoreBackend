using BookstoreManager.IBookstoreManager;
using BookstoreModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NlogImplementation;
using System;
using System.Linq;
using System.Security.Claims;

namespace BookstoreApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : Controller
    {
        public readonly IWishListManager wishListManager;
        NlogOperation nlog = new NlogOperation();
        public WishListController(IWishListManager wishListManager)
        {
            this.wishListManager = wishListManager;
        }
        [HttpPost]
        [Route("AddToWishList")]
        public ActionResult AddToWishList(int bookId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v=>v.Type=="UserId").Value);
                var result = this.wishListManager.AddToWishList(userId,bookId);
                if (result != null)
                {
                    nlog.LogInfo("Wishlist added Successfully");
                    return this.Ok(new { Status = true, Message = "Note resotored Successful", data = result });
                }
                nlog.LogInfo("Wishlist added unSuccessfully");
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                nlog.LogInfo("Wishlist added Successfully"+ex.Message);
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllWishListBooks")]
        public ActionResult GetAllWishListBooks()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "UserId").Value);
                var result = this.wishListManager.GetAllWishListBooks(userId);
                if (result != null)
                {
                    nlog.LogInfo("Wishlist reterived Successfully");
                    return this.Ok(new { Status = true, Message = "got all wishlist", data = result });
                }
                nlog.LogInfo("Wishlist reterived Successfully");
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                nlog.LogInfo("Wishlist reterived Successfully");
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("RemoveBookFromWishList")]
        public ActionResult RemoveBookFromWishList(string bookId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "UserId").Value);
                var result = this.wishListManager.RemoveBookFromWishList(userId,bookId);
                if (result)
                {
                    nlog.LogInfo("Wishlist removed Successfully");
                    return this.Ok(new { Status = true, Message = "WishList removed successfully", data = result });
                }
                nlog.LogInfo("Wishlist removed Successfully");
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                nlog.LogInfo("Wishlist removed Successfully");
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}

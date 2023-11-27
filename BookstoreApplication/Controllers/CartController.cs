using BookstoreManager.IBookstoreManager;
using BookstoreModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        public readonly ICartManager cartManager;
        public CartController(ICartManager cartManager)
        {
            this.cartManager = cartManager;
        }
        [HttpPost]
        [Route("AddToCart")]
        public ActionResult AddToCart(string bookId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "UserId").Value);
                var result = this.cartManager.AddToCart(userId,bookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "cart added Successful", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("Removefromcart")]
        public ActionResult Removefromcart(string bookId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "UserId").Value);
                var result = this.cartManager.Removefromcart(userId, bookId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Book removed from cart Successful", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("UpdateCart")]
        public ActionResult UpdateCart(string bookId,string updateValue)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "UserId").Value);
                var result = this.cartManager.UpdateCart(userId, bookId, updateValue);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Cart Updated Successful", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetallCart")]
        public ActionResult GetallCart()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "UserId").Value);
                var result = this.cartManager.GetAllCart(userId);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Cart Added Successful", data = result });
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

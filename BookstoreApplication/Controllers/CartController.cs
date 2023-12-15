using BookstoreManager.IBookstoreManager;
using BookstoreModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NlogImplementation;
using System;
using System.Linq;

namespace BookstoreApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        public readonly ICartManager cartManager;
        public int userId;
        NlogOperation nlog = new NlogOperation();
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
                if (result!=null)
                {
                    nlog.LogInfo("cart added Successfully");

                    return this.Ok(new { Status = true, Message = "cart added Successful", data = result });
                }
                nlog.LogInfo("cart added UnSuccessfully");
                return this.BadRequest(new { Status = false, Message = "book Already in cart" });
            }
            catch (Exception ex)
            {
                nlog.LogError("cart added UnSuccessfully" + ex.Message);
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
                if (result)
                {
                    nlog.LogInfo("cart removed Successfully");
                    return this.Ok(new { Status = true, Message = "cart removed from cart Successful", data = result });
                }
                nlog.LogInfo("cart removed UnSuccessfully");
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                nlog.LogError("cart removed UnSuccessfully" + ex.Message);
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPut]
        [Route("UpdateCart")]
        public ActionResult UpdateCart(string bookId,string updateValue)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "UserId").Value);
                var result = this.cartManager.UpdateCart(userId, bookId, updateValue);
                if (result!=null)
                {
                    nlog.LogInfo("cart updated Successfully");
                    return this.Ok(new { Status = true, Message = "Cart Updated Successful", data = result });
                }
                nlog.LogInfo("cart updated UnSuccessfully");
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                nlog.LogError("cart updated UnSuccessfully"+ex.Message);
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
                    nlog.LogInfo("cart reterived Successfully");
                    return this.Ok(new { Status = true, Message = "Cart Restored Successful", data = result });
                }
                nlog.LogInfo("cart reterived UnSuccessfully");
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                nlog.LogError("cart reterived UnSuccessfully" + ex.Message);
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}

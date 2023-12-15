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
    public class OrderPlacedController : Controller
    {
        public readonly IOrderPlacedManager orderPlacedManager;
        NlogOperation nlog = new NlogOperation();
        public OrderPlacedController(IOrderPlacedManager orderPlacedManager)
        {
            this.orderPlacedManager = orderPlacedManager;
        }
        [HttpPost]
        [Route("AddNewPlaceOrder")]
        public ActionResult AddNewPlaceOrder(int customerId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "UserId").Value);
                var result = this.orderPlacedManager.PlaceOrder(userId, customerId);
                if (result)
                {
                    nlog.LogInfo("order placed Successfully");
                    return this.Ok(new { Status = true, Message = "order placed Successful", data = result });
                }
                nlog.LogInfo("order placed Unsuccessfully");
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                nlog.LogError("order placed unsuccessfully"+ex.Message);
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}

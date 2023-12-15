using BookstoreManager.BookstoreManager;
using BookstoreManager.IBookstoreManager;
using BookstoreModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog.Fluent;
using NlogImplementation;
using System;
using System.Linq;

namespace BookstoreApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderSummary : Controller
    {
        public readonly IOrderSummaryManager orderSummaryManager; 
        NlogOperation nlog = new NlogOperation();

        public OrderSummary(IOrderSummaryManager orderSummaryManager)
        {
            this.orderSummaryManager = orderSummaryManager;
        }
        [HttpGet]
        [Route("GetAllSummmary")]
        public ActionResult GetAllSummmary()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "UserId").Value);
                var result = this.orderSummaryManager.GetAllSummmary(userId);
                if (result != null)
                {
                    nlog.LogError("Summary reterived successfully");
                    return this.Ok(new { Status = true, Message = "Summmary resotored Successful", data = result });
                }
                nlog.LogError("Summary reterived  unsuccessfully");
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                nlog.LogError("Summary reterived  unsuccessfully" + ex.Message);
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}

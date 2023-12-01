using BookstoreManager.IBookstoreManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BookstoreModel;
using NLog.Fluent;
using NlogImplementation;

namespace BookstoreApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerFeedbackController : Controller
    {
        public readonly ICustomerFeedbackManager customerFeedbackManager;
        NlogOperation nlog = new NlogOperation();
        public CustomerFeedbackController(ICustomerFeedbackManager customerFeedbackManager)
        {
            this.customerFeedbackManager = customerFeedbackManager;
        }
        [HttpPost]
        [Route("AddFeedback")]
        public ActionResult AddFeedback(Feedback objFeedBack)
        {
            try
            {
                objFeedBack.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "UserId").Value);
                var result = this.customerFeedbackManager.AddFeedback(objFeedBack);
                if (result != null)
                {
                    nlog.LogInfo("FeedBack Added Successfully");
                    return this.Ok(new { Status = true, Message = "Feedback added Successful", data = result });
                }
                nlog.LogInfo("FeedBack Added unsuccessfully");
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                nlog.LogInfo("FeedBack Added unSuccessfull"+ex.Message);
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}

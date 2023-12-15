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
        [Authorize]
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
        [HttpGet]
        [Route("GetFeedBack")]
        public ActionResult GetFeedBack(int bookId)
        {
            try
            {
                var result = this.customerFeedbackManager.GetFeedback(bookId);
                if (result != null)
                {
                    nlog.LogInfo("FeedBack Added Successfully");
                    return this.Ok(new { Status = true, Message = "Feedback retrieved Successful", data = result });
                }
                nlog.LogInfo("FeedBack Added unsuccessfully");
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                nlog.LogInfo("FeedBack Added unSuccessfull" + ex.Message);
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}

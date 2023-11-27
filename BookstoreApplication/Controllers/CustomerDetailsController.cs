using BookstoreManager.IBookstoreManager;
using BookstoreModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BookstoreApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerDetailsController : Controller
    {
        public readonly ICustomerDetailsManager customerDetailsManager;
        private int userId;

        public CustomerDetailsController(ICustomerDetailsManager CustomerDetailsManager)
        {
            this.customerDetailsManager = CustomerDetailsManager;
            this.userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "UserId").Value);
        }
        //          1. AddAddress
        //          2. UpdateAddress
        //          3. DeleteAddress
        //          4. GetAllAddressByUserId
        [HttpPost]
        [Route("AddAddress")]
        public ActionResult AddAddress()
        {
            try
            {
                var result = this.customerDetailsManager.AddAddress(this.userId);
                //var result = 1;
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
        [Route("UpdateAddress")]
        public ActionResult UpdateAddress(int customerId)
        {
            try
            {
                var result = this.customerDetailsManager.UpdateAddress(this.userId,customerId);
                //var result = 1;
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
        [Route("DeleteAddress")]
        public ActionResult DeleteAddress(int customerId)
        {
            try
            {
                var result = this.customerDetailsManager.DeleteAddress(customerId);
                //var result = 1;
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
        [Route("GetAllAddressByUserId")]
        public ActionResult GetAllAddressByUserId()
        {
            try
            {
                var result = this.customerDetailsManager.GetAllAddressByUserId(this.userId);
                //var result = 1;
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
    }
}

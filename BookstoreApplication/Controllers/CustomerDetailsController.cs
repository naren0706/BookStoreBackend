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
    public class CustomerDetailsController : Controller
    {
        public readonly ICustomerDetailsManager customerDetailsManager;
        NlogOperation nlog = new NlogOperation();

        public CustomerDetailsController(ICustomerDetailsManager CustomerDetailsManager)
        {
            this.customerDetailsManager = CustomerDetailsManager;
        }
//          1. AddAddress
//          2. UpdateAddress
//          3. DeleteAddress
//          4. GetAllAddressByUserId
        [HttpPost]
        [Route("AddAddress")]
        public ActionResult AddAddress(CustomerDetails objCustomerDetails)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "UserId").Value);
                var result = this.customerDetailsManager.AddAddress(objCustomerDetails,userId);
                if (result != null)
                {
                    nlog.LogInfo("Address Added Successfully");
                    return this.Ok(new { Status = true, Message = "Address added Successful", data = result });
                }
                nlog.LogInfo("Address Added UnSuccessfully");
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                nlog.LogInfo("Address Added Unsuccessfully"+ex.Message);
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("UpdateAddress")]
        public ActionResult UpdateAddress(CustomerDetails objCustomerDetails)
        {
            try
            {
                objCustomerDetails.UserId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "UserId").Value);
                var result = this.customerDetailsManager.UpdateAddress(objCustomerDetails);
                if (result != null)
                {
                    nlog.LogInfo("Address Updated Successfully");
                    return this.Ok(new { Status = true, Message = "Note resotored Successful", data = result });
                }
                nlog.LogInfo("Address Updated Unsuccessfully");
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                nlog.LogInfo("Address Updated Unsuccessfully");
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpPost]
        [Route("DeleteAddress")]
        public ActionResult DeleteAddress(int customerId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "UserId").Value);
                var result = this.customerDetailsManager.DeleteAddress(customerId,userId);
                if (result)
                {
                nlog.LogInfo("Address Deleted successfully");
                    return this.Ok(new { Status = true, Message = "Address Deleted Successful", data = result });
                }
                nlog.LogInfo("Address Deleted Unsuccessfully");
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                nlog.LogInfo("Address Deleted Unsuccessfully"+ex.Message);
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("GetAllAddressByUserId")]
        public ActionResult GetAllAddressByUserId()
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(v => v.Type == "UserId").Value);
                var result = this.customerDetailsManager.GetAllAddressByUserId(userId);
                if (result != null)
                {
                    nlog.LogInfo("Address reterived Successfully");
                    return this.Ok(new { Status = true, Message = "Note resotored Successful", data = result });
                }
                nlog.LogInfo("Address reterived Unsuccessfully");
                return this.BadRequest(new { Status = false, Message = "Not found" });
            }
            catch (Exception ex)
            {
                nlog.LogInfo("cart reterived unsuccessfully"+ex.Message);
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}

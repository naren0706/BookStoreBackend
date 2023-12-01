using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Threading.Tasks;
using System;
using BookstoreModel;
using BookstoreManager.IBookstoreManager;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using NlogImplementation;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserConroller : ControllerBase
    {
        public readonly IUserManager userManager;
        NlogOperation nlog = new NlogOperation();
        public UserConroller(IUserManager userManager)
        {
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public ActionResult UserRegister(User register)
        {
            try
            {
                var result = this.userManager.RegisterUser(register);
                if (result != null)
                {
                    nlog.LogInfo("user registered successfully");
                    return this.Ok(new { Status = true, Message = "User Registration Successful", data = result });                    
                }
                nlog.LogInfo("User Registration UnSuccessful");
                return this.BadRequest(new { Status = false, Message = "User Registration UnSuccessful" });
            }
            catch (Exception ex)
            {
                nlog.LogInfo("User Registration UnSuccessful due to "+ ex.Message);
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [HttpGet]
        [Route("login")]
        public ActionResult UserLogin(string email,string password)
        {
            try
            {
                var result = this.userManager.UserLogin(email,password);
                if (result != String.Empty)
                {
                    nlog.LogInfo("user login successfully");
                    return this.Ok(new { Status = true, Message = "User login Successful", data = result });
                }
                nlog.LogInfo("user login successfully");
                return this.BadRequest(new { Status = false, Message = "User login UnSuccessful" });
            }
            catch (Exception ex)
            {
                nlog.LogInfo("User login UnSuccessful due to " + ex.Message);
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }

        [HttpGet]
        [Route("forgetPassword")]
        public ActionResult ForgetPassword(string email)
        {
            try
            {
                var result = this.userManager.ForgetPassword(email);
                if (result)
                {
                    nlog.LogInfo("Mail sent Successful");
                    return this.Ok(new { Status = true, Message = "Mail sent Successful" });
                }
                nlog.LogInfo("Mail sent unSuccessful");
                return this.BadRequest(new { Status = false, Message = "Mail Sent UnSuccessfull" });
            }
            catch (Exception ex)
            {
                nlog.LogInfo("Mail Sent UnSuccessfull due to " + ex.Message);
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
        [Authorize]
        [HttpPut]
        [Route("ResetPassword")]
        public ActionResult ResetPassword(string password)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = this.userManager.ResetPassword(email,password);
                if (result)
                {
                    nlog.LogInfo("Password reset Successful");
                    return this.Ok(new { Status = true, Message = "Password reset Successful" });
                }
                nlog.LogInfo("Password reset unSuccessful");
                return this.BadRequest(new { Status = false, Message = "Password reset UnSuccessful" });
            }
            catch (Exception ex)
            {
                nlog.LogInfo("Reset password UnSuccessful due to " + ex.Message);
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}

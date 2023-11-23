using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Threading.Tasks;
using System;
using BookstoreModel;
using BookstoreManager.IBookstoreManager;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserConroller : ControllerBase
    {
        public readonly IUserManager userManager;
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
                    return this.Ok(new { Status = true, Message = "User Registration Successful", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "User Registration UnSuccessful" });
            }
            catch (Exception ex)
            {
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
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "User Registration Successful", data = result });
                }
                return this.BadRequest(new { Status = false, Message = "User Registration UnSuccessful" });
            }
            catch (Exception ex)
            {
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
                    return this.Ok(new { Status = true, Message = "Mail sent Successful" });
                }
                return this.BadRequest(new { Status = false, Message = "Mail Sent UnSuccessful" });
            }
            catch (Exception ex)
            {
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
                    return this.Ok(new { Status = true, Message = "Password reset Successful" });
                }
                return this.BadRequest(new { Status = false, Message = "Password reset UnSuccessful" });
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, Message = ex.Message });
            }
        }
    }
}

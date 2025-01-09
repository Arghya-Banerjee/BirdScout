using Microsoft.AspNetCore.Mvc;
using BirdScout.Server.Models;
using Core;

namespace BirdScout.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SessionController : Controller
    {
        [HttpPost("Register")]
        public IActionResult Register(string username, string email, string profilepic, string password)
        {
            RegisterModel userData = DBOperations<RegisterModel>.GetSpecific(new RegisterModel
            {
                OpMode = 1, //Fetch Data
                UserName = username,
                Email = email
            }, Constant.usp_Register);

            if(userData.UserName != null || userData.Email != null)
            {
                if(userData.UserName != null)
                {
                    return Json(new { success = false, message = "UserName already exists" });
                }
                else
                {
                    return Json(new { success = false, message = "Email already exists" });
                }
            }
            else
            {
                DBOperations<RegisterModel>.DMLOperation(new RegisterModel
                {
                    OpMode = 0, //Insert new user data
                    UserName = username,
                    Email = email,
                    PasswordHash = password,
                    ProfilePicture = profilepic
                }, Constant.usp_Register);

                return Json(new { success = true });
            }
        }

        [HttpGet("Login")]
        public IActionResult Login(string username, string password)
        {
            LoginModel userData = DBOperations<LoginModel>.GetSpecific(new LoginModel
            {
                OpMode = 0,
                UserName = username,
                PasswordHash = password
            }, Constant.usp_Login);

            if(userData.UserName == null)
            {
                return Json(new { success = false, message = "User does not exist" });
            }
            else
            {
                if(username == userData.UserName && password == userData.PasswordHash)
                {
                    UserSec sessionDetails = DBOperations<UserSec>.GetSpecific(new UserSec
                    {
                        OpMode = 0, //Get session datails
                        UserName = username
                    }, Constant.usp_UserSec);

                    if(sessionDetails.RoleID == 1) // Admin
                    {
                        HttpContext.Session.SetObjectAsJson("SessionDetails", sessionDetails);
                        return Json(new
                        {
                            success = true,
                            role = sessionDetails.RoleID,
                            username = sessionDetails.UserName,
                        });
                    }
                    else if (sessionDetails.RoleID == 2) // Guide
                    {
                        HttpContext.Session.SetObjectAsJson("SessionDetails", sessionDetails);
                        return Json(new
                        {
                            success = true,
                            role = sessionDetails.RoleID,
                            username = sessionDetails.UserName,
                        });
                    }
                    else if (sessionDetails.RoleID == 3) // Birder
                    {
                        HttpContext.Session.SetObjectAsJson("SessionDetails", sessionDetails);
                        return Json(new
                        {
                            success = true,
                            role = sessionDetails.RoleID,
                            username = sessionDetails.UserName,
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            message = "Invalid Role"
                        });
                    }
                }
                else
                {
                    return Json(new { success = false, message = "Invalid Password" });
                }
            }
            
        }
    }
}

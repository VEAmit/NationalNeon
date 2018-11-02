using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalNeon.Business.Interfaces;
using NationalNeon.Repository.DB;
using NationalNeon.Web.ViewModels;
using Newtonsoft.Json;

namespace NationalNeon.Web.Controllers
{
    public class LoginController : Controller
    {
        //    private readonly ApplicationDbContext db;
           private readonly IUserBusiness iuserBusiness;
        //    private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginController(ApplicationDbContext db, IUserBusiness iuserBusiness)
        {
           //IHttpContextAccessor httpContextAccessor
           // this.db = db;
            this.iuserBusiness = iuserBusiness;
           // this._httpContextAccessor = httpContextAccessor;
        }
        //    public IActionResult Index()
        //    {
        //        return View();
        //    }

        //    public IActionResult Login()
        //    {
        //        return View();
        //    }

        //    public ActionResult LoginMethod(LoginViewModel model)
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var users = iuserBusiness.GetLoginUser(model.Username,model.Password);             
        //            if (users != null)
        //            {
        //                string cookieValueFromContext = _httpContextAccessor.HttpContext.Request.Cookies["key"];
        //                //read cookie from Request object  
        //                string cookieValueFromReq = Request.Cookies["UserName"];
        //                //set the key value in Cookie  
        //                Set("UserName", users.username, 120);
        //                Set("userId", users.userId.ToString(), 120);
        //                return RedirectToAction("dashboard", "Home",ViewBag.message);
        //            }
        //            else
        //            {
        //                //ModelState.AddModelError("", "please fill all the required fields");
        //                ViewBag.ErrorMessage = "Something Went Wrong";
        //                return View("Login");
        //            }
        //        }         
        //        return View("Login");           
        //    }

        //    public string Get(string key)
        //    {
        //        return Request.Cookies[key];
        //    }
        //    /// <summary>  
        //    /// set the cookie  
        //    /// </summary>  
        //    /// <param name="key">key (unique indentifier)</param>  
        //    /// <param name="value">value to store in cookie object</param>  
        //    /// <param name="expireTime">expiration time</param>  
        //    public void Set(string key, string value, int? expireTime)
        //    {
        //        CookieOptions option = new CookieOptions();
        //        if (expireTime.HasValue)
        //            option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
        //        else
        //            option.Expires = DateTime.Now.AddMilliseconds(10);
        //        Response.Cookies.Append(key, value, option);
        //    }
        //    /// <summary>  
        //    /// Delete the key  
        //    /// </summary>  
        //    /// <param name="key">Key</param>  
        //    public void Remove(string key)
        //    {
        //        Response.Cookies.Delete(key);
        //    }
        //}
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public ActionResult LoginMethod(LoginViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                var users = iuserBusiness.GetLoginUser(model.Username, model.Password);
                if (users != null)
                {
                const string sessionKey = "User";
                Domain.User.UserModel userInfo;
                //var userData = HttpContext.Session.GetString(sessionKey);
                //if (string.IsNullOrEmpty(userData))
                //{
                    userInfo = users;
                    var serialiseduserInfo = JsonConvert.SerializeObject(userInfo);
                    HttpContext.Session.SetString(sessionKey, serialiseduserInfo);
                //}
                //else
                //{
                //    userInfo = JsonConvert.DeserializeObject<Domain.User.UserModel>(userData);
                //}

                //read cookie from Request object  
                //set the key value in Cookie                                      
                return RedirectToAction("dashboard", "Home", ViewBag.message);
                }
                else
                {
                    //ModelState.AddModelError("", "please fill all the required fields");
                    ViewBag.ErrorMessage = "Something Went Wrong";
                    return View("Login");
                }
           // }
          //  return View("Login");

        }

        //public class LoginViewModel
        //{

        //    [Required(ErrorMessage = "Required")]
        //    public string Username { get; set; }
        //    [Required(ErrorMessage = "Required")]
        //    public string Password { get; set; }
        //}
    }
}
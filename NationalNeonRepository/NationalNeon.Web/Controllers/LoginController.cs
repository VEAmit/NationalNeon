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
           private readonly IUserBusiness iuserBusiness;

        public LoginController(ApplicationDbContext db, IUserBusiness iuserBusiness)
        {
            this.iuserBusiness = iuserBusiness;
        }      
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
    }
}
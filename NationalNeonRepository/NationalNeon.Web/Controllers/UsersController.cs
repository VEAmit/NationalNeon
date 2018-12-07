using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NationalNeon.Business.Concrete;
using NationalNeon.Business.Interfaces;
using NationalNeon.Domain.User;
using NationalNeon.Repository.DB;
using NationalNeon.Web.ViewModels;
using Newtonsoft.Json;

namespace NationalNeon.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserBusiness iuserBusiness;

        public UsersController(IUserBusiness iuserBusiness)
        {
            this.iuserBusiness = iuserBusiness;
        }
        [Route("Users")]
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult AddUsers()
        {
            return PartialView("_AddUser");
        }

        public ActionResult UserList()
        {
            var model = iuserBusiness.GetAllUser();
            ViewBag.Model = model;
            return PartialView("_UserList");
        }

        [HttpPost]
        public IActionResult AddUsers(UserViewModel userModel)
        {
            if (userModel.Role != null)
            {
                try
                {
                    UserModel model = new UserModel();
                    Mapper.Map(userModel, model);
                    model.created_on = DateTime.Now;
                    model.updated_on = DateTime.Now;
                    iuserBusiness.AddNewUser(model);
                    return Json(new
                    {
                        success = true
                    });
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Something went wrong.";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Something went wrong.";
            }
            //return RedirectToAction("Index")
            return Json(new
            {
                success = false
            });
        }



        [Route("UserDetails")]
        public IActionResult UserDetails(int userId)
        {
            var model = iuserBusiness.GetUser(userId);
            return View(model);
        }

        [HttpGet]
        public IActionResult UserInfo()
        {
            var userModel = new UserModel();
            var userData = HttpContext.Session.GetString("User");
            {
                userModel = JsonConvert.DeserializeObject<Domain.User.UserModel>(userData);
            }

            return Json(userModel);
        }

        [HttpPost]
        public ActionResult Edit(UserModel model)
        {
            iuserBusiness.Update(model);
            //return RedirectToAction("Index");
            return Json(new
            {
                success = true
            });
        }

        public ActionResult Delete(int id)
        {
            iuserBusiness.DeleteUsers(id);
            //return RedirectToAction("UserList");
            return Json(new
            {
                success = true
            });
        }

        public ActionResult ChangePassword(PasswordViewModel passwordModel)
        {

            iuserBusiness.passwordUpdate(passwordModel.Password, passwordModel.userId);
            //return RedirectToAction("Index");
            return Json(new
            {
                success = true
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NationalNeon.Repository.DB;
using System.Security.Claims;
using NationalNeon.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http.Extensions;
using NationalNeon.Web.Models;
using NationalNeon.Domain.User;
using NationalNeon.Business.Interfaces;
using NationalNeon.Domain.Player;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using OnlineRibbonCore.Helpers;
using NationalNeon.Web.Helpers;
using OnlineRibbonCore.Controllers;

namespace NationalNeon.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IPlayerBusiness iplayerBusiness;
        private readonly ICountryBusiness icountryBusiness;
        public AccountController(IPlayerBusiness iplayerBusiness, ICountryBusiness icountryBusiness)
        {
            this.iplayerBusiness = iplayerBusiness;
            this.icountryBusiness = icountryBusiness;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.MailPasswordNotExist = "Exist";
                    ViewBag.MailExist = 0;
                    ViewBag.PlayerNotActive = 0;
                    ViewBag.HomeForgetPassword = "NotReset";
                    ViewBag.PlayerNotExistMsg = "Exist";
                    ViewBag.ResetPasswordMsg = "NotResetPasswordMsg";
                    var plainText = EncHelper.Encryptdata(model.Password);
                    var isValid = iplayerBusiness.IsValidPlayer(model.EmailId, plainText);
                    Player playerdb = iplayerBusiness.GetPlayerByEmail(model.EmailId);
                    if (!isValid)
                    {

                        var valid = iplayerBusiness.IsAccountValidated(model.EmailId.ToLower(), plainText);
                        if (valid == 3)
                        {
                            ModelState.AddModelError("", "User is invalid");
                            ViewBag.MailExist = 3;
                            return View("ForgotPassword");
                        }
                        if (valid == 1)
                        {
                            ModelState.AddModelError("", "User is invalid");
                            ViewBag.MailPasswordNotExist = "NotExist";
                            return View("ForgotPassword");
                        }
                        if (valid == 2)
                        {
                            ModelState.AddModelError("", "User is invalid");
                            ViewBag.PlayerNotActive = 2;
                            return View("ForgotPassword");
                        }

                        ModelState.AddModelError("", "User is invalid");
                        ViewBag.MailNotExist = true;
                        return View("ForgotPassword");
                    }
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, playerdb.ScreenName));
                    identity.AddClaim(new Claim(ClaimTypes.Name, playerdb.ScreenName));
                    identity.AddClaim(new Claim("PlayerId", playerdb.PLayerId.ToString()));

                    // identity.AddClaim(new Claim(ClaimTypes.Role, playerdb.Role.RoleName));
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties { IsPersistent = model.RememberMe });

                    //Send Email
                    string subject = "Login";
                    string body = "<p>Hello Sir/Mam,</p><b>Login Succesfull</b><br/>";
                    EmailHelper.SendEmail(model.EmailId, body, subject);
                    //return RedirectToAction("Index", "Admin");
                    return RedirectToAction("Index", "PlayerAccount");
                }
                else
                {
                    ModelState.AddModelError("", "Username or Password is blank");
                    return View("ForgotPassword");
                }
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
               CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            try
            {
                var countryList = icountryBusiness.GetCountries();
                ViewBag.country = new SelectList(countryList, "CountryId", "CountryName");
                return View();
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegistrationViewModel playerModel)
        {
            try
            {
                var countryList = icountryBusiness.GetCountries();
                ViewBag.country = new SelectList(countryList, "CountryId", "CountryName");
                if (ModelState.IsValid)
                {
                    if (iplayerBusiness.IsPlayerExist(playerModel.EmailId.ToLower()))
                    {
                        ModelState.AddModelError("EmailId", "Email id already exist.");
                        return View();
                    }

                    var plainText = EncHelper.Encryptdata(playerModel.Password);
                    if (string.IsNullOrWhiteSpace(plainText))
                    {
                        ModelState.AddModelError("", "Username or Password is invalid.");
                        return View();
                    }
                    playerModel.EmailId = playerModel.EmailId.ToLower();
                    playerModel.CreatedDate = DateTime.Now;
                    playerModel.Password = plainText;
                    PlayerModel model = new PlayerModel();
                    Mapper.Map(playerModel, model);
                    iplayerBusiness.AddNewPlayer(model);

                    var lnkHref = "<a href='" + Url.Action("ActivateAccount", "Account", new { email = playerModel.EmailId }, "http") + "'>Click here</a>";
                    string subject = "Confirm Email";
                    string body = "Thank you to join us, please " + lnkHref + " to activate your account and proceed further.";
                    EmailHelper.SendEmail(playerModel.EmailId, body, subject);
                    return RedirectToAction("RegistrationSuccessful", new { emailAddress = playerModel.EmailId });
                }
                return View();
            }
            catch (Exception ex)
            {
                return ErrorView(ex);
            }
        }
        public IActionResult ActivateAccount(string email)
        {
            if (iplayerBusiness.IsPlayerExist(email))
            {
                iplayerBusiness.ActivatePlayer(email);
            }
            return RedirectToAction("AccountActivatedSuccessful");
        }
        public JsonResult GetStateById(int countryId)
        {
            var states = icountryBusiness.GetState(countryId).Where(c => c.CountryId == countryId);
            return Json(states);
        }

        public IActionResult ForgotPassword()
        {
            ViewBag.MailPasswordNotExist = "Exist";
            ViewBag.MailExist = 0;
            ViewBag.PlayerNotActive = 0;
            ViewBag.HomeForgetPassword = "Reset";
            ViewBag.PlayerNotExistMsg = "Exist";
            ViewBag.ResetPasswordMsg = "NotResetPasswordMsg";
            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(string EmailId)
        {
            ViewBag.MailPasswordNotExist = "Exist";
            ViewBag.MailExist = 0;
            ViewBag.PlayerNotActive = 0;
            ViewBag.HomeForgetPassword = "Reset";
            ViewBag.PlayerNotExistMsg = "Exist";
            ViewBag.ResetPasswordMsg = "NotResetPasswordMsg";
            if (iplayerBusiness.IsPlayerExist(EmailId))
            {
                var token = Guid.NewGuid();
                iplayerBusiness.UpdateToken(token, EmailId);
                var lnkHref = "<a href='" + Url.Action("ResetPassword", "Account", new { email = EmailId, code = token }, "http") + "'>Reset Password</a>";
                string subject = "Reset Password";
                string body = "<b>Please find the Password Reset Link. </b><br/>" + lnkHref;
                EmailHelper.SendEmail(EmailId, body, subject);
                ViewBag.HomeForgetPassword = "NotReset";
                ViewBag.ResetPasswordMsg = "ResetPasswordMsg";
                return View();
            }
            else
            {
                ViewBag.PlayerNotExistMsg = "PlayerNotExist";
                return View();
            }
        }

        public PartialViewResult LoginPartial()
        {
            LoginViewModel loginViewModel = new LoginViewModel();
            return PartialView("_LoginPartial", loginViewModel);
        }


        public IActionResult ResetPassword(string email, string code)
        {
            ViewBag.Message = 0;
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            model.ReturnToken = code;
            model.EmailId = email;
            return View(model);
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                Player playerModel = iplayerBusiness.UpdatePassword(new Guid(model.ReturnToken), model.EmailId, EncHelper.Encryptdata(model.NewPassword));
                ViewBag.Message = playerModel != null ? 1 : 2;
            }
            return View();
        }
        public IActionResult ErrorView()
        {
            return View();
        }
        public IActionResult DashBoard()
        {
            return View();
        }

        public JsonResult IsMailAlreadyExist(string EmailId)
        {
            bool isExist = iplayerBusiness.IsPlayerExist(EmailId);
            return Json(!isExist);
        }

        public JsonResult IsScreenNameAlreadyExist(string ScreenName)
        {
            bool isExist = iplayerBusiness.IsPlayerScreenNameExist(ScreenName);
            return Json(!isExist);
        }

        public IActionResult ActivatePlayerAccount(string emailId)
        {
            var lnkHref = "<a href='" + Url.Action("ActivateAccount", "Account", new { email = emailId }, "http") + "'>Activate Account</a>";
            string subject = "Confirm Email";
            string body = "<b>Please find the Confirm Link. Click this link to confirm. </b><br/>" + lnkHref;
            EmailHelper.SendEmail(emailId, body, subject);

            ViewBag.MailPasswordNotExist = "Exist";
            ViewBag.MailExist = 0;
            ViewBag.PlayerNotActive = 5;

            return RedirectToAction("RegistrationSuccessful", new { emailAddress = emailId });

        }


        public IActionResult RegistrationSuccessful(string emailAddress)
        {
            return View("RegistrationSuccessful", emailAddress);
        }

        public IActionResult AccountActivatedSuccessful()
        {
            return View();
        }

        public IActionResult SendAccountActivationMail(string emailId)
        {
            var lnkHref = "<a href='" + Url.Action("ActivateAccount", "Account", new { email = emailId }, "http") + "'>Click here</a>";
            string subject = "Confirm Email";
            string body = "Thank you to join us, please " + lnkHref + " to activate your account and proceed further.";
            EmailHelper.SendEmail(emailId, body, subject);
            return RedirectToAction("RegistrationSuccessful", new { emailAddress = emailId });
        }

    }
}
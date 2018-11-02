using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpressMapper;
using NationalNeon.Web.Helpers;
using NationalNeon.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NationalNeon.Web.Controllers
{   
    [Authorize]
    public class PlayerAccountController : Controller
    {
        private readonly IPlayerBusiness iplayerBusiness;
        private readonly ICountryBusiness icountryBusiness;
        public PlayerAccountController(IPlayerBusiness iplayerBusiness, ICountryBusiness icountryBusiness)
        {
            this.iplayerBusiness = iplayerBusiness;
            this.icountryBusiness = icountryBusiness;
        }
        public IActionResult Index()
        {
            var countryList = icountryBusiness.GetCountries();
            ViewBag.CountryId = new SelectList(countryList, "CountryId", "CountryName");
    
            var Id = Convert.ToInt32(User.Claims.ToArray()[2].Value);
                PlayerModel model = iplayerBusiness.GetPlayer(Id);
                return View(model);
            
        }
             
        [HttpPost]
        public IActionResult UpdatePlayer(PlayerModel model)
        {
            model.ModifiedDate = DateTime.Now;
            Player player = iplayerBusiness.GetPlayerById(model.PLayerId);
            player.FirstName = model.FirstName;
            player.LastName = model.LastName;
            player.ModifiedDate = DateTime.Now;
            player.CountryId = model.CountryId;
            player.StateId = model.StateId;
            player.PhoneNo = model.PhoneNo;
            player.Occupation = model.Occupation;
            player.WatchGamePlay = model.WatchGamePlay;
            player.GetUpdateByEmail = model.GetUpdateByEmail;

            //Player playerDb=  iplayerBusiness.UpdatePlayer(model);
            //if (playerDb != null)
            //    ViewBag.Message = "Player details updated successfully";
            return RedirectToAction("Index");
        }
       
    }
}
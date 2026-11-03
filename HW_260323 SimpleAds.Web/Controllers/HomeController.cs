using HW_260323_SimpleAds.Data;
using HW_260323_SimpleAds.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HW_260323_SimpleAds.Web.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=SimpleAds;Integrated Security=true;TrustServerCertificate=true";
        public IActionResult Index()
        {
            var adRepository = new AdRepository(_connectionString);

            var cookie = Request.Cookies["ad-id"];

            return View(new AdsCookiesViewModel
            {
                Ads = adRepository.GetAllAds(),
                DeleteAdIds = cookie
            });
        }
        public IActionResult NewAd()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewAd(Ad ad)
        {
            var adRepository = new AdRepository(_connectionString);
            ad.DateCreated = DateTime.Now;
            adRepository.AddAd(ad);
                                    
            Response.Cookies.Append("ad-id", $"{ad.Id},", new CookieOptions
            {
                Expires = new DateTimeOffset(DateTime.Now.AddDays(7))
            });

            return Redirect("/home/index");
        }

        [HttpPost]
        public IActionResult DeleteAd(int id)
        {
            var adRepository = new AdRepository(_connectionString);
            adRepository.DeleteAd(id);
            return Redirect("/home/index");
        }

    }
}

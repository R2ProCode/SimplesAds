using HW_260323_SimpleAds.Data;
using HW_260323_SimpleAds.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;


namespace HW_260323_SimpleAds.Web.Controllers
{
    public class HomeSessionController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=SimpleAds;Integrated Security=true;TrustServerCertificate=true";
        public IActionResult Index()
        {
            var adRepository = new AdRepository(_connectionString);

            var vm = new AdsSessionViewModel
            {
                DeleteAdIds = HttpContext.Session.Get<List<int>>("ids-list"),
                Ads = adRepository.GetAllAds()
            };

            return View(vm);
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
            ad.Id = adRepository.AddAd(ad);

            List<int> deleteAdIds = HttpContext.Session.Get<List<int>>("ids-list");

            if (deleteAdIds == null)
            {
                deleteAdIds = new List<int>();
            }

            deleteAdIds.Add(ad.Id);

            HttpContext.Session.Set("ids-list", deleteAdIds);
            return Redirect($"/homesession/index");
        }

        [HttpPost]
        public IActionResult DeleteAd(int id)
        {
            var adRepository = new AdRepository(_connectionString);
            adRepository.DeleteAd(id);
            return Redirect("/homesession/index");
        }
    }

    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            string value = session.GetString(key);

            return value == null ? default(T) :
                JsonSerializer.Deserialize<T>(value);
        }
    }
}

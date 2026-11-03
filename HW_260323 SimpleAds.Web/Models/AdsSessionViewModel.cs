using HW_260323_SimpleAds.Data;

namespace HW_260323_SimpleAds.Web.Models
{
    public class AdsSessionViewModel
    {
        public List<Ad> Ads { get; set; }
        public List<int> DeleteAdIds { get; set; } = new List<int>();
    }
}

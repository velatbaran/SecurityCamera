using SecurityCamera.Core.Entities;

namespace SecurityCamera.WebUI.Models
{
    public class ServicePriceViewModel
    {
        public List<Services> Services { get; set; }
        public List<Price> Prices { get; set; }
    }
}

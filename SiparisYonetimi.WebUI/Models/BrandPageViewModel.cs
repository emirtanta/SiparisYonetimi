using SiparisYonetimi.Entities;

namespace SiparisYonetimi.WebUI.Models
{
    public class BrandPageViewModel
    {
        public Brand Brand { get; set; }
        public List<Product>? Products { get; set; }
    }
}

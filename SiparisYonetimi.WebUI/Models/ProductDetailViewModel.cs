using Microsoft.AspNetCore.Mvc.RazorPages;
using SiparisYonetimi.Entities;

namespace SiparisYonetimi.WebUI.Models
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public List<Product>? Products { get; set; } //ilişkili ürünleri getirir
    }
}

using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.WebUI.Models;

namespace SiparisYonetimi.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;

        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "http://localhost:5129/api/";
        }
        public async Task<IActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres + "Products");

            return View(model);
        }

        public async Task<IActionResult> Search(string aranacakKelime)
        {
            var products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres + "Products");

            var model = products.Where(p => p.IsActive && p.Name.ToLower().Contains(aranacakKelime.ToLower()));

            return View(model);
        }

        

        public async Task<IActionResult> Detail(int id)
        {
            var products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres + "Products");


            var product = products.FirstOrDefault(p => p.Id == id); //api'den çektiğimiz listeden route'den gelen id ile eşleşen kaydı bulduk

            var model = new ProductDetailViewModel()
            {
                Product = product, // model içindeki ürün
                Products = products.Where(p => p.CategoryId == product.CategoryId && p.Id != id).Take(4).ToList()// aynı kategorideki diğer ürünler
            };
       

            return View(model);
        }
    }
}

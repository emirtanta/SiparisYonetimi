using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.WebUI.Models;

namespace SiparisYonetimi.WebUI.Controllers
{
    public class BrandsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string  _apiAdres = "http://localhost:5129/api/";

        public BrandsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index(int id)
        {
            var marka = await _httpClient.GetFromJsonAsync<Brand>(_apiAdres + "Brands/" + id);

            var products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres + "Products");

            var model = new BrandPageViewModel()
            {
                Brand = marka,
                Products = products.Where(p => p.IsActive && p.BrandId == id).ToList()
            };

            return View(model);
        }
    }
}

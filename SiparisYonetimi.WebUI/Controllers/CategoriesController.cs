using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;

namespace SiparisYonetimi.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;

        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "http://localhost:5129/api/Categories";
        }

        public async Task<IActionResult> Index(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Category>($"{_apiAdres}/{id}");

            return View(model);
        }
    }
}

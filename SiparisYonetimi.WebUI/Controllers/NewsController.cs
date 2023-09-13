using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;

namespace SiparisYonetimi.WebUI.Controllers
{
    public class NewsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;

        public NewsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "http://localhost:5129/api/News";
        }

        public async Task<IActionResult> Index()
        {
            var model = await _httpClient.GetFromJsonAsync<List<News>>(_apiAdres);


            return View(model);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id==null)
            {
                return BadRequest();
            }

            var model = await _httpClient.GetFromJsonAsync<News>(_apiAdres + id.Value);

            if (model==null)
            {
                return NotFound();
            }

            return View(model);
        }
    }
}

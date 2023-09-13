using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.WebUI.Models;
using System.Diagnostics;

namespace SiparisYonetimi.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres = "http://localhost:5129/api/";

        public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var model = new HomePageViewModel
            {
                Slides = await _httpClient.GetFromJsonAsync<List<Slide>>(_apiAdres + "Slider"),
                Products = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres + "Products"),
                Brands = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdres + "Brands")
            };

            return View(model);
        }

        [Route("iletisim")]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        [Route("iletisim")]
        public async Task<IActionResult> ContactUs(Contact contact)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    var responseMessage = await _httpClient.PostAsJsonAsync(_apiAdres + "Contacts", contact);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        TempData["Mesaj"] = "<div class='alert alert-success'>Mesajınız Gönderildi.Teşekkürler..</div>";

                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {

                    ModelState.AddModelError("","Hata oluştu. Mesajınız gönderilemedi");
                }
            }

            return View(contact);
        }

        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
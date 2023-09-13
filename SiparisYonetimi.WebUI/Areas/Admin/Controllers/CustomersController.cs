using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.Service.Abstract;

namespace SiparisYonetimi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class CustomersController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;

        public CustomersController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "http://localhost:5129/api/Customers";
        }

        public async Task<IActionResult> CustomerList()
        {
            var request = await _httpClient.GetFromJsonAsync<List<Customer>>(_apiAdres);

            return View(request);
        }

        [HttpGet]
        public IActionResult CustomerCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomerCreate(Customer entity)
        {
            try
            {
                var responseMessage = await _httpClient.PostAsJsonAsync(_apiAdres, entity);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(CustomerList));
                }

                else
                {
                    ModelState.AddModelError("", "Kayıt Başarısız");
                }
            }
            catch
            {

                ModelState.AddModelError("", "Hata Oluştu");
            }

            return View(entity);
        }

        [HttpGet]
        public async Task<IActionResult> CustomerEdit(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Customer>($"{_apiAdres}/{id}");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomerEdit(int id, Customer entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var responseMessage = await _httpClient.PutAsJsonAsync(_apiAdres + "/" + id, entity);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(CustomerList));
                    }
                }
                catch
                {

                    ModelState.AddModelError("", "Hata Oluştu");
                }
            }

            return View(entity);
        }

        [HttpGet]
        public async Task<IActionResult> CustomerDelete(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Customer>($"{_apiAdres}/{id}");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomerDelete(int id, Customer entity)
        {
            try
            {
                var responseMessage = await _httpClient.DeleteAsync($"{_apiAdres}/{id}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(CustomerList));
                }
            }
            catch
            {

                return View();
            }

            return View(entity);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using System.Drawing.Drawing2D;

namespace SiparisYonetimi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;

        public CategoriesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "http://localhost:5129/api/Categories";
        }

        

        public async Task<IActionResult> CategoryList()
        {
            var request = await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres);

            return View(request);
        }

        [HttpGet]
        public IActionResult CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryCreate(Category entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var responseMessage = await _httpClient.PostAsJsonAsync(_apiAdres, entity);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(CategoryList));
                    }

                    else
                    {
                        ModelState.AddModelError("", "Kayıt Başarısız");
                    }
                }
                catch
                {

                    ModelState.AddModelError("","Hata Oluştu");
                }
            }

            return View(entity);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryEdit(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Category>($"{_apiAdres}/{id}");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryEdit(int id,Category entity)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    var responseMessage=await _httpClient.PutAsJsonAsync(_apiAdres+"/"+id,entity);

                    return RedirectToAction(nameof(CategoryList));
                }
                catch 
                {

                    ModelState.AddModelError("","Hata Oluştu");
                }
            }

            return View(entity);
        }

        [HttpGet]
        public async Task<IActionResult> CategoryDelete(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Category>($"{_apiAdres}/{id}");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryDelete(int id, Category entity)
        {
            try
            {
                var responseMessage = await _httpClient.DeleteAsync($"{_apiAdres}/{id}");

                return RedirectToAction(nameof(CategoryList));
            }
            catch 
            {

                return View();
            }
        }
    }
}

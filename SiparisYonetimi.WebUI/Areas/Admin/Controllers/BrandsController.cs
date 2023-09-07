using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.WebUI.Utils;
using System.Drawing.Drawing2D;

namespace SiparisYonetimi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")/*,Authorize(Policy ="AdminPolicy")*/]
    public class BrandsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;

        public BrandsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "http://localhost:5129/api/Brands";
        }

        

        public async Task<IActionResult> BrandList()
        {
            var request = await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdres);

            return View(request);
        }

        [HttpGet]
        public IActionResult BrandCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BrandCreate(Brand brand, IFormFile? Logo)
        {
            

            if (ModelState.IsValid)
            {
                try
                {

                    if (Logo is not null)
                    {
                        brand.Logo = await FileHelper.FileLoaderAsync(Logo);

                    }

                    var responseMessage = await _httpClient.PostAsJsonAsync(_apiAdres, brand);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(BrandList));
                    }

                    else
                    {
                        ModelState.AddModelError("", "Kayıt Başarısız");
                    }


                }
                catch 
                {

                    ModelState.AddModelError("","Hata Oluştu!");
                }
            }

            return View(brand);
        }

        [HttpGet]
        public async Task<IActionResult> BrandEdit(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Brand>($"{_apiAdres}/{id}");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BrandEdit(int id,Brand brand,IFormFile? Logo)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    if (Logo is not null)
                    {
                        brand.Logo=await FileHelper.FileLoaderAsync(Logo);
                    }

                    var responseMessage = await _httpClient.PutAsJsonAsync(_apiAdres + "/" + id, brand);

                    return RedirectToAction(nameof(BrandList));
                }
                catch 
                {

                    ModelState.AddModelError("","Hata Oluştu");
                }
            }

            return View(brand);
        }

        [HttpGet]
        public async Task<IActionResult> BrandDelete(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Brand>($"{_apiAdres}/{id}");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BrandDelete(int id,Brand brand)
        {
            try
            {
                var responseMessage = await _httpClient.DeleteAsync($"{_apiAdres}/{id}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    FileHelper.FileRemover(brand.Logo);
                }

                return RedirectToAction(nameof(BrandList));
            }
            catch
            {

                ModelState.AddModelError("", "Hata Oluştu!!!");
            }

            return View(brand);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.WebUI.Utils;
using System.Reflection.Metadata.Ecma335;

namespace SiparisYonetimi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Policy ="AdminPolicy")]
    public class NewsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;

        public NewsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "http://localhost:5129/api/News";
        }

        public async Task<IActionResult> NewsList()
        {
            var model = await _httpClient.GetFromJsonAsync<List<News>>(_apiAdres);

            return View(model);
        }

        [HttpGet]
        public IActionResult NewsCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewsCreate(News news,IFormFile? Image)
        {
            try
            {
                if (Image is not null)
                {
                    news.Image = await FileHelper.FileLoaderAsync(Image);
                }

                var responseMessage = await _httpClient.PostAsJsonAsync(_apiAdres, news);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(NewsList));
                }
            }
            catch 
            {

                ModelState.AddModelError("","Hata oluştu");
            }

            return View(news);
        }

        [HttpGet]
        public async Task<IActionResult> NewsEdit(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<News>(_apiAdres + "/" + id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewsEdit(int id,News news,IFormFile? Image,bool? resmiSil)
        {
            try
            {
                if (resmiSil is not null && resmiSil==true)
                {
                    FileHelper.FileRemover(news.Image);

                    news.Image = "";
                }

                if (Image is not null)
                {
                    news.Image=await FileHelper.FileLoaderAsync(Image); 
                }

                var responseMessage=await _httpClient.PutAsJsonAsync(_apiAdres , news);

                if (responseMessage.IsSuccessStatusCode) 
                {
                    return RedirectToAction(nameof(NewsList));
                }
            }
            catch 
            {

                ModelState.AddModelError("","Hata oluştu");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> NewsDelete(int id)
        {
            var model=await _httpClient.GetFromJsonAsync<News>(_apiAdres+"/" + id);

            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewsDelete(int id,News news)
        {
            try
            {
                FileHelper.FileRemover(news.Image);

                HttpResponseMessage result=await _httpClient.DeleteAsync(_apiAdres+"/" + id);

                return RedirectToAction(nameof(NewsList));    
            }
            catch 
            {

                return View();
            }
        }
    }
}

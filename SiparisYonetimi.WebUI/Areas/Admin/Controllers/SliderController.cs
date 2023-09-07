using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.WebUI.Utils;

namespace SiparisYonetimi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;

        public SliderController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "http://localhost:5129/api/Slider";
        }

        public async Task<IActionResult> SliderList()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Slide>>(_apiAdres);

            return View(model);
        }

        public IActionResult SliderCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SliderCreate(Slide slide,IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null)
                    {
                        slide.Image = await FileHelper.FileLoaderAsync(Image);
                        
                    }

                    var responseMessage = await _httpClient.PostAsJsonAsync(_apiAdres, slide);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(SliderList));
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

            return View(slide);
        }

        [HttpGet]
        public async Task<IActionResult> SliderEdit(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Slide>($"{_apiAdres}/{id}");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SliderEdit(int id,Slide slide,IFormFile? Image)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    if (Image is not null)
                    {
                        slide.Image=await FileHelper.FileLoaderAsync(Image);
                    }

                    var responseMessage = await _httpClient.PutAsJsonAsync(_apiAdres + "/" + id, slide);

                    return RedirectToAction(nameof(SliderList));
                }
                catch 
                {

                    ModelState.AddModelError("","Hata Oluştu!!!");
                }
            }

            return View(slide);
        }

        [HttpGet]
        public async Task<IActionResult> SliderDelete(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Slide>($"{_apiAdres}/{id}");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SliderDelete(int id,Slide slide)
        {
            try
            {
                var responseMessage = await _httpClient.DeleteAsync($"{_apiAdres}/{id}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    FileHelper.FileRemover(slide.Image);
                }

                return RedirectToAction(nameof(SliderList));
            }
            catch 
            {

                ModelState.AddModelError("","Hata Oluştu!!!");
            }

            return View(slide);
        }
    }
}

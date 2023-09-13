using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;

namespace SiparisYonetimi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Policy ="AdminPolicy")]
    public class LogsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;

        public LogsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "http://localhost:5129/api/Logs";
        }

        public async Task<IActionResult> LogList()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Log>>(_apiAdres);

            return View(model);
        }

        [HttpGet]
        public IActionResult LogCreate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogCreate(Log log)
        {
            try
            {
                var responseMessage = await _httpClient.PostAsJsonAsync(_apiAdres, log);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(LogList));
                }
            }
            catch 
            {

                ModelState.AddModelError("","Hata oluştu");
            }

            return View(log);
        }

        [HttpGet]
        public async Task<IActionResult> LogEdit(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Log>(_apiAdres + "/" + id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogEdit(int id,Log log)
        {
            try
            {
                var responseMessage = await _httpClient.PutAsJsonAsync(_apiAdres, log);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(LogList));
                }
            }
            catch 
            {

                ModelState.AddModelError("","Hata oluştu");
            }

            return View();
        }

        public async Task<IActionResult> LogDelete(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Log>(_apiAdres + "/" + id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogDelete(int id,Log log)
        {
            try
            {
                await _httpClient.DeleteAsync(_apiAdres + "/" + id);

                return RedirectToAction(nameof(LogList));
            }
            catch 
            {

                return View();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;

namespace SiparisYonetimi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ContactsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;
        public ContactsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres= "http://localhost:5129/api/Contacts";
        }

        [HttpGet]
        public async Task<IActionResult> ContactList()
        {
            var request = await _httpClient.GetFromJsonAsync<List<Contact>>(_apiAdres);

            return View(request);
        }

        [HttpGet]
        public IActionResult ContactCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ContactCreate(Contact entity)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var responseMessage=await _httpClient.PostAsJsonAsync(_apiAdres, entity);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(ContactList));
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
        public async Task<IActionResult> ContactEdit(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Contact>($"{_apiAdres}/{id}");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactEdit(int id,Contact entity)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    var responseMessage=await _httpClient.PutAsJsonAsync(_apiAdres+"/"+id, entity);

                    return RedirectToAction(nameof(ContactList));
                }
                catch 
                {

                    ModelState.AddModelError("","Hata Oluştu");
                }
            }

            return View(entity);
        }

        [HttpGet]
        public async Task<IActionResult> ContactDelete(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Contact>($"{_apiAdres}/{id}");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactDelete(int id,Contact entity)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiAdres}/{id}");

                return RedirectToAction(nameof(ContactList));
            }
            catch 
            {

                return View();
            }
        }
    }
}

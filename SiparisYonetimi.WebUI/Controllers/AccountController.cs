using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.WebUI.Models;

namespace SiparisYonetimi.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private HttpClient _httpClient;
        private readonly string _apiAdres;

        public AccountController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "http://localhost:5129/api/Users";
        }

        
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("userId");

            if (userId is not null)
            {
                TempData["Message"]= "<div class='alert alert-danger'>Lütfen Giriş Yapınız!</div>";

                return RedirectToAction(nameof(Login));
            }

            else 
            {
                //var user = await _httpClient.GetFromJsonAsync<User>(_apiAdres + "/" + userId);
                var user = await _httpClient.GetFromJsonAsync<User>(_apiAdres + "/" + userId);

                return View(user);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel)
        {
            var users = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres);

            var user = users.FirstOrDefault(x => x.Email == userLoginViewModel.Email && x.Password == userLoginViewModel.Password && x.IsActive);

            if (user == null)
            {
                ModelState.AddModelError("", "Hata oluştu");
            }

             else
            {
                HttpContext.Session.SetInt32("userId", user.Id);
                HttpContext.Session.SetString("userId", user.UserGuid.ToString());

                return RedirectToAction(nameof(Index));
            }

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(User entity)
        {
            try
            {
                var userId=HttpContext.Session.GetInt32("userId");

                var user=await _httpClient.GetFromJsonAsync<User>(_apiAdres+"/" + userId);

                if (user is not null)
                {
                    user.Name = entity.Name;
                    user.Surname = entity.Surname;
                    user.Email = entity.Email;
                    user.Phone = entity.Phone;
                    user.Password = entity.Password;

                    var responseMessage = await _httpClient.PutAsJsonAsync(_apiAdres, user);

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            catch (Exception)
            {

                ModelState.AddModelError("","Hata oluştu!!");
            }

            return View(nameof(Index),entity);  
        }

        public IActionResult SigIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SigIn(User entity)
        {
            try
            {
                var users = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres);

                var user = users.FirstOrDefault(x => x.Email == entity.Email);

                if (user!=null)
                {
                    ModelState.AddModelError("", "Bu mail ile daha önce kayıt olunmuş");

                    return View();
                }

                else
                {
                    entity.UserGuid = Guid.NewGuid();
                    entity.IsActive=true;
                    entity.IsActive = false;

                    var result = await _httpClient.PostAsJsonAsync(_apiAdres, entity);

                    if (result.IsSuccessStatusCode) 
                    {
                        TempData["Message"] = "<div class='alert alert-success'>Kayıt Başarılı! Giriş Yapabilirsiniz..</div>";

                        return RedirectToAction(nameof(Login)); 
                    }
                }
            }
            catch 
            {

                ModelState.AddModelError("","Hata Oluştu");
            }

            return View(entity);
        }

        public IActionResult TestLogin()
        { 
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> TestLogin(User entity)
        {
            try
            {
                var users = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres);

                var user = users.FirstOrDefault(x => x.Email == entity.Email);

                

                
                    //entity.UserGuid = Guid.NewGuid();
                    //entity.IsActive = true;
                    //entity.IsActive = false;

                    var result = await _httpClient.PostAsJsonAsync(_apiAdres, entity);

                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Message"] = "<div class='alert alert-success'>Kayıt Başarılı! Giriş Yapabilirsiniz..</div>";

                        return RedirectToAction("Index","Home");
                    }
                
            }
            catch
            {

                ModelState.AddModelError("", "Hata Oluştu");
            }

            return View(entity);
        }




        public IActionResult NewPassword()
        {
            return View();
        }

        public IActionResult Logout()
        {
            try
            {
                HttpContext.Session.Remove("userId");
                HttpContext.Session.Remove("userGuid");
            }
            catch 
            {

                HttpContext.Session.Clear();
            }

            return RedirectToAction("Index","Home");
        }
    }
}

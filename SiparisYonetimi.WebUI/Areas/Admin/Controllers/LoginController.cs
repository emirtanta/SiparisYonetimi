using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.WebUI.Models;
using System.Security.Claims;

namespace SiparisYonetimi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;
        public LoginController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "http://localhost:5129/api/Users";
        }

        public IActionResult Index(string ReturnUrl)
        {
            var model =new AdminLoginViewModel();
            model.ReturnUrl = ReturnUrl;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AdminLoginViewModel adminLoginViewModel)
        {
            try
            {
                var userList = await _httpClient.GetFromJsonAsync<List<User>>(_apiAdres);
                
                var account = userList.FirstOrDefault(x => x.Email == adminLoginViewModel.Email && x.Password==adminLoginViewModel.Password &&
                x.IsActive);


                if (account == null) 
                {
                    ModelState.AddModelError("", "Giriş Başarısız");
                }

                else
                {
                    var kullaniciYetkileri = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email,account.Email),
                        new Claim("Role",account.IsAdmin ? "Admin" :"User"),
                        new Claim("UserGuid",account.UserGuid.ToString())
                    };

                    var kullaniciKimligi = new ClaimsIdentity(kullaniciYetkileri, "Login");

                    ClaimsPrincipal principal = new(kullaniciKimligi);

                    await HttpContext.SignInAsync(principal);

                    return RedirectToAction(string.IsNullOrEmpty(adminLoginViewModel.ReturnUrl) ? "/Admin/Main" : adminLoginViewModel.ReturnUrl);
                }
            }
            catch (Exception)
            {

                ModelState.AddModelError("","Hata oluştu");
            }

            return View(adminLoginViewModel);
        }
    }
}

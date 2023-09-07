using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using SiparisYonetimi.Entities;
using SiparisYonetimi.Service.Abstract;

namespace SiparisYonetimi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IService<User> _userService;

        public UsersController(IService<User> userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> UserList()
        {
            var model = await _userService.GetAllAsync();

            return View(model);
        }

        

        [HttpGet]
        public IActionResult UserCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UserCreate(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.AddAsync(user);

                    await _userService.SaveChangesAsync();

                    return RedirectToAction(nameof(UserList));
                }
                catch 
                {

                    ModelState.AddModelError("","Hata Oluştu");
                }
            }

            return View(user);
        }

        [HttpGet]
        public  IActionResult UserEdit(int id)
        {
            var model =  _userService.Find(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEdit(int id,User user)
        {
            if (ModelState.IsValid) 
            {
                try
                {
                    _userService.Update(user);

                    await _userService.SaveChangesAsync();

                    return RedirectToAction(nameof(UserList));
                }
                catch 
                {

                    ModelState.AddModelError("","Hata Oluştu");
                }
            }

            return View(user);
        }

        public IActionResult UserDelete(int id)
        {
            var model=_userService.Find(id);

            return View(model);
        }

        [HttpPost,ActionName(nameof(UserDelete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserDeleteConfirmed(int id,User user)
        {
            try
            {
                _userService.Delete(user);

                await _userService.SaveChangesAsync();

                return RedirectToAction(nameof(UserList));
            }
            catch 
            {

                return View();
            }
        }
    }
}

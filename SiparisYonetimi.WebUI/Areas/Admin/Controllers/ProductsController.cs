using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SiparisYonetimi.Entities;
using SiparisYonetimi.WebUI.Utils;

namespace SiparisYonetimi.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class ProductsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiAdres;

        public ProductsController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _apiAdres = "http://localhost:5129/api/";
        }

        // GET: ProductsController
        public async Task<ActionResult> ProductList()
        {
            var model = await _httpClient.GetFromJsonAsync<List<Product>>(_apiAdres + "Products");
            return View(model);
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public async Task<ActionResult> ProductCreate()
        {
            ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres + "Categories"), "Id", "Name");

            ViewBag.BrandId = new SelectList(await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdres + "Brands"), "Id", "Name");

            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProductCreate(Product product, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null)
                    {
                        product.Image = await FileHelper.FileLoaderAsync(Image);

                    }

                    var response = await _httpClient.PostAsJsonAsync(_apiAdres + "Products", product);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(ProductList));

                    }

                    else
                    {
                        ModelState.AddModelError("", "Kayıt Başarısız!");
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }

            ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres + "Categories"), "Id", "Name");

            ViewBag.BrandId = new SelectList(await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdres + "Brands"), "Id", "Name");

            return View(product);
        }

        // GET: ProductsController/Edit/5
        public async Task<ActionResult> ProductEdit(int id)
        {
            ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres + "Categories"), "Id", "Name");

            ViewBag.BrandId = new SelectList(await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdres + "Brands"), "Id", "Name");

            var model = await _httpClient.GetFromJsonAsync<Product>($"{_apiAdres}Products/{id}");

            return View(model);
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProductEdit(int id, Product product, IFormFile? Image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Image is not null)
                    {
                        product.Image = await FileHelper.FileLoaderAsync(Image);
                    
                    }
                    
                    var response = await _httpClient.PutAsJsonAsync(_apiAdres + "Products/" + id, product);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(ProductList));
                    }
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }
            }

            ViewBag.CategoryId = new SelectList(await _httpClient.GetFromJsonAsync<List<Category>>(_apiAdres + "Categories"), "Id", "Name");

            ViewBag.BrandId = new SelectList(await _httpClient.GetFromJsonAsync<List<Brand>>(_apiAdres + "Brands"), "Id", "Name");

            return View(product);
        }

        // GET: ProductsController/Delete/5
        public async Task<ActionResult> ProductDelete(int id)
        {
            var model = await _httpClient.GetFromJsonAsync<Product>($"{_apiAdres}Products/{id}");
            return View(model);
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProductDelete(int id, Product product)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiAdres}Products/{id}");

                if (response.IsSuccessStatusCode)
                {
                    FileHelper.FileRemover(product.Image); // resmi sunucudan silmek için

                }

                return RedirectToAction(nameof(ProductList));
            }
            catch
            {
                return View();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SiparisYonetimi.Entities;
using SiparisYonetimi.Service.Abstract;
using SiparisYonetimi.WebUI.Utils;

namespace SiparisYonetimi.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductService _productService;

        public CartController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(GetCart());
        }

        public async Task<IActionResult> AddToCart(int productId,int quantity=1)
        {
            var product=await _productService.FindAsync(productId);

            if (product!=null)
            {
                var cart = GetCart();
                cart.AddProduct(product,quantity);

                SaveCart(cart);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromCart(int productId) 
        {
            var product=_productService.Find(productId);

            if (product!=null) 
            {
                var cart = GetCart();
                cart.RemoveProduct(product);
                
                SaveCart(cart);
            }

            return RedirectToAction(nameof(Index));
        }


        public void SaveCart(Cart cart) 
        {
            HttpContext.Session.SetJson("Cart", cart);
        }

        public Cart GetCart()
        {
            // session a bakıyoruz eğer cart isminde session varsa onu yoksa yeni bir cart nesnesi döndürüyoruz
            return HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart(); 
        }
    }
}

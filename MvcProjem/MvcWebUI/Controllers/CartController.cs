using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using MvcWebUI.Models;
using MvcWebUI.Services;

namespace MvcWebUI.Controllers
{
    public class CartController : Controller
    {
        private ICartSessionService _cartSessionService;
        private ICartService _cartService;
        private IProductService _productService;
        public CartController(ICartSessionService cartSessionService, ICartService cartService, IProductService productService)
        {
            _cartSessionService= cartSessionService;
            _cartService = cartService;
            _productService = productService;
        }

        public ActionResult AddToCart(int productId)
        {
            var addedProduct = _productService.GetById(productId);
            //Sessiona eklenecek ürünü veritabanından çektim
            //şimdi sepete ulaşayım
            var cart = _cartSessionService.GetCart();
            //Bu cart nesnesine ürün ekleyeceğim.
            _cartService.AddToCart(cart,addedProduct );
            //Ekledikten sonra bunu geri session a basmalıyım
            _cartSessionService.SetCart(cart);
            //Temp data tek seferlik data taşır.
            TempData.Add("message", String.Format(addedProduct.ProductName));
            //TempData.Add("message",String.Format("Your Product,{0} ,was added to the cart",addedProduct.ProductName));
            return RedirectToAction("Index","Products");
        }

        public ActionResult List()
        {
            //Sepeti listeleyelim
            var cart = _cartSessionService.GetCart();
            CartSummaryViewModel cartSummaryViewModel = new CartSummaryViewModel
            {
                Cart = cart,
            };
            return View(cartSummaryViewModel);
        }
        public ActionResult Remove(int productId)
        {
            //Önce Session ı çekip ordaki bilgileri productId olarak silip tekrardan kalan kartı session a yazacagım.
            var cart = _cartSessionService.GetCart();
            _cartService.RemoveFromCart(cart, productId);
            _cartSessionService.SetCart(cart);
            TempData.Add("message", String.Format("Product was Successfully removed from the cart"));
            return RedirectToAction("List");
        }

        public ActionResult Complete()
        {
            var shippingDetailsViewModel = new ShippingDetailsViewModel
            {
                ShippingDetails = new ShippingDetails()
            };
            return View(shippingDetailsViewModel);
        }

        [HttpPost]
        public ActionResult Complete(ShippingDetails shippingDetails)
        {
            //Girilen model dogru mu ?
            if (!ModelState.IsValid)
            {
                return View();
            }
            TempData.Add("message", String.Format("Thank you {0}, your order is in process", shippingDetails.FirstName));
            return View();
        }
    }
}

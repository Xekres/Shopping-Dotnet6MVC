using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcWebUI.Models;

namespace MvcWebUI.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        public AdminController(IProductService productService,ICategoryService categoryService)
        {
            _productService = productService; 
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            //Ürünleri listeleyelim önce
            var productListViewModel = new ProductListViewModel
            {
                Products = _productService.GetAll()
            };
            return View(productListViewModel);
        }

        public ActionResult Add()
        {
            var model = new ProductAddViewModel
            {
                Product = new Product(),
                Categories = _categoryService.GetAll()
            };
                
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Product product)
        {
            //ModelState sadece valid ise bu işlemi yap demeliyim.
            if (ModelState.IsValid)
            {
                _productService.Add(product);
                TempData.Add("message", "Product was successfully Added");
            }
            //Return View da diyebilirim ancak o zaman içi boş olacagı için hata veriyor
            //En iyisi Return Redirect To Action =>Add diyelim aksiyonu çalıstırsın
            return RedirectToAction("Add");
        }

        public ActionResult Update(int productId)
        {
            var model = new ProductUpdateViewModel
            {
                Product = _productService.GetById(productId),
                Categories = _categoryService.GetAll()

            };
            return View();
        }
        //Şimdi bu aslında add işlemi ile hemen hemen aynı oluyor ancak ürünü direk çekmem gerek
        [HttpPost]
        public ActionResult Update(Product product)
        {
            
            if (ModelState.IsValid)
            {
                _productService.Update(product);
                TempData.Add("message", "Product was successfully Updated");
            }
            
            return RedirectToAction("Update");
        }

        public ActionResult Delete(Product product)
        {
            //Foreign Keyleri olan ürünleri silemezsin :)
            _productService.Delete(product);

            return RedirectToAction("Index");
        }
    }
}

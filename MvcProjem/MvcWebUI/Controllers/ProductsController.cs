using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using MvcWebUI.Models;

namespace MvcWebUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        
        public ProductsController(IProductService productService)
        {
            _productService = productService;
            
        }
        public ActionResult Index(int page=1,int category=0)
        {
            //Sayfalama yapacagım için sayfa sayısını belirtmeliyim.
            int pageSize = 10;
            //Her sayfada 10 ürün olsun.
            var products=_productService.GetByCategory(category);
            ProductListViewModel model = new ProductListViewModel
            {
                Products = products.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                //2. sayfayı seçtim diyelim (2-1)*10=10 ürünü atladım.

                //TagHelper yazalım pageCount bilgisi vs gibi.
                //Sayfa sayısı: product sayısının 1 sayfada ki product sayısına bölümünden bulunacagı için:
                PegeCount=(int)Math.Ceiling(products.Count/(double)pageSize),
                //Bir sayfada kaç ürün oldugu :
                PageSize=pageSize,
                CurrentCategory= category,
                //Hangi sayfadayız?
                CurrentPage=page
            };
            return View(model);
        }

        //public string Session()
        //{
        //    //Sessionda tutmak istediğim Objeleri tutabilmek için o objeyi önce string e çevirmemiz
        //    //sonra da onu DESERIALIZE ederek yeniden nesne haline getirmemiz gerek.
        //    //

        //    //HttpContext.Session.SetString("city", "Mersin");
        //    //HttpContext.Session.SetInt32("age", 30);

        //    //HttpContext.Session.GetString("city");
        //    //HttpContext.Session.GetInt32("age");
        //}

        //public ActionResult AddToCart()
        //{
        //    //HttpSetObject vs Burada kullanılmaz çünkü test edilebilirliği ortadan kalkar
        //    //session web odaklı bir nesnedir ve yayındayken kullanılmalıdır.
        //    //Bu kısma da abstract olarak çekilmelidir bu yüzden Services(Arayüzler için) diye bir klasör içerisinde işlem yapacagım.

        //}
        
    }
}

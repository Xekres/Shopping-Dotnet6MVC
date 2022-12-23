using Entities.Concrete;
using MvcWebUI.ExtensionMethods;

namespace MvcWebUI.Services
{
    public class CartSessionManager : ICartSessionService
    {
        IHttpContextAccessor _httpContextAccessor;
        public CartSessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Cart GetCart()
        {
            //Sepette var mı kontrol edelim
            Cart cartToCheck= _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");
            if (cartToCheck==null)
            {
                //HttpContext i direk kullanamadıgımız için(Session Controllerlarda kullanılır default olarak)
                //Bu yüzden IHttpContextAccessor u enjekte ettim.
                //Eğer yoksa ilk kez olusturacagım için yeni bir sepet olusturmalıyım.
                _httpContextAccessor.HttpContext.Session.SetObject("cart", new Cart());
                cartToCheck = _httpContextAccessor.HttpContext.Session.GetObject<Cart>("cart");


            }
            return cartToCheck;
        }

        public void SetCart(Cart cart)
        {
            _httpContextAccessor.HttpContext.Session.SetObject("cart", cart);
        }
    }
}

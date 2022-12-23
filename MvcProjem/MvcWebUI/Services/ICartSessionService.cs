using Entities.Concrete;

namespace MvcWebUI.Services
{
    public interface ICartSessionService
    {
        //Session ı okuma ve yazma işlemlerimi yazalım
        Cart GetCart();
        void SetCart(Cart cart);
        
    }
}

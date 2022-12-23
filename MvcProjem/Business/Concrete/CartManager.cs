using Business.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CartManager : ICartService
    {
        public void AddToCart(Cart cart, Product product)
        {
            //Böyle bir ürün var mı?
            CartLine cartLine = cart.CartLines.FirstOrDefault(c => c.Product.ProductId == product.ProductId);
            if (cartLine!=null)
            {
                cartLine.Quantity++;
                return;
                //Eğer sepette ürün varsa cartline sayısını arttır.
            }
            //yoksa product olarak benim gönderdiğim productı al adet olarak ta 1 ekle.;
            cart.CartLines.Add(new CartLine { Product = product,Quantity = 1 });
        }

        public List<CartLine> List(Cart cart)
        {
            return cart.CartLines;
        }

        public void RemoveFromCart(Cart cart, int productId)
        {
            cart.CartLines.Remove(cart.CartLines.FirstOrDefault(c => c.Product.ProductId == productId));
        }
    }
}

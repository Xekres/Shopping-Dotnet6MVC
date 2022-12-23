using Entities.Concrete;

namespace MvcWebUI.Models
{
    public class ProductListViewModel
    {
        public List<Product>Products { get; set; }
        public int PegeCount { get; internal set; }
        public int PageSize { get; internal set; }
        public int CurrentCategory { get; internal set; }
        public int CurrentPage { get; internal set; }
    }
}

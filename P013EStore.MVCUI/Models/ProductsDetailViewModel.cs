using P013EStore.Core.Entities;

namespace P013EStore.MVCUI.Models
{
    public class ProductsDetailViewModel
    {
        public Product Product { get; set; }
        public List<Product>? RelatedProducts { get; set; }
    }
}

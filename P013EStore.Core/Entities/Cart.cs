using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P013EStore.Core.Entities
{
    public class Cart
    {
        private List<Cartline> products = new List<Cartline>();
        public List<Cartline> Products => products;
        public void AddProduct(Product product, int quantity)
        {
            var prd = products.Where(p => p.Product.Id == product.Id).FirstOrDefault();
            if (prd == null)
            {
                products.Add(new Cartline() { Product = product, Quantity = quantity });
            }
            else
            {
                prd.Quantity += quantity;
            }
        }
        public void RemoveProduct(Product product)
        {
            products.RemoveAll(p => p.Product.Id == product.Id); //sepetten ürün silme metodumuz
        }
        public decimal TotalPrice()
        {
            return products.Sum(p => p.Product.Price* p.Quantity); // sepet toplamını dönen metodumuz
        }
        public void ClearAll()
        {
            products.Clear();   // sepeti boşalt metodumuz
        }
    }
}

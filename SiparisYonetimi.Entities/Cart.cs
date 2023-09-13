using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiparisYonetimi.Entities
{
    public class Cart
    {
        private List<CartLine> products=new List<CartLine>();
        public List<CartLine> Products=>products;

        public void AddProduct(Product product,int quantity)
        {
            var prd=products.Where(p=>p.Product.Id==product.Id).FirstOrDefault();

            if (prd == null) 
            {
                products.Add(new CartLine()
                {
                    Product = product,
                    Quantity = quantity
                });
            }

            else 
            {
                prd.Quantity += quantity;
            }
        }

        /// <summary>
        /// sepetten ürün siler
        /// </summary>
        /// <param name="product"></param>
        public void RemoveProduct(Product product) 
        {
            products.RemoveAll(p=>p.Product.Id==product.Id);
        }

        /// <summary>
        /// sepetteki toplam fiyatı hesaplar
        /// </summary>
        /// <returns></returns>
        public decimal TotalPrice()
        {
            return products.Sum(p => p.Product.Price * p.Quantity);
        }

        /// <summary>
        /// sepetteki ürünleri temizler
        /// </summary>
        public void ClearAll()
        {
            products.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myRetail.DataLayer.Queries
{
    public class ProductQueries
    {
        public Product GetProductByName(string name)
        {
            using (var db = new ProductsEntities())
            {
                return db.Products.FirstOrDefault(c => c.name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            }
        }
        public List<Product> GetProductsByCategory(string category)
        {
            using (var db = new ProductsEntities())
            {
                return db.Products.Where(c => c.category.Equals(category, StringComparison.InvariantCultureIgnoreCase)).ToList();
            }
        }
    }
}

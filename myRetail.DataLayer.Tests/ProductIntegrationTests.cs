using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using myRetail.DataLayer;
using System.Linq;
using System.Collections.Generic;
using myRetail.DataLayer.Queries;

namespace myRetail.DataLayer.Tests
{
    [TestClass]
    public class ProductIntegrationTests
    {

        private readonly ProductQueries _productQueries = new ProductQueries();

        [TestMethod]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            using (var db = new ProductsEntities())
            {
                var all = db.Products.OrderBy(c => c.name);
                Assert.IsNotNull(all);
                Assert.IsTrue(all.Count() > 0);
            }
        }
        [TestMethod]
        public void GetAllProductsByCategory_ShouldReturnAllProducts()
        {

            var results = _productQueries.GetProductsByCategory("toys").ToList();
            Assert.IsNotNull(results);
            Assert.IsTrue(results.All(p => p.category.ToLower().Trim() == "toys"));
        }

        [TestMethod]
        public void GetProductByID_ShouldReturnCorrectProduct()
        {
            using (var db = new ProductsEntities())
            {
                var result = db.Products.FirstOrDefault(c => c.id == 1);
                Assert.IsNotNull(result);
                Assert.AreEqual(1, result.id);
                Assert.AreEqual("Stroller", result.name);
            }
        }

        [TestMethod]
        public void GetProductByName_ShouldReturnCorrectProduct()
        {
            var result = _productQueries.GetProductByName("Optimus Prime");
            Assert.IsNotNull(result);
            Assert.AreEqual("Optimus Prime", result.name);
        }

        [TestMethod]
        public void GetProduct_ShouldNotFindProduct()
        {
            var result = _productQueries.GetProductByName("does not exist");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void AddNewProduct_ShouldAddToDatabase()
        {
            var product = new Product() { name = "Test Product", category = "Cat", last_updated = DateTime.Now, price = 10.00M, sku = "TEST" };
            using (var db = new ProductsEntities())
            {
                db.Products.Add(product);
                db.SaveChanges();          

                var result = db.Products.FirstOrDefault(c => c.id == product.id);
                Assert.IsNotNull(result);
                Assert.AreEqual(product, result);

                db.Products.Remove(product);
                db.SaveChanges();
            }
        }
    }
}

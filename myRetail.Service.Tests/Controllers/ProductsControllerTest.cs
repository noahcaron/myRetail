using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using myRetail.RestService;
using myRetail.RestService.Controllers;
using myRetail.DataLayer;
using Newtonsoft.Json;
using System.Web.Http.Results;

namespace myRetail.RestService.Tests.Controllers
{
    [TestClass]
    public class ProductsControllerTest
    {
        [TestMethod]
        public void GetProduct()
        {
            // Arrange
            ProductsController controller = new ProductsController();

            // Act
            IEnumerable<Product> result = controller.GetProducts();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count());
            Assert.AreEqual(GetTestProducts()[0].name, result.ElementAt(0).name);
        }
        [TestMethod]
        public void GetProductByCategory()
        {
            // Arrange
            ProductsController controller = new ProductsController();

            // Act
            IEnumerable<Product> result = controller.GetProductsByCategory("toys");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual("toys", result.ElementAt(0).category.Trim());
        }

        [TestMethod]
        public void GetById()
        {
            // Arrange
            ProductsController controller = new ProductsController();

            // Act
            var result = controller.GetProduct(3) as OkNegotiatedContentResult<Product>;
            Assert.AreEqual(GetTestProducts()[1].name, result.Content.name);
        }

        [TestMethod]
        public void Post()
        {
            // Arrange
            ProductsController controller = new ProductsController();

            // Act
            Product prod = GetTestProducts()[0];
            controller.PostProduct(prod);

            // Assert
            var result = controller.GetProduct(prod.id) as OkNegotiatedContentResult<Product>;
            Assert.AreEqual(prod.name, result.Content.name);

            //remove
            controller.DeleteProduct(prod.id);
        }

        [TestMethod]
        public void Put()
        {
            // Arrange
            ProductsController controller = new ProductsController();

            // Act
            Product prod = GetTestProducts()[0];
            prod.name = "test";
            controller.PutProduct(prod.id, prod);

            // Assert
            var result = controller.GetProduct(prod.id) as OkNegotiatedContentResult<Product>;
            Assert.AreEqual("test", result.Content.name);

            //return
            prod.name = GetTestProducts()[0].name;
            controller.PutProduct(prod.id, prod);
        }

        [TestMethod]
        public void Delete()
        {
            // Arrange
            ProductsController controller = new ProductsController();

            // Act
            Product prod = controller.GetProducts().ToList()[0];
            controller.DeleteProduct(prod.id);

            // Assert
            var result = controller.GetProduct(prod.id) as OkNegotiatedContentResult<Product>;
            Assert.IsNull(result);

            //return
            controller.PostProduct(prod);
        }

        private List<Product> GetTestProducts()
        {
            var testProducts = new List<Product>();
            testProducts.Add(new Product { name = "Optimus Prime", price = 1.78M, category = "toys", sku = "XYZ904", last_updated = DateTime.Now });
            testProducts.Add(new Product { name = "Sega Genesis", price = 1.78M, category = "toys", sku = "XYZ904", last_updated = DateTime.Now });
            testProducts.Add(new Product { name = "Stroller", price = 1.78M, category = "baby", sku = "XYZ904", last_updated = DateTime.Now });

            return testProducts;
        }

    }
}

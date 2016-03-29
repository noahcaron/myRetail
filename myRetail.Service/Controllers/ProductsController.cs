using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using myRetail.DataLayer;

namespace myRetail.RestService.Controllers
{
    public class ProductsController : ApiController
    {
        private ProductsEntities db = new ProductsEntities();

        // GET: api/Products
        public IQueryable<Product> GetProducts()
        {
            return db.Products.OrderBy(c => c.name).ThenBy(c => c.category);
        }

        // GET: api/Products/GetProductsByCategory/toys
        [ActionName("GetProductsByCategory")]
        [HttpGet]
        public IQueryable<Product> GetProductsByCategory(string category)
        {
            return db.Products.Where(p => p.category.Equals(category, StringComparison.InvariantCultureIgnoreCase))
                .OrderBy(c => c.name).ThenBy(c => c.category);
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product Product = db.Products.Find(id);
            if (Product == null)
            {
                return NotFound();
            }

            return Ok(Product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product Product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Product.id)
            {
                return BadRequest();
            }

            db.Entry(Product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product Product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(Product);
            db.SaveChanges();


            return CreatedAtRoute("DefaultApi", new { id = Product.id }, Product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product Product = db.Products.Find(id);
            if (Product == null)
            {
                return NotFound();
            }

            db.Products.Remove(Product);
            db.SaveChanges();

            return Ok(Product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.id == id) > 0;
        }
    }
}
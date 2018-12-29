using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task2.Data;
using Task2.Models;

namespace Task2.Controllers
{
    [ApiController, Route("api/products")]
    public class ProductsV2Controller : Controller
    {
        static readonly IProductRepository productRepository = new ProductRepository();

        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {
            return productRepository.GetAll();
        }

        [HttpGet, Route("{id:int}")]
        public IActionResult GetProduct(int id)
        {
            Product item = productRepository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpGet, Route("{category}")]
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return productRepository.GetAll()
                .Where(p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        [HttpPost]
        public IActionResult CreateProduct(Product item)
        {
            if (ModelState.IsValid)
            {
                item = productRepository.Add(item);
                return Ok("Item succesfully created");
            }
            else
            {
                return BadRequest("Invalid object as parameter.");
            }
        }

        [HttpPut, Route("{id:int}")]
        public IActionResult UpdateProduct(int id, Product item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid object as parameter.");
            }
            Product temp = productRepository.Get(id);
            if (temp == null) { return NotFound("Product with the id of " + item.Id + " does not exist."); }
            if (temp.Id != item.Id) { return Conflict("Potential conflict of proposed Id detected."); }

            if (!productRepository.Update(item))
            {
                throw new Exception("Update Failed");
            }
            return Ok("Item succesfully updated");
        }

        [HttpDelete, Route("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            Product item = productRepository.Get(id);
            if (item == null)
            {
                throw new Exception("Item not found");
            }

            productRepository.Remove(id);
            return Ok("Item succesfully deleted");
        }

    }

}

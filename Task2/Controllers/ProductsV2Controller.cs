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

        #region GET Controllers
        //1. Returns list of products 
        //Load balance work since a slight delay in one thread will not cause cascading delays throughout
        [HttpGet]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await Task.FromResult(productRepository.GetAll());
        }

        //2. Returns selected product
        [HttpGet, Route("{id:int}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            Product item = productRepository.Get(id);
            if (item == null)
            {
                return await Task.FromResult(NotFound());
            }
            return await Task.FromResult(Ok(item));
        }

        //3. Returns product of selected category
        [HttpGet, Route("{category}")]
        public async Task<IEnumerable<Product>> GetProductsByCategory(string category)
        {
            return await Task.FromResult(productRepository.GetAll()
                .Where(p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase)));
        }
        #endregion

        #region POST Controller
        //4. Create product
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
        #endregion

        #region PUT Controller
        //5. Updates product
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
        #endregion

        #region Delete Controller
        //6. Delete product
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
        #endregion

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task2.Data;
using Task2.Models;

namespace Task2.Controllers
{
    public class ProductsV1Controller : Controller
    {
        static readonly IProductRepository productRepository = new ProductRepository();

        [HttpGet]
        [Route("api/v1/products/version")]
        //http://localhost:9000/api/v1/products/version
        public IActionResult GetVersion()
        {
            return Ok("Version One");
        }

        [HttpGet]
        [Route("api/v1/products")]
        public IEnumerable<Product> GetAllProducts()
        {
            return productRepository.GetAll();
        }

        [HttpGet]
        [Route("api/v1/products/{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = productRepository.GetAll().FirstOrDefault((p) => p.Id == id);
            if (product == null) NotFound();
            return Ok(product);
        }

    }
}

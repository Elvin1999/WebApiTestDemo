using Entities;
using Microsoft.AspNetCore.Mvc;
using WebApiTestDemo.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiTestDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public  ActionResult<IEnumerable<Product>> Get(int top=0)
        {
            var products = _productService.GetProducts(top);
            return Ok(products);
        }

        // POST api/<ProductsController>
        [HttpPost]
        public ActionResult<Product> Post([FromBody]Product product)
        {
            var products = _productService.GetProducts();
            product.Id = products.Count() > 0 ? products.Max(p => p.Id) + 1 : 1;
            _productService.Add(product);
            return product;
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]
        public ActionResult<Product> Put(int id, [FromBody]Product product)
        {
            var item = _productService.Update(product);
            return Ok(item);
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var result = _productService.Delete(id);
            if (!result) return BadRequest();
            return Ok();
        }
    }
}

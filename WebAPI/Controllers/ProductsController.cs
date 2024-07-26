using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(new[]
            {
                new { Name="Bilgisayar",Price=15000 },
                new { Name="Telefon",Price=10000}
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            return Ok(new { Name = "Bilgisayar", Price = 15000 });
        }
    }
}

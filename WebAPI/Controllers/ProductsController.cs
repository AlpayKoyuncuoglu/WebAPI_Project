using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
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

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        //api/products ~ GET
        //api/products/id ~ GET/DELETE
        //api/products ~ POST/PUT

        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        //public async Task<List<Product>> GetAll()
        public async Task<IActionResult> GetAll()
        {
            //return await _productRepository.GetAllAsync();
            var result =  await _productRepository.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        //public async Task<Product> GetById(int id)
        public async Task<IActionResult> GetById(int id)
        {
            //return await _productRepository.GetByIdAsync(id);
            var data = await _productRepository.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);

        }

        //[HttpGet]
        //public IActionResult GetProducts()
        //{
        //    return Ok(new[]
        //    {
        //        new { Name="Bilgisayar",Price=15000 },
        //        new { Name="Telefon",Price=10000}
        //    });
        //}

        //[HttpGet("{id}")]
        //public IActionResult GetProduct(int id)
        //{
        //    return Ok(new { Name = "Bilgisayar", Price = 15000 });
        //}
    }
}


//Repository Pattern, yazılım geliştirmede veri erişimini soyutlamak ve düzenlemek için kullanılan bir tasarım desenidir. Temel amacı, veri erişim kodunu iş mantığından ayırmak ve kodun test edilebilirliğini, bakımını ve yeniden kullanımını artırmaktır. İşte Repository Pattern’ın temel kullanım amaçları ve avantajları:

//Kullanım Amaçları
//Veri Erişim Soyutlama: Repository Pattern, veri erişim katmanını iş mantığından ayırarak veri tabanıyla etkileşimi soyutlar. Bu, veri erişim kodunun daha modüler ve yönetilebilir olmasını sağlar.

//İş Mantığı ile Veri Erişimi Ayırma: İş mantığı ve veri erişimi arasında net bir ayrım yaparak, iş mantığının veri erişim detaylarından bağımsız olmasını sağlar. Bu, iş mantığını daha temiz ve anlaşılır hale getirir.

//Test Edilebilirlik: Veri erişim kodunu soyutlayarak, testler sırasında gerçek veritabanı yerine taklit (mock) veri erişim sınıfları kullanabilirsiniz. Bu, birim testlerinizi daha hızlı ve güvenilir hale getirir.

//Kodun Yeniden Kullanımı ve Bakımı: Veri erişim kodunu tek bir yerde toplamak, kodun bakımını ve yeniden kullanılabilirliğini artırır. Farklı iş mantığı sınıfları aynı veri erişim kodunu kullanabilir.

//Değişikliklere Uyum Sağlama: Veri erişim yöntemlerinde yapılacak değişiklikler (örneğin, veritabanı değişiklikleri veya farklı bir veri kaynağı kullanımı) sadece repository katmanında yapılabilir. Bu değişiklikler, iş mantığını etkilemeden yapılabilir.
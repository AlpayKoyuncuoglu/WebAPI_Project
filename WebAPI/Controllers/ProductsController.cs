using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Interfaces;
using static System.Net.WebRequestMethods;

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
            var result = await _productRepository.GetAllAsync();
            return Ok(result);
        }

        //[HttpGet("{id}")]
        [HttpGet("getById")] // => for query
        //api/products/1 => fromRoute
        //api/products?id=1 => fromQuery
        //[FromRoute] yukarıdaki id ifadesi aslında doğrudan route'tan almasını sağlar

        public async Task<IActionResult> GetById([FromQuery]int id)
        {
            var data = await _productRepository.GetByIdAsync(id);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        //FromQuery olarak işaretlenirse api/products?id=1&name=telefon ...
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        //public async Task<IActionResult> Create([FromBody]Product product) //=>nesne alıyorsa burada aslında fromBody vardır
        {
            var addedProduct = await _productRepository.CreateAsync(product);
            return Created(string.Empty, addedProduct);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            var productToUpdate = await _productRepository.GetByIdAsync(product.Id);
            if (productToUpdate == null)
            {
                return NotFound(product.Id);
            }

            await _productRepository.UpdateAsync(product);
            return Ok(productToUpdate);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id)
        {
            var productToRemove = await _productRepository.GetByIdAsync(id);
            if (productToRemove == null)
            {
                return NotFound(id);
            }
            await _productRepository.RemoveAsync(id);
            return NoContent();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync(IFormFile formFile)
        {
            var newName = Guid.NewGuid() + "" + Path.GetExtension(formFile.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", newName);
            var stream = new FileStream(path, FileMode.Create);
            await formFile.CopyToAsync(stream);
            return Created(string.Empty, formFile);
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

//var newName = Guid.NewGuid() + "" + Path.GetExtension(formFile.FileName);
//
//Guid.NewGuid() yeni bir GUID (Globally Unique Identifier) oluşturur. Bu, dosya için benzersiz bir isim sağlamak için kullanılır.
//Path.GetExtension(formFile.FileName) dosyanın uzantısını alır (örneğin, .jpg, .txt).
//GUID ve uzantı birleştirilerek dosya ismi oluşturulur. Bu, dosya çakışmalarını önlemek için kullanılır.
//var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", newName);
//
//Directory.GetCurrentDirectory() uygulamanın çalışma dizinini alır.
//Path.Combine bu dizinle wwwroot klasörünü ve yeni dosya ismini birleştirir.
//path değişkeni, dosyanın sunucuda kaydedileceği tam yolu belirtir.
//var stream = new FileStream(path, FileMode.Create);
//
//FileStream sınıfı, dosya ile okuma ve yazma işlemleri yapmanıza olanak tanır.
//FileMode.Create parametresi, belirtilen dosya yolunda yeni bir dosya oluşturur (dosya varsa üzerine yazar).

//Repository Pattern, yazılım geliştirmede veri erişimini soyutlamak ve düzenlemek için kullanılan bir tasarım desenidir. Temel amacı, veri erişim kodunu iş mantığından ayırmak ve kodun test edilebilirliğini, bakımını ve yeniden kullanımını artırmaktır. İşte Repository Pattern’ın temel kullanım amaçları ve avantajları:

//Kullanım Amaçları
//Veri Erişim Soyutlama: Repository Pattern, veri erişim katmanını iş mantığından ayırarak veri tabanıyla etkileşimi soyutlar. Bu, veri erişim kodunun daha modüler ve yönetilebilir olmasını sağlar.

//İş Mantığı ile Veri Erişimi Ayırma: İş mantığı ve veri erişimi arasında net bir ayrım yaparak, iş mantığının veri erişim detaylarından bağımsız olmasını sağlar. Bu, iş mantığını daha temiz ve anlaşılır hale getirir.

//Test Edilebilirlik: Veri erişim kodunu soyutlayarak, testler sırasında gerçek veritabanı yerine taklit (mock) veri erişim sınıfları kullanabilirsiniz. Bu, birim testlerinizi daha hızlı ve güvenilir hale getirir.

//Kodun Yeniden Kullanımı ve Bakımı: Veri erişim kodunu tek bir yerde toplamak, kodun bakımını ve yeniden kullanılabilirliğini artırır. Farklı iş mantığı sınıfları aynı veri erişim kodunu kullanabilir.

//Değişikliklere Uyum Sağlama: Veri erişim yöntemlerinde yapılacak değişiklikler (örneğin, veritabanı değişiklikleri veya farklı bir veri kaynağı kullanımı) sadece repository katmanında yapılabilir. Bu değişiklikler, iş mantığını etkilemeden yapılabilir.
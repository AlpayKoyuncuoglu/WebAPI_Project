using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Data;

namespace WebAPI.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllAsync();
        public Task<Product> GetByIdAsync(int id);
        public Task<Product> CreateAsync(Product product);
        public Task<Product> UpdateAsync(Product product);
        public Task RemoveAsync(int id);
    }
}
//update - remove
//Kodun Senkron Olması
//Kodun Basitliği: Bazı eski kodlar veya basit projelerde, asenkron işlemler yerine senkron işlemler tercih edilebilir. Bu durum genellikle, performans gereksinimlerinin düşük olduğu veya yüksek veri trafiği gerektirmeyen senaryolarda görülür.
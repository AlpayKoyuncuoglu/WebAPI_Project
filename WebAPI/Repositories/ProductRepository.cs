using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Interfaces;

namespace WebAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products4.AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products4.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _context.Products4.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var productToUpdate = await _context.Products4.FindAsync(product.Id);
            _context.Entry(productToUpdate).CurrentValues.SetValues(product);
            await _context.SaveChangesAsync();
            return productToUpdate;
        }

        public async Task RemoveAsync(int id)
        {
            var removeEntity= await _context.Products4.FindAsync(id);
            _context.Products4.Remove(removeEntity);
            _context.SaveChanges();
        }

        //public Task<Product> UpdateProduct(Product product)
        //{
        //    var productToUpdate = _context.Products4.FindAsync(product.Id);
        //    _context.Products4.Update(productToUpdate);
        //    _context.SaveChangesAsync();
        //    return product;
        //}

    }
}


//notes:
//AsNoTracking() yöntemi, Entity Framework Core'da bir sorgu yapılırken kullanılan bir özellik olup, sorgunun sonuçlarını takip etmemeyi sağlar. Yani, veritabanından çekilen veriler, değişiklik takibi yapılmadan doğrudan elde edilir. Bunun bazı avantajları ve kullanım durumları şunlardır:
//Performans İyileştirmesi: AsNoTracking() kullanıldığında, EF Core verilerin izlenmesini ve değişikliklerin takip edilmesini yapmaz. Bu, özellikle büyük veri setleriyle çalışırken performansı artırabilir çünkü EF Core, değişiklik takibi yapma iş yükünden kaçınır.
//Okuma Senaryoları İçin Uygundur: Sadece veriyi okuma amacıyla sorgulama yapıyorsanız ve verilerde herhangi bir değişiklik yapmayacaksanız, AsNoTracking() kullanmak daha uygun olabilir. Bu, genellikle raporlama veya veri görüntüleme senaryolarında tercih edilir.
//Bellek Kullanımını Azaltma: Değişiklik takibi yapılmadığı için bellek tüketimi daha düşük olabilir. Bu, özellikle büyük veri kümeleriyle çalışırken faydalı olabilir.

//SingleOrDefault() metodu, Entity Framework Core ve LINQ sorgularında kullanılan bir yöntemdir ve genellikle şu amaçlar için kullanılır:
//Tek Bir Eleman Bekleme: SingleOrDefault(), sorgudan yalnızca bir öğe döndürmeyi amaçlar. Eğer sorgu sonucunda birden fazla öğe varsa, bir InvalidOperationException hatası fırlatır. Ancak eğer hiç öğe yoksa, varsayılan değeri (genellikle null veya sıfır) döndürür.
//Varsayılan Değer Dönme: Eğer sorgudan elde edilen sonuç kümesi boşsa (yani, hiç öğe yoksa), SingleOrDefault() metodu null döndürecektir (referans türleri için) veya varsayılan değer döndürecektir (değer türleri için). Bu, sorgu sonucunun boş olduğu durumlarda kolayca kontrol yapmanızı sağlar.

//Find()
//Belirli Bir Anahtar Kullanımı: Find() metodu, genellikle birincil anahtar (primary key) değeri kullanarak bir varlık arar. Bu, genellikle daha hızlı bir erişim sağlar çünkü Find() metodu veritabanında doğrudan anahtar değerine göre sorgu yapar ve önbelleğe (cache) bakar.
//Performans: Find() metodu, EF Core’un önbelleğinde (cache) arama yaparak veriyi daha hızlı bir şekilde bulabilir. Veritabanında doğrudan sorgu yapmadan önce önbelleği kontrol eder.

//Farklar ve Ne Zaman Kullanılacağı
//Koşul Kullanımı: SingleOrDefault() metodu, daha karmaşık koşullar ile sorgulama yapmanıza olanak sağlar. Find() ise yalnızca anahtar değeri kullanarak arama yapabilir.
//Performans: Find() genellikle performans açısından daha hızlıdır, çünkü önbellekten (cache) veri alabilir ve doğrudan anahtar kullanarak sorgu yapar. Ancak, Find() sadece birincil anahtar ile çalışır.
//Sorgu Türü: SingleOrDefault() ile LINQ sorguları oluşturabilirsiniz ve daha fazla özelleştirilmiş sorgu yapabilirsiniz. Find() ise sadece belirli bir anahtar ile çalışır.
//Sonuç
//Eğer anahtar (primary key) ile bir varlığı arıyorsanız ve performans ön plandaysa, Find() metodunu kullanabilirsiniz.
//Eğer daha karmaşık bir koşul kullanarak tekil bir öğe arıyorsanız, SingleOrDefault() tercih edilmelidir.
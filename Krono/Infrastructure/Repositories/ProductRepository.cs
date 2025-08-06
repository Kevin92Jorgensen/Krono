using Krono.Infrastructure.Data;
using Krono.Infrastructure.Models;

namespace Krono.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly KronoDbContext _context;

        public ProductRepository(KronoDbContext context)
        {
            _context = context;
        }

        public Product? GetByName(string name)
        {
            return _context.Products.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());
        }

        public Product Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }
    }
}

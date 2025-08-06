using Krono.Infrastructure.Models;

namespace Krono.Infrastructure.Repositories
{
    public interface IProductRepository
    {
        Product? GetByName(string name);
        Product Add(Product product);
    }
}

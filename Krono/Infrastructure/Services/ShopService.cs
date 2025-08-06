using Krono.Infrastructure.Data;
using Krono.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Krono.Infrastructure.Services
{
    public interface IShopService
    {
        Task<Shop> GetShopByName(string name);
    }

    public class ShopService : IShopService
    {
        private readonly KronoDbContext _context;

        public ShopService(KronoDbContext context)
        {
            _context = context;
        }

        public async Task<Shop> GetShopByName(string name)
        {
            var shop = await _context.Shops.FirstOrDefaultAsync(s => s.Name.ToLower() == name.ToLower());
            return shop;
        }
    }
}

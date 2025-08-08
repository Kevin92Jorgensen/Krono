using Krono.Infrastructure.Data;
using Krono.Infrastructure.Models;
using Krono.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Krono.Infrastructure.Services
{
    public interface IProductService
    {
        Task<Infrastructure.Models.Product> GetProduct(string gtid);
        Task<bool> SetEU(string gtid, bool isEU);
        Task<bool> SetOrganic(string gtid, bool isOrganic);
        Task<Product> CreateProduct(string gtid, string name, string description, string brand, string producttype, string unitOfMeasure, bool isEu = true, bool isOrganic = false);
        Task<PriceEntry> AddPriceForProduct(string gtid, int price, string store);
        Task<List<Product>> GetAllProducts();
        Task<List<Product>> SearchProduct(string query, bool organicOnly);
        Task StoreImageForProduct(int productId, byte[] data, string mimeType);
        Task<(bool Success, string Message, Barcode? Barcode)> SaveBarcodeAsync(string barcode);
        Task<List<Barcode>> GetUnsavedBarcodes();
        Task<bool> DeleteBarcode(string barcode);
    }

    public class ProductService : IProductService
    {
        private readonly KronoDbContext _context;
        private readonly IShopService _shopService;

        public ProductService(KronoDbContext context, IShopService shopService)
        {
            _context = context;
            _shopService = shopService;
        }



        public async Task<List<Product>> SearchProduct(string query, bool organicOnly)
        { 
            var result = await _context.Products.Include(x => x.PriceEntries)
            .ThenInclude(x => x.Shop).Where(x => x.Brand.Contains(query) || x.Gtid.Contains(query) || x.Name.Contains(query) || x.Description.Contains(query) || x.ProductType.Contains(query)).ToListAsync();

            if(organicOnly == true)
            {
                result = result.Where(x => x.IsOrganic == 1).ToList();
            }

            foreach (var product in result)
            {
                product.PriceEntries = product.PriceEntries
                    .GroupBy(pe => pe.ShopId)
                    .Select(g => g.OrderByDescending(pe => pe.CreatedAt).First())
                    .ToList();
            }

            return result;
        }

        public async Task<bool?> AddOrRemoveFavorite(string gtid)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Gtid == gtid);
            if (product == null) return null;

            product.Favorite = 1;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Product> GetProduct(string gtid)
        {
            try
            {

            

            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Gtid == gtid);

            if (product != null) return product;

            
            return null;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<Infrastructure.Models.Product> CreateProduct(string gtid, string name, string description, string brand, string producttype, string unitOfMeasure, bool isEu = true, bool isOrganic = false)
        {
            try
            {


            var product = new Infrastructure.Models.Product
            { 
                Gtid = gtid, 
                Name = name, 
                Description = description, 
                Brand = brand ?? "UKENDT", 
                ProductType = producttype,
                IsEU = isEu ? 1 : 0,
                UnitsOfMeasure = unitOfMeasure,
                IsOrganic = isOrganic ? 1 : 0,
                CreatedAt = DateTime.UtcNow,
            };

                var existing = await _context.Products.FirstOrDefaultAsync(p => p.Gtid == gtid);
                if (existing != null) return existing;

                _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
            }
            catch (DbUpdateException ex)
            {
                // Optional: log and throw a meaningful error
                throw new InvalidOperationException("Could not save product. It may already exist.", ex);
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task StoreImageForProduct(int productId, byte[] data, string mimeType)
        {

            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new Exception("Product not found");

            product.ImageData = data;
            product.ImageMimeType = mimeType;

            await _context.SaveChangesAsync();
        }

        public async Task<PriceEntry> AddPriceForProduct(string gtid, int price, string store)
        {
            try
            {

            var product = await GetProduct(gtid);
            if (product == null) throw new InvalidOperationException("Product not found");

            var shop = await _shopService.GetShopByName(store);
            if (shop == null) throw new InvalidOperationException("Shop not found");

            var latest = await _context.PriceEntries
      .Where(p => p.ProductId == product.Id && p.ShopId == shop.Id)
      .OrderByDescending(x => x.CreatedAt)
      .FirstOrDefaultAsync();

            if (latest != null && (latest.Price == price || latest.CreatedAt.Date == DateTime.UtcNow.Date))
            {
                return null;
            }

            var entry = new PriceEntry
            {
                ProductId = product.Id,
                Price = price, Gtid = gtid.ToString(),
                ShopId = shop.Id,
                CreatedAt = DateTime.UtcNow
            };

            _context.PriceEntries.Add(entry);
            _context.SaveChanges();

            return entry;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        public async Task<bool> SetEU(string gtid, bool isEU)
        {
            var product = await _context.Products
              .FirstOrDefaultAsync(p => p.Gtid == gtid);

            product.IsEU = isEU ? 1 : 0;
            _context.Products.Update(product);
            _context.SaveChanges();
            return isEU;
        }

        public async Task<bool> SetOrganic(string gtid, bool isOrganic)
        {
            var product = await _context.Products
               .FirstOrDefaultAsync(p => p.Gtid == gtid);

            product.IsOrganic = isOrganic ? 1 : 0;
            _context.Products.Update(product);
            _context.SaveChanges();
            return isOrganic;
        }

        public Task<List<Product>> GetAllProducts()
        {
            return _context.Products
                .Include(p => p.PriceEntries)
                .ToListAsync();
        }

        public async Task<(bool Success, string Message, Barcode? Barcode)> SaveBarcodeAsync(string barcode)
        {
            if (string.IsNullOrWhiteSpace(barcode))
                return (false, "Invalid barcode.", null);

            barcode = barcode.Trim();

            bool exists = await _context.Barcodes.AnyAsync(b => b.Gtid == barcode);
            if (exists)
                return (false, "Barcode already exists.", null);

            var entity = new Barcode { Gtid = barcode };
            _context.Barcodes.Add(entity);
            await _context.SaveChangesAsync();

            return (true, "Saved successfully.", entity);
        }

        public Task<List<Barcode>> GetUnsavedBarcodes()
        {
            return _context.Barcodes
                .Where(b => b.Gtid != null || b.Gtid != string.Empty)
                .ToListAsync();
        }

        public async Task<bool> DeleteBarcode(string barcode)
        {
            try
            {
                _context.Barcodes.Remove(new Barcode { Gtid = barcode });
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }   
            
        }
    }
}

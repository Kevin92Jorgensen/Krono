using Krono.Infrastructure.Services;
using Krono.IntegrationServices.Coop;
using Krono.IntegrationServices.Fotex;
using Krono.IntegrationServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Krono.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {

        private NettoImporter _nettoImporter;
        private FotexImporter _fotexImporter;
        private CoopImporter _coopImporter;
        private readonly IProductService _productService;
        private readonly IShopService _shopService;

        public ProductController(IProductService productService, NettoImporter nettoImporter, IShopService shopService, FotexImporter fotexImporter, CoopImporter coopImporter)
        {
            _productService = productService;
            _nettoImporter = nettoImporter;
            _shopService = shopService;
            _fotexImporter = fotexImporter;
            _coopImporter = coopImporter;
        }

        [HttpGet()]
        public async Task<IActionResult> SearchProduct([FromQuery] string search, [FromQuery] bool organicOnly)
        {
            var products = await _productService.SearchProduct(search, organicOnly);
            return Ok(products);
        }

        //[HttpPost("{gtid}")]
        //public async Task<IActionResult> AddOrRemoveFavorite(string gtid)
        //{
        //    var product = await _context.Products.FirstOrDefaultAsync(p => p.Gtid == gtid);
        //    if (product == null) return NotFound();

        //    product.Favorite = true;
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}
    }

}


using Krono.Infrastructure.Services;
using Krono.Infrastructure.Repositories; // Make sure this path matches your solution
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using Tesseract;
using Krono.IntegrationServices;
using Krono.IntegrationServices.Models;
using Krono.IntegrationServices.Fotex;
using System.Text.Json;
using System.Collections.Generic;
using Krono.Infrastructure.Models;
using Krono.IntegrationServices.Coop;
using Microsoft.EntityFrameworkCore;

namespace Krono.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ScanController : Controller
    {

        private NettoImporter _nettoImporter;
        private FotexImporter _fotexImporter;
        private CoopImporter _coopImporter;
        private readonly IProductService _productService;
        private readonly IShopService _shopService;

        public ScanController(IProductService productService, NettoImporter nettoImporter, IShopService shopService, FotexImporter fotexImporter, CoopImporter coopImporter)
        {
            _productService = productService;
            _nettoImporter = nettoImporter;
            _shopService = shopService;
            _fotexImporter = fotexImporter;
            _coopImporter = coopImporter;
        }

        [HttpGet("import")]
        public async Task<IActionResult> Importer(string query = "")
        {
            try
            {
            
            var nettoHits = await _nettoImporter.CallNetto(query);

            foreach (var item in nettoHits)
            {
                if(string.IsNullOrEmpty(item.Gtin?.ToString()))
                    continue;

                var product = await _productService.GetProduct(item.Gtin?.ToString());
                if (product != null && product.ImageMimeType != null && product.PriceEntries.Where(x => x.ShopId == 1 && x.CreatedAt == DateTime.UtcNow) != null)
                    continue;

                if(product == null)
                {
                        var unitMeasure = string.Empty;
                        if(item.UnitsOfMeasure != null && item.Units != null)
                        {
                            unitMeasure = item?.Units.ToString() + " " + item?.UnitsOfMeasure.ToString();
                        }
                        var isOrganic = item.SearchHierachy.Any(x => x.ToLower().Contains("øko"));

                        var add = await _productService.CreateProduct(item.Gtin, item.Name, item.Description, item.Brand, item.ProductType, unitMeasure, isOrganic: isOrganic);
                }

                if (item.Images != null && item.Images.Length > 0 && item.Images.FirstOrDefault() != null && product != null && product.ImageMimeType == null)
                {
                    var imageData = await _nettoImporter.DownloadImageAsync(item.Images!.FirstOrDefault()!.AbsoluteUri);
                    if (imageData.data != null)
                        await _productService.StoreImageForProduct(product.Id, imageData.data, imageData.mimeType);
                }
                var price = await _productService.AddPriceForProduct(item.Gtin, int.Parse(item.StoreData.The7701.Price.ToString()), "Netto");

            }

                var fotexHits = await _fotexImporter.CallFotex(query);

                foreach (var item in fotexHits)
                {
                    try
                    {

                    if (string.IsNullOrEmpty(item.Gtin?.ToString()))
                        continue;

                    var product = await _productService.GetProduct(item.Gtin?.ToString());
                    if (product == null)
                    {
                        var unitMeasure = string.Empty;
                        if (item.UnitsOfMeasure != null && item.Units != null)
                        {
                            unitMeasure = item?.Units.ToString() + " " + item?.UnitsOfMeasure.ToString();
                        }
                        
                        product = await _productService.CreateProduct(item.Gtin, item.Name, item.Description, item.Brand, item.ProductType, unitMeasure);
                    }
                    
                   

                    var price = await _productService.AddPriceForProduct(item.Gtin, int.Parse(item.SalesPrice.ToString()), "Føtex");
                    
                    var isEU = false;
                    if (item.Attributes.FirstOrDefault(z => z.AttributeName.ToLower().Contains("europæisk")) != null)
                    {
                        isEU = true;
                    }
                    await _productService.SetEU(item.Gtin, isEU);


                        var organic = false;
                    if (item.Attributes.Length > 0 && item.Attributes.Any(x => x.AttributeName.ToLower().Contains("Økomærket")) != null)
                    {
                            organic = true;
                            await _productService.SetOrganic(item.Gtin, organic);
                    } else {
                            await _productService.SetOrganic(item.Gtin, false);
                        }
                }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                     
                    }

                }


                  return Ok(query + " indhentet");
            }
            catch (Exception e)
            {

                throw;
            }
        }
        
        [HttpGet("CoopImporter")]
        public async Task<bool> CoopAnalyser()
        {
            var unscannedCodes = await _productService.GetUnsavedBarcodes();
            var products = await _productService.GetAllProducts();
            foreach (var item in unscannedCodes)
            {

                var coop = await _coopImporter.CallCoop(item.Gtid);
                var random = new Random();
                await Task.Delay(random.Next(1000, 5000));
                if (coop == null)
                {
                    continue;
                }
                if(products.FirstOrDefault(x => x.Gtid == item.Gtid) == null)
                {
                   var product = await _productService.CreateProduct(item.Gtid, coop.DisplayName, coop.DisplayName +" " + coop.Category, null, coop.Category, null, isEu: false, isOrganic: false);
                }

                await _productService.AddPriceForProduct(item.Gtid, (int)Math.Round(coop.SalePrice * 100), "Coop");
                await _productService.DeleteBarcode(item.Gtid);
            }
            foreach (var item in products.Where(x => x.PriceEntries.Any(z => z.ShopId == 3)))
            {

                var coop = await _coopImporter.CallCoop(item.Gtid);
                var random = new Random();
                await Task.Delay(random.Next(1000, 5000));
                if (coop == null)
                {
                    continue;
                }
                await _productService.AddPriceForProduct(item.Gtid, (int)Math.Round(coop.SalePrice * 100), "Coop");
            }

            return await Task.FromResult(true);
        } 

        [HttpGet("GetProductByGtin/{gtid}")]
        public async Task<IActionResult> GetProductByGtin([FromRoute]string gtid)
        {
            Infrastructure.Models.Product product = await _productService.GetProduct(gtid);
            if(product == null)
            {
                return NotFound("Product not found");
            }
            var latestEntry = product.PriceEntries.OrderByDescending(x => x.CreatedAt).FirstOrDefault();
            if(latestEntry == null)
            {
                await Importer(product.Name);
                latestEntry = product.PriceEntries.OrderByDescending(x => x.CreatedAt).FirstOrDefault();
                if(latestEntry == null)
                {
                return NoContent();
                }
            }
            var bestPrice = product.PriceEntries
                .Where(x => x.CreatedAt == latestEntry.CreatedAt)
                .OrderBy(x => x.Price)
                .FirstOrDefault();

            var AveragePrice = product.PriceEntries
                .Where(x => x.CreatedAt.Date == DateTime.UtcNow.Date)
                .Average(x => x.Price); 

            return Ok(new { product = product, BestCurrentPrice = bestPrice, AveragePrice = AveragePrice });
        }


        [HttpPost(Name = "Scan")]
        public async Task<IActionResult> Scan([FromBody] string barcode)
        {
            var (success, message, entity) = await _productService.SaveBarcodeAsync(barcode);

            if (!success)
            {
                if (message.Contains("exists"))
                    return Conflict(message);
                return BadRequest(message);
            }

            return Ok(entity);
        }

       
       

    }
}

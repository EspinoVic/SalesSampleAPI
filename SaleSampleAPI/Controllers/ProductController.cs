using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleSampleAPI.Models;
using SaleSampleAPI.Services.interfaces;

namespace SaleSampleAPI.Controllers
{
    //Controllers are the actions the user (consumer) can call. and can have some restrictions using authorization.
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;
        
        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet]
        public object Get(int id)
        {
            //call services to get produc
            Product product = _productService.GetProductById(id);
            return new { product };
        }

        [HttpPost]
        public bool Post(Product product)
        {

            return _productService.AddNewProduct(product);
        }

        [HttpPut]
        public bool Put(int id, int inventoryAmount)
        {
            return _productService.UpdateInventoryProduct(id, inventoryAmount) == 1;
        }

    }
}

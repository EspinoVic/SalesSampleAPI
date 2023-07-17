using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleSampleAPI.Models;
using SaleSampleAPI.Services;

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
        public Product Get(int id)
        {
            //call services to get produc

            return _productService.GetProductById(id);
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

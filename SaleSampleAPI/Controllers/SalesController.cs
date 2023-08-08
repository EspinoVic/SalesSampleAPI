using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SaleSampleAPI.Models;
using SaleSampleAPI.Services.interfaces;
using System.Reflection.Metadata.Ecma335;

namespace SaleSampleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {

        private ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            this._salesService = salesService;
        }

        [HttpPost]
        public bool Post(int regionId,  Dictionary<int, int> products)
        {
            bool result = this._salesService.CreateSale(regionId, products);
            
            return result;
        }

        [HttpGet]
        [Route("api/[controller]/Total")]
        public float Total(int saleId)//return total sale
        {
           /* if(saleId < 1) //there won't neve be an id < 1
            {
                throw new ArgumentOutOfRangeException(nameof(saleId));
            }*/

            float total = (float) this._salesService.TotalSale(saleId);
            return total; 
        }


        [HttpGet]
        [Route("api/[controller]/Detail")]
        public List<Tuple<Product, int>>  Detail(int saleId)//return products
        {
            if (saleId < 1) throw new ArgumentException("Argumento no válido");
            List<Tuple<Product, int>> products = this._salesService.DetailSale(saleId);

            return products;
        }

    }
}

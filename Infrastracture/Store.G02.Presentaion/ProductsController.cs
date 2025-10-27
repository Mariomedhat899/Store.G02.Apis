using Microsoft.AspNetCore.Mvc;
using Store.G02.Services.Abstractions;
using Store.G02.Services.Abstractions.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentaion
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _Service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetallProduts()
        {
            var result = await _Service.ProductService.GetAllProductsAsync();
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);

        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetProdutById(int? Id)
        {
            if (Id is null) return BadRequest();
            var result = await _Service.ProductService.GetProductByIdAsync(Id.Value);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);

        }
        [HttpGet("brands")]
        public async Task<IActionResult> GetallBrands()
        {
            var result = await _Service.ProductService.GetAllBrandsAsync();
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);

        }

        [HttpGet("types")]
        public async Task<IActionResult> GetallProdutsTypoes()
        {
            var result = await _Service.ProductService.GetAllTypesAsync();
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);

        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.G02.Services.Abstractions;
using Store.G02.Services.Abstractions.Product;
using Store.G02.Shared;
using Store.G02.Shared.Dtos.Product;
using Store.G02.Shared.ErrorModles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.G02.Presentaion.Attr;
using Microsoft.AspNetCore.Authorization;

namespace Store.G02.Presentaion
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IServiceManager _Service) : ControllerBase
    {
        [HttpGet]

        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(PaginationResponse<ProductResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [CacheAttibute(120)]
        [Authorize]
        public async Task<ActionResult<PaginationResponse<ProductResponse>>> GetallProduts([FromQuery] ProductParams parameters)
        {



            var result = await _Service.ProductService.GetAllProductsAsync(parameters);
           

            return Ok(result);

        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]

        public async Task<ActionResult<ProductResponse>> GetProdutById(int? Id)
        {
            if (Id is null) return BadRequest();
            var result = await _Service.ProductService.GetProductByIdAsync(Id.Value);
            

            return Ok(result);

        }
        [HttpGet("brands")]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]

        public async Task<ActionResult<BrandTypeResponse>> GetallBrands()
        {
            var result = await _Service.ProductService.GetAllBrandsAsync();
            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);

        }

        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandTypeResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<BrandTypeResponse>> GetallProdutsTypoes()
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

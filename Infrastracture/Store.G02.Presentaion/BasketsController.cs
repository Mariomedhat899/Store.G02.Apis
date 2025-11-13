using Microsoft.AspNetCore.Mvc;
using Store.G02.Services.Abstractions;
using Store.G02.Shared.Dtos.Baskets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentaion
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController(IServiceManager _ServiceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> getBasketById(string Id)
        {
            var Result = await _ServiceManager.basketService.GetBasketByIdAsync(Id);

            return Ok(Result);
        }

        [HttpPost]

        public async Task<IActionResult> CreateOrUpdateBasket(BasketDto dto)
        {
            var Result = await _ServiceManager.basketService.CrateOrUpdateBasketByIdAsync(dto,TimeSpan.FromDays(1));

            return Ok(Result);

        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasketById(string Id)
        {

            var Result = await _ServiceManager.basketService.DelBasketAsync(Id);

            return NoContent();

        }

    }
}

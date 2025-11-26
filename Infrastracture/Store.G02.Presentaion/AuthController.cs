using Microsoft.AspNetCore.Mvc;
using Store.G02.Services.Abstractions;
using Store.G02.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Presentaion
{
    [ApiController]

    [Route("api/[controller]")]
    public class AuthController(IServiceManager _serviceManager) :ControllerBase
    {
        [HttpPost("login")]
       public async Task<IActionResult> Login(LogInRequest request)
        {
            var result = await _serviceManager.AuthService.LogInAsync(request);

            return Ok(result);

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _serviceManager.AuthService.RegisterAsync(request);

            return Ok(result);

        }
    }
}

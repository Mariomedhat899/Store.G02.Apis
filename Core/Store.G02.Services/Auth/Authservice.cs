using Microsoft.AspNetCore.Identity;
using Store.G02.Domain.Entity.Identity;
using Store.G02.Domain.Exceptions.BadRequest;
using Store.G02.Domain.Exceptions.NotFound;
using Store.G02.Domain.Exceptions.UnAuthorized;
using Store.G02.Services.Abstractions.Auth;
using Store.G02.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Auth
{
    public class Authservice(UserManager<AppUser> _userManager) : IAuthService
    {
        public async Task<UserResponse?> LogInAsync(LogInRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if ( user is null ) throw new UserNotFoundExeption(request.Email);

           var flag = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!flag) throw new UnAuthorizedExeption();

            return new UserResponse
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "dummy-token"
            };
           

            }

        public async Task<UserResponse?> RegisterAsync(RegisterRequest request)
        {
            var user = new AppUser()
            {
                UserName = request.UserName,
                DisplayName = request.DisplayName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber

            };

           var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new RegisterationBadRequestExeption(result.Errors.Select(e => e.Description).ToList());


            return new UserResponse
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "dummy-token"
            };

        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Store.G02.Domain.Entity.Identity;
using Store.G02.Domain.Exceptions.BadRequest;
using Store.G02.Domain.Exceptions.NotFound;
using Store.G02.Domain.Exceptions.UnAuthorized;
using Store.G02.Services.Abstractions.Auth;
using Store.G02.Shared;
using Store.G02.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Auth
{
    public class Authservice(UserManager<AppUser> _userManager,IOptions<JwtOptions> options) : IAuthService
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
                Token = await GenterateTokenAsync(user)
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
                Token = await GenterateTokenAsync(user)
            };

        }



        private async Task<string> GenterateTokenAsync(AppUser user)
        {

                var authClaims = new List<Claim>() 
            {
                new Claim(ClaimTypes.GivenName ,user.DisplayName),
                new Claim(ClaimTypes.Email ,user.Email),
            
            
            };


            var jwtOptions = options.Value;

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: authClaims,
                expires: DateTime.Now.AddDays(jwtOptions.expires),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)


                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

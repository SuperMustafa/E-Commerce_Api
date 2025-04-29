using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Entities.Identity;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction.Authentecation;
using Shared.Dtos;
using Shared.Dtos.Order;
using Shared.Dtos.Sequrity;

namespace Services.Authentication
{
    public class AuthenticationService(UserManager<User> userManager,IOptions<JwtOptionsDto> options,IMapper mapper) : IAuthenticationService
    {
        public async Task<ReturnedUserInfoDto> LoginAsync(UserLoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Email);
            if (user == null) { throw new UnAuthorizedException("InCorrect Email");}
            var result = await userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result) { throw new UnAuthorizedException("InCorrect Password"); }
            return new ReturnedUserInfoDto(user.DisplayName, user.Email, await CreateTokenAsync(user));
        }

        public async Task<ReturnedUserInfoDto> RegiesterAsync(UserRegisterDto regiesterDto)
        {
            var User = new User()
            {
                DisplayName = regiesterDto.DisplayName,
                Email = regiesterDto.Email,
                PhoneNumber = regiesterDto.PhoneNumber,
                UserName = regiesterDto.UserName,

            };
            var result = await userManager.CreateAsync(User, regiesterDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(E => E.Description).ToList();
                throw new RegisterValidationException(errors);
            }

            return new ReturnedUserInfoDto(User.DisplayName, User.Email, await CreateTokenAsync(User));
        }
        public async Task<string> CreateTokenAsync(User user)
        {
            var JwtOptions = options.Value;
            
            //private claims
            var AuthClaims = new List<Claim> { new Claim(ClaimTypes.Name,user.UserName!),
                                             new Claim(ClaimTypes.Email,user.Email!)};

            //add roles to claims if exist
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            //secret key 
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SecretKey)); // For Key
            var SigningCredintials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            // Token
            var Token = new JwtSecurityToken(
                audience: JwtOptions.Audience,
                issuer: JwtOptions.Issure,
                expires: DateTime.UtcNow.AddDays(JwtOptions.DurationInDays),
                claims: AuthClaims,
                signingCredentials: SigningCredintials
            );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

        public async Task<ReturnedUserInfoDto> GetUserByEmail(string email)
        {
           var user = await userManager.FindByEmailAsync(email)??throw new UserNotFoundexception(email);
            return new ReturnedUserInfoDto(user.DisplayName,user.Email,await CreateTokenAsync(user));
        }

        public async Task<bool> CheckIfEmailExist(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return user != null;
        }

        public async Task<ShipingAddressDto> UpdateUserAddress(ShipingAddressDto addressDto, string email)
        {
            var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);
            if (user.Address is not null)
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
                user.Address.Street = addressDto.Street;
            }
            else
            {
                var address = mapper.Map<Address>(addressDto);
                user.Address = address;
            }
            await userManager.UpdateAsync(user);
            return mapper.Map<ShipingAddressDto>(user.Address);
           
        }

        public async Task<ShipingAddressDto> GetUserAddress(string email)
        {
            var user = await userManager.Users.Include(u=>u.Address).FirstOrDefaultAsync(u=>u.Email==email)??
               throw new UserNotFoundexception(email) ;
            return mapper.Map<ShipingAddressDto>(user.Address);
        }
    }
}
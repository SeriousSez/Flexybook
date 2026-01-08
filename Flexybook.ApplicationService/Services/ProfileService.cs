using Flexybook.ApplicationService.JwtFeatures;
using Flexybook.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Flexybook.ApplicationService.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly JwtHandler _jwtHandler;

        private static readonly string Password = "Flexybook1234";

        public ProfileService(UserManager<UserEntity> userManager, JwtHandler jwtHandler)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }

        public async Task<string?> LoginAsync()
        {

            var user = await _userManager.FindByNameAsync("");
            if (user == null)
                user = await _userManager.FindByEmailAsync("");

            if (user == null)
                return null;

            if (!await _userManager.IsEmailConfirmedAsync(user))
                return null;

            if (!await _userManager.CheckPasswordAsync(user, Password))
                return null;

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = await _jwtHandler.GetClaims(user);

            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims, true);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return token;
        }
    }
}

using System.Security.Claims;
using System.Security.Cryptography;
using Malv.Data.EF.Entity;

namespace Malv.Services.Auth.Refresh
{
    public class RefreshService : IRefreshService
    {
        private readonly JwtSettings _jwtSettings;

        public RefreshService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public async Task<bool> ValidateRefreshToken(string refreshToken, string jwtToken)
        {
            if (refreshToken == null)
                throw new InvalidOperationException("/refresh: Invalid refresh token");

            MalvUser user = null; //await _userRepository.FindUserWithUserDataByRefreshToken(refreshToken);
            if (user == null)
                throw new InvalidOperationException("/refresh: Invalid refresh token");

            if (string.IsNullOrEmpty(jwtToken))
                throw new InvalidOperationException("/refresh: Invalid jwt token");

            //This will validate the jwt token.
            ClaimsPrincipal principal = JwtHelpers.GetPrincipalFromExpiredToken(jwtToken, _jwtSettings);

            return true;
        }
    }
}
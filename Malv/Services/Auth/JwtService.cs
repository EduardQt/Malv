using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Malv.Data.EF.Entity;
using Malv.Models.AuthController;
using Malv.Services.Auth.Refresh;
using Microsoft.IdentityModel.Tokens;

namespace Malv.Services.Auth;

public class JwtService : IJwtService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IRefreshService _refreshService;

    public JwtService(JwtSettings jwtSettings, IRefreshService refreshService)
    {
        _jwtSettings = jwtSettings;
        _refreshService = refreshService;
    }

    public UserToken_Res GenerateUserToken(MalvUser user)
    {
        UserToken_Res userTokenRes = new UserToken_Res();

        DateTime expireTime = DateTime.UtcNow.AddDays(1);
        userTokenRes.ValidTo = expireTime;

        var claims = GetClaims(user);
        var jwtToken = new JwtSecurityToken(issuer: _jwtSettings.ValidIssuer, audience: _jwtSettings.ValidAudience,
            claims: claims, notBefore: new DateTimeOffset(DateTime.Now).DateTime,
            expires: new DateTimeOffset(expireTime).DateTime,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.IssuerSigningKey)),
                SecurityAlgorithms.HmacSha256));

        userTokenRes.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        userTokenRes.RefreshToken = _refreshService.GenerateRefreshToken();
        userTokenRes.Username = user.FullName;

        return userTokenRes;
    }

    private IList<Claim> GetClaims(MalvUser atrastiUser)
    {
        IList<Claim> claims = new List<Claim>
        {
            new(JwtClaims.Id, atrastiUser.Id.ToString()),
            new(JwtClaims.NameIdentifier, atrastiUser.Id.ToString()),
            new(JwtClaims.Name, atrastiUser.Id.ToString()),
            new(JwtClaims.Email, atrastiUser.Email),
            new(JwtClaims.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt")),
        };

        return claims;
    }
}
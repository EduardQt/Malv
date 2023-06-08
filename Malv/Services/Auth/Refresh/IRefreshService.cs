namespace Malv.Services.Auth.Refresh
{
    public interface IRefreshService
    {
        string GenerateRefreshToken();

        Task<bool> ValidateRefreshToken(string refreshToken, string jwtToken);
    }
}
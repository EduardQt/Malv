using Malv.Data.EF.Entity;

namespace Malv.Services.Auth.Security;

public interface ITwoFactorService
{
    Task SendMailVerification(MalvUser malvUser);

    Task ValidateEmailVerification(string token);
}
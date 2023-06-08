using Malv.Data.EF.Entity;
using Malv.Models.AuthController;

namespace Malv.Services.Auth;

public interface IJwtService
{
    UserToken_Res GenerateUserToken(MalvUser user);
}
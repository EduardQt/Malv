using Malv.Data.EF.Entity;

namespace Malv.Data.Repository;

public interface IAuthRepository
{
    Task<bool> EmailExists(string email);

    Task<MalvUser> FindUserByRefreshToken(string refreshToken);

    Task UpdateUser(MalvUser malvUser);
    
    Task UpdateUserData(UserData userData);
}
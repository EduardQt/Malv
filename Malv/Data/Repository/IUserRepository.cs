using Malv.Data.EF.Entity;

namespace Malv.Data.Repository;

public interface IUserRepository
{
    Task<MalvUser> FindUserById(int id);
    
    Task<MalvUser> FindUserByEmail(string email);

    Task CreateUser(MalvUser malvUser);

    Task UpdateUser(MalvUser malvUser);
}
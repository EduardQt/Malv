using Malv.Data.EF.Entity;
using Malv.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Malv.Data.EF.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AuthRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<bool> EmailExists(string email)
    {
        return _dbContext.Users.AnyAsync(a => a.Email == email);
    }

    public Task<MalvUser> FindUserByRefreshToken(string refreshToken)
    {
        return _dbContext.Users
            .Include(i => i.UserData)
            .SingleOrDefaultAsync(s => s.UserData.RefreshToken == refreshToken);
    }

    public async Task UpdateUser(MalvUser malvUser)
    {
        _dbContext.Users.Update(malvUser);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateUserData(UserData userData)
    {
        _dbContext.UserDatas.Update(userData);
        await _dbContext.SaveChangesAsync();
    }
}
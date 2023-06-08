using Malv.Data.EF.Entity;
using Malv.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Malv.Data.EF.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _applicationDbContext;

    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public Task<MalvUser> FindUserById(int id)
    {
        return _applicationDbContext.Users
            .Include(i => i.UserData)
            .SingleOrDefaultAsync(s => s.Id == id);
    }

    public Task<MalvUser> FindUserByEmail(string email)
    {
        return _applicationDbContext.Users
            .Include(i => i.UserData)
            .SingleOrDefaultAsync(s => s.Email == email);
    }

    public async Task CreateUser(MalvUser malvUser)
    {
        await _applicationDbContext.Users.AddAsync(malvUser);
        await _applicationDbContext.SaveChangesAsync();
    }

    public async Task UpdateUser(MalvUser malvUser)
    {
        _applicationDbContext.Users.Update(malvUser);
        await _applicationDbContext.SaveChangesAsync();
    }
}
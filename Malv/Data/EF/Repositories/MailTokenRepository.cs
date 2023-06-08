using Malv.Data.EF.Entity;
using Malv.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Malv.Data.EF.Repositories;

public class MailTokenRepository : IMailTokenRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MailTokenRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddToken(MailToken mailToken)
    {
        await _dbContext.MailTokens.AddAsync(mailToken);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<MailToken> FindMailToken(string token, MailToken.MailTokenType type)
    {
        return await _dbContext.MailTokens
            .Include(i => i.User)
            .SingleOrDefaultAsync(s => s.Token == token && s.TokenType == type);
    }

    public async Task RemoveToken(MailToken mailToken)
    {
        _dbContext.MailTokens.Remove(mailToken);
        await _dbContext.SaveChangesAsync();
    }
}
using Malv.Data.EF.Entity;
using Malv.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace Malv.Data.EF.Repositories;

public class ChatRepository : IChatRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ChatRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<Chat>> FindUserChats(int userId)
    {
        var myAdIds = await _dbContext.Ads.Where(w => w.UserId == userId).Select(s => s.Id).ToListAsync();

        return await _dbContext.Chats
            .Include(i => i.Ad)
            .Where(w => myAdIds.Contains(w.AdId) || w.UserId == userId).ToListAsync();
    }

    public async Task<Chat> FindChat(int chatId)
    {
        return await _dbContext.Chats
            .Include(i => i.Ad)
            .SingleOrDefaultAsync(s => s.Id == chatId);
    }

    public async Task<IList<ChatMessage>> FindChatMessages(int chatId)
    {
        return await _dbContext.ChatMessages
            .Include(i => i.User)
            .Where(w => w.ChatId == chatId)
            .OrderBy(o => o.Id)
            .ToListAsync();
    }

    public async Task SaveChatMessage(ChatMessage chatMessage)
    {
        await _dbContext.ChatMessages.AddAsync(chatMessage);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateChatMessages(IEnumerable<ChatMessage> messages)
    {
        _dbContext.ChatMessages.UpdateRange(messages);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> UnreadMessages(int userId)
    {
        var myChats = await FindUserChatIds(userId);
        return await _dbContext.ChatMessages
            .Where(w => myChats.Contains(w.ChatId))
            .CountAsync(c => !c.IsRead && c.UserId != userId);
    }

    private async Task<IList<int>> FindUserChatIds(int userId)
    {
        var myAdIds = await _dbContext.Ads.Where(w => w.UserId == userId).Select(s => s.Id).ToListAsync();

        return await _dbContext.Chats
            .Include(i => i.Ad)
            .Where(w => myAdIds.Contains(w.AdId) || w.UserId == userId)
            .Select(s => s.Id)
            .ToListAsync();
    }
}
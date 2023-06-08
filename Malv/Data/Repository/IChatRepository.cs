using Malv.Data.EF.Entity;

namespace Malv.Data.Repository;

public interface IChatRepository
{
    public Task<IList<Chat>> FindUserChats(int userId);

    Task<Chat> FindChat(int chatId);

    Task<IList<ChatMessage>> FindChatMessages(int chatId);

    Task SaveChatMessage(ChatMessage chatMessage);

    Task UpdateChatMessages(IEnumerable<ChatMessage> messages);

    Task<int> UnreadMessages(int userId);
}
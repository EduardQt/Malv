using Malv.Data.EF.Entity;
using Malv.Models.ChatController;

namespace Malv.Controllers.Mappers;

public static class ChatMapper
{
    public static ICollection<Chat_Mod> MapChats(this IEnumerable<Chat> chats) =>
        chats.Select(s => s.MapChat()).ToList();

    public static Chat_Mod MapChat(this Chat chat)
    {
        return new Chat_Mod
        {
            Id = chat.Id,
            Name = chat.Ad.Title
        };
    }

    public static ICollection<ChatMessage_Mod> MapChatMessages(this IEnumerable<ChatMessage> chatMessages, int userId) =>
        chatMessages.Select(s => s.MapChatMessage(userId)).ToList();

    public static ChatMessage_Mod MapChatMessage(this ChatMessage chatMessage, int userId)
    {
        return new ChatMessage_Mod
        {
            Author = chatMessage.User.FullName,
            Message = chatMessage.Content,
            IsSelf = chatMessage.UserId == userId,
            SentDate = chatMessage.SentDate
        };
    }
}
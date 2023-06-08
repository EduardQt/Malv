using Malv.Controllers.Exceptions;
using Malv.Controllers.Helpers;
using Malv.Controllers.Mappers;
using Malv.Data.EF.Entity;
using Malv.Data.Repository;
using Malv.Filters;
using Malv.Models;
using Malv.Models.ChatController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Malv.Controllers;

public class ChatController : Controller
{
    private readonly IChatRepository _chatRepository;
    private readonly IAdRepository _adRepository;
    private readonly IUserRepository _userRepository;

    public ChatController(IChatRepository chatRepository, IAdRepository adRepository, IUserRepository userRepository)
    {
        _chatRepository = chatRepository;
        _adRepository = adRepository;
        _userRepository = userRepository;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> UnreadMessages()
    {
        int unread = await _chatRepository.UnreadMessages(User.GetUserId());

        return Ok(new Chat_UnreadMessages_Res()
        {
            Count = unread
        });
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> MyChats()
    {
        var chats = await _chatRepository.FindUserChats(User.GetUserId());

        return Ok(chats.MapChats());
    }

    [HttpPost]
    [Authorize]
    [Transaction]
    public async Task<IActionResult> Get([FromBody] Chat_Get_Req req)
    {
        var chat = await ValidateChat(req.ChatId);

        var allMessages = await _chatRepository.FindChatMessages(chat.Id);
        var unread = allMessages.Where(w => !w.IsRead && w.UserId != User.GetUserId()).ToList();
        if (unread.Count > 0)
        {
            unread.ForEach(f => f.IsRead = true);
            await _chatRepository.UpdateChatMessages(unread);
        }
        
        chat.ChatMessages = allMessages;

        return Ok(new Chat_Get_Res
        {
            Chat = chat.MapChat(),
            ChatMessages = chat.ChatMessages.MapChatMessages(User.GetUserId())
        });
    }
    
    [HttpPost]
    [Authorize]
    [Transaction]
    public async Task<IActionResult> Send([FromBody] Chat_SendChat_Req req)
    {
        if (string.IsNullOrWhiteSpace(req.Content))
            throw new AccessMalvException(new Error_Res().AddError("INVALID_CHAT_CONTENT",
                "Chat content can't be empty!"));

        var user = await _userRepository.FindUserById(User.GetUserId());
        var chat = await ValidateChat(req.ChatId);

        ChatMessage chatMessage = new ChatMessage()
        {
            ChatId = chat.Id,
            UserId = User.GetUserId(),
            Content = req.Content,
            User = user,
            SentDate = DateTime.Now,
            IsRead = false
        };

        await _chatRepository.SaveChatMessage(chatMessage);
        
        return Ok(new Chat_SendChat_Res
        {
            ChatMessage = chatMessage.MapChatMessage(User.GetUserId())
        });
    }

    private async Task<Chat> ValidateChat(int chatId)
    {
        var chat = await _chatRepository.FindChat(chatId);
        var adIds = await _adRepository.FindAdIdsByUserId(User.GetUserId());

        if (chat == null || (!adIds.Contains(chat.AdId) && chat.UserId != User.GetUserId()))
            throw new AccessMalvException(new Error_Res().AddError("UNAUTHORIZED_CHAT",
                "You do not have access to this chat!"));

        return chat;
    }
}
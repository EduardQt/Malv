namespace Malv.Models.ChatController;

public class Chat_Get_Res
{
    public Chat_Mod Chat { get; set; }
    public ICollection<ChatMessage_Mod> ChatMessages { get; set; }
}
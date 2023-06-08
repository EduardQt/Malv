namespace Malv.Models.ChatController;

public class ChatMessage_Mod
{
    public string Author { get; set; }
    public string Message { get; set; }
    public bool IsSelf { get; set; }
    public DateTime SentDate { get; set; }
}
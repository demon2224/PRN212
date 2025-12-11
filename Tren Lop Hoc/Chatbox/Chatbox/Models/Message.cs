using System.Text.Json.Serialization;

namespace Chatbox.Models
{
    public class Message
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Content { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public MessageType Type { get; set; } = MessageType.User;
        public bool IsRead { get; set; } = false;
    }

    public enum MessageType
    {
        User,
        Bot,
        System,
        Notification
    }
}
using System.Text.Json.Serialization;
using Chatbox.Models;

namespace Chatbox.Models
{
    public class ChatSession
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastActivity { get; set; } = DateTime.Now;
        public List<Message> Messages { get; set; } = new List<Message>();
        public string CurrentUser { get; set; } = string.Empty;
        public ChatTheme Theme { get; set; } = ChatTheme.Default;
    }

    public enum ChatTheme
    {
        Default,
        Dark,
        Light,
        Colorful
    }
}
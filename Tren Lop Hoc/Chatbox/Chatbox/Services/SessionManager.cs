using System.Text.Json;
using Chatbox.Models;

namespace Chatbox.Services
{
    public class SessionManager
    {
        private readonly string _sessionsDirectory;
        private readonly string _currentSessionFile;

        public SessionManager()
        {
            _sessionsDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Chatbox", "Sessions");
            _currentSessionFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Chatbox", "current_session.json");
            
            Directory.CreateDirectory(_sessionsDirectory);
            Directory.CreateDirectory(Path.GetDirectoryName(_currentSessionFile)!);
        }

        public async Task<ChatSession> CreateNewSession(string userName, string sessionName = "")
        {
            var session = new ChatSession
            {
                Name = string.IsNullOrEmpty(sessionName) ? $"Chat {DateTime.Now:dd/MM/yyyy HH:mm}" : sessionName,
                CurrentUser = userName,
                CreatedAt = DateTime.Now,
                LastActivity = DateTime.Now
            };

            // Thêm tin nh?n chào m?ng
            session.Messages.Add(new Message
            {
                Content = $"?? Chào m?ng {userName} ??n v?i phiên chat m?i!",
                Author = "System",
                Type = MessageType.System,
                Timestamp = DateTime.Now
            });

            await SaveSession(session);
            await SaveCurrentSession(session.Id);
            return session;
        }

        public async Task SaveSession(ChatSession session)
        {
            session.LastActivity = DateTime.Now;
            var filePath = Path.Combine(_sessionsDirectory, $"{session.Id}.json");
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(session, options);
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task<ChatSession?> LoadSession(string sessionId)
        {
            var filePath = Path.Combine(_sessionsDirectory, $"{sessionId}.json");
            if (!File.Exists(filePath)) return null;

            try
            {
                var json = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<ChatSession>(json);
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ChatSession>> GetAllSessions()
        {
            var sessions = new List<ChatSession>();
            var files = Directory.GetFiles(_sessionsDirectory, "*.json");

            foreach (var file in files)
            {
                try
                {
                    var json = await File.ReadAllTextAsync(file);
                    var session = JsonSerializer.Deserialize<ChatSession>(json);
                    if (session != null)
                    {
                        sessions.Add(session);
                    }
                }
                catch
                {
                    // B? qua file b? l?i
                }
            }

            return sessions.OrderByDescending(s => s.LastActivity).ToList();
        }

        public async Task<string?> GetCurrentSessionId()
        {
            if (!File.Exists(_currentSessionFile)) return null;

            try
            {
                return await File.ReadAllTextAsync(_currentSessionFile);
            }
            catch
            {
                return null;
            }
        }

        public async Task SaveCurrentSession(string sessionId)
        {
            await File.WriteAllTextAsync(_currentSessionFile, sessionId);
        }

        public async Task DeleteSession(string sessionId)
        {
            var filePath = Path.Combine(_sessionsDirectory, $"{sessionId}.json");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // N?u ?ang xóa session hi?n t?i, xóa luôn current session
            var currentId = await GetCurrentSessionId();
            if (currentId == sessionId && File.Exists(_currentSessionFile))
            {
                File.Delete(_currentSessionFile);
            }
        }
    }
}
using Chatbox.Models;
using Chatbox.Services;
using Chatbox.Templates;
using Chatbox.UI;

namespace Chatbox
{
    internal class Program
    {
        private static SessionManager _sessionManager = new();
        private static ChatBot _chatBot = new();
        private static ChatSession? _currentSession;
        private static string _currentUser = "";

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding = System.Text.Encoding.UTF8;

            try
            {
                await InitializeApplication();
                await RunChatApplication();
            }
            catch (Exception ex)
            {
                ConsoleUI.ShowError($"Đã xảy ra lỗi: {ex.Message}");
                ConsoleUI.WaitForKeyPress();
            }
        }

        private static async Task InitializeApplication()
        {
            ConsoleUI.ShowInfo("Khởi động ChatBox...");
            
            // Lấy tên người dùng
            Console.Write("Nhập tên của bạn: ");
            _currentUser = Console.ReadLine()?.Trim() ?? "Người dùng";
            
            if (string.IsNullOrEmpty(_currentUser))
                _currentUser = "Người dùng";

            // Kiểm tra session hiện tại
            var currentSessionId = await _sessionManager.GetCurrentSessionId();
            if (!string.IsNullOrEmpty(currentSessionId))
            {
                _currentSession = await _sessionManager.LoadSession(currentSessionId);
            }

            // Nếu không có session hoặc load thất bại, tạo mới
            if (_currentSession == null)
            {
                _currentSession = await _sessionManager.CreateNewSession(_currentUser);
                
                // Thêm tin nhắn chào mừng từ bot
                var welcomeMessage = _chatBot.GetWelcomeMessage(_currentUser);
                _currentSession.Messages.Add(welcomeMessage);
                await _sessionManager.SaveSession(_currentSession);
            }
            else
            {
                // Cập nhật user nếu khác
                if (_currentSession.CurrentUser != _currentUser)
                {
                    _currentSession.CurrentUser = _currentUser;
                    await _sessionManager.SaveSession(_currentSession);
                }
            }
        }

        private static async Task RunChatApplication()
        {
            bool running = true;

            while (running)
            {
                try
                {
                    DisplayCurrentChat();
                    
                    ConsoleUI.ShowInputPrompt(_currentUser);
                    string? input = Console.ReadLine()?.Trim();

                    if (string.IsNullOrEmpty(input))
                        continue;

                    if (input.StartsWith("/"))
                    {
                        running = await HandleCommand(input);
                    }
                    else
                    {
                        await HandleUserMessage(input);
                    }
                }
                catch (Exception ex)
                {
                    ConsoleUI.ShowError($"Lỗi: {ex.Message}");
                    ConsoleUI.WaitForKeyPress();
                }
            }
        }

        private static void DisplayCurrentChat()
        {
            if (_currentSession == null) return;

            ConsoleUI.ShowHeader(_currentSession.Name, _currentUser);

            var recentMessages = _currentSession.Messages.TakeLast(10).ToList();
            
            foreach (var message in recentMessages)
            {
                bool isCurrentUser = message.Author == _currentUser;
                ConsoleUI.DisplayMessage(message, isCurrentUser);
            }
        }

        private static async Task<bool> HandleCommand(string command)
        {
            var parts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var cmd = parts[0].ToLower();

            switch (cmd)
            {
                case "/exit":
                case "/quit":
                    ConsoleUI.ShowInfo("Tạm biệt! Cảm ơn bạn đã sử dụng ChatBox! 👋");
                    await Task.Delay(1000);
                    return false;

                case "/help":
                    ConsoleUI.ShowHelp();
                    ConsoleUI.WaitForKeyPress();
                    break;

                case "/clear":
                    Console.Clear();
                    break;

                case "/templates":
                    await HandleTemplatesCommand();
                    break;

                case "/sessions":
                    await HandleSessionsCommand();
                    break;

                case "/theme":
                    await HandleThemeCommand(parts);
                    break;

                case "/new":
                    await CreateNewSession();
                    break;

                default:
                    ConsoleUI.ShowError($"Lệnh không hợp lệ: {command}");
                    ConsoleUI.ShowInfo("Gõ /help để xem danh sách lệnh.");
                    ConsoleUI.WaitForKeyPress();
                    break;
            }

            return true;
        }

        private static async Task HandleUserMessage(string message)
        {
            if (_currentSession == null) return;

            // Thêm tin nhắn của user
            var userMessage = new Message
            {
                Content = message,
                Author = _currentUser,
                Type = MessageType.User,
                Timestamp = DateTime.Now
            };

            _currentSession.Messages.Add(userMessage);

            // Tạo phản hồi từ bot
            var botResponse = _chatBot.GenerateResponse(message, _currentUser);
            _currentSession.Messages.Add(botResponse);

            // Lưu session
            await _sessionManager.SaveSession(_currentSession);

            // Hiển thị tin nhắn mới
            Console.WriteLine();
            ConsoleUI.DisplayMessage(userMessage, true);
            ConsoleUI.DisplayMessage(botResponse, false);
        }

        private static async Task HandleTemplatesCommand()
        {
            var categories = ChatTemplates.GetAllCategories();
            bool inTemplatesMenu = true;

            while (inTemplatesMenu)
            {
                Console.Clear();
                ConsoleUI.DisplayTemplateMenu(categories);
                
                Console.Write("Chọn danh mục (0 để quay lại): ");
                var input = Console.ReadLine()?.Trim();

                if (input == "0" || string.IsNullOrEmpty(input))
                {
                    inTemplatesMenu = false;
                    continue;
                }

                if (int.TryParse(input, out int choice) && choice > 0 && choice <= categories.Length)
                {
                    var selectedCategory = categories[choice - 1];
                    await ShowTemplatesByCategory(selectedCategory);
                }
                else
                {
                    ConsoleUI.ShowError("Lựa chọn không hợp lệ!");
                    ConsoleUI.WaitForKeyPress();
                }
            }
        }

        private static async Task ShowTemplatesByCategory(string category)
        {
            var templates = ChatTemplates.GetTemplatesByCategory(category);
            bool inCategoryMenu = true;

            while (inCategoryMenu)
            {
                Console.Clear();
                ConsoleUI.DisplayTemplates(category, templates);
                
                Console.Write("Chọn mẫu tin nhắn (0 để quay lại): ");
                var input = Console.ReadLine()?.Trim();

                if (input == "0" || string.IsNullOrEmpty(input))
                {
                    inCategoryMenu = false;
                    continue;
                }

                if (int.TryParse(input, out int choice) && choice > 0 && choice <= templates.Length)
                {
                    var selectedTemplate = templates[choice - 1];
                    await HandleUserMessage(selectedTemplate);
                    inCategoryMenu = false;
                }
                else
                {
                    ConsoleUI.ShowError("Lựa chọn không hợp lệ!");
                    ConsoleUI.WaitForKeyPress();
                }
            }
        }

        private static async Task HandleSessionsCommand()
        {
            var sessions = await _sessionManager.GetAllSessions();
            bool inSessionMenu = true;

            while (inSessionMenu)
            {
                Console.Clear();
                ConsoleUI.DisplaySessionList(sessions);
                
                Console.Write("Lựa chọn của bạn: ");
                var input = Console.ReadLine()?.Trim()?.ToLower();

                if (input == "q" || string.IsNullOrEmpty(input))
                {
                    inSessionMenu = false;
                }
                else if (input == "n")
                {
                    await CreateNewSession();
                    inSessionMenu = false;
                }
                else if (int.TryParse(input, out int choice) && choice > 0 && choice <= sessions.Count)
                {
                    var selectedSession = sessions[choice - 1];
                    _currentSession = selectedSession;
                    await _sessionManager.SaveCurrentSession(selectedSession.Id);
                    ConsoleUI.ShowSuccess($"Đã chuyển sang phiên chat: {selectedSession.Name}");
                    ConsoleUI.WaitForKeyPress();
                    inSessionMenu = false;
                }
                else
                {
                    ConsoleUI.ShowError("Lựa chọn không hợp lệ!");
                    ConsoleUI.WaitForKeyPress();
                }
            }
        }

        private static async Task CreateNewSession()
        {
            Console.Write("Nhập tên cho phiên chat mới (để trống sẽ tự đặt tên): ");
            var sessionName = Console.ReadLine()?.Trim();

            _currentSession = await _sessionManager.CreateNewSession(_currentUser, sessionName);
            
            // Thêm tin nhắn chào mừng từ bot
            var welcomeMessage = _chatBot.GetWelcomeMessage(_currentUser);
            _currentSession.Messages.Add(welcomeMessage);
            await _sessionManager.SaveSession(_currentSession);

            ConsoleUI.ShowSuccess("Đã tạo phiên chat mới!");
            ConsoleUI.WaitForKeyPress();
        }

        private static async Task HandleThemeCommand(string[] parts)
        {
            if (parts.Length < 2)
            {
                ConsoleUI.ShowInfo("Các theme có sẵn: default, dark, light, colorful");
                ConsoleUI.ShowInfo("Sử dụng: /theme <tên_theme>");
                ConsoleUI.WaitForKeyPress();
                return;
            }

            var themeName = parts[1].ToLower();
            ChatTheme theme = themeName switch
            {
                "dark" => ChatTheme.Dark,
                "light" => ChatTheme.Light,
                "colorful" => ChatTheme.Colorful,
                "default" => ChatTheme.Default,
                _ => ChatTheme.Default
            };

            ConsoleUI.SetTheme(theme);
            
            if (_currentSession != null)
            {
                _currentSession.Theme = theme;
                await _sessionManager.SaveSession(_currentSession);
            }

            ConsoleUI.ShowSuccess($"Đã thay đổi theme thành: {themeName}");
            ConsoleUI.WaitForKeyPress();
        }
    }
}

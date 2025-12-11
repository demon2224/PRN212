using Chatbox.Models;

namespace Chatbox.Services
{
    public class ChatBot
    {
        private readonly Random _random = new();
        private readonly Dictionary<string, string[]> _responses = new()
        {
            ["chào"] = new[]
            {
                "Xin chào! Tôi là ChatBot, r?t vui ???c trò chuy?n v?i b?n! ??",
                "Chào b?n! Hôm nay b?n th? nào? ??",
                "Hi! Có gì tôi có th? giúp b?n không? ??"
            },
            ["t?m bi?t"] = new[]
            {
                "T?m bi?t! H?n g?p l?i b?n sau nhé! ??",
                "Bye bye! Chúc b?n ngày t?t lành! ??",
                "See you! Có gì c?n h? tr? hãy quay l?i nhé! ??"
            },
            ["c?m ?n"] = new[]
            {
                "Không có gì! Tôi luôn s?n sàng giúp ?? b?n! ??",
                "R?t vui khi ???c giúp b?n! ??",
                "?ó là ni?m vui c?a tôi! ??"
            },
            ["th?i ti?t"] = new[]
            {
                "Tôi không th? ki?m tra th?i ti?t, nh?ng hy v?ng hôm nay là ngày ??p tr?i! ??",
                "Th?i ti?t nh? th? nào v?y b?n? Tôi thích nghe b?n k?! ???",
                "Dù th?i ti?t ra sao, tâm tr?ng t?t là quan tr?ng nh?t! ??"
            },
            ["âm nh?c"] = new[]
            {
                "Âm nh?c th?t tuy?t v?i! B?n thích th? lo?i nh?c gì? ??",
                "Tôi thích m?i lo?i nh?c! Có bài nào hay b?n mu?n chia s? không? ??",
                "Nh?c có th? ch?a lành tâm h?n ph?i không? ??"
            },
            ["game"] = new[]
            {
                "Game r?t thú v?! B?n thích ch?i game gì? ??",
                "Tôi không th? ch?i game nh?ng r?t thích nghe b?n k? v? chúng! ???",
                "Gaming là m?t hobby tuy?t v?i ?? gi?i trí! ??"
            }
        };

        public Message GenerateResponse(string userMessage, string userName)
        {
            var lowerMessage = userMessage.ToLower();
            string response;

            // Tìm ph?n h?i phù h?p
            var matchedKey = _responses.Keys.FirstOrDefault(key => lowerMessage.Contains(key));
            
            if (matchedKey != null)
            {
                var responses = _responses[matchedKey];
                response = responses[_random.Next(responses.Length)];
            }
            else if (lowerMessage.Contains("?"))
            {
                response = GetQuestionResponse(userMessage);
            }
            else
            {
                response = GetGeneralResponse(userName);
            }

            return new Message
            {
                Content = response,
                Author = "ChatBot",
                Type = MessageType.Bot,
                Timestamp = DateTime.Now
            };
        }

        private string GetQuestionResponse(string question)
        {
            var questionResponses = new[]
            {
                "?ó là m?t câu h?i hay! Tôi ngh? r?ng... ??",
                "Hmm, ?? tôi suy ngh? v? ?i?u này... ??",
                "Thú v?! B?n có th? chia s? thêm v? v?n ?? này không? ??",
                "Tôi không ch?c ch?n, nh?ng theo tôi ngh?... ?????",
                "Câu h?i hay! B?n ngh? sao v? v?n ?? này? ??"
            };

            return questionResponses[_random.Next(questionResponses.Length)];
        }

        private string GetGeneralResponse(string userName)
        {
            var generalResponses = new[]
            {
                $"Th?t thú v?, {userName}! K? cho tôi nghe thêm v? ?i?u này nhé! ??",
                "?, tôi hi?u r?i! Còn gì khác b?n mu?n chia s? không? ??",
                "Nghe có v? hay ??y! B?n c?m th?y th? nào v? ?i?u này? ??",
                "C?m ?n b?n ?ã chia s?! Tôi r?t thích nghe nh?ng câu chuy?n c?a b?n! ??",
                "Tuy?t v?i! Còn ?i?u gì khác b?n mu?n nói không? ??"
            };

            return generalResponses[_random.Next(generalResponses.Length)];
        }

        public Message GetWelcomeMessage(string userName)
        {
            var welcomeMessages = new[]
            {
                $"Chào m?ng {userName}! Tôi là ChatBot, ng??i b?n ?o c?a b?n! ???",
                $"Xin chào {userName}! R?t vui ???c g?p b?n hôm nay! ????",
                $"Hi {userName}! Hãy cùng trò chuy?n nhé! Tôi s?n sàng l?ng nghe b?n! ????"
            };

            return new Message
            {
                Content = welcomeMessages[_random.Next(welcomeMessages.Length)],
                Author = "ChatBot",
                Type = MessageType.Bot,
                Timestamp = DateTime.Now
            };
        }
    }
}
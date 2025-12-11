namespace Chatbox.Templates
{
    public static class ChatTemplates
    {
        public static readonly Dictionary<string, string[]> Templates = new()
        {
            ["Chào h?i"] = new[]
            {
                "Xin chào! B?n kh?e không?",
                "Chào b?n! Hôm nay th? nào?",
                "Hi! R?t vui ???c g?p b?n!",
                "Chào bu?i sáng! Chúc b?n ngày m?i t?t lành!"
            },
            ["C?m xúc"] = new[]
            {
                "Tôi c?m th?y r?t vui hôm nay! ??",
                "Hôm nay h?i bu?n m?t chút... ??",
                "Tôi r?t hào h?ng v? ?i?u này! ??",
                "C?m ?n b?n r?t nhi?u! ??"
            },
            ["Công vi?c"] = new[]
            {
                "Công vi?c hôm nay khá b?n r?n",
                "D? án ?ang ti?n tri?n t?t",
                "C?n h? tr? v?i v?n ?? này",
                "?ã hoàn thành task ???c giao"
            },
            ["Gi?i trí"] = new[]
            {
                "Có phim hay nào ?? xem không?",
                "Cu?i tu?n ?i ?âu ch?i nh??",
                "Game này r?t thú v?!",
                "Nh?c này hay quá!"
            },
            ["Th?i ti?t"] = new[]
            {
                "Hôm nay tr?i ??p quá!",
                "Tr?i m?a to th? này...",
                "N?ng chang chang, nóng quá!",
                "Gió mát, th?i ti?t d? ch?u"
            },
            ["?n u?ng"] = new[]
            {
                "Hôm nay ?n gì nh??",
                "Quán này ngon l?m!",
                "?ói b?ng quá r?i...",
                "Món này trông ngon ghê!"
            }
        };

        public static string[] GetRandomTemplates(int count = 3)
        {
            var random = new Random();
            var allTemplates = Templates.Values.SelectMany(x => x).ToList();
            return allTemplates.OrderBy(x => random.Next()).Take(count).ToArray();
        }

        public static string[] GetTemplatesByCategory(string category)
        {
            return Templates.ContainsKey(category) ? Templates[category] : Array.Empty<string>();
        }

        public static string[] GetAllCategories()
        {
            return Templates.Keys.ToArray();
        }
    }
}
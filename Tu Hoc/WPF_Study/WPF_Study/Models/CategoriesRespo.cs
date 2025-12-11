using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Study.Models
{
    public class CategoriesRespo
    {
        private readonly BookManagementDbContext _context;

        public CategoriesRespo()  // Khởi tạo _context trong constructor
        {
            _context = new BookManagementDbContext();
        }

        public List<BookCategory> GetBookCategories()
        {
            return _context.BookCategories.ToList();
        }

        public void Add(BookCategory bookCategory)
        {
            _context.BookCategories.Add(bookCategory);
            _context.SaveChanges();
        }

        public void Update(BookCategory bookCategory)
        {
            _context.BookCategories.Update(bookCategory);
            _context.SaveChanges();
        }

        public void Delete(BookCategory bookCategory)
        {
            _context.BookCategories.Remove(bookCategory);
            _context.SaveChanges();
        }
    }
}

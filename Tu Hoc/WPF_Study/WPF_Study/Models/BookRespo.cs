using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WPF_Study.Models
{
    public class BookRespo
    {
        private readonly BookManagementDbContext _context;

        public BookRespo()  // Khởi tạo _context trong constructor
        {
            _context = new BookManagementDbContext();
        }

        public List<Book> Books()
        {
            return _context.Books.Include(b => b.BookCategory).ToList();
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void Delete(Book book)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}

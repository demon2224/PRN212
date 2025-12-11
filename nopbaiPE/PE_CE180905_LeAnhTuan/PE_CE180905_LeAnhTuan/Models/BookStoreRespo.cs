using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PE_CE180905_LeAnhTuan.Models
{
    public class BookStoreRespo
    {
        private readonly BookStoreContext _context;

        public BookStoreRespo()  // Khởi tạo _context trong constructor
        {
            _context = new BookStoreContext();
        }

        public List<BookStore> GetAllBook()
        {
            return _context.BookStores.ToList();
        }

        public void Add(BookStore bookStore)
        {
            _context.BookStores.Add(bookStore);
            _context.SaveChanges();
        }

        public void Update(BookStore bookStore)
        {
            _context.BookStores.Update(bookStore);
            _context.SaveChanges();
        }

        public void Delete(BookStore bookStore)
        {
            _context.BookStores.Remove(bookStore);
            _context.SaveChanges();
        }

    }
}

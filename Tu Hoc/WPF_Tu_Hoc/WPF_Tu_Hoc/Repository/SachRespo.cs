using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Tu_Hoc.Entities;

namespace WPF_Tu_Hoc.Repository
{
    public class SachRespo
    {
        TuHocContext _context;

        public List<Sach> saches()
        {
            _context = new();
            return _context.Saches.ToList();
        }

        public void Add(Sach sach)
        {
            _context = new();
            _context.Saches.Add(sach);
            _context.SaveChanges();
        }

        public void Update(Sach sach)
        {
            _context = new();
            _context.Saches.Update(sach);
            _context.SaveChanges();
        }

        public void Delete(Sach sach)
        {
            _context = new();
            _context.Saches.Remove(sach);
            _context.SaveChanges();
        }
    }
}

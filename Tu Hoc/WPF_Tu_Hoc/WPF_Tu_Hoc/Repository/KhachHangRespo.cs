using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Tu_Hoc.Entities;

namespace WPF_Tu_Hoc.Repository
{
    class KhachHangRespo
    {
        private TuHocContext _context;

        public List<KhachHang> GetKhachHang()
        {
            _context = new();
            return _context.KhachHangs.ToList();
        }

        public void Add(KhachHang khach)
        {
            _context = new();
            _context.KhachHangs.Add(khach);
            _context.SaveChanges();
        }

        public void Delete(KhachHang khach)
        {
            _context = new();
            _context.KhachHangs.Remove(khach);
            _context.SaveChanges();
        }

        public void Update(KhachHang khach)
        {
            _context = new();
            _context.KhachHangs.Update(khach);
            _context.SaveChanges();
        }
    }
}

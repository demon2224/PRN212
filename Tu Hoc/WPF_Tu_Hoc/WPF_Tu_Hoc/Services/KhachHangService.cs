using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Tu_Hoc.Entities;
using WPF_Tu_Hoc.Repository;

namespace WPF_Tu_Hoc.Services
{
    class KhachHangService
    {
        private KhachHangRespo _repo = new KhachHangRespo();

        public List<KhachHang> GetAllKhachHang()
        {
            return _repo.GetKhachHang();
        }

        public void AddKhachHang(KhachHang khach)
        {
            _repo.Add(khach);
        }

        public void UpdateKhachHang(KhachHang khach)
        {
            _repo.Update(khach);
        }

        public void DeleteKhachHang(KhachHang khach)
        {
            _repo.Delete(khach);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Tu_Hoc.Entities;
using WPF_Tu_Hoc.Repository;

namespace WPF_Tu_Hoc.Services
{
    class SachService
    {
        SachRespo _repo = new SachRespo();

        public List<Sach> GetAllSach()
        {
            return _repo.saches();
        }

        public void AddSach(Sach sach)
        {
            _repo.Add(sach);
        }

        public void UpdateSach(Sach sach)
        {
            _repo.Update(sach);
        }

        public void DeleteSach(Sach sach)
        {
            _repo.Delete(sach);
        }
    }
}

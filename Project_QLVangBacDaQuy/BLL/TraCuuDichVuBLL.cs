using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_QLVangBacDaQuy.DAL;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.BLL
{
    class TraCuuDichVuBLL
    {
        private TraCuuDichVuDAL dal = new TraCuuDichVuDAL();

        public List<PhieuDichVuDTO> TimKiemPhieuDichVu(DateTime? tuNgay, DateTime? denNgay, string tuKhoa, string tinhTrang)
        {
            return dal.SearchPhieuDichVu(tuNgay, denNgay, tuKhoa, tinhTrang);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_QLVangBacDaQuy.DAL;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.BLL
{
    class TraCuuSanPhamBLL
    {
        private TraCuuSanPhamDAL dal = new TraCuuSanPhamDAL();

        public List<LoaiSanPhamDTO> LayDanhSachLoaiSP()
        {
            return dal.GetListLoaiSP();
        }

        public List<SanPhamDTO> TimKiemSanPham(string tuKhoa, int maLoaiSP)
        {
            return dal.SearchSanPham(tuKhoa, maLoaiSP);
        }
    }
}

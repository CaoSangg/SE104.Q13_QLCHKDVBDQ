using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_QLVangBacDaQuy.DAL;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.BLL
{
    class MuaHangBLL
    {
        private MuaHangDAL dal = new MuaHangDAL();

        public List<NhaCungCapDTO> LayDanhSachNhaCungCap()
        {
            return dal.GetListNhaCungCap();
        }

        public List<LoaiSanPhamDTO> LayDanhSachLoaiSP()
        {
            return dal.GetListLoaiSP();
        }

        public List<SanPhamDTO> LayDanhSachSanPham(int maLoai = 0)
        {
            return dal.GetListSanPham(maLoai);
        }

        // Hàm Nhập Kho 
        public bool NhapKho(DateTime ngayLap, int maNCC, List<CartItem> gioHang, decimal tongTien, out string message)
        {
            message = "";
            try
            {
                int maPhieu = dal.InsertPhieuMua(ngayLap, maNCC, tongTien);
                if (maPhieu <= 0)
                {
                    message = "Lỗi không tạo được phiếu nhập!";
                    return false;
                }

                foreach (var item in gioHang)
                {
                    dal.InsertChiTietMua(maPhieu, item, item.DonGiaBan);

                    dal.CapNhatKhoVaGia(item.MaSP, item.SoLuong, item.DonGiaBan);
                }

                message = "Nhập kho thành công!";
                return true;
            }
            catch (Exception ex)
            {
                message = "Lỗi hệ thống: " + ex.Message;
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_QLVangBacDaQuy.DAL;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.BLL
{
    class BanHangBLL
    {
        private BanHangDAL dal = new BanHangDAL();

        public List<SanPhamDTO> LayDanhSachSanPham()
        {
            return dal.GetListSanPham();
        }

        // Hàm xử lý thanh toán
        public bool ThanhToan(string tenKH, string sdt, string diaChi, DateTime ngayLap, List<CartItem> gioHang, decimal tongTien, out string message)
        {
            message = "";
            try
            {
                int maKH = dal.InsertKhachHang(tenKH, sdt, diaChi);

                int maPhieu = dal.InsertPhieuBan(ngayLap, maKH, tongTien);
                if (maPhieu <= 0)
                {
                    message = "Lỗi không tạo được phiếu bán hàng!";
                    return false;
                }

                foreach (var item in gioHang)
                {
                    dal.InsertChiTietBan(maPhieu, item);
                    dal.TruTonKho(item.MaSP, item.SoLuong);
                }

                message = "Thanh toán thành công!";
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

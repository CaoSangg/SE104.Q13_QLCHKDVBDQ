using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_QLVangBacDaQuy.DAL;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.BLL
{
    internal class ThanhToanDichVuBLL
    {
        private ThanhToanDichVuDAL dal = new ThanhToanDichVuDAL();

        public List<PhieuDichVuDTO> LayPhieuChuaHoanThanh()
        {
            return dal.GetPhieuChuaHoanThanh();
        }

        public List<ChiTietThanhToanDTO> LayChiTietPhieu(int maPhieu)
        {
            return dal.GetChiTietPhieu(maPhieu);
        }

        public bool ThanhToanMon(int maPhieu, int maLoaiDV, out string msg)
        {
            try
            {
                if (dal.ThanhToanTungMon(maPhieu, maLoaiDV))
                {
                    dal.KiemTraHoanThanhPhieu(maPhieu);
                    msg = "Thanh toán thành công món này!";
                    return true;
                }
                else
                {
                    msg = "Lỗi cập nhật dữ liệu.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                msg = "Lỗi hệ thống: " + ex.Message;
                return false;
            }
        }
        public bool ThanhToanTatCa(int maPhieu, out string msg)
        {
            if (dal.ThanhToanTatCa(maPhieu))
            {
                msg = "Đã thanh toán hoàn tất toàn bộ phiếu!";
                return true;
            }
            else
            {
                msg = "Lỗi khi thanh toán tất cả.";
                return false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_QLVangBacDaQuy.DAL;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.BLL
{
    class DichVuBLL
    {
        private DichVuDAL dal = new DichVuDAL();

        public List<LoaiDichVuDTO> LayDanhSachLoaiDichVu()
        {
            return dal.GetListLoaiDichVu();
        }

        // Lấy quy định trả trước 
        public double LayTyLeTraTruoc()
        {
            return dal.GetTyLeTraTruoc();
        }

        // Luu phiếu dịch vụ
        public bool LuuPhieuDichVu(string tenKH, string sdt, string diaChi, DateTime ngayLap,
                                   List<ServiceItem> listDichVu, decimal tongTien, decimal tongTraTruoc, decimal tongConLai,
                                   out string message)
        {
            message = "";
            try
            {
                int maKH = dal.InsertKhachHang(tenKH, sdt, diaChi);

                int maPhieu = dal.InsertPhieuDichVu(ngayLap, maKH, tongTien, tongTraTruoc, tongConLai);
                if (maPhieu <= 0)
                {
                    message = "Lỗi hệ thống: Không tạo được phiếu dịch vụ!";
                    return false;
                }

                foreach (var item in listDichVu)
                {
                    dal.InsertChiTietDichVu(maPhieu, item);
                }

                message = "Lập phiếu dịch vụ thành công!";
                return true;
            }
            catch (Exception ex)
            {
                message = "Lỗi: " + ex.Message;
                return false;
            }
        }

        // Tự động kiểm tra và hủy phiếu quá hạn
        public void TuDongKiemTraHuyPhieu()
        {
            try
            {
                string queryGetDay = "SELECT GiaTri FROM THAMSO WHERE TenThamSo = 'ThoiHanHuyPhieu'";
                object result = Project_QLVangBacDaQuy.DAL.DataProvider.Instance.ExecuteScalar(queryGetDay);

                if (result != null)
                {
                    int soNgay = Convert.ToInt32(result);
                    if (soNgay > 0)
                    {
                        dal.HuyPhieuQuaHan(soNgay);
                    }
                }
            }
            catch {}
        }
    }
}

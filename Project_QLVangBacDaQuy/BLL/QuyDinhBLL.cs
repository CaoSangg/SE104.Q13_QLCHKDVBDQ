using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_QLVangBacDaQuy.DAL;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.BLL
{
    class QuyDinhBLL
    {
        private QuyDinhDAL dal = new QuyDinhDAL();

        // Lấy danh sách và dịch tên
        public List<QuyDinhDTO> LayDanhSachQuyDinh()
        {
            var list = dal.GetListQuyDinh();

            foreach (var item in list)
            {
                item.TenHienThi = DichTenSangTiengViet(item.TenThamSo);
            }
            return list;
        }

        // Dịch tên tham số sang tiếng Việt
        private string DichTenSangTiengViet(string tenGoc)
        {
            switch (tenGoc)
            {
                case "TyLeTraTruoc": return "Tỷ Lệ Trả Trước";
                case "ThoiHanHuyPhieu": return "Thời Hạn Hủy Phiếu Dịch Vụ (Ngày)";
                default: return tenGoc;
            }
        }

        // Cập nhật
        public bool CapNhatQuyDinh(List<QuyDinhDTO> listQuyDinh, out string message)
        {
            try
            {
                foreach (var item in listQuyDinh)
                {
                    dal.UpdateQuyDinh(item.TenThamSo, item.GiaTri);
                }
                message = "Cập nhật quy định thành công!";
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

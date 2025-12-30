using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_QLVangBacDaQuy.DAL;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.BLL
{
    internal class QuanLyDanhMucBLL
    {
        private QuanLyDanhMuc dal = new QuanLyDanhMuc();

        public List<NhaCungCapDTO> GetListNCC() => dal.GetListNCC();
        public string AddNCC(string ten, string dc, string sdt)
        {
            if (string.IsNullOrWhiteSpace(ten)) return "Tên NCC không được để trống";
            return dal.InsertNCC(ten, dc, sdt) ? "Thêm thành công" : "Lỗi thêm NCC";
        }
        public string EditNCC(int ma, string ten, string dc, string sdt)
        {
            return dal.UpdateNCC(ma, ten, dc, sdt) ? "Sửa thành công" : "Lỗi sửa NCC";
        }
        public string DeleteNCC(int ma)
        {
            try { return dal.DeleteNCC(ma) ? "Xóa thành công" : "Lỗi xóa NCC"; }
            catch { return "Không thể xóa NCC này vì đã có dữ liệu giao dịch!"; }
        }

        public List<LoaiSanPhamDTO> GetListLoaiSP() => dal.GetListLoaiSP();
        public string AddLoaiSP(string ten, string loiNhuanStr, string donvi)
        {
            if (string.IsNullOrWhiteSpace(ten)) return "Tên loại không được trống";
            if (!double.TryParse(loiNhuanStr, out double loi)) return "Lợi nhuận phải là số (VD: 0.1)";
            return dal.InsertLoaiSP(ten, loi, donvi) ? "Thêm thành công" : "Lỗi thêm Loại SP";
        }
        public string EditLoaiSP(int ma, string ten, string loiNhuanStr, string donvi)
        {
            if (!double.TryParse(loiNhuanStr, out double loi)) return "Lợi nhuận phải là số";
            return dal.UpdateLoaiSP(ma, ten, loi, donvi) ? "Sửa thành công" : "Lỗi sửa Loại SP";
        }
        public string DeleteLoaiSP(int ma)
        {
            try { return dal.DeleteLoaiSP(ma) ? "Xóa thành công" : "Lỗi xóa"; }
            catch { return "Đang có sản phẩm thuộc loại này, không thể xóa!"; }
        }

        public List<LoaiDichVuDTO> GetListLoaiDV() => dal.GetListLoaiDV();
        public string AddLoaiDV(string ten, string giaStr)
        {
            if (string.IsNullOrWhiteSpace(ten)) return "Tên DV không được trống";
            if (!decimal.TryParse(giaStr, out decimal gia)) return "Đơn giá không hợp lệ";
            return dal.InsertLoaiDV(ten, gia) ? "Thêm thành công" : "Lỗi thêm Loại DV";
        }
        public string EditLoaiDV(int ma, string ten, string giaStr)
        {
            if (!decimal.TryParse(giaStr, out decimal gia)) return "Đơn giá không hợp lệ";
            return dal.UpdateLoaiDV(ma, ten, gia) ? "Sửa thành công" : "Lỗi sửa";
        }
        public string DeleteLoaiDV(int ma)
        {
            try { return dal.DeleteLoaiDV(ma) ? "Xóa thành công" : "Lỗi xóa"; }
            catch { return "Đã có phiếu sử dụng dịch vụ này, không thể xóa!"; }
        }
    }
}

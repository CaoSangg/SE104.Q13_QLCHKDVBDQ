using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_QLVangBacDaQuy.DAL;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.BLL
{
    class NhanVienBLL
    {
        private NhanVienDAL dal = new NhanVienDAL();

        public NhanVienDTO DangNhap(string username, string password)
        {
            // Kiểm tra rỗng trước khi gọi xuống Database 
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            return dal.CheckLogin(username, password);
        }
        public bool TaoTaiKhoanMoi(string user, string pass, string name, string role, out string message)
        {
            message = "";
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass) || string.IsNullOrWhiteSpace(name))
            {
                message = "Vui lòng nhập đầy đủ thông tin!";
                return false;
            }

            if (dal.KiemTraTonTai(user))
            {
                message = "Tên đăng nhập đã tồn tại! Vui lòng chọn tên khác.";
                return false;
            }

            NhanVienDTO nv = new NhanVienDTO()
            {
                TenDangNhap = user,
                MatKhau = pass, 
                HoTen = name,
                QuyenHan = role
            };

            if (dal.ThemNhanVien(nv))
            {
                message = "Tạo tài khoản thành công!";
                return true;
            }
            else
            {
                message = "Lỗi hệ thống: Không thể thêm tài khoản.";
                return false;
            }
        }
        public bool XoaNhanVien(string tenDangNhap, out string error)
        {
            error = "";
            if (tenDangNhap.ToLower() == "admin")
            {
                error = "Không thể xóa tài khoản Quản trị gốc!";
                return false;
            }

            if (dal.XoaNhanVien(tenDangNhap))
            {
                return true;
            }
            else
            {
                error = "Lỗi khi xóa nhân viên này.";
                return false;
            }
        }
        public List<NhanVienDTO> LayDanhSachNhanVien()
        {
            return dal.GetListNhanVien();
        }
    }
}

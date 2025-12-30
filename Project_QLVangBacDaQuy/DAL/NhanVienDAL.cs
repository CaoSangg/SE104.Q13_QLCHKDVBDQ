using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.DAL
{
    class NhanVienDAL
    {
        public NhanVienDTO CheckLogin(string username, string password)
        {
            string query = "SELECT * FROM NHANVIEN WHERE TenDangNhap = @user AND MatKhau = @pass";

            object[] param = new object[] { username, password };

            DataTable data = DataProvider.Instance.ExecuteQuery(query, param);

            if (data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                return new NhanVienDTO()
                {
                    TenDangNhap = row["TenDangNhap"].ToString(),
                    MatKhau = row["MatKhau"].ToString(),
                    HoTen = row["HoTen"].ToString(),
                    QuyenHan = row["QuyenHan"].ToString()
                };
            }

            return null;
        }
        public bool KiemTraTonTai(string username)
        {
            string query = "SELECT COUNT(*) FROM NHANVIEN WHERE TenDangNhap = @user";
            object result = DataProvider.Instance.ExecuteScalar(query, new object[] { username });
            return Convert.ToInt32(result) > 0;
        }

        public bool ThemNhanVien(NhanVienDTO nv)
        {
            string query = "INSERT INTO NHANVIEN (TenDangNhap, MatKhau, HoTen, QuyenHan) VALUES (@user, @pass, @name, @role)";
            object[] param = new object[] { nv.TenDangNhap, nv.MatKhau, nv.HoTen, nv.QuyenHan };

            return DataProvider.Instance.ExecuteNonQuery(query, param) > 0;
        }
        public bool XoaNhanVien(string tenDangNhap)
        {
            if (tenDangNhap.ToLower() == "admin") return false;

            string query = "DELETE FROM NHANVIEN WHERE TenDangNhap = @user";
            object[] param = new object[] { tenDangNhap };

            return DataProvider.Instance.ExecuteNonQuery(query, param) > 0;
        }
        public List<NhanVienDTO> GetListNhanVien()
        {
            List<NhanVienDTO> list = new List<NhanVienDTO>();
            string query = "SELECT TenDangNhap, HoTen, QuyenHan FROM NHANVIEN";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                NhanVienDTO nv = new NhanVienDTO();
                nv.TenDangNhap = item["TenDangNhap"].ToString();
                nv.HoTen = item["HoTen"].ToString();
                nv.QuyenHan = item["QuyenHan"].ToString();
                list.Add(nv);
            }
            return list;
        }
    }
}

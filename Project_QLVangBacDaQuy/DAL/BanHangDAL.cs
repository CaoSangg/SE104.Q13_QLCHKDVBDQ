using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.DAL
{
    class BanHangDAL
    {
        // Lấy danh sách sản phẩm
        public List<SanPhamDTO> GetListSanPham()
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();
            string query = @"SELECT S.MaSP, S.TenSP, S.DonGiaMua, S.SoLuongTon, L.DonViTinh, L.PhanTramLoiNhuan 
                             FROM SANPHAM S 
                             JOIN LOAISANPHAM L ON S.MaLoaiSP = L.MaLoaiSP";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                list.Add(new SanPhamDTO()
                {
                    MaSP = Convert.ToInt32(row["MaSP"]),
                    TenSP = row["TenSP"].ToString(),
                    DonGiaMua = Convert.ToDecimal(row["DonGiaMua"]),
                    SoLuongTon = Convert.ToInt32(row["SoLuongTon"]),
                    DonViTinh = row["DonViTinh"].ToString(),
                    PhanTramLoiNhuan = Convert.ToDouble(row["PhanTramLoiNhuan"])
                });
            }
            return list;
        }

        public int InsertKhachHang(string ten, string sdt, string diachi)
        {
            string query = @"INSERT INTO KHACHHANG(TenKH, SDT, DiaChi) VALUES (@ten, @sdt, @diachi); SELECT SCOPE_IDENTITY();";
            object[] param = new object[] { ten, sdt, diachi };
            return Convert.ToInt32(DataProvider.Instance.ExecuteScalar(query, param));
        }

        public int InsertPhieuBan(DateTime ngay, int maKH, decimal tongTien)
        {
            string query = @"INSERT INTO PHIEUBANHANG (NgayLap, MaKH, TongTien) VALUES (@ngay, @maKH, @tongTien); SELECT SCOPE_IDENTITY();";
            object[] param = new object[] { ngay, maKH, tongTien };
            return Convert.ToInt32(DataProvider.Instance.ExecuteScalar(query, param));
        }

        public void InsertChiTietBan(int maPhieu, CartItem item)
        {
            string query = @"INSERT INTO CT_PHIEUBAN (MaPhieuBan, MaSP, SoLuong, DonGiaBan, ThanhTien) 
                             VALUES (@maPhieu, @maSP, @sl, @donGia, @thanhTien)";
            object[] param = new object[] { maPhieu, item.MaSP, item.SoLuong, item.DonGiaBan, item.ThanhTien };
            DataProvider.Instance.ExecuteNonQuery(query, param);
        }

        // trừ tồn kho
        public void TruTonKho(int maSP, int soLuong)
        {
            string query = "UPDATE SANPHAM SET SoLuongTon = SoLuongTon - @sl WHERE MaSP = @maSP";
            object[] param = new object[] { soLuong, maSP };
            DataProvider.Instance.ExecuteNonQuery(query, param);
        }
    }
}

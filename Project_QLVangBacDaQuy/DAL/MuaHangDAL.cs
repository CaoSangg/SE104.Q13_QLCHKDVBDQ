using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.DAL
{
    class MuaHangDAL
    {
        public List<NhaCungCapDTO> GetListNhaCungCap()
        {
            List<NhaCungCapDTO> list = new List<NhaCungCapDTO>();
            string query = "SELECT * FROM NHACUNGCAP";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                list.Add(new NhaCungCapDTO()
                {
                    MaNCC = Convert.ToInt32(row["MaNCC"]),
                    TenNCC = row["TenNCC"].ToString(),
                    DiaChi = row["DiaChi"].ToString(),
                    SDT = row["SDT"].ToString()
                });
            }
            return list;
        }

        public List<LoaiSanPhamDTO> GetListLoaiSP()
        {
            List<LoaiSanPhamDTO> list = new List<LoaiSanPhamDTO>();
            string query = "SELECT * FROM LOAISANPHAM";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                list.Add(new LoaiSanPhamDTO()
                {
                    MaLoaiSP = Convert.ToInt32(row["MaLoaiSP"]),
                    TenLoaiSP = row["TenLoaiSP"].ToString()
                });
            }
            return list;
        }

        public List<SanPhamDTO> GetListSanPham(int maLoai)
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();
            string query = @"SELECT S.MaSP, S.TenSP, S.DonGiaMua, S.SoLuongTon, L.DonViTinh, L.PhanTramLoiNhuan 
                             FROM SANPHAM S 
                             JOIN LOAISANPHAM L ON S.MaLoaiSP = L.MaLoaiSP
                             WHERE 1=1";

            List<object> parameters = new List<object>();

            if (maLoai > 0)
            {
                query += " AND S.MaLoaiSP = @maLoai";
                parameters.Add(maLoai);
            }

            DataTable data = DataProvider.Instance.ExecuteQuery(query, parameters.ToArray());

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

        public int InsertPhieuMua(DateTime ngay, int maNCC, decimal tongTien)
        {
            string query = @"INSERT INTO PHIEUMUAHANG (NgayLap, MaNCC, TongTien) VALUES (@ngay, @maNCC, @tongTien); SELECT SCOPE_IDENTITY();";
            object[] param = new object[] { ngay, maNCC, tongTien };
            return Convert.ToInt32(DataProvider.Instance.ExecuteScalar(query, param));
        }

        public void InsertChiTietMua(int maPhieu, CartItem item, decimal donGiaNhap)
        {
            string query = @"INSERT INTO CT_PHIEUMUA (MaPhieuMua, MaSP, SoLuong, DonGiaMua, ThanhTien) 
                             VALUES (@maPhieu, @maSP, @sl, @donGia, @thanhTien)";

            object[] param = new object[] { maPhieu, item.MaSP, item.SoLuong, donGiaNhap, item.ThanhTien };
            DataProvider.Instance.ExecuteNonQuery(query, param);
        }

        public void CapNhatKhoVaGia(int maSP, int soLuongNhap, decimal giaNhapMoi)
        {
            string query = "UPDATE SANPHAM SET SoLuongTon = SoLuongTon + @sl, DonGiaMua = @donGia WHERE MaSP = @maSP";
            object[] param = new object[] { soLuongNhap, giaNhapMoi, maSP };
            DataProvider.Instance.ExecuteNonQuery(query, param);
        }
    }
}

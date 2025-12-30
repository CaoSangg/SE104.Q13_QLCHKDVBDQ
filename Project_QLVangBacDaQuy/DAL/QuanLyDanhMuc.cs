using Project_QLVangBacDaQuy.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_QLVangBacDaQuy.DAL
{
    public class QuanLyDanhMuc
    {
        public List<NhaCungCapDTO> GetListNCC()
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

        public bool InsertNCC(string ten, string diachi, string sdt)
        {
            string query = "INSERT INTO NHACUNGCAP(TenNCC, DiaChi, SDT) VALUES (@ten, @diachi, @sdt)";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { ten, diachi, sdt }) > 0;
        }

        public bool UpdateNCC(int ma, string ten, string diachi, string sdt)
        {
            string query = "UPDATE NHACUNGCAP SET TenNCC = @ten, DiaChi = @diachi, SDT = @sdt WHERE MaNCC = @ma";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { ten, diachi, sdt, ma }) > 0;
        }

        public bool DeleteNCC(int ma)
        {
            string query = "DELETE FROM NHACUNGCAP WHERE MaNCC = @ma";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { ma }) > 0;
        }

        public List<LoaiSanPhamDTO> GetListLoaiSP()
        {
            List<LoaiSanPhamDTO> list = new List<LoaiSanPhamDTO>();
            string query = "SELECT * FROM LOAISANPHAM";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                var loai = new LoaiSanPhamDTO();
                loai.MaLoaiSP = Convert.ToInt32(row["MaLoaiSP"]);
                loai.TenLoaiSP = row["TenLoaiSP"].ToString();

                if (row["PhanTramLoiNhuan"] != DBNull.Value)
                    loai.PhanTramLoiNhuan = Convert.ToDouble(row["PhanTramLoiNhuan"]);
                if (row["DonViTinh"] != DBNull.Value)
                    loai.DonViTinh = row["DonViTinh"].ToString();

                list.Add(loai);
            }
            return list;
        }

        public bool InsertLoaiSP(string ten, double loiNhuan, string donVi)
        {
            string query = "INSERT INTO LOAISANPHAM(TenLoaiSP, PhanTramLoiNhuan, DonViTinh) VALUES (@ten, @loi, @donVi)";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { ten, loiNhuan, donVi }) > 0;
        }

        public bool UpdateLoaiSP(int ma, string ten, double loiNhuan, string donVi)
        {
            string query = "UPDATE LOAISANPHAM SET TenLoaiSP = @ten, PhanTramLoiNhuan = @loi, DonViTinh = @donVi WHERE MaLoaiSP = @ma";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { ten, loiNhuan, donVi, ma }) > 0;
        }

        public bool DeleteLoaiSP(int ma)
        {
            string query = "DELETE FROM LOAISANPHAM WHERE MaLoaiSP = @ma";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { ma }) > 0;
        }

        public List<LoaiDichVuDTO> GetListLoaiDV()
        {
            List<LoaiDichVuDTO> list = new List<LoaiDichVuDTO>();
            string query = "SELECT * FROM LOAIDICHVU";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                list.Add(new LoaiDichVuDTO()
                {
                    MaLoaiDV = Convert.ToInt32(row["MaLoaiDV"]),
                    TenLoaiDV = row["TenLoaiDV"].ToString(),
                    DonGiaDV = Convert.ToDecimal(row["DonGiaDV"])
                });
            }
            return list;
        }

        public bool InsertLoaiDV(string ten, decimal dongia)
        {
            string query = "INSERT INTO LOAIDICHVU(TenLoaiDV, DonGiaDV) VALUES (@ten, @gia)";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { ten, dongia }) > 0;
        }

        public bool UpdateLoaiDV(int ma, string ten, decimal dongia)
        {
            string query = "UPDATE LOAIDICHVU SET TenLoaiDV = @ten, DonGiaDV = @gia WHERE MaLoaiDV = @ma";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { ten, dongia, ma }) > 0;
        }

        public bool DeleteLoaiDV(int ma)
        {
            string query = "DELETE FROM LOAIDICHVU WHERE MaLoaiDV = @ma";
            return DataProvider.Instance.ExecuteNonQuery(query, new object[] { ma }) > 0;
        }
    }
}

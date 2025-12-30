using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.DAL
{
    class TraCuuSanPhamDAL
    {
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

        public List<SanPhamDTO> SearchSanPham(string tuKhoa, int maLoaiSP)
        {
            List<SanPhamDTO> list = new List<SanPhamDTO>();
            string query = @"SELECT S.MaSP, S.TenSP, S.DonGiaMua, S.SoLuongTon, 
                                    L.TenLoaiSP, L.DonViTinh, L.PhanTramLoiNhuan
                             FROM SANPHAM S 
                             JOIN LOAISANPHAM L ON S.MaLoaiSP = L.MaLoaiSP
                             WHERE 1=1 ";

            List<object> parameters = new List<object>();

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query += " AND S.TenSP LIKE @ten ";
                parameters.Add("%" + tuKhoa + "%");
            }

            if (maLoaiSP > 0) 
            {
                query += " AND S.MaLoaiSP = @maLoai ";
                parameters.Add(maLoaiSP);
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
                    PhanTramLoiNhuan = Convert.ToDouble(row["PhanTramLoiNhuan"]),
                    TenLoaiSP = row["TenLoaiSP"].ToString() 
                });
            }

            return list;
        }
    }
}

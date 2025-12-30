using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.DAL
{
    class DichVuDAL
    {
        // Lấy danh sách Loại Dịch Vụ
        public List<LoaiDichVuDTO> GetListLoaiDichVu()
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

        public double GetTyLeTraTruoc()
        {
            string query = "SELECT GiaTri FROM THAMSO WHERE TenThamSo = 'TyLeTraTruoc'";
            object result = DataProvider.Instance.ExecuteScalar(query);
            return result != null ? Convert.ToDouble(result) : 0.5; // Mặc định 50% nếu không tìm thấy
        }

        public int InsertKhachHang(string ten, string sdt, string diachi)
        {
            string query = @"INSERT INTO KHACHHANG(TenKH, SDT, DiaChi) VALUES (@ten, @sdt, @diachi); SELECT SCOPE_IDENTITY();";
            object[] param = new object[] { ten, sdt, diachi };
            return Convert.ToInt32(DataProvider.Instance.ExecuteScalar(query, param));
        }

        public int InsertPhieuDichVu(DateTime ngay, int maKH, decimal tongTien, decimal traTruoc, decimal conLai)
        {
            string query = @"INSERT INTO PHIEUDICHVU (NgayLap, MaKH, TongTien, TongTraTruoc, TongConLai, TinhTrang) 
                             VALUES (@ngay, @maKH, @tongTien, @traTruoc, @conLai, N'Chưa hoàn thành'); 
                             SELECT SCOPE_IDENTITY();";
            object[] param = new object[] { ngay, maKH, tongTien, traTruoc, conLai };
            return Convert.ToInt32(DataProvider.Instance.ExecuteScalar(query, param));
        }

        public void InsertChiTietDichVu(int maPhieu, ServiceItem item)
        {
            string query = @"INSERT INTO CT_PHIEUDICHVU (MaPhieuDV, MaLoaiDV, ChiPhiRieng, DonGiaDuocTinh, SoLuong, ThanhTien, TraTruoc, ConLai, NgayGiao, TinhTrang) 
                             VALUES (@maPhieu, @maLoai, @cpRieng, @dgTinh, @sl, @thanhTien, @traTruoc, @conLai, @ngayGiao, N'Chưa giao')";

            object[] param = new object[] {
                maPhieu, item.MaLoaiDV, item.ChiPhiRieng, item.DonGiaDuocTinh,
                item.SoLuong, item.ThanhTien, item.TraTruoc, item.ConLai, item.NgayGiao
            };
            DataProvider.Instance.ExecuteNonQuery(query, param);
        }
        public int HuyPhieuQuaHan(int soNgayQuaHan)
        {
            string query = $@"UPDATE PHIEUDICHVU 
                      SET TinhTrang = N'Đã hủy' 
                      WHERE TinhTrang = N'Chưa hoàn thành' 
                      AND DATEDIFF(day, NgayLap, GETDATE()) > {soNgayQuaHan}";

            return DataProvider.Instance.ExecuteNonQuery(query);
        }
    }
}

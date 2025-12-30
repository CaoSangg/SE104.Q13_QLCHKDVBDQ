using Project_QLVangBacDaQuy.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_QLVangBacDaQuy.DAL
{
    internal class ThanhToanDichVuDAL
    {
        public List<PhieuDichVuDTO> GetPhieuChuaHoanThanh()
        {
            List<PhieuDichVuDTO> list = new List<PhieuDichVuDTO>();
            string query = @"SELECT P.*, K.TenKH, K.SDT 
                             FROM PHIEUDICHVU P 
                             JOIN KHACHHANG K ON P.MaKH = K.MaKH
                             WHERE P.TinhTrang = N'Chưa hoàn thành'";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                list.Add(new PhieuDichVuDTO()
                {
                    MaPhieuDV = Convert.ToInt32(row["MaPhieuDV"]),
                    NgayLap = Convert.ToDateTime(row["NgayLap"]),
                    TenKH = row["TenKH"].ToString(), 
                    SDT = row["SDT"].ToString(),
                    TongTien = Convert.ToDecimal(row["TongTien"]),
                    TongTraTruoc = Convert.ToDecimal(row["TongTraTruoc"]),
                    TongConLai = Convert.ToDecimal(row["TongConLai"]),
                    TinhTrang = row["TinhTrang"].ToString()
                });
            }
            return list;
        }

        public List<ChiTietThanhToanDTO> GetChiTietPhieu(int maPhieu)
        {
            List<ChiTietThanhToanDTO> list = new List<ChiTietThanhToanDTO>();
            string query = @"SELECT CT.*, L.TenLoaiDV 
                             FROM CT_PHIEUDICHVU CT 
                             JOIN LOAIDICHVU L ON CT.MaLoaiDV = L.MaLoaiDV 
                             WHERE CT.MaPhieuDV = " + maPhieu;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow row in data.Rows)
            {
                list.Add(new ChiTietThanhToanDTO()
                {
                    MaPhieuDV = Convert.ToInt32(row["MaPhieuDV"]),
                    MaLoaiDV = Convert.ToInt32(row["MaLoaiDV"]),
                    TenLoaiDV = row["TenLoaiDV"].ToString(),
                    DonGiaDuocTinh = Convert.ToDecimal(row["DonGiaDuocTinh"]),
                    SoLuong = Convert.ToInt32(row["SoLuong"]),
                    ThanhTien = Convert.ToDecimal(row["ThanhTien"]),
                    TraTruoc = Convert.ToDecimal(row["TraTruoc"]),
                    ConLai = Convert.ToDecimal(row["ConLai"]),
                    NgayGiao = Convert.ToDateTime(row["NgayGiao"]),
                    TinhTrang = row["TinhTrang"].ToString()
                });
            }
            return list;
        }

        public bool ThanhToanTungMon(int maPhieu, int maLoaiDV)
        {
            string queryCT = @"UPDATE CT_PHIEUDICHVU 
                               SET ConLai = 0, TinhTrang = N'Đã giao' 
                               WHERE MaPhieuDV = @mp AND MaLoaiDV = @ml";

            int result = DataProvider.Instance.ExecuteNonQuery(queryCT, new object[] { maPhieu, maLoaiDV });
            if (result <= 0) return false;

            string queryUpdateTong = @"UPDATE PHIEUDICHVU 
                                       SET TongConLai = (SELECT SUM(ConLai) FROM CT_PHIEUDICHVU WHERE MaPhieuDV = @mp),
                                           TongTraTruoc = TongTien - (SELECT SUM(ConLai) FROM CT_PHIEUDICHVU WHERE MaPhieuDV = @mp)
                                       WHERE MaPhieuDV = @mp";
            DataProvider.Instance.ExecuteNonQuery(queryUpdateTong, new object[] { maPhieu });

            return true;
        }

        public void KiemTraHoanThanhPhieu(int maPhieu)
        {
            string query = "SELECT COUNT(*) FROM CT_PHIEUDICHVU WHERE MaPhieuDV = @mp AND TinhTrang = N'Chưa giao'";
            int count = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(query, new object[] { maPhieu }));

            if (count == 0)
            {
                string update = "UPDATE PHIEUDICHVU SET TinhTrang = N'Đã hoàn thành' WHERE MaPhieuDV = @mp";
                DataProvider.Instance.ExecuteNonQuery(update, new object[] { maPhieu });
            }
        }
        public bool ThanhToanTatCa(int maPhieu)
        {
            try
            {
                string queryCT = @"UPDATE CT_PHIEUDICHVU 
                           SET ConLai = 0, TinhTrang = N'Đã giao' 
                           WHERE MaPhieuDV = @mp";
                DataProvider.Instance.ExecuteNonQuery(queryCT, new object[] { maPhieu });

                string queryPhieu = @"UPDATE PHIEUDICHVU 
                              SET TongConLai = 0, 
                                  TongTraTruoc = TongTien, 
                                  TinhTrang = N'Đã hoàn thành' 
                              WHERE MaPhieuDV = @mp";
                return DataProvider.Instance.ExecuteNonQuery(queryPhieu, new object[] { maPhieu }) > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}

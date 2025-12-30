using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.DAL
{
    class TraCuuDichVuDAL
    {
        public List<PhieuDichVuDTO> SearchPhieuDichVu(DateTime? tuNgay, DateTime? denNgay, string tuKhoa, string tinhTrang)
        {
            List<PhieuDichVuDTO> list = new List<PhieuDichVuDTO>();

            string query = @"SELECT 
                                P.MaPhieuDV, P.NgayLap, P.TongTien, P.TongTraTruoc, P.TongConLai, P.TinhTrang,
                                K.TenKH, K.SDT
                             FROM PHIEUDICHVU P
                             JOIN KHACHHANG K ON P.MaKH = K.MaKH
                             WHERE 1=1 ";

            List<object> parameters = new List<object>();

            if (tuNgay != null && denNgay != null)
            {
                query += " AND P.NgayLap >= @tuNgay ";
                parameters.Add(tuNgay.Value.Date);

                query += " AND P.NgayLap < @denNgay ";
                parameters.Add(denNgay.Value.Date.AddDays(1));
            }

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query += " AND (K.TenKH LIKE @tuKhoa1 OR K.SDT LIKE @tuKhoa2 OR CAST(P.MaPhieuDV AS VARCHAR) LIKE @tuKhoa3) ";
                string valueTuKhoa = "%" + tuKhoa + "%";
                parameters.Add(valueTuKhoa);
                parameters.Add(valueTuKhoa);
                parameters.Add(valueTuKhoa);
            }

            if (!string.IsNullOrEmpty(tinhTrang) && tinhTrang != "Tất cả")
            {
                query += " AND P.TinhTrang = @tinhTrang ";
                parameters.Add(tinhTrang);
            }

            query += " ORDER BY P.MaPhieuDV";

            DataTable data = DataProvider.Instance.ExecuteQuery(query, parameters.ToArray());

            foreach (DataRow row in data.Rows)
            {
                list.Add(new PhieuDichVuDTO()
                {
                    MaPhieuDV = Convert.ToInt32(row["MaPhieuDV"]),
                    NgayLap = Convert.ToDateTime(row["NgayLap"]),
                    TongTien = Convert.ToDecimal(row["TongTien"]),
                    TongTraTruoc = Convert.ToDecimal(row["TongTraTruoc"]),
                    TongConLai = Convert.ToDecimal(row["TongConLai"]),
                    TinhTrang = row["TinhTrang"].ToString(),
                    TenKH = row["TenKH"].ToString(),
                    SDT = row["SDT"].ToString()
                });
            }

            return list;
        }
    }
}

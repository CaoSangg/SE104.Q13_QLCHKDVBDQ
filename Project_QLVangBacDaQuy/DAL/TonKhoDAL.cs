using Project_QLVangBacDaQuy.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_QLVangBacDaQuy.DAL
{
    class TonKhoDAL
    {
        public List<BaoCaoTonKhoDTO> GetBaoCaoTonKho(int thang, int nam)
        {
            List<BaoCaoTonKhoDTO> list = new List<BaoCaoTonKhoDTO>();

            string query = $@"
                SELECT 
                    SP.MaSP, 
                    SP.TenSP, 
                    L.DonViTinh,
                    SP.SoLuongTon AS TonCuoi,
                    
                    ISNULL((SELECT SUM(CTM.SoLuong) 
                            FROM CT_PHIEUMUA CTM 
                            JOIN PHIEUMUAHANG PM ON CTM.MaPhieuMua = PM.MaPhieuMua 
                            WHERE CTM.MaSP = SP.MaSP AND MONTH(PM.NgayLap) = {thang} AND YEAR(PM.NgayLap) = {nam}), 0) AS MuaVao,
                            
                    ISNULL((SELECT SUM(CTB.SoLuong) 
                            FROM CT_PHIEUBAN CTB 
                            JOIN PHIEUBANHANG PB ON CTB.MaPhieuBan = PB.MaPhieuBan 
                            WHERE CTB.MaSP = SP.MaSP AND MONTH(PB.NgayLap) = {thang} AND YEAR(PB.NgayLap) = {nam}), 0) AS BanRa
                
                FROM SANPHAM SP
                JOIN LOAISANPHAM L ON SP.MaLoaiSP = L.MaLoaiSP";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            int stt = 1;
            foreach (DataRow row in data.Rows)
            {
                int tonCuoi = Convert.ToInt32(row["TonCuoi"]);
                int muaVao = Convert.ToInt32(row["MuaVao"]);
                int banRa = Convert.ToInt32(row["BanRa"]);

                int tonDau = tonCuoi - muaVao + banRa;

                list.Add(new BaoCaoTonKhoDTO()
                {
                    STT = stt++,
                    MaSP = Convert.ToInt32(row["MaSP"]),
                    TenSP = row["TenSP"].ToString(),
                    DonViTinh = row["DonViTinh"].ToString(),
                    TonCuoi = tonCuoi,
                    MuaVao = muaVao,
                    BanRa = banRa,
                    TonDau = tonDau
                });
            }

            return list;
        }
    }
}

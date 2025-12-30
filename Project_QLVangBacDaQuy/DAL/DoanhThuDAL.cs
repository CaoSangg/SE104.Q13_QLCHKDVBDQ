using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.DAL
{
    class DoanhThuDAL
    {
        public DataTable GetDoanhThuBanHang(int thang, int nam)
        {
            string query = $@"SELECT DAY(NgayLap) as Ngay, COUNT(*) as SL, SUM(TongTien) as Tien 
                              FROM PHIEUBANHANG 
                              WHERE MONTH(NgayLap) = {thang} AND YEAR(NgayLap) = {nam} 
                              GROUP BY DAY(NgayLap)";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetDoanhThuDichVu(int thang, int nam)
        {
            string query = $@"SELECT DAY(NgayLap) as Ngay, COUNT(*) as SL, SUM(TongTien) as Tien 
                              FROM PHIEUDICHVU 
                              WHERE MONTH(NgayLap) = {thang} AND YEAR(NgayLap) = {nam} 
                              GROUP BY DAY(NgayLap)";
            return DataProvider.Instance.ExecuteQuery(query);
        }
    }
}

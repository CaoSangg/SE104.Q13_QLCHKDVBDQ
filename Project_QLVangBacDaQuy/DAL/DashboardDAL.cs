using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.DAL
{
    class DashboardDAL
    {
        // Tinh toán và lấy dữ liệu thống kê cho Dashboard
        public DashboardDTO GetDashboardStats()
        {
            DashboardDTO data = new DashboardDTO();

            string queryBan = @"SELECT SUM(TongTien) FROM PHIEUBANHANG 
                                WHERE MONTH(NgayLap) = MONTH(GETDATE()) AND YEAR(NgayLap) = YEAR(GETDATE())";
            decimal dtBan = ConvertToDecimal(DataProvider.Instance.ExecuteScalar(queryBan));

            string queryDV = @"SELECT SUM(TongTien) FROM PHIEUDICHVU 
                               WHERE MONTH(NgayLap) = MONTH(GETDATE()) AND YEAR(NgayLap) = YEAR(GETDATE())";
            decimal dtDV = ConvertToDecimal(DataProvider.Instance.ExecuteScalar(queryDV));

            data.TongDoanhThu = dtBan + dtDV;

            string queryDon = @"SELECT COUNT(*) FROM PHIEUBANHANG 
                                WHERE MONTH(NgayLap) = MONTH(GETDATE()) AND YEAR(NgayLap) = YEAR(GETDATE())";
            data.SoDonHangMoi = ConvertToInt(DataProvider.Instance.ExecuteScalar(queryDon));

            string queryKhach = @"SELECT COUNT(*) FROM KHACHHANG 
                                  WHERE MONTH(NgayThamGia) = MONTH(GETDATE()) AND YEAR(NgayThamGia) = YEAR(GETDATE())";
            data.SoKhachHangMoi = ConvertToInt(DataProvider.Instance.ExecuteScalar(queryKhach));

            return data;
        }

        // Xử lý Null 
        private decimal ConvertToDecimal(object value)
        {
            if (value == null || value == DBNull.Value) return 0;
            return Convert.ToDecimal(value);
        }

        private int ConvertToInt(object value)
        {
            if (value == null || value == DBNull.Value) return 0;
            return Convert.ToInt32(value);
        }
    }
}

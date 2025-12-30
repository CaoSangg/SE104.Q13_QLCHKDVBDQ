using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_QLVangBacDaQuy.DTO
{
    class BaoCaoDoanhThuDTO
    {
        public int Ngay { get; set; }

        // Bán Hàng
        public int SoPhieuBan { get; set; }
        public decimal DoanhThuBan { get; set; }

        // Dịch Vụ
        public int SoPhieuDV { get; set; }
        public decimal DoanhThuDV { get; set; }

        public decimal TongDoanhThu
        {
            get { return DoanhThuBan + DoanhThuDV; }
        }
    }
}

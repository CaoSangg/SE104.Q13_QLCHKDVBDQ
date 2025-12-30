using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_QLVangBacDaQuy.DTO
{
    class PhieuDichVuDTO
    {
        public int MaPhieuDV { get; set; }
        public DateTime NgayLap { get; set; }
        public int MaKH { get; set; }
        public decimal TongTien { get; set; }
        public decimal TongTraTruoc { get; set; }
        public decimal TongConLai { get; set; }
        public string TinhTrang { get; set; }
        public string TenKH { get; set; }
        public string SDT { get; set; }
    }
}

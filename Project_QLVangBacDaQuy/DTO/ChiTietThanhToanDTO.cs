using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_QLVangBacDaQuy.DTO
{
    internal class ChiTietThanhToanDTO
    {
        public int MaPhieuDV { get; set; }
        public int MaLoaiDV { get; set; }
        public string TenLoaiDV { get; set; } 
        public decimal DonGiaDuocTinh { get; set; }
        public int SoLuong { get; set; }
        public decimal ThanhTien { get; set; }
        public decimal TraTruoc { get; set; }
        public decimal ConLai { get; set; }
        public DateTime NgayGiao { get; set; }
        public string TinhTrang { get; set; } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_QLVangBacDaQuy.DTO
{
    class ServiceItem
    {
        public int MaLoaiDV { get; set; }
        public string TenLoaiDV { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGiaChuan { get; set; }
        public decimal ChiPhiRieng { get; set; }
        public decimal DonGiaDuocTinh { get { return DonGiaChuan + ChiPhiRieng; } }
        public decimal ThanhTien { get { return SoLuong * DonGiaDuocTinh; } }

        public decimal TraTruoc { get; set; }
        public decimal ConLai { get { return ThanhTien - TraTruoc; } }
        public DateTime NgayGiao { get; set; }
    }
}

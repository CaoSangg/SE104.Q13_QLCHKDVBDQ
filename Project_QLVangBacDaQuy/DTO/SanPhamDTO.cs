using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_QLVangBacDaQuy.DTO
{
    class SanPhamDTO
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public decimal DonGiaMua { get; set; }
        public int SoLuongTon { get; set; }
        public string DonViTinh { get; set; }
        public double PhanTramLoiNhuan { get; set; }
        public string TenLoaiSP { get; set; }

        public decimal DonGiaBan
        {
            get { return DonGiaMua + (DonGiaMua * (decimal)PhanTramLoiNhuan); }
        }
    }
}

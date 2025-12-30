using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_QLVangBacDaQuy.DTO
{
    class CartItem
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string DonViTinh { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGiaBan { get; set; }
        public decimal ThanhTien { get { return SoLuong * DonGiaBan; } }
    }
}

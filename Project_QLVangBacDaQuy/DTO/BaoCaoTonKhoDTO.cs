using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_QLVangBacDaQuy.DTO
{
    class BaoCaoTonKhoDTO
    {
        public int STT { get; set; }
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public string DonViTinh { get; set; }

        public int TonDau { get; set; }
        public int MuaVao { get; set; }
        public int BanRa { get; set; }

        public int TonCuoi { get; set; }
    }
}

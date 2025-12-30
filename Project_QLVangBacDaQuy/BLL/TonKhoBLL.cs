using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_QLVangBacDaQuy.DAL;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.BLL
{
    class TonKhoBLL
    {
        private TonKhoDAL dal = new TonKhoDAL();

        public List<BaoCaoTonKhoDTO> LapBaoCao(int thang, int nam)
        {
            return dal.GetBaoCaoTonKho(thang, nam);
        }
    }
}

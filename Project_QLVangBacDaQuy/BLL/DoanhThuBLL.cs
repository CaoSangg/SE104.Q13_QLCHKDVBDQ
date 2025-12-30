using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Project_QLVangBacDaQuy.DAL;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.BLL
{
    class DoanhThuBLL
    {
        private DoanhThuDAL dal = new DoanhThuDAL();

        public List<BaoCaoDoanhThuDTO> LapBaoCaoDoanhThu(int thang, int nam)
        {
            var listBaoCao = new List<BaoCaoDoanhThuDTO>();

            int soNgayTrongThang = DateTime.DaysInMonth(nam, thang);
            for (int i = 1; i <= soNgayTrongThang; i++)
            {
                listBaoCao.Add(new BaoCaoDoanhThuDTO { Ngay = i });
            }

            DataTable dtBan = dal.GetDoanhThuBanHang(thang, nam);
            foreach (DataRow row in dtBan.Rows)
            {
                int ngay = Convert.ToInt32(row["Ngay"]);
                var item = listBaoCao.FirstOrDefault(x => x.Ngay == ngay);
                if (item != null)
                {
                    item.SoPhieuBan = Convert.ToInt32(row["SL"]);
                    item.DoanhThuBan = Convert.ToDecimal(row["Tien"]);
                }
            }

            DataTable dtDV = dal.GetDoanhThuDichVu(thang, nam);
            foreach (DataRow row in dtDV.Rows)
            {
                int ngay = Convert.ToInt32(row["Ngay"]);
                var item = listBaoCao.FirstOrDefault(x => x.Ngay == ngay);
                if (item != null)
                {
                    item.SoPhieuDV = Convert.ToInt32(row["SL"]);
                    item.DoanhThuDV = Convert.ToDecimal(row["Tien"]);
                }
            }

            return listBaoCao;
        }
    }
}

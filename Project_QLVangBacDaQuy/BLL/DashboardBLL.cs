using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_QLVangBacDaQuy.DAL;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.BLL
{
    class DashboardBLL
    {
        private DashboardDAL dal = new DashboardDAL();

        public DashboardDTO LayThongTinTongQuan()
        {
            return dal.GetDashboardStats();
        }
    }
}

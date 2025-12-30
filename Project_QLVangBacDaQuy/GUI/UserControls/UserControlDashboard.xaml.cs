using Project_QLVangBacDaQuy.BLL;
using Project_QLVangBacDaQuy.DAL;
using Project_QLVangBacDaQuy.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project_QLVangBacDaQuy.GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlDashboard.xaml
    /// </summary>
    public partial class UserControlDashboard : UserControl
    {
        private DashboardBLL bll = new DashboardBLL();
        public UserControlDashboard()
        {
            InitializeComponent();

            LoadDashboardData();
        }
        private void LoadDashboardData()
        {
            try
            {
                DashboardDTO data = bll.LayThongTinTongQuan();

                txtDoanhThuThang.Text = string.Format("{0:N0} ₫", data.TongDoanhThu);
                txtDonHangTrongThang.Text = $"{data.SoDonHangMoi} Đơn";
                txtKhachHangMoi.Text = $"{data.SoKhachHangMoi} Khách";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu Dashboard: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

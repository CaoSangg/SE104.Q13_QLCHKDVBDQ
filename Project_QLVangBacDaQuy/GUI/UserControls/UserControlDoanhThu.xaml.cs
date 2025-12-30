using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using System.Data;
using Project_QLVangBacDaQuy.DAL;
using Project_QLVangBacDaQuy.BLL; 
using Project_QLVangBacDaQuy.DTO; 

namespace Project_QLVangBacDaQuy.GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlDoanhThu.xaml
    /// </summary>
    public partial class UserControlDoanhThu : System.Windows.Controls.UserControl
    {

        private DoanhThuBLL bll = new DoanhThuBLL();

        public UserControlDoanhThu()
        {
            InitializeComponent();
            cboThang.SelectedIndex = DateTime.Now.Month - 1;
            txtNam.Text = DateTime.Now.Year.ToString();
        }
        private void BtnXem_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNam.Text) || !int.TryParse(txtNam.Text, out int nam) || nam < 2000)
            {
                MessageBox.Show("Vui lòng nhập Năm hợp lệ!");
                return;
            }

            if (cboThang.SelectedItem == null) return;
            int thang = int.Parse(cboThang.Text);

            var listBaoCao = bll.LapBaoCaoDoanhThu(thang, nam);

            dgvBaoCao.ItemsSource = listBaoCao;

            decimal tongThang = listBaoCao.Sum(x => x.TongDoanhThu);
            lblTongThang.Text = string.Format("{0:N0} VNĐ", tongThang);

            if (tongThang == 0)
            {
                MessageBox.Show("Tháng này chưa có doanh thu!");
            }
        }

        private void BtnIn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tính năng Xuất Báo Cáo đang được cập nhật!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

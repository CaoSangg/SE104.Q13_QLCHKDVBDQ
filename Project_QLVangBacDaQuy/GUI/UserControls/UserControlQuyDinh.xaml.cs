using Project_QLVangBacDaQuy.DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Linq; 
using Project_QLVangBacDaQuy.BLL; 
using Project_QLVangBacDaQuy.DTO; 

namespace Project_QLVangBacDaQuy.GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlQuyDinh.xaml
    /// </summary>
    public partial class UserControlQuyDinh : System.Windows.Controls.UserControl
    {
        private QuyDinhBLL bll = new QuyDinhBLL();

        private ObservableCollection<QuyDinhDTO> danhSachQuyDinh;

        public UserControlQuyDinh()
        {
            InitializeComponent();

            LoadData();
        }
        private void LoadData()
        {
            var list = bll.LayDanhSachQuyDinh();

            danhSachQuyDinh = new ObservableCollection<QuyDinhDTO>(list);
            dgvQuyDinh.ItemsSource = danhSachQuyDinh;
        }

        private void btnCapNhat_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn lưu các thay đổi này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) return;

            string thongBao = "";
            bool ketQua = bll.CapNhatQuyDinh(danhSachQuyDinh.ToList(), out thongBao);

            if (ketQua)
            {
                MessageBox.Show(thongBao, "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadData(); 
            }
            else
            {
                MessageBox.Show(thongBao, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}

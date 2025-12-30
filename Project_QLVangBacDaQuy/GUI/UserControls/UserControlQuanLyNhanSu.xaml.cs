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
using Project_QLVangBacDaQuy.BLL; 
using Project_QLVangBacDaQuy.DTO; 

namespace Project_QLVangBacDaQuy.GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlQuanLyNhanSu.xaml
    /// </summary>
    public partial class UserControlQuanLyNhanSu : UserControl
    {
        private NhanVienBLL bll = new NhanVienBLL();
        public UserControlQuanLyNhanSu()
        {
            InitializeComponent();

            LoadDanhSachNhanVien();
        }
        // Tải danh sách nhân viên lên lưới
        void LoadDanhSachNhanVien()
        {
            dgvNhanVien.ItemsSource = bll.LayDanhSachNhanVien(); 
        }

        // Tạo tài khoản nhân viên mới
        private void btnTao_Click(object sender, RoutedEventArgs e)
        {
            string role = "NhanVien";
            if (cboQuyen.SelectedItem != null)
            {
                role = ((ComboBoxItem)cboQuyen.SelectedItem).Content.ToString();
            }

            string msg = "";
            bool ketQua = bll.TaoTaiKhoanMoi(
                txtUser.Text.Trim(),
                txtPass.Text.Trim(),
                txtHoTen.Text.Trim(),
                role,
                out msg
            );

            if (ketQua)
            {
                MessageBox.Show(msg, "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                txtUser.Clear();
                txtPass.Clear();
                txtHoTen.Clear();
                cboQuyen.SelectedIndex = 0;

                LoadDanhSachNhanVien();
            }
            else
            {
                MessageBox.Show(msg, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Xóa tài khoản nhân viên
        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            NhanVienDTO nvChon = ((FrameworkElement)sender).DataContext as NhanVienDTO;

            if (nvChon == null) return;

            MessageBoxResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa tài khoản '{nvChon.TenDangNhap}' không?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                string msg = "";
                if (bll.XoaNhanVien(nvChon.TenDangNhap, out msg))
                {
                    MessageBox.Show("Đã xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadDanhSachNhanVien(); 
                }
                else
                {
                    MessageBox.Show(msg, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

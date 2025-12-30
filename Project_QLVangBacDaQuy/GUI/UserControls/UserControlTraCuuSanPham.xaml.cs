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
using System.Data;
using System.Collections.Generic; 
using Project_QLVangBacDaQuy.DAL;
using System.Windows.Input; 
using Project_QLVangBacDaQuy.BLL; 
using Project_QLVangBacDaQuy.DTO; 

namespace Project_QLVangBacDaQuy.GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlTraCuuSanPham.xaml
    /// </summary>
    public partial class UserControlTraCuuSanPham : System.Windows.Controls.UserControl
    {
        private TraCuuSanPhamBLL bll = new TraCuuSanPhamBLL();
        public UserControlTraCuuSanPham()
        {
            InitializeComponent();
            LoadLoaiSP();
            LoadDanhSachSanPham(); 
        }
        void LoadLoaiSP()
        {
            cboLoaiSP.ItemsSource = bll.LayDanhSachLoaiSP();
        }

        void LoadDanhSachSanPham()
        {
            string tuKhoa = txtTuKhoa.Text.Trim();

            int maLoai = 0;
            if (cboLoaiSP.SelectedValue != null)
            {
                maLoai = (int)cboLoaiSP.SelectedValue;
            }

            dgvKetQua.ItemsSource = bll.TimKiemSanPham(tuKhoa, maLoai);
        }

        private void BtnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            LoadDanhSachSanPham();
        }

        private void BtnLamMoi_Click(object sender, RoutedEventArgs e)
        {
            txtTuKhoa.Clear();
            cboLoaiSP.SelectedIndex = -1;
            LoadDanhSachSanPham();
            txtTuKhoa.Focus();
        }

        private void txtTuKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoadDanhSachSanPham();
            }
        }
    }
}

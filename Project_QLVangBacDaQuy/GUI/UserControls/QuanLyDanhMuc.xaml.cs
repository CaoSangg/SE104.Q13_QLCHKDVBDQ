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
    /// Interaction logic for QuanLyDanhMuc.xaml
    /// </summary>
    public partial class QuanLyDanhMuc : UserControl
    {
        private QuanLyDanhMucBLL bll = new QuanLyDanhMucBLL();
        public QuanLyDanhMuc()
        {
            InitializeComponent();

            LoadAllData();
        }
        private void LoadAllData()
        {
            LoadNCC();
            LoadLoaiSP();
            LoadLoaiDV();
        }

        // Load dl cho dgvNCC
        void LoadNCC()
        {
            dgvNCC.ItemsSource = bll.GetListNCC();
        }

        private void dgvNCC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvNCC.SelectedItem is NhaCungCapDTO item)
            {
                txtTenNCC.Text = item.TenNCC;
                txtDiaChiNCC.Text = item.DiaChi;
                txtSdtNCC.Text = item.SDT;
            }
        }

        // Thêm, Sửa, Xóa, Làm mới NCC
        private void btnThemNCC_Click(object sender, RoutedEventArgs e)
        {
            string msg = bll.AddNCC(txtTenNCC.Text, txtDiaChiNCC.Text, txtSdtNCC.Text);
            MessageBox.Show(msg);
            LoadNCC(); 
            btnLamMoiNCC_Click(null, null); 
        }

        private void btnSuaNCC_Click(object sender, RoutedEventArgs e)
        {
            if (dgvNCC.SelectedItem is NhaCungCapDTO item)
            {
                string msg = bll.EditNCC(item.MaNCC, txtTenNCC.Text, txtDiaChiNCC.Text, txtSdtNCC.Text);
                MessageBox.Show(msg);
                LoadNCC();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp cần sửa!");
            }
        }

        private void btnXoaNCC_Click(object sender, RoutedEventArgs e)
        {
            if (dgvNCC.SelectedItem is NhaCungCapDTO item)
            {
                if (MessageBox.Show($"Bạn có chắc muốn xóa '{item.TenNCC}'?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    string msg = bll.DeleteNCC(item.MaNCC);
                    MessageBox.Show(msg);
                    LoadNCC();
                    btnLamMoiNCC_Click(null, null);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn Nhà cung cấp cần xóa!");
            }
        }

        private void btnLamMoiNCC_Click(object sender, RoutedEventArgs e)
        {
            txtTenNCC.Clear();
            txtDiaChiNCC.Clear();
            txtSdtNCC.Clear();
            dgvNCC.SelectedIndex = -1;
        }


        // Load dgvLoaiSP
        void LoadLoaiSP()
        {
            dgvLoaiSP.ItemsSource = bll.GetListLoaiSP();
        }

        private void dgvLoaiSP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvLoaiSP.SelectedItem is LoaiSanPhamDTO item)
            {
                txtTenLoaiSP.Text = item.TenLoaiSP;
                txtLoiNhuan.Text = item.PhanTramLoiNhuan.ToString();
                txtDonVi.Text = item.DonViTinh;
            }
        }

        // Thêm, Sửa, Xóa, Làm mới Loại SP
        private void btnThemLoaiSP_Click(object sender, RoutedEventArgs e)
        {
            string msg = bll.AddLoaiSP(txtTenLoaiSP.Text, txtLoiNhuan.Text, txtDonVi.Text);
            MessageBox.Show(msg);
            LoadLoaiSP();
            btnLamMoiLoaiSP_Click(null, null);
        }

        private void btnSuaLoaiSP_Click(object sender, RoutedEventArgs e)
        {
            if (dgvLoaiSP.SelectedItem is LoaiSanPhamDTO item)
            {
                string msg = bll.EditLoaiSP(item.MaLoaiSP, txtTenLoaiSP.Text, txtLoiNhuan.Text, txtDonVi.Text);
                MessageBox.Show(msg);
                LoadLoaiSP();
            }
            else
            {
                MessageBox.Show("Chưa chọn Loại sản phẩm!");
            }
        }

        private void btnXoaLoaiSP_Click(object sender, RoutedEventArgs e)
        {
            if (dgvLoaiSP.SelectedItem is LoaiSanPhamDTO item)
            {
                if (MessageBox.Show($"Xóa loại '{item.TenLoaiSP}'?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    string msg = bll.DeleteLoaiSP(item.MaLoaiSP);
                    MessageBox.Show(msg);
                    LoadLoaiSP();
                    btnLamMoiLoaiSP_Click(null, null);
                }
            }
        }

        private void btnLamMoiLoaiSP_Click(object sender, RoutedEventArgs e)
        {
            txtTenLoaiSP.Clear();
            txtLoiNhuan.Clear();
            txtDonVi.Clear();
            dgvLoaiSP.SelectedIndex = -1;
        }

        // Load dgvLoaiDV
        void LoadLoaiDV()
        {
            dgvLoaiDV.ItemsSource = bll.GetListLoaiDV();
        }

        private void dgvLoaiDV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvLoaiDV.SelectedItem is LoaiDichVuDTO item)
            {
                txtTenLoaiDV.Text = item.TenLoaiDV;
                txtDonGiaDV.Text = item.DonGiaDV.ToString("N0");
            }
        }

        // Thêm, Sửa, Xóa, Làm mới Loại DV
        private void btnThemLoaiDV_Click(object sender, RoutedEventArgs e)
        {
            string giaRaw = txtDonGiaDV.Text.Replace(",", "").Replace(".", "").Trim();

            string msg = bll.AddLoaiDV(txtTenLoaiDV.Text, giaRaw);
            MessageBox.Show(msg);
            LoadLoaiDV();
            btnLamMoiLoaiDV_Click(null, null);
        }

        private void btnSuaLoaiDV_Click(object sender, RoutedEventArgs e)
        {
            if (dgvLoaiDV.SelectedItem is LoaiDichVuDTO item)
            {
                string giaRaw = txtDonGiaDV.Text.Replace(",", "").Replace(".", "").Trim();

                string msg = bll.EditLoaiDV(item.MaLoaiDV, txtTenLoaiDV.Text, giaRaw);
                MessageBox.Show(msg);
                LoadLoaiDV();
            }
            else
            {
                MessageBox.Show("Chưa chọn Loại dịch vụ!");
            }
        }

        private void btnXoaLoaiDV_Click(object sender, RoutedEventArgs e)
        {
            if (dgvLoaiDV.SelectedItem is LoaiDichVuDTO item)
            {
                if (MessageBox.Show($"Xóa loại dịch vụ '{item.TenLoaiDV}'?", "Xác nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    string msg = bll.DeleteLoaiDV(item.MaLoaiDV);
                    MessageBox.Show(msg);
                    LoadLoaiDV();
                    btnLamMoiLoaiDV_Click(null, null);
                }
            }
        }

        private void btnLamMoiLoaiDV_Click(object sender, RoutedEventArgs e)
        {
            txtTenLoaiDV.Clear();
            txtDonGiaDV.Clear();
            dgvLoaiDV.SelectedIndex = -1;
        }
    }
}

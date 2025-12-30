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
    /// Interaction logic for UserControlMuaHang.xaml
    /// </summary>
    public partial class UserControlMuaHang : System.Windows.Controls.UserControl
    {

        private MuaHangBLL bll = new MuaHangBLL();

        ObservableCollection<CartItem> gioHang = new ObservableCollection<CartItem>();
        decimal tongTienPhieu = 0;
        public UserControlMuaHang()
        {
            InitializeComponent();
            dpNgayLap.SelectedDate = DateTime.Now;
            dgvChiTiet.ItemsSource = gioHang;

            LoadData();
        }
        void LoadData()
        {
            cboNhaCungCap.ItemsSource = bll.LayDanhSachNhaCungCap();

            cboLoaiSP.ItemsSource = bll.LayDanhSachLoaiSP();

            LoadSanPham(0);
        }

        // Hàm phụ trợ để load sản phẩm theo mã loại
        void LoadSanPham(int maLoai)
        {
            cboSanPham.ItemsSource = bll.LayDanhSachSanPham(maLoai);
        }

        private void cboLoaiSP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboLoaiSP.SelectedValue != null)
            {
                int maLoai = (int)cboLoaiSP.SelectedValue;
                LoadSanPham(maLoai);
            }
            else
            {
                LoadSanPham(0); 
            }
        }

        private void CboSanPham_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboSanPham.SelectedItem != null)
            {
                SanPhamDTO sp = (SanPhamDTO)cboSanPham.SelectedItem;
                txtDonGia.Text = sp.DonGiaMua.ToString("N0");
                txtSoLuong.Focus();
                txtSoLuong.SelectAll();
            }
        }

        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            if (cboSanPham.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtSoLuong.Text, out int sl) || sl <= 0)
            {
                MessageBox.Show("Số lượng phải > 0"); return;
            }

            string donGiaStr = txtDonGia.Text.Replace(",", "").Replace(".", "").Trim();
            if (!decimal.TryParse(donGiaStr, out decimal giaNhap) || giaNhap <= 0)
            {
                MessageBox.Show("Đơn giá nhập không hợp lệ!"); return;
            }

            SanPhamDTO sp = (SanPhamDTO)cboSanPham.SelectedItem;

            var itemTonTai = gioHang.FirstOrDefault(x => x.MaSP == sp.MaSP);
            if (itemTonTai != null)
            {
                itemTonTai.SoLuong += sl;
                itemTonTai.DonGiaBan = giaNhap; 
            }
            else
            {
                gioHang.Add(new CartItem()
                {
                    MaSP = sp.MaSP,
                    TenSP = sp.TenSP,
                    DonViTinh = sp.DonViTinh,
                    SoLuong = sl,
                    DonGiaBan = giaNhap 
                });
            }

            dgvChiTiet.Items.Refresh();
            TinhTongTien();

            txtSoLuong.Text = "1";
            txtDonGia.Clear();
        }

        void TinhTongTien()
        {
            tongTienPhieu = gioHang.Sum(x => x.ThanhTien);
            lblTongTien.Text = string.Format("{0:N0} VNĐ", tongTienPhieu);
        }

        private void BtnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (gioHang.Count == 0) { MessageBox.Show("Danh sách nhập trống!"); return; }
            if (cboNhaCungCap.SelectedItem == null) { MessageBox.Show("Chưa chọn Nhà cung cấp!"); return; }
            if (dpNgayLap.SelectedDate == null) { MessageBox.Show("Ngày nhập không hợp lệ!"); return; }

            int maNCC = (int)cboNhaCungCap.SelectedValue;
            string thongBao = "";

            bool ketQua = bll.NhapKho(
                dpNgayLap.SelectedDate.Value,
                maNCC,
                gioHang.ToList(),
                tongTienPhieu,
                out thongBao
            );

            if (ketQua)
            {
                MessageBox.Show(thongBao, "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                gioHang.Clear();
                TinhTongTien();
                cboNhaCungCap.SelectedIndex = -1;
                cboSanPham.SelectedIndex = -1;
                cboLoaiSP.SelectedIndex = -1; 

                txtDonGia.Clear();
                txtSoLuong.Text = "1";

                LoadData(); 
            }
            else
            {
                MessageBox.Show(thongBao, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

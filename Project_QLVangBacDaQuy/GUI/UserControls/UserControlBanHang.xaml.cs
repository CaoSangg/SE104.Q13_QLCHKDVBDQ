using System;
using System.Collections.Generic;
using System.Collections.ObjectModel; 
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
using Project_QLVangBacDaQuy.DAL;
using Project_QLVangBacDaQuy.BLL; 
using Project_QLVangBacDaQuy.DTO; 

namespace Project_QLVangBacDaQuy.GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlBanHang.xaml
    /// </summary>
    public partial class UserControlBanHang : System.Windows.Controls.UserControl
    {
        private BanHangBLL bll = new BanHangBLL();

        ObservableCollection<CartItem> gioHang = new ObservableCollection<CartItem>();
        decimal tongTienCaPhieu = 0;

        public UserControlBanHang()
        {
            InitializeComponent();

            dpNgayLap.SelectedDate = DateTime.Now; 
            dgvChiTiet.ItemsSource = gioHang;      
            LoadSanPham();                         
        }

        void LoadSanPham()
        {
            cboSanPham.ItemsSource = bll.LayDanhSachSanPham();
        }

        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            if (cboSanPham.SelectedItem == null) return;

            SanPhamDTO spChon = (SanPhamDTO)cboSanPham.SelectedItem;

            if (!int.TryParse(txtSoLuong.Text, out int slMua) || slMua <= 0)
            {
                MessageBox.Show("Số lượng phải > 0"); return;
            }

            if (slMua > spChon.SoLuongTon)
            {
                MessageBox.Show($"Không đủ hàng! Tồn kho chỉ còn {spChon.SoLuongTon}");
                return;
            }

            gioHang.Add(new CartItem()
            {
                MaSP = spChon.MaSP,
                TenSP = spChon.TenSP,
                DonViTinh = spChon.DonViTinh,
                SoLuong = slMua,
                DonGiaBan = spChon.DonGiaBan 
            });

            TinhTongTien();
        }

        void TinhTongTien()
        {
            tongTienCaPhieu = gioHang.Sum(x => x.ThanhTien);
            lblTongTien.Text = string.Format("{0:N0} VNĐ", tongTienCaPhieu);
        }

        private void BtnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (gioHang.Count == 0) { MessageBox.Show("Giỏ hàng trống!"); return; }
            if (dpNgayLap.SelectedDate < DateTime.Now.Date) { MessageBox.Show("Ngày lập không hợp lệ!"); return; }
            if (string.IsNullOrWhiteSpace(txtTenKH.Text) || string.IsNullOrWhiteSpace(txtSDT.Text) || string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin khách hàng!"); return;
            }

            string thongBao = "";
            bool ketQua = bll.ThanhToan(
                txtTenKH.Text.Trim(),
                txtSDT.Text.Trim(),
                txtDiaChi.Text.Trim(),
                dpNgayLap.SelectedDate.Value,
                gioHang.ToList(), 
                tongTienCaPhieu,
                out thongBao
            );

            if (ketQua)
            {
                MessageBox.Show(thongBao, "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                gioHang.Clear();
                txtTenKH.Clear(); txtSDT.Clear(); txtDiaChi.Clear();
                TinhTongTien();
                LoadSanPham(); 
            }
            else
            {
                MessageBox.Show(thongBao, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

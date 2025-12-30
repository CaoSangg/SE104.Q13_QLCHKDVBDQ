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
    /// Interaction logic for UserControlDichVu.xaml
    /// </summary>
    public partial class UserControlDichVu : System.Windows.Controls.UserControl
    {

        private DichVuBLL bll = new DichVuBLL();

        ObservableCollection<ServiceItem> listDichVu = new ObservableCollection<ServiceItem>();
        public UserControlDichVu()
        {
            InitializeComponent();

            dpNgayLap.SelectedDate = DateTime.Now;
            dpNgayGiao.SelectedDate = DateTime.Now.AddDays(3);

            dgvChiTiet.ItemsSource = listDichVu;
            LoadLoaiDichVu();
        }
        void LoadLoaiDichVu()
        {
            cboLoaiDV.ItemsSource = bll.LayDanhSachLoaiDichVu();
        }

        private void CboLoaiDV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboLoaiDV.SelectedItem != null)
            {
                LoaiDichVuDTO item = (LoaiDichVuDTO)cboLoaiDV.SelectedItem;

                txtDonGiaChuan.Text = item.DonGiaDV.ToString("N0");
                txtChiPhiRieng.Text = "0";
                txtSoLuong.Text = "1";
                txtTraTruoc.Clear();
            }
        }

        private void BtnThem_Click(object sender, RoutedEventArgs e)
        {
            if (cboLoaiDV.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại dịch vụ!"); return;
            }

            DateTime ngayLap = dpNgayLap.SelectedDate.Value.Date;
            DateTime ngayGiao = dpNgayGiao.SelectedDate.Value.Date;
            if (ngayLap < DateTime.Now.Date) { MessageBox.Show("Ngày lập phiếu không được nhỏ hơn ngày hiện tại!"); return; }
            if (ngayGiao < ngayLap) { MessageBox.Show("Ngày hẹn giao không hợp lệ!"); return; }

            if (!int.TryParse(txtSoLuong.Text, out int sl) || sl <= 0) { MessageBox.Show("Số lượng phải > 0"); return; }
            decimal.TryParse(txtChiPhiRieng.Text.Replace(",", ""), out decimal chiPhiRieng);
            decimal.TryParse(txtTraTruoc.Text.Replace(",", ""), out decimal traTruoc);

            LoaiDichVuDTO loaiDV = (LoaiDichVuDTO)cboLoaiDV.SelectedItem;
            decimal donGiaTinh = loaiDV.DonGiaDV + chiPhiRieng;
            decimal thanhTien = donGiaTinh * sl;

            double tyLe = bll.LayTyLeTraTruoc(); 
            decimal toiThieu = thanhTien * (decimal)tyLe;

            if (traTruoc < toiThieu)
            {
                MessageBox.Show($"Số tiền trả trước chưa đủ!\nPhải trả trước ít nhất {tyLe * 100}% ({toiThieu:N0} VNĐ).",
                                "Quy định", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtTraTruoc.Focus();
                return;
            }

            listDichVu.Add(new ServiceItem()
            {
                MaLoaiDV = loaiDV.MaLoaiDV,
                TenLoaiDV = loaiDV.TenLoaiDV,
                SoLuong = sl,
                DonGiaChuan = loaiDV.DonGiaDV,
                ChiPhiRieng = chiPhiRieng,
                TraTruoc = traTruoc,
                NgayGiao = ngayGiao
            });

            CapNhatTong();

            txtSoLuong.Text = "1"; txtChiPhiRieng.Text = "0"; txtTraTruoc.Clear();
        }

        void CapNhatTong()
        {
            decimal tongTien = listDichVu.Sum(x => x.ThanhTien);
            decimal tongTraTruoc = listDichVu.Sum(x => x.TraTruoc);
            decimal tongConLai = listDichVu.Sum(x => x.ConLai);

            lblTongTien.Text = string.Format("{0:N0} đ", tongTien);
            lblTongTraTruoc.Text = string.Format("{0:N0} đ", tongTraTruoc);
            lblTongConLai.Text = string.Format("{0:N0} VNĐ", tongConLai);
        }

        private void BtnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (listDichVu.Count == 0) { MessageBox.Show("Danh sách trống!"); return; }
            if (string.IsNullOrWhiteSpace(txtTenKH.Text) || string.IsNullOrWhiteSpace(txtSDT.Text) || string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Nhập đủ thông tin khách hàng!"); return;
            }

            decimal tongTien = listDichVu.Sum(x => x.ThanhTien);
            decimal tongTraTruoc = listDichVu.Sum(x => x.TraTruoc);
            decimal tongConLai = listDichVu.Sum(x => x.ConLai);

            string thongBao = "";
            bool ketQua = bll.LuuPhieuDichVu(
                txtTenKH.Text.Trim(),
                txtSDT.Text.Trim(),
                txtDiaChi.Text.Trim(),
                dpNgayLap.SelectedDate.Value,
                listDichVu.ToList(),
                tongTien,
                tongTraTruoc,
                tongConLai,
                out thongBao
            );

            if (ketQua)
            {
                MessageBox.Show(thongBao, "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                listDichVu.Clear(); CapNhatTong();
                txtTenKH.Clear(); txtSDT.Clear(); txtDiaChi.Clear();
            }
            else
            {
                MessageBox.Show(thongBao, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

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
    /// Interaction logic for UserControlThanhToanDichVu.xaml
    /// </summary>
    public partial class UserControlThanhToanDichVu : UserControl
    {
        private ThanhToanDichVuBLL bll = new ThanhToanDichVuBLL();
        public UserControlThanhToanDichVu()
        {
            InitializeComponent();
            LoadDanhSachPhieu();
        }
        // Load danh sách phiếu dịch vụ chưa thanh toán 
        void LoadDanhSachPhieu()
        {
            dgvPhieu.ItemsSource = bll.LayPhieuChuaHoanThanh();
        }

        // Laod chi tiết phiếu dịch vụ được chọn 
        private void dgvPhieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvPhieu.SelectedItem is PhieuDichVuDTO phieu)
            {
                lblMaPhieu.Text = "#" + phieu.MaPhieuDV.ToString();
                lblTongConLai.Text = phieu.TongConLai.ToString("N0") + " VNĐ";

                LoadChiTiet(phieu.MaPhieuDV);
            }
        }

        void LoadChiTiet(int maPhieu)
        {
            dgvChiTiet.ItemsSource = bll.LayChiTietPhieu(maPhieu);
        }

        // Thanh toán từng món 
        private void btnThanhToanMon_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            ChiTietThanhToanDTO item = btn.DataContext as ChiTietThanhToanDTO;

            if (item == null) return;

            string msgConfirm = $"Xác nhận khách đã thanh toán {item.ConLai:N0} VNĐ cho dịch vụ '{item.TenLoaiDV}'?";
            if (MessageBox.Show(msgConfirm, "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string msgResult = "";
                if (bll.ThanhToanMon(item.MaPhieuDV, item.MaLoaiDV, out msgResult))
                {
                    MessageBox.Show(msgResult, "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                    LoadChiTiet(item.MaPhieuDV);
                    LoadDanhSachPhieu(); 

                    if (dgvPhieu.SelectedItem is PhieuDichVuDTO phieuDangChon)
                    {
                        lblTongConLai.Text = "Đang cập nhật...";
                    }
                }
                else
                {
                    MessageBox.Show(msgResult, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Thanh toán toàn bộ phiếu
        private void btnThanhToanAll_Click(object sender, RoutedEventArgs e)
        {
            if (dgvPhieu.SelectedItem is PhieuDichVuDTO phieu)
            {
                if (MessageBox.Show($"Xác nhận thanh toán toàn bộ số tiền còn lại ({phieu.TongConLai:N0} VNĐ) cho phiếu #{phieu.MaPhieuDV}?",
                    "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    string msg = "";
                    if (bll.ThanhToanTatCa(phieu.MaPhieuDV, out msg))
                    {
                        MessageBox.Show(msg, "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);

                        LoadChiTiet(phieu.MaPhieuDV);
                        LoadDanhSachPhieu();

                        lblTongConLai.Text = "0 VNĐ";
                        lblMaPhieu.Text = "...";
                    }
                    else
                    {
                        MessageBox.Show(msg, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một phiếu bên trái trước!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

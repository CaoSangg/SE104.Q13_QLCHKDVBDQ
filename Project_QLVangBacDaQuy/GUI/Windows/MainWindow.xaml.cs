using Project_QLVangBacDaQuy.BLL;
using Project_QLVangBacDaQuy.DTO;
using Project_QLVangBacDaQuy.GUI.UserControls;
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
using System.Windows.Shapes;

namespace Project_QLVangBacDaQuy.GUI.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public NhanVienDTO CurrentUser { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            RenderBody.Content = new UserControlDashboard();

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PhanQuyen();
            HienThiThongTin();
            Task.Run(() => new DichVuBLL().TuDongKiemTraHuyPhieu());
        }

        private void PhanQuyen()
        {
            if (CurrentUser == null) return;

            if (CurrentUser.QuyenHan == "NhanVien")
            {
                btnDoanhThu.Visibility = Visibility.Collapsed;
                btnQuyDinh.Visibility = Visibility.Collapsed;
                btnQuanLyNhanSu.Visibility = Visibility.Collapsed;
            }
        }

        private void HienThiThongTin()
        {
            if (CurrentUser != null)
            {
                this.Title = $"Phần mềm Quản lý - Xin chào: {CurrentUser.HoTen}";
            }
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            switch (btn.Name)
            {
                case "btnBanHang": RenderBody.Content = new UserControlBanHang(); break;
                case "btnMuaHang": RenderBody.Content = new UserControlMuaHang(); break;
                case "btnDichVu": RenderBody.Content = new UserControlDichVu(); break;
                case "btnTraCuuSP": RenderBody.Content = new UserControlTraCuuSanPham(); break;
                case "btnTraCuuDV": RenderBody.Content = new UserControlTraCuuDichVu(); break;
                case "btnTonKho": RenderBody.Content = new UserControlTonKho(); break;
                case "btnDoanhThu": RenderBody.Content = new UserControlDoanhThu(); break;
                case "btnQuyDinh": RenderBody.Content = new UserControlQuyDinh(); break;
                case "btnTrangChu": RenderBody.Content = new UserControlDashboard(); break;
                case "btnQuanLyNhanSu": RenderBody.Content = new UserControlQuanLyNhanSu(); break;
                case "btnQuanLyDanhMuc": RenderBody.Content = new QuanLyDanhMuc(); break;
                case "btnThanhToanDichVu": RenderBody.Content = new UserControlThanhToanDichVu(); break;
            }
        }

        private void BtnDangXuat_Click(object sender, RoutedEventArgs e)
        {
            new WindowLogin().Show();
            this.Close();
        }

        private void Header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) btnMaximize_Click(sender, e);
            else this.DragMove();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e) => this.WindowState = WindowState.Minimized;
        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();
    }
}

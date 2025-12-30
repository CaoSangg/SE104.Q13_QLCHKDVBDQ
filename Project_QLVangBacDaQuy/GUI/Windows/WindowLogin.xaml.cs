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
using Project_QLVangBacDaQuy.BLL; 
using Project_QLVangBacDaQuy.DTO; 

namespace Project_QLVangBacDaQuy.GUI.Windows
{
    /// <summary>
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        private NhanVienBLL bll = new NhanVienBLL();
        public WindowLogin()
        {
            InitializeComponent();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            try { this.DragMove(); } catch { }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string tk = txtUsername.Text.Trim();
            string mk = txtPassword.Password.Trim();

            NhanVienDTO nhanVien = bll.DangNhap(tk, mk);

            if (nhanVien != null)
            {
                MainWindow main = new MainWindow();

                main.CurrentUser = nhanVien;

                main.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}

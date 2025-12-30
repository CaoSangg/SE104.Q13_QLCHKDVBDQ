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
using System.Collections.Generic;
using System.Data;
using Project_QLVangBacDaQuy.DAL;
using Project_QLVangBacDaQuy.BLL; 
using Project_QLVangBacDaQuy.DTO; 
using System.ComponentModel; 

namespace Project_QLVangBacDaQuy.GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlTraCuuDichVu.xaml
    /// </summary>
    public partial class UserControlTraCuuDichVu : System.Windows.Controls.UserControl
    {
        private TraCuuDichVuBLL bll = new TraCuuDichVuBLL();
        public UserControlTraCuuDichVu()
        {
            InitializeComponent();

            ResetForm(); 

            LoadDanhSach();
        }
        void ResetForm()
        {
            DateTime today = DateTime.Now;
            dpTuNgay.SelectedDate = new DateTime(today.Year, today.Month, 1);
            dpDenNgay.SelectedDate = today;
            txtTuKhoa.Clear();
            cboTinhTrang.SelectedIndex = 0; 
        }

        void LoadDanhSach()
        {
            DateTime? tuNgay = dpTuNgay.SelectedDate;
            DateTime? denNgay = dpDenNgay.SelectedDate;
            string tuKhoa = txtTuKhoa.Text.Trim();

            string tinhTrang = "Tất cả";
            if (cboTinhTrang.SelectedItem != null)
            {
                tinhTrang = ((ComboBoxItem)cboTinhTrang.SelectedItem).Content.ToString();
            }

            var listKetQua = bll.TimKiemPhieuDichVu(tuNgay, denNgay, tuKhoa, tinhTrang);

            dgvKetQua.Items.SortDescriptions.Clear();

            dgvKetQua.ItemsSource = listKetQua;
        }

        private void BtnTimKiem_Click(object sender, RoutedEventArgs e)
        {
            LoadDanhSach();
        }

        private void BtnLamMoi_Click(object sender, RoutedEventArgs e)
        {
            ResetForm();
            LoadDanhSach();
        }

        private void txtTuKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoadDanhSach();
            }
        }
    }
}

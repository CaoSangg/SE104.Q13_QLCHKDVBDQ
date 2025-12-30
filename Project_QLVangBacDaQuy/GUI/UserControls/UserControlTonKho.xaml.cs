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
using Project_QLVangBacDaQuy.BLL; 
using Project_QLVangBacDaQuy.DAL;
using System.ComponentModel; 

namespace Project_QLVangBacDaQuy.GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UserControlTonKho.xaml
    /// </summary>
    public partial class UserControlTonKho : System.Windows.Controls.UserControl
    {
        private TonKhoBLL bll = new TonKhoBLL();
        public UserControlTonKho()
        {
            InitializeComponent();

            cboThang.SelectedIndex = DateTime.Now.Month - 1;
            txtNam.Text = DateTime.Now.Year.ToString();
        }
        private void BtnXem_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNam.Text))
            {
                MessageBox.Show("Vui lòng nhập năm báo cáo!"); return;
            }
            if (!int.TryParse(txtNam.Text, out int nam) || nam < 2000)
            {
                MessageBox.Show("Năm không hợp lệ!"); return;
            }

            int thang = int.Parse(cboThang.Text);

            var listBaoCao = bll.LapBaoCao(thang, nam);

            dgvBaoCao.Items.SortDescriptions.Clear(); 
            dgvBaoCao.ItemsSource = listBaoCao;

            if (listBaoCao.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu sản phẩm trong kỳ báo cáo này!");
            }
        }
    }
}

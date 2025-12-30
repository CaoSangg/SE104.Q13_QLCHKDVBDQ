using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;

namespace Project_QLVangBacDaQuy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Ép buộc định dạng ngày tháng/số liệu theo chuẩn Việt Nam
            CultureInfo culture = new CultureInfo("vi-VN");
            culture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            culture.DateTimeFormat.LongTimePattern = ""; // Bỏ giờ nếu không cần

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            base.OnStartup(e);
        }
    }

}

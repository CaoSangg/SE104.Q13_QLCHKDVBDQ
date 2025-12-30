using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Project_QLVangBacDaQuy.DTO;

namespace Project_QLVangBacDaQuy.DAL
{
    class QuyDinhDAL
    {
        public List<QuyDinhDTO> GetListQuyDinh()
        {
            List<QuyDinhDTO> list = new List<QuyDinhDTO>();
            string query = "SELECT * FROM THAMSO";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            int stt = 1;
            foreach (DataRow row in data.Rows)
            {
                list.Add(new QuyDinhDTO()
                {
                    STT = stt++,
                    TenThamSo = row["TenThamSo"].ToString(),
                    GiaTri = Convert.ToDouble(row["GiaTri"]),
                    TinhTrang = "Áp dụng"
                });
            }
            return list;
        }

        public void UpdateQuyDinh(string tenThamSo, double giaTriMoi)
        {
            string query = "UPDATE THAMSO SET GiaTri = @GiaTri WHERE TenThamSo = @TenThamSo";
            object[] param = new object[] { giaTriMoi, tenThamSo };
            DataProvider.Instance.ExecuteNonQuery(query, param);
        }
    }
}

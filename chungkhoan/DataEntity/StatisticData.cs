using System;
using System.Windows.Media;

namespace PhoneApp1.Data
{
    public class StatisticData
    {
        #region property
        public string Date { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
        public string VNIndex { get; set; }
        public string VNIndexChange { get; set; }
        public string GTKL { get; set; }
        public string SoGD { get; set; }
        public int CountTran { get; set; }
        public int CountSan { get; set; }
        public int CountThamChieu { get; set; }
        public int CountTang { get; set; }
        public int CountGiam { get; set; }
        public SolidColorBrush TextColor { get; set; }
        #endregion

        public StatisticData()
        {

        }

        public StatisticData(string s)
        {
            System.Diagnostics.Debug.WriteLine("StatisticData: " + s);

            var data = s.Split('|');

            var vnIndex = double.Parse(data[Constant.Int_VnIndex]);
            var prevVnIndex = double.Parse(data[Constant.Int_PrevVNIndex]);
            VNIndex = (vnIndex).ToString("N2");
            VNIndexChange = (double.Parse(data[Constant.Int_VnIndexChange]) * 100 / prevVnIndex).ToString("N2");

            TextColor = (vnIndex > prevVnIndex ? Constant.SolidColorBrush_MauTang : Constant.SolidColorBrush_MauGiam);
            if (vnIndex == prevVnIndex)
                TextColor = Constant.SolidColorBrush_MauThamChieu;

            GTKL = (double.Parse(data[Constant.Int_GiaTriKhopLenh])).ToString("N3");
            SoGD = (double.Parse(data[Constant.Int_SoLuongGiaoDich])).ToString("N0");

            Date = data[Constant.Int_Ngay];
            Time = data[Constant.Int_Gio];
            Status = data[Constant.Int_Phien];
        }

    }
}

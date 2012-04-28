using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PhoneApp1.Data
{
    public class StatisticData
    {
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
        public SolidColorBrush TextColor { get; set; }

        public StatisticData(string s)
        {
            var temp = s.Split(new string[] { "**" }, StringSplitOptions.RemoveEmptyEntries);

            var item = temp[temp.Length - 1];

            var data = item.Split('|');

            double vnIndex = double.Parse(data[Constant.Int_VnIndex]);
            double prevVnIndex = double.Parse(data[Constant.Int_PrevVNIndex]);
            VNIndex = (vnIndex).ToString("N2");
            VNIndexChange = (double.Parse(data[Constant.Int_VnIndexChange]) * 100 / prevVnIndex).ToString("N2");

            TextColor = (vnIndex > prevVnIndex ? Constant.SolidColorBrush_MauTang : Constant.SolidColorBrush_MauGiam);
            if (vnIndex == prevVnIndex)
                TextColor = Constant.SolidColorBrush_MauThamChieu;

            GTKL = (double.Parse(data[Constant.Int_GiaTriKhopLenh])).ToString("N3");
            SoGD = (double.Parse(data[Constant.Int_SoLuongGiaoDich])).ToString("N0");

            string[] t = data[Constant.Int_Ngay].Split(' ');
            Date = t[0];
            int[] time = ConvertToTime(t[1]);

            Time = string.Format("{0:D2}:{1:D2}:{2:D2}", time[0], time[1], time[2]);
            Status = GetStatusFromTime(data[Constant.Int_Phien], t[1]);
        }

        private static int[] ConvertToTime(string time)
        {
            var result = new int[3];
            result[0] = int.Parse(time.Substring(0, time.Length == 5 ? 1 : 2));
            result[1] = int.Parse(time.Substring(time.Length - 4, 2));
            result[2] = int.Parse(time.Substring(time.Length - 2, 2));
            return result;
        }

        private static string GetStatusFromTime(string phien, string time)
        {
            string result = string.Empty;

            int t = int.Parse(time);
            switch (phien)
            {
                case "1":
                    result = t < 90000 ? Constant.String_DongCua : Constant.String_MoCua;
                    break;
                case "2":
                    if (t <= 113000)
                        result = Constant.String_Dot1;
                    else if (t < 130000)
                        result = Constant.String_Nghi;
                    else
                        result = Constant.String_Dot2;
                    ;
                    break;
                case "3":
                    if (t <= 140000)
                    {
                        result = Constant.String_KLDongCua;
                    }
                    else
                    {
                        if (t <= 141500)
                        {
                            result = Constant.String_SauGio;
                        }
                        else
                            result = Constant.String_DongCua;
                    }
                    break;

            }
            return result;
        }
    }
}
